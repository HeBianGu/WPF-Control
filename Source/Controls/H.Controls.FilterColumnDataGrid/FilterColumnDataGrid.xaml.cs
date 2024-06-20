
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;
namespace H.Controls.FilterColumnDataGrid
{
    public class FilterColumnDataGrid : DataGrid, INotifyPropertyChanged
    {
        #region Constructors

        public FilterColumnDataGrid()
        {
            this.CommandBindings.Add(new CommandBinding(ShowFilter, ShowFilterCommand, CanShowFilter));
            this.CommandBindings.Add(new CommandBinding(ApplyFilter, ApplyFilterCommand, CanApplyFilter)); // Ok
            this.CommandBindings.Add(new CommandBinding(CancelFilter, CancelFilterCommand));
            this.CommandBindings.Add(new CommandBinding(RemoveFilter, RemoveFilterCommand, CanRemoveFilter));
            this.CommandBindings.Add(new CommandBinding(IsChecked, CheckedAllCommand));
            this.CommandBindings.Add(new CommandBinding(ClearSearchBox, ClearSearchBoxClick));
            this.CommandBindings.Add(new CommandBinding(RemoveAllFilter, RemoveAllFilterCommand, CanRemoveAllFilter));

            this.Loaded += (l, k) =>
            {
                this.GeneratingCustomsColumn();
            };

        }

