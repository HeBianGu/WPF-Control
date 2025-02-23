using H.Extensions.Geometry;
using H.Themes.Default;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml.Serialization;

namespace H.Controls.Diagram.Extension
{
    public interface ITextPortData : IPortData
    {
        FontFamily FontFamily { get; set; }
        double FontSize { get; set; }
        FontStretch FontStretch { get; set; }
        FontStyle FontStyle { get; set; }
        FontWeight FontWeight { get; set; }
        Brush Foreground { get; set; }
        string Icon { get; set; }
        Brush Stroke { get; set; }
        string Text { get; set; }
        Thickness TextMargin { get; set; }
    }

    public class TextPortData : PortData, ITextPortData
    {
        public TextPortData()
        {
            //var display = this.GetType().GetCustomAttribute<DisplayAttribute>();
            //this.Icon = display?.ShortName;
            //this.Text = display?.Name;
            //this.Description = display?.Description;
            this.Data = this.GetGeometry();
            //this.Text = this.Name;
        }


        public override void LoadDefault()
        {
            base.LoadDefault();
            this.FontSize = (double)Application.Current.FindResource(FontSizeKeys.Default);
            //this.FontFamily = Application.Current.FindResource(LayoutKeys.FontFamily) as FontFamily;
            this.FontWeight = FontWeights.Normal;
            this.FontStyle = FontStyles.Normal;
            this.FontStretch = FontStretches.Normal;
            this.TextMargin = new Thickness(-50, -30, -50, 0);
        }

        public TextPortData(string nodeID, PortType portType) : this()
        {
            this.NodeID = nodeID;
            this.PortType = portType;
        }

        private string _text;
        [XmlIgnore]
        [Display(Name = "文本", GroupName = "常用")]
        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                RaisePropertyChanged();
            }
        }


        private string _icon;
        [XmlIgnore]
        [Display(Name = "图标", GroupName = "常用")]
        public override string Icon
        {
            get { return _icon; }
            set
            {
                _icon = value;
                RaisePropertyChanged();
            }
        }

        private double _fontSize;
        [Display(Name = "字号", GroupName = "文本")]
        public double FontSize
        {
            get { return _fontSize; }
            set
            {
                _fontSize = value;
                RaisePropertyChanged();
            }
        }

        private FontFamily _fontFamily;
        [Display(Name = "字体", GroupName = "文本")]
        public FontFamily FontFamily
        {
            get { return _fontFamily; }
            set
            {
                _fontFamily = value;
                RaisePropertyChanged();
            }
        }


        private FontStyle _fontStyle;
        [Display(Name = "字体样式", GroupName = "文本")]
        public FontStyle FontStyle
        {
            get { return _fontStyle; }
            set
            {
                _fontStyle = value;
                RaisePropertyChanged();
            }
        }

        private FontWeight _fontWeight;
        [Display(Name = "字体加粗", GroupName = "文本")]
        public FontWeight FontWeight
        {
            get { return _fontWeight; }
            set
            {
                _fontWeight = value;
                RaisePropertyChanged();
            }
        }

        private FontStretch _fontStretch;
        [Display(Name = "字体展开", GroupName = "文本")]
        public FontStretch FontStretch
        {
            get { return _fontStretch; }
            set
            {
                _fontStretch = value;
                RaisePropertyChanged();
            }
        }

        private Thickness _textMargin;
        [XmlIgnore]
        [Display(Name = "文本边距", GroupName = "样式")]
        public Thickness TextMargin
        {
            get { return _textMargin; }
            set
            {
                _textMargin = value;
                RaisePropertyChanged();
            }
        }


        private Brush _foreground;
        [DefaultValue(null)]
        [Display(Name = "文本颜色", GroupName = "样式")]
        public Brush Foreground
        {
            get { return _foreground; }
            set
            {
                _foreground = value;
                RaisePropertyChanged();
            }
        }

        private Brush _fill;
        [DefaultValue(null)]
        [Display(Name = "背景颜色", GroupName = "样式")]
        public Brush Fill
        {
            get { return _fill; }
            set
            {
                _fill = value;
                RaisePropertyChanged();
            }
        }


        private Brush _stroke;
        [DefaultValue(null)]
        [Display(Name = "边框颜色", GroupName = "样式")]
        public Brush Stroke
        {
            get { return _stroke; }
            set
            {
                _stroke = value;
                RaisePropertyChanged();
            }
        }

        //private Stretch _stretch = Stretch.Fill;
        //[Display(Name = "拉伸", GroupName = "样式")]
        //public Stretch Stretch
        //{
        //    get { return _stretch; }
        //    set
        //    {
        //        _stretch = value;
        //        RaisePropertyChanged();
        //    }
        //}


        private double _strokeThickness;
        [DefaultValue(1)]
        [Display(Name = "边框宽度", GroupName = "样式")]
        public double StrokeThickness
        {
            get { return _strokeThickness; }
            set
            {
                _strokeThickness = value;
                RaisePropertyChanged();
            }
        }


        private Geometry _data;
        [XmlIgnore]
        [Browsable(false)]
        public Geometry Data
        {
            get { return _data; }
            set
            {
                _data = value;
                RaisePropertyChanged();
            }
        }

        protected virtual Geometry GetGeometry()
        {
            return GeometryFactory.Circle;
        }

        public override void ApplayStyleTo(IPortData to)
        {
            base.ApplayStyleTo(to);

            if (to is TextPortData textNodeData)
            {
                textNodeData.Foreground = this.Foreground;
                textNodeData.FontFamily = this.FontFamily;
                textNodeData.FontSize = this.FontSize;
                textNodeData.FontStretch = this.FontStretch;
                textNodeData.FontStyle = this.FontStyle;
                textNodeData.FontWeight = this.FontWeight;

                textNodeData.Fill = this.Fill;
                textNodeData.Stroke = this.Stroke;
                textNodeData.StrokeThickness = this.StrokeThickness;
            }
        }
    }

    public static class TextPortDataExtension
    {
        public static void BuildData(this ITextPortData text, int offset = -30)
        {
            text.Icon = text.GetIcon();
            text.TextMargin = text.Dock.GetTextMargin(offset);
        }

        public static void BuildTextData(this IPortData port, int offset = -30)
        {
            if(port is ITextPortData text)
                text.BuildData(offset);
        }
    }
}
