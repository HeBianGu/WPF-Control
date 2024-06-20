// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Markup;

namespace H.Controls.PropertyGrid
{
    /// <summary>
    /// Represents a spinner control that includes two Buttons.
    /// </summary>
    [TemplatePart(Name = PART_IncreaseButton, Type = typeof(ButtonBase))]
    [TemplatePart(Name = PART_DecreaseButton, Type = typeof(ButtonBase))]
    [ContentProperty("Content")]
    public class ButtonSpinner : Spinner
    {
        private const string PART_IncreaseButton = "PART_IncreaseButton";
        private const string PART_DecreaseButton = "PART_DecreaseButton";

        #region Properties

        #region AllowSpin

        public static readonly DependencyProperty AllowSpinProperty = DependencyProperty.Register("AllowSpin", typeof(bool), typeof(ButtonSpinner), new UIPropertyMetadata(true, AllowSpinPropertyChanged));
        public bool AllowSpin
        {
            get
            {
                return (bool)GetValue(AllowSpinProperty);
            }
            set
            {
                SetValue(AllowSpinProperty, value);
            }
        }

        private static void AllowSpinPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ButtonSpinner source = d as ButtonSpinner;
            source.OnAllowSpinChanged((bool)e.OldValue, (bool)e.NewValue);
        }

        #endregion //AllowSpin

        #region ButtonSpinnerLocation

        public static readonly DependencyProperty ButtonSpinnerLocationProperty = DependencyProperty.Register("ButtonSpinnerLocation", typeof(Location), typeof(ButtonSpinner), new UIPropertyMetadata(Location.Right));
        public Location ButtonSpinnerLocation
        {
            get
            {
                return (Location)GetValue(ButtonSpinnerLocationProperty);
            }
            set
            {
                SetValue(ButtonSpinnerLocationProperty, value);
            }
        }

        #endregion //ButtonSpinnerLocation

        #region Content

        /// <summary>
        /// Identifies the Content dependency property.
        /// </summary>
        public static readonly DependencyProperty ContentProperty = DependencyProperty.Register("Content", typeof(object), typeof(ButtonSpinner), new PropertyMetadata(null, OnContentPropertyChanged));
        public object Content
        {
            get
            {
                return GetValue(ContentProperty);
            }
            set
            {
                SetValue(ContentProperty, value);
            }
        }

