// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace H.Controls.PropertyGrid
{
    [TemplatePart(Name = PART_Popup, Type = typeof(Popup))]
    public class CheckComboBox : H.Controls.PropertyGrid.Selector
    {
        private const string PART_Popup = "PART_Popup";

        #region Members

        private ValueChangeHelper _displayMemberPathValuesChangeHelper;
        private bool _ignoreTextValueChanged;
        private Popup _popup;
        private List<object> _initialValue = new List<object>();

        #endregion

        #region Constructors

        static CheckComboBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CheckComboBox), new FrameworkPropertyMetadata(typeof(CheckComboBox)));
        }

        public CheckComboBox()
        {
            Keyboard.AddKeyDownHandler(this, OnKeyDown);
            Mouse.AddPreviewMouseDownOutsideCapturedElementHandler(this, OnMouseDownOutsideCapturedElement);
            _displayMemberPathValuesChangeHelper = new ValueChangeHelper(this.OnDisplayMemberPathValuesChanged);
        }

        #endregion //Constructors

        #region Properties

        #region IsEditable

        public static readonly DependencyProperty IsEditableProperty = DependencyProperty.Register("IsEditable", typeof(bool), typeof(CheckComboBox)
          , new UIPropertyMetadata(false));
        public bool IsEditable
        {
            get
            {
                return (bool)GetValue(IsEditableProperty);
            }
            set
            {
                SetValue(IsEditableProperty, value);
            }
        }

        #endregion

        #region Text

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(CheckComboBox)
          , new UIPropertyMetadata(null, OnTextChanged));
        public string Text
        {
            get
            {
                return (string)GetValue(TextProperty);
            }
            set
            {
                SetValue(TextProperty, value);
            }
        }

        private static void OnTextChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            CheckComboBox checkComboBox = o as CheckComboBox;
            if (checkComboBox != null)
                checkComboBox.OnTextChanged((string)e.OldValue, (string)e.NewValue);
        }

        protected virtual void OnTextChanged(string oldValue, string newValue)
        {
            if (!this.IsInitialized || _ignoreTextValueChanged || !this.IsEditable)
                return;

            this.UpdateFromText();
        }

        #endregion

        #region IsDropDownOpen

        public static readonly DependencyProperty IsDropDownOpenProperty = DependencyProperty.Register("IsDropDownOpen", typeof(bool), typeof(CheckComboBox), new UIPropertyMetadata(false, OnIsDropDownOpenChanged));
        public bool IsDropDownOpen
        {
            get
            {
                return (bool)GetValue(IsDropDownOpenProperty);
            }
            set
            {
                SetValue(IsDropDownOpenProperty, value);
            }
        }

        private static void OnIsDropDownOpenChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            CheckComboBox comboBox = o as CheckComboBox;
            if (comboBox != null)
                comboBox.OnIsDropDownOpenChanged((bool)e.OldValue, (bool)e.NewValue);
        }

        protected virtual void OnIsDropDownOpenChanged(bool oldValue, bool newValue)
        {
            if (newValue)
            {
                _initialValue.Clear();
                foreach (object o in this.SelectedItems)
                    _initialValue.Add(o);
            }
            else
            {
                _initialValue.Clear();
            }

            // TODO: Add your property changed side-effects. Descendants can override as well.
        }

        #endregion //IsDropDownOpen

        #region MaxDropDownHeight

        public static readonly DependencyProperty MaxDropDownHeightProperty = DependencyProperty.Register("MaxDropDownHeight", typeof(double), typeof(CheckComboBox), new UIPropertyMetadata(SystemParameters.PrimaryScreenHeight / 3.0, OnMaxDropDownHeightChanged));
        public double MaxDropDownHeight
        {
            get
            {
                return (double)GetValue(MaxDropDownHeightProperty);
            }
            set
            {
                SetValue(MaxDropDownHeightProperty, value);
            }
        }

        private static void OnMaxDropDownHeightChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            CheckComboBox comboBox = o as CheckComboBox;
            if (comboBox != null)
                comboBox.OnMaxDropDownHeightChanged((double)e.OldValue, (double)e.NewValue);
        }

        protected virtual void OnMaxDropDownHeightChanged(double oldValue, double newValue)
        {
            // TODO: Add your property changed side-effects. Descendants can override as well.
        }

        #endregion

        #endregion //Properties

        #region Base Class Overrides

        protected override void OnSelectedValueChanged(string oldValue, string newValue)
        {
            base.OnSelectedValueChanged(oldValue, newValue);
            UpdateText();
        }

        protected override void OnDisplayMemberPathChanged(string oldDisplayMemberPath, string newDisplayMemberPath)
        {
            base.OnDisplayMemberPathChanged(oldDisplayMemberPath, newDisplayMemberPath);
            this.UpdateDisplayMemberPathValuesBindings();
        }

        protected override void OnItemsSourceChanged(System.Collections.IEnumerable oldValue, System.Collections.IEnumerable newValue)
        {
            base.OnItemsSourceChanged(oldValue, newValue);
            this.UpdateDisplayMemberPathValuesBindings();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (_popup != null)
                _popup.Opened -= Popup_Opened;

            _popup = GetTemplateChild(PART_Popup) as Popup;

            if (_popup != null)
                _popup.Opened += Popup_Opened;
        }


        #endregion //Base Class Overrides

        #region Event Handlers

        private void OnMouseDownOutsideCapturedElement(object sender, MouseButtonEventArgs e)
        {
            CloseDropDown(true);
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (!this.IsDropDownOpen)
            {
                if (KeyboardUtilities.IsKeyModifyingPopupState(e))
                {
                    this.IsDropDownOpen = true;
                    // Popup_Opened() will Focus on ComboBoxItem.
                    e.Handled = true;
                }
            }
            else
            {
                if (KeyboardUtilities.IsKeyModifyingPopupState(e))
                {
                    CloseDropDown(true);
                    e.Handled = true;
                }
                else if (e.Key == Key.Enter)
                {
                    CloseDropDown(true);
                    e.Handled = true;
                }
                else if (e.Key == Key.Escape)
                {
                    this.SelectedItems.Clear();
                    foreach (object o in _initialValue)
                        this.SelectedItems.Add(o);
                    CloseDropDown(true);
                    e.Handled = true;
                }
            }
        }

        private void Popup_Opened(object sender, EventArgs e)
        {
            UIElement item = this.ItemContainerGenerator.ContainerFromItem(this.SelectedItem) as UIElement;
            if ((item == null) && (this.Items.Count > 0))
                item = this.ItemContainerGenerator.ContainerFromItem(this.Items[0]) as UIElement;
            if (item != null)
                item.Focus();
        }

        #endregion //Event Handlers

        #region Methods

        protected virtual void UpdateText()
        {
#if VS2008
      string newValue = String.Join( Delimiter, SelectedItems.Cast<object>().Select( x => GetItemDisplayValue( x ).ToString() ).ToArray() ); 
#else
            string newValue = String.Join(this.Delimiter, this.SelectedItems.Cast<object>().Select(x => GetItemDisplayValue(x)));
#endif

            if (String.IsNullOrEmpty(this.Text) || !this.Text.Equals(newValue))
            {
                _ignoreTextValueChanged = true;
#if VS2008
        Text = newValue;
#else
                this.SetCurrentValue(CheckComboBox.TextProperty, newValue);
#endif
                _ignoreTextValueChanged = false;
            }
        }

        private void UpdateDisplayMemberPathValuesBindings()
        {
            _displayMemberPathValuesChangeHelper.UpdateValueSource(this.ItemsCollection, this.DisplayMemberPath);
        }

        private void OnDisplayMemberPathValuesChanged()
        {
            this.UpdateText();
        }

        /// <summary>
        /// Updates the SelectedItems collection based on the content of
        /// the Text property.
        /// </summary>
        private void UpdateFromText()
        {
            List<string> selectedValues = null;
            if (!String.IsNullOrEmpty(this.Text))
            {
                selectedValues = this.Text.Replace(" ", string.Empty).Split(new string[] { this.Delimiter }, StringSplitOptions.RemoveEmptyEntries).ToList();
            }

            this.UpdateFromList(selectedValues, this.GetItemDisplayValue);
        }

        protected object GetItemDisplayValue(object item)
        {
            if (String.IsNullOrEmpty(this.DisplayMemberPath))
                return item;

            string[] nameParts = this.DisplayMemberPath.Split('.');
            if (nameParts.Length == 1)
            {
                System.Reflection.PropertyInfo property = item.GetType().GetProperty(this.DisplayMemberPath);
                if (property != null)
                    return property.GetValue(item, null);
                return item;
            }

            for (int i = 0; i < nameParts.Count(); ++i)
            {
                Type type = item.GetType();
                System.Reflection.PropertyInfo info = type.GetProperty(nameParts[i]);
                if (info == null)
                {
                    return item;
                }

                if (i == nameParts.Count() - 1)
                {
                    return info.GetValue(item, null);
                }
                else
                {
                    item = info.GetValue(item, null);
                }
            }
            return item;
        }

        private void CloseDropDown(bool isFocusOnComboBox)
        {
            if (this.IsDropDownOpen)
                this.IsDropDownOpen = false;
            ReleaseMouseCapture();

            if (isFocusOnComboBox)
                Focus();
        }

        #endregion //Methods
    }
}
