// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

global using H.Common;
global using H.Mvvm.Commands;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace H.Controls.PagerBox
{
    [TemplatePart(Name = ElementButtonLeft, Type = typeof(Button))]
    [TemplatePart(Name = ElementButtonRight, Type = typeof(Button))]
    [TemplatePart(Name = ElementButtonFirst, Type = typeof(RadioButton))]
    [TemplatePart(Name = ElementTextBlockLeft, Type = typeof(TextBlock))]
    [TemplatePart(Name = ElementPanelMain, Type = typeof(Panel))]
    [TemplatePart(Name = ElementTextBlockRight, Type = typeof(TextBlock))]
    [TemplatePart(Name = ElementButtonLast, Type = typeof(RadioButton))]
    public class PagerBox : ContentControl
    {

        static PagerBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PagerBox), new FrameworkPropertyMetadata(typeof(PagerBox)));
        }

        #region Constants

        private const string ElementButtonLeft = "PART_ButtonLeft";
        private const string ElementButtonRight = "PART_ButtonRight";
        private const string ElementButtonFirst = "PART_ButtonFirst";
        private const string ElementTextBlockLeft = "PART_TextBlockLeft";
        private const string ElementPanelMain = "PART_PanelMain";
        private const string ElementTextBlockRight = "PART_TextBlockRight";
        private const string ElementButtonLast = "PART_ButtonLast";

        #endregion Constants

        #region Data

        private Button _buttonLeft;
        private Button _buttonRight;
        private RadioButton _buttonFirst;
        private TextBlock _textBlockLeft;
        private Panel _panelMain;
        private TextBlock _textBlockRight;
        private RadioButton _buttonLast;

        private bool _appliedTemplate;

        #endregion Data

        #region Public Events

        /// <summary>
        ///     页面更新事件
        /// </summary>
        public static readonly RoutedEvent PageUpdatedEvent =
            EventManager.RegisterRoutedEvent("PageUpdated", RoutingStrategy.Bubble,
                typeof(EventHandler<RoutedEventArgs<int>>), typeof(PagerBox));

        /// <summary>
        ///     页面更新事件
        /// </summary>
        public event EventHandler<RoutedEventArgs<int>> PageUpdated
        {
            add => AddHandler(PageUpdatedEvent, value);
            remove => RemoveHandler(PageUpdatedEvent, value);
        }

        #endregion Public Events

        public PagerBox()
        {
            this.CommandBindings.Add(new CommandBinding(Commands.Prev, ButtonPrev_OnClick));
            this.CommandBindings.Add(new CommandBinding(Commands.Next, ButtonNext_OnClick));
            this.CommandBindings.Add(new CommandBinding(Commands.Selected, ToggleButton_OnChecked));
            //this.VisibilityWith(MaxPageCount > 1);
            this.Visibility = this.MaxPageCount > 1 ? Visibility.Visible : Visibility.Collapsed;
        }

        #region Public Properties

        #region MaxPageCount

        /// <summary>
        ///     最大页数
        /// </summary>
        public static readonly DependencyProperty MaxPageCountProperty = DependencyProperty.Register(
            "MaxPageCount", typeof(int), typeof(PagerBox), new PropertyMetadata(1, (o, args) =>
            {
                if (o is PagerBox && args.NewValue is int)
                {
                    PagerBox PagerBox = o as PagerBox;
                    int value = (int)args.NewValue;

                    if (PagerBox.PageIndex > PagerBox.MaxPageCount)
                    {
                        PagerBox.PageIndex = PagerBox.MaxPageCount;
                    }

                    PagerBox.Visibility = value > 1 ? Visibility.Visible : Visibility.Collapsed;
                    PagerBox.Update();
                }
            }, (o, value) =>
            {
                if (!(o is PagerBox)) return 1;
                int intValue = (int)value;
                if (intValue < 1)
                {
                    return 1;
                }
                return intValue;
            }));

        /// <summary>
        ///     最大页数
        /// </summary>
        public int MaxPageCount
        {
            get => (int)GetValue(MaxPageCountProperty);
            set => SetValue(MaxPageCountProperty, value);
        }

        #endregion MaxPageCount

        #region DataCountPerPage

        /// <summary>
        ///     每页的数据量
        /// </summary>
        public static readonly DependencyProperty DataCountPerPageProperty = DependencyProperty.Register(
            "DataCountPerPage", typeof(int), typeof(PagerBox), new PropertyMetadata(20, (o, args) =>
            {
                if (o is PagerBox)
                {
                    PagerBox PagerBox = o as PagerBox;
                    PagerBox.Update();
                }
            }, (o, value) =>
            {
                if (!(o is PagerBox)) return 1;
                int intValue = (int)value;
                if (intValue < 1)
                {
                    return 1;
                }
                return intValue;
            }));

        /// <summary>
        ///     每页的数据量
        /// </summary>
        public int DataCountPerPage
        {
            get => (int)GetValue(DataCountPerPageProperty);
            set => SetValue(DataCountPerPageProperty, value);
        }

        #endregion

        #region PageIndex

        /// <summary>
        ///     当前页
        /// </summary>
        public static readonly DependencyProperty PageIndexProperty = DependencyProperty.Register(
            "PageIndex", typeof(int), typeof(PagerBox), new FrameworkPropertyMetadata(1, (o, args) =>
            {
                if (o is PagerBox && args.NewValue is int)
                {
                    PagerBox PagerBox = o as PagerBox;

                    int value = (int)args.NewValue;

                    PagerBox.Update();
                    PagerBox.RaiseEvent(new RoutedEventArgs<int>(PageUpdatedEvent, PagerBox)
                    {
                        Entity = value
                    });
                }
            }, (o, value) =>
            {
                if (!(o is PagerBox)) return 1;

                PagerBox PagerBox = o as PagerBox;

                int intValue = (int)value;
                if (intValue < 0)
                {
                    return 0;
                }
                if (intValue > PagerBox.MaxPageCount) return PagerBox.MaxPageCount;
                return intValue;
            }));

        /// <summary>
        ///     当前页
        /// </summary>
        public int PageIndex
        {
            get => (int)GetValue(PageIndexProperty);
            set => SetValue(PageIndexProperty, value);
        }

        #endregion PageIndex

        #region MaxPageInterval

        /// <summary>
        ///     表示当前选中的按钮距离左右两个方向按钮的最大间隔（4表示间隔4个按钮，如果超过则用省略号表示）
        /// </summary>       
        public static readonly DependencyProperty MaxPageIntervalProperty = DependencyProperty.Register(
            "MaxPageInterval", typeof(int), typeof(PagerBox), new PropertyMetadata(5, (o, args) =>
            {
                if (o is PagerBox)
                {
                    PagerBox PagerBox = o as PagerBox;

                    PagerBox.Update();
                }
            }), value =>
            {
                int intValue = (int)value;
                return intValue >= 0;
            });

        /// <summary>
        ///     表示当前选中的按钮距离左右两个方向按钮的最大间隔（4表示间隔4个按钮，如果超过则用省略号表示）
        /// </summary>   
        public int MaxPageInterval
        {
            get => (int)GetValue(MaxPageIntervalProperty);
            set => SetValue(MaxPageIntervalProperty, value);
        }

        #endregion MaxPageInterval

        #endregion

        #region Public Methods

        public override void OnApplyTemplate()
        {
            _appliedTemplate = false;
            base.OnApplyTemplate();

            _buttonLeft = GetTemplateChild(ElementButtonLeft) as Button;
            _buttonRight = GetTemplateChild(ElementButtonRight) as Button;
            _buttonFirst = GetTemplateChild(ElementButtonFirst) as RadioButton;
            _textBlockLeft = GetTemplateChild(ElementTextBlockLeft) as TextBlock;
            _panelMain = GetTemplateChild(ElementPanelMain) as Panel;
            _textBlockRight = GetTemplateChild(ElementTextBlockRight) as TextBlock;
            _buttonLast = GetTemplateChild(ElementButtonLast) as RadioButton;

            CheckNull();

            _appliedTemplate = true;
            Update();
        }

        #endregion Public Methods

        #region Private Methods

        private void CheckNull()
        {
            if (_buttonLeft == null || _buttonRight == null || _buttonFirst == null ||
                _textBlockLeft == null || _panelMain == null || _textBlockRight == null ||
                _buttonLast == null) throw new Exception();
        }

        /// <summary>
        ///     更新
        /// </summary>
        private void Update()
        {
            if (!_appliedTemplate) return;
            _buttonLeft.IsEnabled = this.PageIndex > 1;
            _buttonRight.IsEnabled = this.PageIndex < this.MaxPageCount;
            if (this.MaxPageInterval == 0)
            {
                _buttonFirst.Visibility = Visibility.Collapsed;
                _buttonLast.Visibility = Visibility.Collapsed;
                _textBlockLeft.Visibility = Visibility.Collapsed;
                _textBlockRight.Visibility = Visibility.Collapsed;
                _panelMain.Children.Clear();
                RadioButton selectButton = CreateButton(this.PageIndex);
                _panelMain.Children.Add(selectButton);
                selectButton.IsChecked = true;
                return;
            }
            _buttonFirst.Visibility = Visibility.Visible;
            _buttonLast.Visibility = Visibility.Visible;
            _textBlockLeft.Visibility = Visibility.Visible;
            _textBlockRight.Visibility = Visibility.Visible;

            //更新最后一页
            if (this.MaxPageCount == 1)
            {
                _buttonLast.Visibility = Visibility.Collapsed;
            }
            else
            {
                _buttonLast.Visibility = Visibility.Visible;
                _buttonLast.Tag = this.MaxPageCount.ToString();
            }

            //更新省略号
            int right = this.MaxPageCount - this.PageIndex;
            int left = this.PageIndex - 1;
            _textBlockRight.Visibility = right > this.MaxPageInterval ? Visibility.Visible : Visibility.Collapsed;
            _textBlockLeft.Visibility = left > this.MaxPageInterval ? Visibility.Visible : Visibility.Collapsed;

            //更新中间部分
            _panelMain.Children.Clear();
            if (this.PageIndex > 1 && this.PageIndex < this.MaxPageCount)
            {
                RadioButton selectButton = CreateButton(this.PageIndex);
                _panelMain.Children.Add(selectButton);
                selectButton.IsChecked = true;
            }
            else if (this.PageIndex == 1)
            {
                _buttonFirst.IsChecked = true;
            }
            else
            {
                _buttonLast.IsChecked = true;
            }

            int sub = this.PageIndex;
            for (int i = 0; i < this.MaxPageInterval - 1; i++)
            {
                if (--sub > 1)
                {
                    _panelMain.Children.Insert(0, CreateButton(sub));
                }
                else
                {
                    break;
                }
            }
            int add = this.PageIndex;
            for (int i = 0; i < this.MaxPageInterval - 1; i++)
            {
                if (++add < this.MaxPageCount)
                {
                    _panelMain.Children.Add(CreateButton(add));
                }
                else
                {
                    break;
                }
            }
        }

        private void ButtonPrev_OnClick(object sender, RoutedEventArgs e) => this.PageIndex--;

        private void ButtonNext_OnClick(object sender, RoutedEventArgs e) => this.PageIndex++;

        private RadioButton CreateButton(int page)
        {
            return new RadioButton
            {
                Style = this.RadioButtonStyle,
                Tag = page.ToString()
            };
        }

        private void ToggleButton_OnChecked(object sender, RoutedEventArgs e)
        {
            if (!(e.OriginalSource is RadioButton button)) return;
            if (button.IsChecked == false) return;
            this.PageIndex = int.Parse(button.Tag.ToString());
        }

        #endregion Private Methods       


        public Style RadioButtonStyle
        {
            get { return (Style)GetValue(RadioButtonStyleProperty); }
            set { SetValue(RadioButtonStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RadioButtonStyleProperty =
            DependencyProperty.Register("RadioButtonStyle", typeof(Style), typeof(PagerBox), new FrameworkPropertyMetadata(default(Style), (d, e) =>
             {
                 PagerBox control = d as PagerBox;

                 if (control == null) return;

                 if (e.OldValue is Style o)
                 {

                 }

                 if (e.NewValue is Style n)
                 {

                 }

             }));
    }
}
