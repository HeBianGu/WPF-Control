
using H.Controls.Dock.Layout;
using H.Controls.Dock.Themes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace H.Controls.Dock.Controls
{
    /// <inheritdoc />
    /// <summary>
    /// Implements a floating window for navigating between documents and toolwindows in H.Controls.Dock.
    /// The floating navigator window can be invoked with CTRL+TAB.
    /// </summary>
    /// <seealso cref="Window"/>
    [TemplatePart(Name = PART_AnchorableListBox, Type = typeof(ListBox))]
    [TemplatePart(Name = PART_DocumentListBox, Type = typeof(ListBox))]
    public class NavigatorWindow : Window
    {
        #region fields
        private ResourceDictionary currentThemeResourceDictionary; // = null

        private const string PART_AnchorableListBox = "PART_AnchorableListBox";
        private const string PART_DocumentListBox = "PART_DocumentListBox";

        private DockingManager _manager;
        private bool _isSelectingDocument;
        private ListBox _anchorableListBox;
        private ListBox _documentListBox;
        private bool _internalSetSelectedDocument = false;
        private bool _internalSetSelectedAnchorable = false;

        #endregion fields

        #region Constructors

        static NavigatorWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NavigatorWindow), new FrameworkPropertyMetadata(typeof(NavigatorWindow)));
            ShowActivatedProperty.OverrideMetadata(typeof(NavigatorWindow), new FrameworkPropertyMetadata(false));
            ShowInTaskbarProperty.OverrideMetadata(typeof(NavigatorWindow), new FrameworkPropertyMetadata(false));
        }

        internal NavigatorWindow(DockingManager manager)
        {
            _manager = manager;
            _internalSetSelectedDocument = true;
            SetAnchorables(_manager.Layout.Descendents()
                .OfType<LayoutAnchorable>()
                .Where(a => a.IsVisible)
                .OrderByDescending(d => d.LastActivationTimeStamp.GetValueOrDefault())
                .Select(d => (LayoutAnchorableItem)_manager.GetLayoutItemFromModel(d))
                .ToArray());
            SetDocuments(_manager.Layout.Descendents()
                .OfType<LayoutDocument>()
                .OrderByDescending(d => d.LastActivationTimeStamp.GetValueOrDefault())
                .Select(d => (LayoutDocumentItem)_manager.GetLayoutItemFromModel(d))
                .ToArray());
            _internalSetSelectedDocument = false;

            // if there are multiple documents, select the next document.
            // if there is only one document, select that document.
            // if there are no documents, select the first anchorable.
            if (this.Documents.Length > 1)
            {
                InternalSetSelectedDocument(this.Documents[1]);
                _isSelectingDocument = true;
            }
            else if (this.Documents.Length == 1)
            {
                InternalSetSelectedDocument(this.Documents[0]);
                _isSelectingDocument = true;
            }
            else
            {
                LayoutAnchorableItem anchorable = this.Anchorables.FirstOrDefault();
                if (anchorable != null)
                {
                    InternalSetSelectedAnchorable(anchorable);
                    _isSelectingDocument = false;
                }
            }

            this.DataContext = this;
            Loaded += OnLoaded;
            Unloaded += OnUnloaded;
            UpdateThemeResources();
        }

        #endregion Constructors

        #region Properties

        #region Documents

        /// <summary><see cref="Documents"/> read-only dependency property.</summary>
        private static readonly DependencyPropertyKey DocumentsPropertyKey = DependencyProperty.RegisterReadOnly(nameof(Documents), typeof(IEnumerable<LayoutDocumentItem>), typeof(NavigatorWindow),
                new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty DocumentsProperty = DocumentsPropertyKey.DependencyProperty;

        /// <summary>Gets the list of documents managed in this framework.</summary>
        [Bindable(true), Description("Gets the list of documents managed in this framework."), Category("Document")]
        public LayoutDocumentItem[] Documents => (LayoutDocumentItem[])GetValue(DocumentsProperty);

        #endregion Documents

        #region Anchorables

        /// <summary><see cref="Anchorables"/> read-only dependency property.</summary>
        private static readonly DependencyPropertyKey AnchorablesPropertyKey = DependencyProperty.RegisterReadOnly(nameof(Anchorables), typeof(IEnumerable<LayoutAnchorableItem>), typeof(NavigatorWindow),
                new FrameworkPropertyMetadata((IEnumerable<LayoutAnchorableItem>)null));

        public static readonly DependencyProperty AnchorablesProperty = AnchorablesPropertyKey.DependencyProperty;

        /// <summary>Gets the list of anchorables managed in the framework.</summary>
        [Bindable(true), Description("Gets the list of anchorables managed in the framework."), Category("Anchorable")]
        public IEnumerable<LayoutAnchorableItem> Anchorables => (IEnumerable<LayoutAnchorableItem>)GetValue(AnchorablesProperty);

        #endregion Anchorables

        #region SelectedDocument

        /// <summary><see cref="SelectedDocument"/> dependency property.</summary>
        public static readonly DependencyProperty SelectedDocumentProperty = DependencyProperty.Register(nameof(SelectedDocument), typeof(LayoutDocumentItem), typeof(NavigatorWindow),
                new FrameworkPropertyMetadata(null, OnSelectedDocumentChanged));

        /// <summary>Gets/sets the currently selected document.</summary>
        [Bindable(true), Description("Gets/sets the currently selected document."), Category("Document")]
        public LayoutDocumentItem SelectedDocument
        {
            get => (LayoutDocumentItem)GetValue(SelectedDocumentProperty);
            set => SetValue(SelectedDocumentProperty, value);
        }

        /// <summary>Handles changes to the <see cref="SelectedDocument"/> property.</summary>
        private static void OnSelectedDocumentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) => ((NavigatorWindow)d).OnSelectedDocumentChanged(e);

        /// <summary>Provides derived classes an opportunity to handle changes to the <see cref="SelectedDocument"/> property.</summary>
        protected virtual void OnSelectedDocumentChanged(DependencyPropertyChangedEventArgs e)
        {
            if (_internalSetSelectedDocument || this.SelectedDocument == null)
            {
                return;
            }

            if (!this.SelectedDocument.ActivateCommand.CanExecute(null))
            {
                return;
            }

            Close();
            this.SelectedDocument.ActivateCommand.Execute(null);
        }

        #endregion SelectedDocument

        #region SelectedAnchorable

        /// <summary><see cref="SelectedAnchorable"/> dependency property.</summary>
        public static readonly DependencyProperty SelectedAnchorableProperty = DependencyProperty.Register(nameof(SelectedAnchorable), typeof(LayoutAnchorableItem), typeof(NavigatorWindow),
                new FrameworkPropertyMetadata(null, OnSelectedAnchorableChanged));

        /// <summary>Gets/sets the currently selected anchorable.</summary>
        [Bindable(true), Description("Gets/sets the currently selected anchorable."), Category("Anchorable")]
        public LayoutAnchorableItem SelectedAnchorable
        {
            get => (LayoutAnchorableItem)GetValue(SelectedAnchorableProperty);
            set => SetValue(SelectedAnchorableProperty, value);
        }

        /// <summary>Handles changes to the <see cref="SelectedAnchorable"/> property.</summary>
        private static void OnSelectedAnchorableChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) => ((NavigatorWindow)d).OnSelectedAnchorableChanged(e);

        /// <summary>Provides derived classes an opportunity to handle changes to the <see cref="SelectedAnchorable"/> property.</summary>
        protected virtual void OnSelectedAnchorableChanged(DependencyPropertyChangedEventArgs e)
        {
            if (_internalSetSelectedAnchorable) return;
            // TODO: What goes on here??
            LayoutAnchorableItem selectedAnchorable = e.NewValue as LayoutAnchorableItem;
            if (this.SelectedAnchorable != null && this.SelectedAnchorable.ActivateCommand.CanExecute(null))
            {
                Close();
                this.SelectedAnchorable.ActivateCommand.Execute(null);
            }
        }

        #endregion SelectedAnchorable

        #endregion Properties

        #region Overrides

        /// <inheritdoc />
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _anchorableListBox = GetTemplateChild(PART_AnchorableListBox) as ListBox;
            _documentListBox = GetTemplateChild(PART_DocumentListBox) as ListBox;

            if (_anchorableListBox != null)
            {
                _anchorableListBox.ItemContainerGenerator.StatusChanged += ItemContainerGenerator_StatusChanged;
            }

            if (_documentListBox != null)
            {
                _documentListBox.ItemContainerGenerator.StatusChanged += ItemContainerGenerator_StatusChanged;
            }
        }

        private void ItemContainerGenerator_StatusChanged(object sender, EventArgs e)
        {
            bool isListOfDocuments = sender == _documentListBox.ItemContainerGenerator;
            IEnumerable itemsCollection = isListOfDocuments ? this.Documents : this.Anchorables.ToArray();
            ItemContainerGenerator generator = (ItemContainerGenerator)sender;
            switch (generator.Status)
            {
                case GeneratorStatus.ContainersGenerated:
                    foreach (object item in itemsCollection)
                    {
                        ListBoxItem container = (ListBoxItem)generator.ContainerFromItem(item);
                        if (container != null)
                        {
                            if (isListOfDocuments)
                            {
                                container.IsKeyboardFocusedChanged += DocumentsItemContainer_IsKeyboardFocusedChanged;
                            }
                            else
                            {
                                container.IsKeyboardFocusedChanged += AnchorablesItemContainer_IsKeyboardFocusedChanged;
                            }
                        }
                    }
                    break;
            }
        }

        private void AnchorablesItemContainer_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ListBoxItem item = (ListBoxItem)sender;
            if (item.IsKeyboardFocused)
            {
                _internalSetSelectedAnchorable = true;
                item.IsSelected = true;
                _internalSetSelectedAnchorable = false;
            }
        }

        private void DocumentsItemContainer_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ListBoxItem item = (ListBoxItem)sender;
            if (item.IsKeyboardFocused)
            {
                _internalSetSelectedDocument = true;
                item.IsSelected = true;
                _internalSetSelectedDocument = false;
            }
        }

        /// <inheritdoc />
        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.Key)
            {
                // Press Tab to switch Selected LayoutContent.
                case Key.Tab:
                    SetNextLayoutContent(true);
                    e.Handled = true;
                    break;
                case Key.Left:
                case Key.Right:
                    if (_isSelectingDocument)
                    {
                        LayoutAnchorableItem anchorable = this.Anchorables.ElementAtOrDefault(this.Documents.IndexOf(this.SelectedDocument))
                            ?? this.Anchorables.LastOrDefault();
                        if (anchorable != null)
                        {
                            _isSelectingDocument = false;
                            InternalSetSelectedDocument(null);
                            InternalSetSelectedAnchorable(anchorable);
                        }
                    }
                    else
                    {
                        int index = _anchorableListBox?.SelectedIndex
                            ?? this.Anchorables.ToArray().IndexOf(this.SelectedAnchorable);
                        LayoutDocumentItem document = this.Documents.ElementAtOrDefault(index)
                            ?? this.Documents.LastOrDefault();
                        if (document != null)
                        {
                            _isSelectingDocument = true;
                            InternalSetSelectedAnchorable(null);
                            InternalSetSelectedDocument(document);
                        }
                    }
                    e.Handled = true;
                    break;
                case Key.Up:
                    SetNextLayoutContent(false);
                    e.Handled = true;
                    break;
                case Key.Down:
                    SetNextLayoutContent(true);
                    e.Handled = true;
                    break;
            }
            if (!e.Handled)
            {
                base.OnKeyDown(e);
            }

            void SetNextLayoutContent(bool next)
            {
                // Selecting LayoutDocuments
                if (_isSelectingDocument)
                {
                    if (this.SelectedDocument != null)
                    {
                        // Jump to previous/next LayoutDocument
                        if (next)
                        {
                            SelectNextDocument();
                        }
                        else
                        {
                            SelectPreviousDocument();
                        }
                    }
                    // There is no SelectedDocument, select the first one.
                    else if (this.Documents.Length > 0)
                    {
                        InternalSetSelectedDocument(this.Documents[0]);
                    }
                }
                // Selecting LayoutAnchorables
                else
                {
                    if (this.SelectedAnchorable != null)
                    {
                        // Jump to previous/next LayoutAnchorable
                        if (next)
                        {
                            SelectNextAnchorable();
                        }
                        else
                        {
                            SelectPreviousAnchorable();
                        }
                    }
                    // There is no SelectedAnchorable, select the first one.
                    else
                    {
                        LayoutAnchorableItem anchorable = this.Anchorables.FirstOrDefault();
                        if (anchorable != null)
                        {
                            InternalSetSelectedAnchorable(anchorable);
                        }
                    }
                }
            }
        }

        /// <inheritdoc />
        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (!(e.Key == Key.Tab || e.Key == Key.Left || e.Key == Key.Right || e.Key == Key.Up || e.Key == Key.Down))
            {
                Close();
                if (this.SelectedDocument != null && this.SelectedDocument.ActivateCommand.CanExecute(null))
                    this.SelectedDocument.ActivateCommand.Execute(null);
                if (this.SelectedDocument == null && this.SelectedAnchorable != null && this.SelectedAnchorable.ActivateCommand.CanExecute(null))
                    this.SelectedAnchorable.ActivateCommand.Execute(null);
                e.Handled = true;
            }
            base.OnKeyUp(e);
        }

        #endregion Overrides

        #region Internal Methods

        /// <summary>
        /// Provides a secure method for setting the Anchorables property.
        /// This dependency property indicates the list of anchorables.
        /// </summary>
        /// <param name="value">The new value for the property.</param>
        protected void SetAnchorables(IEnumerable<LayoutAnchorableItem> value) => SetValue(AnchorablesPropertyKey, value);

        /// <summary>
        /// Provides a secure method for setting the Documents property.
        /// This dependency property indicates the list of documents.
        /// </summary>
        /// <param name="value">The new value for the property.</param>
        protected void SetDocuments(LayoutDocumentItem[] value) => SetValue(DocumentsPropertyKey, value);

        /// <summary>Is Invoked when H.Controls.Dock's WPF Theme changes via the <see cref="DockingManager.OnThemeChanged()"/> method.</summary>
        /// <param name="oldTheme"></param>
        internal void UpdateThemeResources(Theme oldTheme = null)
        {
            if (oldTheme != null) // Remove the old theme if present
            {
                if (oldTheme is DictionaryTheme)
                {
                    if (currentThemeResourceDictionary != null)
                    {
                        this.Resources.MergedDictionaries.Remove(currentThemeResourceDictionary);
                        currentThemeResourceDictionary = null;
                    }
                }
                else
                {
                    ResourceDictionary resourceDictionaryToRemove = this.Resources.MergedDictionaries.FirstOrDefault(r => r.Source == oldTheme.GetResourceUri());
                    if (resourceDictionaryToRemove != null) this.Resources.MergedDictionaries.Remove(resourceDictionaryToRemove);
                }
            }

            // Implicit parameter to this method is the new theme already set here
            if (_manager.Theme == null) return;
            if (_manager.Theme is DictionaryTheme dictionaryTheme)
            {
                currentThemeResourceDictionary = dictionaryTheme.ThemeResourceDictionary;
                this.Resources.MergedDictionaries.Add(currentThemeResourceDictionary);
            }
            else
                this.Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = _manager.Theme.GetResourceUri() });
        }

        internal void SelectNextDocument()
        {
            if (this.SelectedDocument == null) return;
            int docIndex = this.Documents.IndexOf(this.SelectedDocument);
            docIndex++;
            if (docIndex == this.Documents.Length) docIndex = 0;
            InternalSetSelectedDocument(this.Documents[docIndex]);
        }

        internal void SelectNextAnchorable()
        {
            if (this.SelectedAnchorable == null) return;
            LayoutAnchorableItem[] anchorablesArray = this.Anchorables.ToArray();
            int anchorableIndex = anchorablesArray.IndexOf(this.SelectedAnchorable);
            anchorableIndex++;
            if (anchorableIndex == anchorablesArray.Length) anchorableIndex = 0;
            InternalSetSelectedAnchorable(anchorablesArray[anchorableIndex]);
        }

        internal void SelectPreviousDocument()
        {
            if (this.SelectedDocument == null) return;
            int docIndex = this.Documents.IndexOf(this.SelectedDocument);
            docIndex--;
            if (docIndex < 0) docIndex = this.Documents.Length - 1;
            InternalSetSelectedDocument(this.Documents[docIndex]);
        }

        internal void SelectPreviousAnchorable()
        {
            if (this.SelectedAnchorable == null) return;
            LayoutAnchorableItem[] anchorablesArray = this.Anchorables.ToArray();
            int anchorableIndex = anchorablesArray.IndexOf(this.SelectedAnchorable);
            anchorableIndex--;
            if (anchorableIndex < 0) anchorableIndex = anchorablesArray.Length - 1;
            InternalSetSelectedAnchorable(anchorablesArray[anchorableIndex]);
        }

        #endregion Internal Methods

        #region Private Methods

        private void InternalSetSelectedAnchorable(LayoutAnchorableItem anchorableToSelect)
        {
            _internalSetSelectedAnchorable = true;
            this.SelectedAnchorable = anchorableToSelect;
            _internalSetSelectedAnchorable = false;
            if (_anchorableListBox != null)
            {
                FocusSelectedItem(_anchorableListBox);
            }
        }

        private void InternalSetSelectedDocument(LayoutDocumentItem documentToSelect)
        {
            _internalSetSelectedDocument = true;
            this.SelectedDocument = documentToSelect;
            _internalSetSelectedDocument = false;
            if (_documentListBox != null)
            {
                FocusSelectedItem(_documentListBox);
            }
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            Loaded -= OnLoaded;
            if (_documentListBox != null && this.SelectedDocument != null)
            {
                FocusSelectedItem(_documentListBox);
            }
            else if (_anchorableListBox != null && this.SelectedAnchorable != null)
            {
                FocusSelectedItem(_anchorableListBox);
            }
            this.WindowStartupLocation = WindowStartupLocation.CenterOwner;
        }

        private void OnUnloaded(object sender, RoutedEventArgs e) => Unloaded -= OnUnloaded;

        private void FocusSelectedItem(ListBox list)
        {
            if (list.SelectedIndex >= 0)
            {
                ListBoxItem listBoxItem = (ListBoxItem)list.ItemContainerGenerator.ContainerFromIndex(list.SelectedIndex);
                if (listBoxItem != null)
                {
                    this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Loaded, (Func<bool>)listBoxItem.Focus);
                }
            }
        }

        #endregion Private Methods
    }
}