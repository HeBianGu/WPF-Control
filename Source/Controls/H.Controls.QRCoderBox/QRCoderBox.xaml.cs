using QRCoder;
using QRCoder.Xaml;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace H.Controls.QRCoderBox
{
    [TemplatePart(Name = "PART_Image")]
    public class QRCoderBox : ContentControl
    {
        private Image _image;
        static QRCoderBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(QRCoderBox), new FrameworkPropertyMetadata(typeof(QRCoderBox)));
        }

        public QRCoderBox()
        {
            this.Loaded += (l, k) =>
            {
                this.RefreshData();
            };

        }


        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this._image = this.Template.FindName("PART_Image", this) as Image;
        }


        public void RefreshData()
        {
            this.DelayInvoke(() => this.RefreshDataInternal());
        }

        private void RefreshDataInternal()
        {
            if (this._image == null)
                return;
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(this.PlainText, this.ECCLevel, this.ForceUtf8, this.Utf8BOM, this.EciMode, this.RequestedVersion);
            XamlQRCode qrCode = new XamlQRCode(qrCodeData);
            DrawingImage qrCodeAsXaml = qrCode.GetGraphic(new Size(this.PixelsPerModule, this.PixelsPerModule), this.DarkBrush, this.LightBrush, this.DrawQuietZones);
            this._image.Source = qrCodeAsXaml;
        }

        public bool DrawQuietZones
        {
            get { return (bool)GetValue(DrawQuietZonesProperty); }
            set { SetValue(DrawQuietZonesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DrawQuietZonesProperty =
            DependencyProperty.Register("DrawQuietZones", typeof(bool), typeof(QRCoderBox), new FrameworkPropertyMetadata(true, (d, e) =>
            {
                QRCoderBox control = d as QRCoderBox;

                if (control == null) return;

                if (e.OldValue is bool o)
                {

                }

                if (e.NewValue is bool n)
                {

                }
                control.RefreshData();
            }));


        public Brush LightBrush
        {
            get { return (Brush)GetValue(LightBrushProperty); }
            set { SetValue(LightBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LightBrushProperty =
            DependencyProperty.Register("LightBrush", typeof(Brush), typeof(QRCoderBox), new FrameworkPropertyMetadata(Brushes.White, (d, e) =>
            {
                QRCoderBox control = d as QRCoderBox;

                if (control == null) return;

                if (e.OldValue is Brush o)
                {

                }

                if (e.NewValue is Brush n)
                {

                }
                control.RefreshData();
            }));


        public Brush DarkBrush
        {
            get { return (Brush)GetValue(DarkBrushProperty); }
            set { SetValue(DarkBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DarkBrushProperty =
            DependencyProperty.Register("DarkBrush", typeof(Brush), typeof(QRCoderBox), new FrameworkPropertyMetadata(Brushes.Black, (d, e) =>
            {
                QRCoderBox control = d as QRCoderBox;

                if (control == null) return;

                if (e.OldValue is Brush o)
                {

                }

                if (e.NewValue is Brush n)
                {

                }
                control.RefreshData();
            }));




        public double IconSize
        {
            get { return (double)GetValue(IconSizeProperty); }
            set { SetValue(IconSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconSizeProperty =
            DependencyProperty.Register("IconSize", typeof(double), typeof(QRCoderBox), new FrameworkPropertyMetadata(100.0, (d, e) =>
            {
                QRCoderBox control = d as QRCoderBox;

                if (control == null) return;

                if (e.OldValue is double o)
                {

                }

                if (e.NewValue is double n)
                {

                }

            }));


        public ImageSource Icon
        {
            get { return (ImageSource)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(ImageSource), typeof(QRCoderBox), new FrameworkPropertyMetadata(default(ImageSource), (d, e) =>
            {
                QRCoderBox control = d as QRCoderBox;

                if (control == null) return;

                if (e.OldValue is ImageSource o)
                {

                }

                if (e.NewValue is ImageSource n)
                {

                }

            }));


        public int RequestedVersion
        {
            get { return (int)GetValue(RequestedVersionProperty); }
            set { SetValue(RequestedVersionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RequestedVersionProperty =
            DependencyProperty.Register("RequestedVersion", typeof(int), typeof(QRCoderBox), new FrameworkPropertyMetadata(-1, (d, e) =>
            {
                QRCoderBox control = d as QRCoderBox;

                if (control == null) return;

                if (e.OldValue is int o)
                {

                }

                if (e.NewValue is int n)
                {

                }
                control.RefreshData();
            }));


        public bool Utf8BOM
        {
            get { return (bool)GetValue(Utf8BOMProperty); }
            set { SetValue(Utf8BOMProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Utf8BOMProperty =
            DependencyProperty.Register("Utf8BOM", typeof(bool), typeof(QRCoderBox), new FrameworkPropertyMetadata(default(bool), (d, e) =>
            {
                QRCoderBox control = d as QRCoderBox;

                if (control == null) return;

                if (e.OldValue is bool o)
                {

                }

                if (e.NewValue is bool n)
                {

                }
                control.RefreshData();
            }));


        public bool ForceUtf8
        {
            get { return (bool)GetValue(ForceUtf8Property); }
            set { SetValue(ForceUtf8Property, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ForceUtf8Property =
            DependencyProperty.Register("ForceUtf8", typeof(bool), typeof(QRCoderBox), new FrameworkPropertyMetadata(default(bool), (d, e) =>
            {
                QRCoderBox control = d as QRCoderBox;

                if (control == null) return;

                if (e.OldValue is bool o)
                {

                }

                if (e.NewValue is bool n)
                {

                }
                control.RefreshData();

            }));


        public QRCodeGenerator.EciMode EciMode
        {
            get { return (QRCodeGenerator.EciMode)GetValue(EciModeProperty); }
            set { SetValue(EciModeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EciModeProperty =
            DependencyProperty.Register("EciMode", typeof(QRCodeGenerator.EciMode), typeof(QRCoderBox), new FrameworkPropertyMetadata(QRCodeGenerator.EciMode.Default, (d, e) =>
            {
                QRCoderBox control = d as QRCoderBox;

                if (control == null) return;

                if (e.OldValue is QRCodeGenerator.EciMode o)
                {

                }

                if (e.NewValue is QRCodeGenerator.EciMode n)
                {

                }
                control.RefreshData();

            }));


        public string PlainText
        {
            get { return (string)GetValue(PlainTextProperty); }
            set { SetValue(PlainTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PlainTextProperty =
            DependencyProperty.Register("PlainText", typeof(string), typeof(QRCoderBox), new FrameworkPropertyMetadata(default(string), (d, e) =>
            {
                QRCoderBox control = d as QRCoderBox;

                if (control == null) return;

                if (e.OldValue is string o)
                {

                }

                if (e.NewValue is string n)
                {

                }
                control.RefreshData();

            }));


        public QRCodeGenerator.ECCLevel ECCLevel
        {
            get { return (QRCodeGenerator.ECCLevel)GetValue(ECCLevelProperty); }
            set { SetValue(ECCLevelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ECCLevelProperty =
            DependencyProperty.Register("ECCLevel", typeof(QRCodeGenerator.ECCLevel), typeof(QRCoderBox), new FrameworkPropertyMetadata(QRCodeGenerator.ECCLevel.Q, (d, e) =>
            {
                QRCoderBox control = d as QRCoderBox;

                if (control == null) return;

                if (e.OldValue is QRCodeGenerator.ECCLevel o)
                {

                }

                if (e.NewValue is QRCodeGenerator.ECCLevel n)
                {

                }
                control.RefreshData();

            }));


        public int PixelsPerModule
        {
            get { return (int)GetValue(PixelsPerModuleProperty); }
            set { SetValue(PixelsPerModuleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PixelsPerModuleProperty =
            DependencyProperty.Register("PixelsPerModule", typeof(int), typeof(QRCoderBox), new FrameworkPropertyMetadata(20, (d, e) =>
            {
                QRCoderBox control = d as QRCoderBox;

                if (control == null) return;

                if (e.OldValue is int o)
                {

                }

                if (e.NewValue is int n)
                {

                }
                control.RefreshData();
            }));
    }
}
