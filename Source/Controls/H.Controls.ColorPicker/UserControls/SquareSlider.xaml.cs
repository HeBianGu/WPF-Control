using H.Controls.ColorPicker.Models;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace H.Controls.ColorPicker.UserControls
{
    internal partial class SquareSlider : UserControl, INotifyPropertyChanged
    {
        public static readonly DependencyProperty HueProperty
            = DependencyProperty.Register(nameof(Hue), typeof(double), typeof(SquareSlider),
                new PropertyMetadata(0.0, OnHueChanged));

        public static readonly DependencyProperty HeadXProperty
            = DependencyProperty.Register(nameof(HeadX), typeof(double), typeof(SquareSlider),
                new PropertyMetadata(0.0));

        public static readonly DependencyProperty HeadYProperty
            = DependencyProperty.Register(nameof(HeadY), typeof(double), typeof(SquareSlider),
                new PropertyMetadata(0.0));

        public static readonly DependencyProperty PickerTypeProperty
            = DependencyProperty.Register(nameof(PickerType), typeof(PickerType), typeof(SquareSlider),
                new PropertyMetadata(PickerType.HSV, OnColorSpaceChanged));

        private WriteableBitmap _gradientBitmap;

        private double _rangeX;
        private double _rangeY;

        private Func<double, double, double, Tuple<double, double, double>> colorSpaceConversionMethod =
            ColorSpaceHelper.HsvToRgb;

        public SquareSlider()
        {
            this.GradientBitmap = new WriteableBitmap(32, 32, 96, 96, PixelFormats.Rgb24, null);
            InitializeComponent();
            RecalculateGradient();
        }

        public double Hue
        {
            get => (double)GetValue(HueProperty);
            set => SetValue(HueProperty, value);
        }

        public double HeadX
        {
            get => (double)GetValue(HeadXProperty);
            set => SetValue(HeadXProperty, value);
        }

        public double HeadY
        {
            get => (double)GetValue(HeadYProperty);
            set => SetValue(HeadYProperty, value);
        }

        public PickerType PickerType
        {
            get => (PickerType)GetValue(PickerTypeProperty);
            set => SetValue(PickerTypeProperty, value);
        }

        public double RangeX
        {
            get => _rangeX;
            set
            {
                _rangeX = value;
                RaisePropertyChanged(nameof(this.RangeX));
            }
        }

        public double RangeY
        {
            get => _rangeY;
            set
            {
                _rangeY = value;
                RaisePropertyChanged(nameof(this.RangeY));
            }
        }

        public WriteableBitmap GradientBitmap
        {
            get => _gradientBitmap;
            set
            {
                _gradientBitmap = value;
                RaisePropertyChanged(nameof(this.GradientBitmap));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RecalculateGradient()
        {
            var w = this.GradientBitmap.PixelWidth;
            var h = this.GradientBitmap.PixelHeight;
            var hue = this.Hue;
            var pixels = new byte[w * h * 3];
            for (var j = 0; j < h; j++)
                for (var i = 0; i < w; i++)
                {
                    var rgbtuple = colorSpaceConversionMethod(hue, i / (double)(w - 1), (h - 1 - j) / (double)(h - 1));
                    double r = rgbtuple.Item1, g = rgbtuple.Item2, b = rgbtuple.Item3;
                    var pos = (j * h + i) * 3;
                    pixels[pos] = (byte)(r * 255);
                    pixels[pos + 1] = (byte)(g * 255);
                    pixels[pos + 2] = (byte)(b * 255);
                }

            this.GradientBitmap.WritePixels(new Int32Rect(0, 0, w, h), pixels, w * 3, 0);
        }

        private static void OnColorSpaceChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            var sender = (SquareSlider)d;
            if ((PickerType)args.NewValue == PickerType.HSV)
                sender.colorSpaceConversionMethod = ColorSpaceHelper.HsvToRgb;
            else
                sender.colorSpaceConversionMethod = ColorSpaceHelper.HslToRgb;

            sender.RecalculateGradient();
        }

        private static void OnHueChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            ((SquareSlider)d).RecalculateGradient();
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            ((UIElement)sender).CaptureMouse();
            UpdatePos(e.GetPosition(this));
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            var grid = (Grid)sender;
            if (grid.IsMouseCaptured)
                UpdatePos(e.GetPosition(this));
        }

        private void UpdatePos(Point pos)
        {
            this.HeadX = MathHelper.Clamp(pos.X / this.ActualWidth, 0, 1) * this.RangeX;
            this.HeadY = (1 - MathHelper.Clamp(pos.Y / this.ActualHeight, 0, 1)) * this.RangeY;
        }

        private void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            ((UIElement)sender).ReleaseMouseCapture();
        }

        private void RaisePropertyChanged(string property)
        {
            if (property != null) PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}