        /// <summary>
        /// ContentProperty property changed handler.
        /// </summary>
        /// <param name="d">ButtonSpinner that changed its Content.</param>
        /// <param name="e">Event arguments.</param>
        private static void OnContentPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ButtonSpinner source = d as ButtonSpinner;
            source.OnContentChanged(e.OldValue, e.NewValue);
        }

        #endregion //Content

        #region DecreaseButton

        private ButtonBase _decreaseButton;
        /// <summary>
        /// Gets or sets the DecreaseButton template part.
        /// </summary>
        private ButtonBase DecreaseButton
        {
            get
            {
                return _decreaseButton;
            }
            set
            {
                if (_decreaseButton != null)
                {
                    _decreaseButton.Click -= OnButtonClick;
                }

                _decreaseButton = value;

                if (_decreaseButton != null)
                {
                    _decreaseButton.Click += OnButtonClick;
                }
            }
        }

        #endregion //DecreaseButton

        #region IncreaseButton

        private ButtonBase _increaseButton;
        /// <summary>
        /// Gets or sets the IncreaseButton template part.
        /// </summary>
        private ButtonBase IncreaseButton
        {
            get
            {
                return _increaseButton;
            }
            set
            {
                if (_increaseButton != null)
                {
                    _increaseButton.Click -= OnButtonClick;
                }

                _increaseButton = value;

                if (_increaseButton != null)
                {
                    _increaseButton.Click += OnButtonClick;
                }
            }
        }

        #endregion //IncreaseButton

        #region ShowButtonSpinner

        public static readonly DependencyProperty ShowButtonSpinnerProperty = DependencyProperty.Register("ShowButtonSpinner", typeof(bool), typeof(ButtonSpinner), new UIPropertyMetadata(true));
        public bool ShowButtonSpinner
        {
            get
            {
                return (bool)GetValue(ShowButtonSpinnerProperty);
            }
            set
            {
                SetValue(ShowButtonSpinnerProperty, value);
            }
        }

        #endregion //ShowButtonSpinner

        #endregion //Properties

        #region Constructors

        static ButtonSpinner()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ButtonSpinner), new FrameworkPropertyMetadata(typeof(ButtonSpinner)));
        }

        #endregion //Constructors

        #region Base Class Overrides

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.IncreaseButton = GetTemplateChild(PART_IncreaseButton) as ButtonBase;
            this.DecreaseButton = GetTemplateChild(PART_DecreaseButton) as ButtonBase;

            SetButtonUsage();
        }

        /// <summary>
        /// Cancel LeftMouseButtonUp events originating from a button that has
        /// been changed to disabled.
        /// </summary>
        /// <param name="e">The data for the event.</param>
        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);

            Point mousePosition;
            if (this.IncreaseButton != null && this.IncreaseButton.IsEnabled == false)
            {
                mousePosition = e.GetPosition(this.IncreaseButton);
                if (mousePosition.X > 0 && mousePosition.X < this.IncreaseButton.ActualWidth &&
                    mousePosition.Y > 0 && mousePosition.Y < this.IncreaseButton.ActualHeight)
                {
                    e.Handled = true;
                }
            }

            if (this.DecreaseButton != null && this.DecreaseButton.IsEnabled == false)
            {
                mousePosition = e.GetPosition(this.DecreaseButton);
                if (mousePosition.X > 0 && mousePosition.X < this.DecreaseButton.ActualWidth &&
                    mousePosition.Y > 0 && mousePosition.Y < this.DecreaseButton.ActualHeight)
                {
                    e.Handled = true;
                }
            }
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up:
                    {
                        if (this.AllowSpin)
                        {
                            this.OnSpin(new SpinEventArgs(Spinner.SpinnerSpinEvent, SpinDirection.Increase));
                            e.Handled = true;
                        }

                        break;
                    }
                case Key.Down:
                    {
                        if (this.AllowSpin)
                        {
                            this.OnSpin(new SpinEventArgs(Spinner.SpinnerSpinEvent, SpinDirection.Decrease));
                            e.Handled = true;
                        }

                        break;
                    }
                case Key.Enter:
                    {
                        //Do not Spin on enter Key when spinners have focus
                        if (((this.IncreaseButton != null) && this.IncreaseButton.IsFocused)
                          || ((this.DecreaseButton != null) && this.DecreaseButton.IsFocused))
                        {
                            e.Handled = true;
                        }
                        break;
                    }
            }
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);

            if (!e.Handled && this.AllowSpin)
            {
                if (e.Delta != 0)
                {
                    SpinEventArgs spinnerEventArgs = new SpinEventArgs(Spinner.SpinnerSpinEvent, (e.Delta < 0) ? SpinDirection.Decrease : SpinDirection.Increase, true);
                    this.OnSpin(spinnerEventArgs);
                    e.Handled = spinnerEventArgs.Handled;
                }
            }
        }

        /// <summary>
        /// Called when valid spin direction changed.
        /// </summary>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        protected override void OnValidSpinDirectionChanged(ValidSpinDirections oldValue, ValidSpinDirections newValue)
        {
            SetButtonUsage();
        }


        #endregion //Base Class Overrides

        #region Event Handlers

        /// <summary>
        /// Handle click event of IncreaseButton and DecreaseButton template parts,
        /// translating Click to appropriate Spin event.
        /// </summary>
        /// <param name="sender">Event sender, should be either IncreaseButton or DecreaseButton template part.</param>
        /// <param name="e">Event args.</param>
        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            if (this.AllowSpin)
            {
                SpinDirection direction = sender == this.IncreaseButton ? SpinDirection.Increase : SpinDirection.Decrease;
                OnSpin(new SpinEventArgs(Spinner.SpinnerSpinEvent, direction));
            }
        }

        #endregion //Event Handlers

        #region Methods

        /// <summary>
        /// Occurs when the Content property value changed.
        /// </summary>
        /// <param name="oldValue">The old value of the Content property.</param>
        /// <param name="newValue">The new value of the Content property.</param>
        protected virtual void OnContentChanged(object oldValue, object newValue)
        {
        }

        protected virtual void OnAllowSpinChanged(bool oldValue, bool newValue)
        {
            SetButtonUsage();
        }

        /// <summary>
        /// Disables or enables the buttons based on the valid spin direction.
        /// </summary>
        private void SetButtonUsage()
        {
            // buttonspinner adds buttons that spin, so disable accordingly.
            if (this.IncreaseButton != null)
            {
                this.IncreaseButton.IsEnabled = this.AllowSpin && ((this.ValidSpinDirection & ValidSpinDirections.Increase) == ValidSpinDirections.Increase);
            }

            if (this.DecreaseButton != null)
            {
                this.DecreaseButton.IsEnabled = this.AllowSpin && ((this.ValidSpinDirection & ValidSpinDirections.Decrease) == ValidSpinDirections.Decrease);
            }
        }

        #endregion //Methods
    }
}
