// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")
global using System.Windows;
using H.Controls.ImageColorPicker.Drawings;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace H.Controls.ImageColorPicker
{
    public class ImageColorPickerBox : FrameworkElement
    {
        static ImageColorPickerBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ImageColorPickerBox), new FrameworkPropertyMetadata(typeof(ImageColorPickerBox)));
        }

        private VisualCollection _visualCollection;
        private ImageDrawingVisual _imageDrawingVisual = new ImageDrawingVisual();
        private ColorPickerDrawingVisual _pickerDrawingVisual;
        public ImageColorPickerBox()
        {
            this._visualCollection = new VisualCollection(this);
            this._pickerDrawingVisual = new ColorPickerDrawingVisual();
            this._visualCollection.Add(_imageDrawingVisual);
            this._visualCollection.Add(_pickerDrawingVisual);
            this.MouseDown += this.ROIBox_MouseDown;
            this.MouseMove += this.ROIBox_MouseMove;
            this.MouseLeave += this.ROIBox_MouseLeave;
            this.Loaded += this.ROIBox_Loaded;
        }

        private void ROIBox_Loaded(object sender, RoutedEventArgs e)
        {
            this.UpdateImage();
            this.UpdatePicker();
        }

        private void ROIBox_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            this.PreviewColor = null;
            this.PreviewPosition = null;
            this.UpdatePicker();
        }

        private void ROIBox_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (this.ImageSource is BitmapSource bitmap)
            {
                var p = e.GetPosition(this);
                var color = this.GetPixelColor(bitmap, (int)p.X, (int)p.Y);
                this.PreviewPosition = p;
                this.PreviewColor = color;
                this.UpdatePicker();
            }
        }

        private void ROIBox_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (this.ImageSource is BitmapSource bitmap)
            {
                var p = e.GetPosition(this);
                var color = this.GetPixelColor(bitmap, (int)p.X, (int)p.Y);
                this.Position =new Point((int)p.X, (int)p.Y);
                this.Color = color;
                this.UpdatePicker();
            }
        }

        #region - VisualCollection -

        protected override Visual GetVisualChild(int index)
        {
            return _visualCollection[index];
        }
        protected override int VisualChildrenCount
        {
            get
            {
                return _visualCollection.Count;
            }
        }
        #endregion

        public ImageSource ImageSource
        {
            get { return (ImageSource)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.Register("ImageSource", typeof(ImageSource), typeof(ImageColorPickerBox), new FrameworkPropertyMetadata(default(ImageSource), (d, e) =>
            {
                ImageColorPickerBox control = d as ImageColorPickerBox;

                if (control == null) return;

                if (e.OldValue is ImageSource o)
                {

                }

                if (e.NewValue is ImageSource n)
                {

                }
                control.UpdateImage();
            }));

        private void UpdateImage()
        {
            this.Width = this.ImageSource?.Width ?? 0;
            this.Height = this.ImageSource?.Height ?? 0;
            this._imageDrawingVisual.ImageSource = this.ImageSource;
            this._imageDrawingVisual.Draw();
        }

        public Color? Color
        {
            get { return (Color)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        public static readonly DependencyProperty ColorProperty =
            DependencyProperty.Register("Color", typeof(Color?), typeof(ImageColorPickerBox), new FrameworkPropertyMetadata(default(Color?), (d, e) =>
            {
                ImageColorPickerBox control = d as ImageColorPickerBox;

                if (control == null) return;


            }));


        public Point? Position
        {
            get { return (Point?)GetValue(PositionProperty); }
            set { SetValue(PositionProperty, value); }
        }

        public static readonly DependencyProperty PositionProperty =
            DependencyProperty.Register("Position", typeof(Point?), typeof(ImageColorPickerBox), new FrameworkPropertyMetadata(default(Point?), (d, e) =>
            {
                ImageColorPickerBox control = d as ImageColorPickerBox;

                if (control == null) return;

            }));


        public Point? PreviewPosition
        {
            get { return (Point?)GetValue(PreviewPositionProperty); }
            set { SetValue(PreviewPositionProperty, value); }
        }

        public static readonly DependencyProperty PreviewPositionProperty =
            DependencyProperty.Register("PreviewPosition", typeof(Point?), typeof(ImageColorPickerBox), new FrameworkPropertyMetadata(default(Point?), (d, e) =>
            {
                ImageColorPickerBox control = d as ImageColorPickerBox;

                if (control == null) return;
            }));



        public Color? PreviewColor
        {
            get { return (Color?)GetValue(PreviewColorProperty); }
            set { SetValue(PreviewColorProperty, value); }
        }

        public static readonly DependencyProperty PreviewColorProperty =
            DependencyProperty.Register("PreviewColor", typeof(Color?), typeof(ImageColorPickerBox), new FrameworkPropertyMetadata(default(Color?), (d, e) =>
            {
                ImageColorPickerBox control = d as ImageColorPickerBox;
                if (control == null)
                    return;

            }));

        private void UpdatePicker()
        {
            this._pickerDrawingVisual.Position = this.Position;
            this._pickerDrawingVisual.PreviewPosition = this.PreviewPosition;
            this._pickerDrawingVisual.PreviewColor = this.PreviewColor;
            this._pickerDrawingVisual.Length = Math.Sqrt(this.ImageSource.Width * this.ImageSource.Width + this.ImageSource.Height * this.ImageSource.Height) / 100.0;
            this._pickerDrawingVisual.Draw();
        }

        public Color GetPixelColor(BitmapSource bitmap, int x, int y)
        {
            if (x < 0 || x >= bitmap.PixelWidth || y < 0 || y >= bitmap.PixelHeight)
                return Colors.Transparent;
            byte[] pixel = new byte[4];
            bitmap.CopyPixels(new Int32Rect(x, y, 1, 1), pixel, 4, 0);
            return System.Windows.Media.Color.FromArgb(255, pixel[2], pixel[1], pixel[0]);
        }
    }
}