        static FilterColumnDataGrid()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FilterColumnDataGrid), new FrameworkPropertyMetadata(typeof(FilterColumnDataGrid)));
        }

        #endregion Constructors

        #region Command
        public static readonly ICommand ApplyFilter = new RoutedCommand();
        public static readonly ICommand CancelFilter = new RoutedCommand();
        public static readonly ICommand ClearSearchBox = new RoutedCommand();
        public static readonly ICommand IsChecked = new RoutedCommand();
        public static readonly ICommand RemoveAllFilter = new RoutedCommand();
        public static readonly ICommand RemoveFilter = new RoutedCommand();
        public static readonly ICommand ShowFilter = new RoutedCommand();
        #endregion Command

        #region Public DependencyProperty
        public static readonly DependencyProperty ExcludeFieldsProperty =
            DependencyProperty.Register("ExcludeFields",
                typeof(string),
                typeof(FilterColumnDataGrid),
                new PropertyMetadata(""));

        public static readonly DependencyProperty DateFormatStringProperty =
            DependencyProperty.Register("DateFormatString",
                typeof(string),
                typeof(FilterColumnDataGrid),
                new PropertyMetadata("d"));

        public static readonly DependencyProperty PersistentFilterProperty =
            DependencyProperty.Register("PersistentFilter",
                typeof(bool),
                typeof(FilterColumnDataGrid),
                new PropertyMetadata(false));


        #endregion Public DependencyProperty

        #region Public Event

        public event PropertyChangedEventHandler PropertyChanged;

        public event EventHandler Sorted;

        #endregion Public Event

        #region Private Fields
        private string fileName = "persistentFilter.json";
        private Stopwatch stopWatchFilter = new Stopwatch();
        private DataGridColumnHeadersPresenter columnHeadersPresenter;
        private bool pending;
        private bool search;
        private Button button;

        private Cursor cursor;
        private int searchLength;
        private double minHeight;
        private double minWidth;
        private double sizableContentHeight;
        private double sizableContentWidth;
        private Grid sizableContentGrid;

        private List<string> excludedFields;
        private List<FilterItemDate> treeView;
        private List<FilterItem> listBoxItems;

        private Point popUpSize;
        private Popup popup;

        private string fieldName;
        private string lastFilter;
        private string searchText;
        private TextBox searchTextBox;
        private Thumb thumb;

        private TimeSpan elapsed;

        //private Type collectionType;
        private Type fieldType;

        private bool startsWith;

        private readonly Dictionary<string, Predicate<object>> criteria = new Dictionary<string, Predicate<object>>();

        #endregion Private Fields

        #region Public Properties

        /// <summary>
        ///     Excluded Fields (AutoGeneratingColumn)
        /// </summary>
        public string ExcludeFields
        {
            get => (string)GetValue(ExcludeFieldsProperty);
            set => SetValue(ExcludeFieldsProperty, value);
        }

        /// <summary>
        ///     The string begins with the specific character. Used in pop-up search box
        /// </summary>
        public bool StartsWith
        {
            get => startsWith;
            set
            {
                startsWith = value;
                OnPropertyChanged();

                // refresh filter
                if (!string.IsNullOrEmpty(searchText)) this.ItemCollectionView.Refresh();
            }
        }

        /// <summary>
        ///     Date format displayed
        /// </summary>
        public string DateFormatString
        {
            get => (string)GetValue(DateFormatStringProperty);
            set => SetValue(DateFormatStringProperty, value);
        }


        public int ItemsSourceCount { get; set; }

        private Loc _translate = new Loc { Language = Local.SimplifiedChinese };

        public Loc Translate => _translate;

        public List<FilterItemDate> TreeViewItems
        {
            get => treeView ?? new List<FilterItemDate>();
            set
            {
                treeView = value;
                OnPropertyChanged();
            }
        }

        public List<FilterItem> ListBoxItems
        {
            get => listBoxItems ?? new List<FilterItem>();
            set
            {
                listBoxItems = value;
                OnPropertyChanged();
            }
        }

        public Type FieldType
        {
            get => fieldType;
            set
            {
                fieldType = value;
                OnPropertyChanged();
            }
        }

        public bool PersistentFilter
        {
            get => (bool)GetValue(PersistentFilterProperty);
            set => SetValue(PersistentFilterProperty, value);
        }

        #endregion Public Properties

        #region Private Properties

        private FilterCommon CurrentFilter { get; set; }
        private ICollectionView CollectionViewSource { get; set; }
        private ICollectionView ItemCollectionView { get; set; }
        private List<FilterCommon> GlobalFilterList { get; } = new List<FilterCommon>();


        private IEnumerable<FilterItem> PopupViewItems =>
            this.ItemCollectionView?.OfType<FilterItem>().Where(c => c.Level != 0) ?? new List<FilterItem>();

        private IEnumerable<FilterItem> SourcePopupViewItems =>
            this.ItemCollectionView?.SourceCollection.OfType<FilterItem>().Where(c => c.Level != 0) ??
            new List<FilterItem>();

        #endregion Private Properties

        #region Protected Methods
        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            try
            {
                if (this.AutoGenerateColumns)
                    excludedFields = this.ExcludeFields.Split(',').Select(p => p.Trim()).ToList();
                Sorted += OnSorted;
                Loaded += OnLoaded;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"FilterColumnDataGrid.OnInitialized : {ex.Message}");
                throw;
            }
        }

        /// <summary>
        ///     Auto generated column, set templateHeader
        /// </summary>
        /// <param name="e"></param>
        protected override void OnAutoGeneratingColumn(DataGridAutoGeneratingColumnEventArgs e)
        {
            base.OnAutoGeneratingColumn(e);

            try
            {
                if (e.Column.GetType() != typeof(System.Windows.Controls.DataGridTextColumn)) return;

                var column = new DataGridTextColumn
                {
                    Binding = new Binding(e.PropertyName) { ConverterCulture = this.Translate.Culture /* StringFormat */ },
                    FieldName = e.PropertyName,
                    Header = e.Column.Header.ToString(),
                    IsColumnFiltered = false
                };
                fieldType = Nullable.GetUnderlyingType(e.PropertyType) ?? e.PropertyType;
                if (fieldType == typeof(DateTime) && !string.IsNullOrEmpty(this.DateFormatString))
                    column.Binding.StringFormat = this.DateFormatString;
                if (!fieldType.IsSystemType())
                {
                    column.CanUserSort = false;
                }
                else if (fieldType.IsSystemType() && excludedFields?.FindIndex(c =>
                             string.Equals(c, e.PropertyName, StringComparison.CurrentCultureIgnoreCase)) == -1)
                {
                    column.HeaderTemplate = this.DataGridHeaderTemplate;
                    column.IsColumnFiltered = true;
                }

                e.Column = column;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"FilterColumnDataGrid.OnAutoGeneratingColumn : {ex.Message}");
                throw;
            }
        }



        public DataTemplate DataGridHeaderTemplate
        {
            get { return (DataTemplate)GetValue(DataGridHeaderTemplateProperty); }
            set { SetValue(DataGridHeaderTemplateProperty, value); }
        }

        public static readonly DependencyProperty DataGridHeaderTemplateProperty =
            DependencyProperty.Register("DataGridHeaderTemplate", typeof(DataTemplate), typeof(FilterColumnDataGrid), new FrameworkPropertyMetadata(default(DataTemplate), (d, e) =>
            {
                FilterColumnDataGrid control = d as FilterColumnDataGrid;

                if (control == null) return;

                if (e.OldValue is DataTemplate o)
                {

                }

                if (e.NewValue is DataTemplate n)
                {

                }
                //control.GeneratingCustomsColumn();
            }));
        protected override void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
        {
            base.OnItemsSourceChanged(oldValue, newValue);

            try
            {
                if (newValue == null) return;

                if (oldValue != null)
                {
                    this.CurrentFilter = null;
                    this.GlobalFilterList.Clear();
                    criteria.Clear();

                    if (oldValue is INotifyCollectionChanged)
                        ((INotifyCollectionChanged)oldValue).CollectionChanged -= ItemSource_CollectionChanged;
                    this.CollectionViewSource = System.Windows.Data.CollectionViewSource.GetDefaultView(new object());
                    var scrollViewer = GetTemplateChild("DG_ScrollViewer") as ScrollViewer;
                    scrollViewer?.ScrollToTop();
                }

                if (newValue is INotifyCollectionChanged)
                    ((INotifyCollectionChanged)newValue).CollectionChanged += ItemSource_CollectionChanged;

                this.CollectionViewSource = System.Windows.Data.CollectionViewSource.GetDefaultView(this.ItemsSource);
                if (this.CollectionViewSource.CanFilter) this.CollectionViewSource.Filter = Filter;

                this.ItemsSourceCount = this.Items.Count;
                //ElapsedTime = new TimeSpan(0, 0, 0);
                OnPropertyChanged(nameof(this.ItemsSourceCount));

                // Calculate row header width
                //if (ShowRowsCount)
                //{
                //    var txt = new TextBlock
                //    {
                //        Text = ItemsSourceCount.ToString(),
                //        FontSize = FontSize,
                //        FontFamily = FontFamily,
                //        Padding = new Thickness(0, 0, 4, 0),
                //        Margin = new Thickness(2.0)
                //    };
                //    txt.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                //    RowHeaderWidth = Math.Max(Math.Ceiling(txt.DesiredSize.Width),
                //        RowHeaderWidth >= 0 ? RowHeaderWidth : 0);
                //}
                //else
                //{
                //    RowHeaderWidth = 0;
                //}

                // get collection type
                //if (ItemsSourceCount > 0) // commented out as this caused a NPE if the ItemSource is empty when attempting to show the filter
                // contribution : APFLKUACHA
                //collectionType = ItemsSource is ICollectionView collectionView
                //    ? collectionView.SourceCollection?.GetType().GenericTypeArguments.FirstOrDefault()
                //    : ItemsSource?.GetType().GenericTypeArguments.FirstOrDefault();


                //var sss=     ItemsSource.GetType();
                // set name of persistent filter json file
                // The name of the file is defined by the "Name" property of the FilterDatGrid, otherwise
                // the name of the source collection type is used
                if (this.PersistentFilter && this.CollectionType != null)
                {
                    fileName = !string.IsNullOrEmpty(this.Name) ? $"{this.Name}.json" : $"{this.CollectionType?.Name}.json";
                }

                // generating custom columns
                if (!this.AutoGenerateColumns && this.CollectionType != null) GeneratingCustomsColumn();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"FilterColumnDataGrid.OnItemsSourceChanged : {ex.Message}");
                throw;
            }
        }


        public Type CollectionType
        {
            get { return (Type)GetValue(CollectionTypeProperty); }
            set { SetValue(CollectionTypeProperty, value); }
        }
        public static readonly DependencyProperty CollectionTypeProperty =
            DependencyProperty.Register("CollectionType", typeof(Type), typeof(FilterColumnDataGrid), new FrameworkPropertyMetadata(default(Type), (d, e) =>
            {
                FilterColumnDataGrid control = d as FilterColumnDataGrid;

                if (control == null) return;

                if (e.OldValue is Type o)
                {

                }

                if (e.NewValue is Type n)
                {

                }

            }));

        /// <summary>
        ///     Set the cursor to "Cursors.Wait" during a long sorting operation
        ///     https://stackoverflow.com/questions/8416961/how-can-i-be-notified-if-a-datagrid-column-is-sorted-and-not-sorting
        /// </summary>
        /// <param name="eventArgs"></param>
        protected override void OnSorting(DataGridSortingEventArgs eventArgs)
        {
            if (pending || (popup?.IsOpen ?? false))
                return;
            Mouse.OverrideCursor = Cursors.Wait;
            base.OnSorting(eventArgs);
            Sorted?.Invoke(this, EventArgs.Empty);
        }

        protected override void OnLoadingRow(DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }

        #endregion Protected Methods

        #region Public Methods

        /// <summary>
        /// Access by the Host application to the method of loading active filters
        /// </summary>
        public void LoadPreset()
        {
            DeSerialize();
        }

        /// <summary>
        /// Access by the Host application to the method of saving active filters
        /// </summary>
        public void SavePreset()
        {
            Serialize();
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Loaded Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void OnLoaded(object sender, RoutedEventArgs e)
        {
            var filterDatagrid = (FilterColumnDataGrid)sender;
            if (filterDatagrid?.PersistentFilter == false) return;
            e.Handled = true;
            filterDatagrid?.LoadPreset();
        }

        /// <summary>
        ///     Restore filters from json file
        ///     contribution : ericvdberge
        /// </summary>
        /// <param name="filterPreset">all the saved filters from a FilterColumnDataGrid</param>
        private void OnFilterPresetChanged(List<FilterCommon> filterPreset)
        {
            if (filterPreset == null || filterPreset.Count == 0) return;

            // set cursor
            Mouse.OverrideCursor = Cursors.Wait;

            if (this.GlobalFilterList.Count > 0)
                RemoveAllFilterCommand(null, null);

            // reset previous elapsed time
            //ElapsedTime = new TimeSpan(0, 0, 0);
            stopWatchFilter = Stopwatch.StartNew();

            foreach (var preset in filterPreset)
            {
                var columns = this.Columns
                    .Where(c =>
                        (c is DataGridTextColumn dtx && dtx.IsColumnFiltered & dtx.FieldName == preset.FieldName)
                        || c is DataGridTemplateColumn dtp && dtp.IsColumnFiltered & dtp.FieldName == preset.FieldName
                        || c is DataGridCheckBoxColumn dcb && dcb.IsColumnFiltered & dcb.FieldName == preset.FieldName
                    )
                    .Select(c => c)
                    .ToList();

                foreach (var col in columns)
                {
                    var filterButton = VisualTreeHelpers.GetHeader(col, this)
                        ?.FindVisualChild<Button>("FilterButton");

                    var fieldProperty = this.CollectionType.GetPropertyInfo(preset.FieldName);

                    if (fieldProperty != null)
                        fieldType = Nullable.GetUnderlyingType(fieldProperty.PropertyType) ??
                                    fieldProperty.PropertyType;

                    if (fieldType == typeof(DateTime))
                    {
                        object ConvertDateTime(object o)
                        {
                            var isSuccess = DateTime.TryParse(o?.ToString(), out var dateTime);
                            return isSuccess ? dateTime : (object)(DateTime?)null;
                        }

                        preset.PreviouslyFilteredItems = preset.PreviouslyFilteredItems
                            .ToList()
                            .ConvertAll(ConvertDateTime)
                            .ToHashSet();
                    }

                    preset.FieldType = fieldType;
                    preset.Translate = this.Translate;
                    preset.FilterButton = filterButton;

                    if (filterButton != null)
                        FilterState.SetIsFiltered(filterButton, true);

                    preset.AddFilter(criteria);
                    if (this.GlobalFilterList.All(f => f.FieldName != preset.FieldName))
                        this.GlobalFilterList.Add(preset);
                    lastFilter = preset.FieldName;
                    this.CollectionViewSource.Refresh();
                }
            }
            if (this.Items.Count == 0)
            {
                RemoveAllFilterCommand(null, null);
                SavePreset();
            }

            stopWatchFilter.Stop();
            //ElapsedTime = stopWatchFilter.Elapsed;
            ResetCursor();
        }

        private async void Serialize()
        {
            await Task.Run(() =>
            {
                var result = JsonConvert.Serialize(fileName, this.GlobalFilterList);
            });
        }
        private async void DeSerialize()
        {
            await Task.Run(() =>
            {
                var result = JsonConvert.Deserialize<List<FilterCommon>>(fileName);

                if (result == null) return;
                this.Dispatcher.BeginInvoke((Action)(() => { OnFilterPresetChanged(result); }),
                    DispatcherPriority.Normal);
            });
        }

        private List<FilterItemDate> BuildTree(IEnumerable<FilterItem> dates)
        {
            try
            {
                var tree = new List<FilterItemDate>
                {
                    new FilterItemDate
                    {
                        Label = this.Translate.All, Level = 0, Initialize = true, FieldType = fieldType
                    }
                };

                if (dates == null)
                    return tree;
                var dateTimes = dates.ToList();

                foreach (var y in dateTimes.Where(c => c.Level == 1)
                             .Select(filterItem => new
                             {
                                 ((DateTime)filterItem.Content).Date,
                                 Item = filterItem
                             })
                             .GroupBy(g => g.Date.Year)
                             .Select(year => new FilterItemDate
                             {
                                 Level = 1,
                                 Content = year.Key,
                                 Label = year.FirstOrDefault()?.Date.ToString("yyyy", this.Translate.Culture),
                                 Initialize = true,
                                 FieldType = fieldType,

                                 Children = year.GroupBy(date => date.Date.Month)
                                     .Select(month => new FilterItemDate
                                     {
                                         Level = 2,
                                         Content = month.Key,
                                         Label = month.FirstOrDefault()?.Date.ToString("MMMM", this.Translate.Culture),
                                         Initialize = true,
                                         FieldType = fieldType,

                                         Children = month.GroupBy(date => date.Date.Day)
                                             .Select(day => new FilterItemDate
                                             {
                                                 Level = 3,
                                                 Content = day.Key,
                                                 Label = day.FirstOrDefault()?.Date.ToString("dd", this.Translate.Culture),
                                                 Initialize = true,
                                                 FieldType = fieldType,
                                                 Item = day.FirstOrDefault()?.Item,

                                                 Children = new List<FilterItemDate>()
                                             }).ToList()
                                     }).ToList()
                             }))
                {
                    y.Children.ForEach(m =>
                    {
                        m.Parent = y;

                        m.Children.ForEach(d =>
                        {
                            d.Parent = m;
                            if (d.Item.IsChecked)
                                return;
                            d.IsChecked = false;
                            d.Initialize = d.IsChecked;
                        });
                        m.Initialize = m.IsChecked;
                    });
                    y.Initialize = y.IsChecked;
                    tree.Add(y);
                }
                if (dateTimes.Any(d => d.Level == -1))
                {
                    var empty = dateTimes.FirstOrDefault(x => x.Level == -1);
                    if (empty != null)
                        tree.Add(
                            new FilterItemDate
                            {
                                Label = this.Translate.Empty,
                                Content = null,
                                Level = -1,
                                FieldType = fieldType,
                                Initialize = empty.IsChecked,
                                Item = empty,
                                Children = new List<FilterItemDate>()
                            }
                        );
                }

                tree.First().Tree = tree;
                return tree;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"FilterCommon.BuildTree : {ex.Message}");
                throw;
            }
        }
        private readonly MouseButtonEventHandler onMousedown = (_, eArgs) => { eArgs.Handled = true; };
        private void GeneratingCustomsColumn()
        {
            try
            {
                var columns = this.Columns
                    .Where(c => (c is DataGridTextColumn dtx && dtx.IsColumnFiltered)
                                || (c is DataGridTemplateColumn dtp && dtp.IsColumnFiltered)
                                || (c is DataGridCheckBoxColumn dcb && dcb.IsColumnFiltered)
                    )
                    .Select(c => c)
                    .ToList();
                foreach (var col in columns)
                {
                    var columnType = col.GetType();

                    if (col.HeaderTemplate != null)
                    {
                        var buttonFilter = VisualTreeHelpers.GetHeader(col, this)
                            ?.FindVisualChild<Button>("FilterButton");
                        if (buttonFilter != null) FilterState.SetIsFiltered(buttonFilter, false);
                    }
                    else
                    {
                        if (columnType == typeof(DataGridTextColumn))
                        {
                            var column = (DataGridTextColumn)col;
                            column.HeaderTemplate = this.DataGridHeaderTemplate;
                            fieldType = null;
                            var fieldProperty = this.CollectionType.GetProperty(((Binding)column.Binding).Path.Path);
                            if (fieldProperty != null)
                                fieldType = Nullable.GetUnderlyingType(fieldProperty.PropertyType) ??
                                            fieldProperty.PropertyType;
                            if (fieldType == typeof(DateTime) && !string.IsNullOrEmpty(this.DateFormatString))
                                if (string.IsNullOrEmpty(column.Binding.StringFormat))
                                    column.Binding.StringFormat = this.DateFormatString;
                            if (((Binding)column.Binding).ConverterCulture == null)
                                ((Binding)column.Binding).ConverterCulture = this.Translate.Culture;
                            column.FieldName = ((Binding)column.Binding).Path.Path;
                        }

                        if (columnType == typeof(DataGridTemplateColumn))
                        {
                            var column = (DataGridTemplateColumn)col;
                            column.HeaderTemplate = this.DataGridHeaderTemplate;
                        }
                        if (columnType == typeof(DataGridCheckBoxColumn))
                        {
                            var column = (DataGridCheckBoxColumn)col;
                            column.FieldName = ((Binding)column.Binding).Path.Path;
                            if (((Binding)column.Binding).ConverterCulture == null)
                                ((Binding)column.Binding).ConverterCulture = this.Translate.Culture;
                            column.HeaderTemplate = this.DataGridHeaderTemplate;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"FilterColumnDataGrid.GeneratingCustomColumn : {ex.Message}");
                throw;
            }
        }

        private void OnSorted(object sender, EventArgs e)
        {
            ResetCursor();
        }

        private async void ResetCursor()
        {
            await this.Dispatcher.BeginInvoke((Action)(() => { Mouse.OverrideCursor = null; }),
                DispatcherPriority.ContextIdle);
        }

        private void CanApplyFilter(object sender, CanExecuteRoutedEventArgs e)
        {
            if ((popup?.IsOpen ?? false) == false)
            {
                e.CanExecute = false;
            }
            else
            {
                if (search)
                    e.CanExecute = this.PopupViewItems.Any(f => f?.IsChecked == true);
                else
                    e.CanExecute = this.PopupViewItems.Any(f => f.IsChanged) &&
                                   this.PopupViewItems.Any(f => f?.IsChecked == true);
            }
        }

        private void CancelFilterCommand(object sender, ExecutedRoutedEventArgs e)
        {
            if (popup == null) return;
            popup.IsOpen = false;
        }

        private void CanRemoveAllFilter(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.GlobalFilterList.Count > 0;
        }

        private void CanRemoveFilter(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.CurrentFilter?.IsFiltered ?? false;
        }

        private void CanShowFilter(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.CollectionViewSource?.CanFilter == true && (!popup?.IsOpen ?? true) && !pending;
        }

        private void CheckedAllCommand(object sender, ExecutedRoutedEventArgs e)
        {
            var item = (FilterItem)e.Parameter;
            if (item?.Level != 0 || this.ItemCollectionView == null) return;

            foreach (var obj in this.PopupViewItems.ToList()
                         .Where(f => f.IsChecked != item.IsChecked))
                obj.IsChecked = item.IsChecked;
        }

        private void ClearSearchBoxClick(object sender, RoutedEventArgs routedEventArgs)
        {
            search = false;
            searchTextBox.Text = string.Empty;
        }

        private bool Filter(object o)
        {
            return criteria.Values
                .Aggregate(true, (prevValue, predicate) => prevValue && predicate(o));
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnResizeThumbDragCompleted(object sender, DragCompletedEventArgs e)
        {
            this.Cursor = cursor;
        }

        private void OnResizeThumbDragDelta(object sender, DragDeltaEventArgs e)
        {
            if (sizableContentHeight <= 0)
            {
                sizableContentHeight = sizableContentGrid.ActualHeight;
                sizableContentWidth = sizableContentGrid.ActualWidth;
            }

            var yAdjust = sizableContentGrid.Height + e.VerticalChange;
            var xAdjust = sizableContentGrid.Width + e.HorizontalChange;
            xAdjust = sizableContentGrid.ActualWidth + xAdjust > minWidth ? xAdjust : minWidth;
            yAdjust = sizableContentGrid.ActualHeight + yAdjust > minHeight ? yAdjust : minHeight;
            xAdjust = xAdjust < minWidth ? minWidth : xAdjust;
            yAdjust = yAdjust < minHeight ? minHeight : yAdjust;
            sizableContentGrid.Width = xAdjust;
            sizableContentGrid.Height = yAdjust;
        }
        private void OnResizeThumbDragStarted(object sender, DragStartedEventArgs e)
        {
            cursor = this.Cursor;
            this.Cursor = Cursors.SizeNWSE;
        }
        private void PopupClosed(object sender, EventArgs e)
        {
            var pop = (Popup)sender;
            if (!pending)
            {
                this.ItemCollectionView = System.Windows.Data.CollectionViewSource.GetDefaultView(new object());
                this.CurrentFilter = null;
                ResetCursor();
            }
            pop.Closed -= PopupClosed;
            pop.MouseDown -= onMousedown;
            searchTextBox.TextChanged -= SearchTextBoxOnTextChanged;
            thumb.DragCompleted -= OnResizeThumbDragCompleted;
            thumb.DragDelta -= OnResizeThumbDragDelta;
            thumb.DragStarted -= OnResizeThumbDragStarted;

            sizableContentGrid.Width = sizableContentWidth;
            sizableContentGrid.Height = sizableContentHeight;
            this.Cursor = cursor;

            this.ListBoxItems = new List<FilterItem>();
            this.TreeViewItems = new List<FilterItemDate>();

            searchText = string.Empty;
            search = false;
            if (columnHeadersPresenter != null)
                columnHeadersPresenter.IsEnabled = true;
        }
        private void RemoveAllFilterCommand(object sender, ExecutedRoutedEventArgs e)
        {
            //ElapsedTime = new TimeSpan(0, 0, 0);
            if (this.GlobalFilterList.Count == 0)
                return;

            var columns = this.Columns
                .Where(c =>
                    (c is DataGridTextColumn dtx && dtx.IsColumnFiltered)
                    || c is DataGridTemplateColumn dtp && dtp.IsColumnFiltered
                    || c is DataGridCheckBoxColumn dcb && dcb.IsColumnFiltered
                )
                .Select(c => c)
                .ToList();

            foreach (var filterButton in columns.Select(col => VisualTreeHelpers.GetHeader(col, this)
                         ?.FindVisualChild<Button>("FilterButton")).Where(filterButton => filterButton != null))
            {
                FilterState.SetIsFiltered(filterButton, false);
            }

            criteria.Clear();
            this.GlobalFilterList.Clear();
            this.ItemCollectionView = System.Windows.Data.CollectionViewSource.GetDefaultView(new object());
            this.CollectionViewSource.Refresh();
            if (this.PersistentFilter)
                SavePreset();
        }

        /// <summary>
        ///     Remove current filter
        /// </summary>
        private void RemoveCurrentFilter()
        {
            if (this.CurrentFilter == null)
                return;

            popup.IsOpen = false;
            FilterState.SetIsFiltered(this.CurrentFilter.FilterButton, false);

            //ElapsedTime = new TimeSpan(0, 0, 0);
            stopWatchFilter = Stopwatch.StartNew();

            Mouse.OverrideCursor = Cursors.Wait;

            if (this.CurrentFilter.IsFiltered && criteria.Remove(this.CurrentFilter.FieldName))
                this.CollectionViewSource.Refresh();

            if (this.GlobalFilterList.Contains(this.CurrentFilter))
                _ = this.GlobalFilterList.Remove(this.CurrentFilter);
            lastFilter = this.GlobalFilterList.LastOrDefault()?.FieldName;

            this.CurrentFilter.IsFiltered = false;
            this.CurrentFilter = null;
            ResetCursor();

            if (this.PersistentFilter)
                SavePreset();

            stopWatchFilter.Stop();
            //ElapsedTime = stopWatchFilter.Elapsed;
        }

        private void RemoveFilterCommand(object sender, ExecutedRoutedEventArgs e)
        {
            RemoveCurrentFilter();
        }

        private bool SearchFilter(object obj)
        {
            var item = (FilterItem)obj;
            if (string.IsNullOrEmpty(searchText) || item == null || item.Level == 0)
                return true;
            var content = Convert.ToString(item.Content, this.Translate.Culture);
            if (!this.StartsWith)
                return this.Translate.Culture.CompareInfo.IndexOf(content ?? string.Empty, searchText,
                    CompareOptions.OrdinalIgnoreCase) >= 0;
            if (searchLength > item.ContentLength)
                return false;
            return this.Translate.Culture.CompareInfo.IndexOf(content ?? string.Empty, searchText, 0, searchLength,
                CompareOptions.OrdinalIgnoreCase) >= 0;
        }

        private void SearchTextBoxOnTextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = (TextBox)sender;
            if (textBox == null || textBox.Text == searchText || this.ItemCollectionView == null)
                return;
            e.Handled = true;
            searchText = textBox.Text;
            searchLength = searchText.Length;
            search = !string.IsNullOrEmpty(searchText);
            this.ItemCollectionView.Refresh();
            if (this.CurrentFilter.FieldType != typeof(DateTime) || treeView == null)
                return;
            if (string.IsNullOrEmpty(searchText))
            {
                this.TreeViewItems = BuildTree(this.SourcePopupViewItems);
            }
            else
            {
                var items = this.PopupViewItems.Where(i => i.IsChecked).ToList();
                this.TreeViewItems = BuildTree(items.Any() ? items : null);
            }
        }

        private async void ShowFilterCommand(object sender, ExecutedRoutedEventArgs e)
        {
            //ElapsedTime = new TimeSpan(0, 0, 0);
            stopWatchFilter = Stopwatch.StartNew();
            searchText = string.Empty;
            search = false;

            try
            {
                button = (Button)e.OriginalSource;
                if (this.Items.Count == 0 || button == null)
                    return;
                _ = CommitEdit(DataGridEditingUnit.Row, true);
                var header = VisualTreeHelpers.FindAncestor<DataGridColumnHeader>(button);
                var columnType = header.Column.GetType();
                popup = VisualTreeHelpers.FindChild<Popup>(header, "FilterPopup");
                columnHeadersPresenter = VisualTreeHelpers.FindAncestor<DataGridColumnHeadersPresenter>(header);

                if (popup == null || columnHeadersPresenter == null)
                    return;
                if (columnHeadersPresenter != null)
                    columnHeadersPresenter.IsEnabled = false;
                popup.Closed += PopupClosed;
                popup.MouseDown += onMousedown;
                sizableContentGrid = VisualTreeHelpers.FindChild<Grid>(popup.Child, "SizableContentGrid");
                searchTextBox = VisualTreeHelpers.FindChild<TextBox>(popup.Child, "SearchBox");
                searchTextBox.Text = string.Empty;
                searchTextBox.TextChanged += SearchTextBoxOnTextChanged;
                searchTextBox.Focusable = true;
                thumb = VisualTreeHelpers.FindChild<Thumb>(sizableContentGrid, "PopupThumb");
                thumb.DragCompleted += OnResizeThumbDragCompleted;
                thumb.DragDelta += OnResizeThumbDragDelta;
                thumb.DragStarted += OnResizeThumbDragStarted;
                if (columnType == typeof(DataGridTextColumn))
                {
                    var column = (DataGridTextColumn)header.Column;
                    fieldName = column.FieldName;
                }

                if (columnType == typeof(DataGridTemplateColumn))
                {
                    var column = (DataGridTemplateColumn)header.Column;
                    fieldName = column.FieldName;
                }

                if (columnType == typeof(DataGridCheckBoxColumn))
                {
                    var column = (DataGridCheckBoxColumn)header.Column;
                    fieldName = column.FieldName;
                }
                if (string.IsNullOrEmpty(fieldName))
                    return;
                fieldType = null;
                var fieldProperty = this.CollectionType.GetPropertyInfo(fieldName);
                if (fieldProperty != null)
                    this.FieldType = Nullable.GetUnderlyingType(fieldProperty.PropertyType) ?? fieldProperty.PropertyType;
                this.CurrentFilter = this.GlobalFilterList.FirstOrDefault(f => f.FieldName == fieldName) ??
                                new FilterCommon
                                {
                                    FieldName = fieldName,
                                    FieldType = fieldType,
                                    Translate = this.Translate,
                                    FilterButton = button
                                };
                var sourceObjectList = new List<object>();
                Mouse.OverrideCursor = Cursors.Wait;
                List<FilterItem> filterItemList = null;
                await Task.Run(() =>
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        if (fieldType == typeof(DateTime))
                            sourceObjectList = this.Items.Cast<object>()
                                .Select(x => (object)((DateTime?)x.GetPropertyValue(fieldName))?.Date)
                                .Distinct()
                                .ToList();
                        else
                            sourceObjectList = this.Items.Cast<object>()
                                .Select(x => x.GetPropertyValue(fieldName))
                                .Distinct()
                                .ToList();
                    });

                    if (lastFilter == this.CurrentFilter.FieldName)
                        sourceObjectList.AddRange(this.CurrentFilter?.PreviouslyFilteredItems ?? new HashSet<object>());
                    var emptyItem = sourceObjectList.RemoveAll(v => v == null || v.Equals(string.Empty)) > 0;
                    sourceObjectList = sourceObjectList.AsParallel().OrderBy(x => x).ToList();

                    if (fieldType == typeof(bool))
                    {
                        filterItemList = new List<FilterItem>(sourceObjectList.Count + 1);
                    }
                    else
                    {
                        filterItemList = new List<FilterItem>(sourceObjectList.Count + 2)
                        {
                            new FilterItem { Label = this.Translate.All, IsChecked = true, Level = 0 }
                        };
                    }
                    filterItemList.AddRange(sourceObjectList.Select(item => new FilterItem
                    {
                        Content = item,
                        ContentLength = item?.ToString()?.Length ?? 0,
                        FieldType = fieldType,
                        Label = item,
                        Level = 1,
                        Initialize = this.CurrentFilter.PreviouslyFilteredItems?.Contains(item) == false
                    }));

                    if (fieldType == typeof(bool))
                        filterItemList.ToList().ForEach(c =>
                        {
                            c.Label = (bool)c.Content ? this.Translate.IsTrue : this.Translate.IsFalse;
                        });
                    if (!emptyItem) return;

                    sourceObjectList.Insert(sourceObjectList.Count, null);

                    filterItemList.Add(new FilterItem
                    {
                        FieldType = fieldType,
                        Content = null,
                        Label = fieldType == typeof(bool) ? this.Translate.Indeterminate : this.Translate.Empty,
                        Level = -1,
                        Initialize = this.CurrentFilter?.PreviouslyFilteredItems?.Contains(null) == false
                    });
                });

                if (fieldType == typeof(DateTime))
                    this.TreeViewItems = BuildTree(filterItemList);
                else
                    this.ListBoxItems = filterItemList;
                this.ItemCollectionView = System.Windows.Data.CollectionViewSource.GetDefaultView(filterItemList);
                if (this.ItemCollectionView.CanFilter) this.ItemCollectionView.Filter = SearchFilter;
                PopupPlacement(sizableContentGrid, header);
                popup.UpdateLayout();
                popup.IsOpen = true;
                searchTextBox.Focus();
                Keyboard.Focus(searchTextBox);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"FilterColumnDataGrid.ShowFilterCommand error : {ex.Message}");
                throw;
            }
            finally
            {
                ResetCursor();
                stopWatchFilter.Stop();
                //ElapsedTime = stopWatchFilter.Elapsed;
            }
        }

        private async void ApplyFilterCommand(object sender, ExecutedRoutedEventArgs e)
        {
            stopWatchFilter.Start();
            pending = true;
            popup.IsOpen = false;
            Mouse.OverrideCursor = Cursors.Wait;

            try
            {
                await Task.Run(() =>
                {
                    var previousFiltered = this.CurrentFilter.PreviouslyFilteredItems;
                    var blankIsChanged = new FilterItem();
                    if (search)
                    {
                        blankIsChanged.IsChecked = false;
                        blankIsChanged.IsChanged = !previousFiltered.Any(c => c != null && c.Equals(string.Empty));
                        var searchResult = this.PopupViewItems.Where(c => c.IsChecked).ToList();
                        var uncheckedItems = this.SourcePopupViewItems.Except(searchResult).ToList();
                        uncheckedItems.AddRange(searchResult.Where(c => c.IsChecked == false));
                        previousFiltered.ExceptWith(searchResult.Select(c => c.Content));
                        previousFiltered.UnionWith(uncheckedItems.Select(c => c.Content));
                    }
                    else
                    {
                        var changedItems = this.PopupViewItems.Where(c => c.IsChanged).ToList();
                        var checkedItems = changedItems.Where(c => c.IsChecked);
                        var uncheckedItems = changedItems.Where(c => !c.IsChecked).ToList();
                        previousFiltered.ExceptWith(checkedItems.Select(c => c.Content));
                        previousFiltered.UnionWith(uncheckedItems.Select(c => c.Content));
                        blankIsChanged.IsChecked = changedItems.Any(c => c.Level == -1 && c.IsChecked);
                        blankIsChanged.IsChanged = changedItems.Any(c => c.Level == -1);
                    }

                    if (blankIsChanged.IsChanged && this.CurrentFilter.FieldType == typeof(string))
                    {
                        if (blankIsChanged.IsChecked == false)
                            previousFiltered.Add(string.Empty);
                        else if (blankIsChanged.IsChecked && previousFiltered.Any(c => c?.ToString() == string.Empty))
                            previousFiltered.RemoveWhere(item => item?.ToString() == string.Empty);
                    }
                    if (!this.CurrentFilter.IsFiltered) this.CurrentFilter.AddFilter(criteria);
                    if (this.GlobalFilterList.All(f => f.FieldName != this.CurrentFilter.FieldName))
                        this.GlobalFilterList.Add(this.CurrentFilter);
                    lastFilter = this.CurrentFilter.FieldName;
                });
                this.CollectionViewSource.Refresh();
                FilterState.SetIsFiltered(this.CurrentFilter.FilterButton, this.CurrentFilter?.IsFiltered ?? false);
                if (this.CurrentFilter != null && !this.CurrentFilter.PreviouslyFilteredItems.Any())
                    RemoveCurrentFilter();
                else if (this.PersistentFilter)
                    Serialize();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"FilterColumnDataGrid.ApplyFilterCommand error : {ex.Message}");
                throw;
            }
            finally
            {
                ResetCursor();
                this.ItemCollectionView = System.Windows.Data.CollectionViewSource.GetDefaultView(new object());
                pending = false;
                this.CurrentFilter = null;

                stopWatchFilter.Stop();
                //ElapsedTime = stopWatchFilter.Elapsed;
            }
        }
        private void PopupPlacement(FrameworkElement grid, FrameworkElement header)
        {
            try
            {
                popup.PlacementTarget = header;
                popup.HorizontalOffset = 0d;
                popup.VerticalOffset = -1d;
                popup.Placement = PlacementMode.Bottom;
                var hostingWindow = Window.GetWindow(this);

                if (hostingWindow != null)
                {
                    double MaxSize(double size) => (size >= 0.0d) ? size : 0.0d;
                    const double border = 1d;
                    var contentPresenter = VisualTreeHelpers.FindChild<ContentPresenter>(hostingWindow);
                    var hostSize = new Point
                    {
                        X = contentPresenter.ActualWidth,
                        Y = contentPresenter.ActualHeight
                    };
                    var headerContentOrigin = header.TransformToVisual(contentPresenter).Transform(new Point(0, 0));
                    var headerDataGridOrigin = header.TransformToVisual(this).Transform(new Point(0, 0));
                    var headerSize = new Point { X = header.ActualWidth, Y = header.ActualHeight };
                    var offset = popUpSize.X - headerSize.X + border;
                    if (headerDataGridOrigin.X + headerSize.X > popUpSize.X) popup.HorizontalOffset -= offset;
                    var delta = new Point
                    {
                        X = hostSize.X - (headerContentOrigin.X + headerSize.X),
                        Y = hostSize.Y - (headerContentOrigin.Y + headerSize.Y + popUpSize.Y)
                    };
                    if (popup.HorizontalOffset == 0)
                        grid.MaxWidth = MaxSize(Math.Abs(grid.MaxWidth - offset));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"FilterColumnDataGrid.PopupPlacement error : {ex.Message}");
                throw;
            }
        }

        private void ItemSource_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.ItemsSourceCount = this.Items.Count;
            OnPropertyChanged(nameof(this.ItemsSourceCount));
            for (int i = 0; i < this.Items.Count; i++)
            {
                if (this.ItemContainerGenerator.ContainerFromIndex(i) is DataGridRow row)
                {
                    row.Header = $"{i + 1}";
                }
            }
        }
        #endregion Private Methods
    }
}
