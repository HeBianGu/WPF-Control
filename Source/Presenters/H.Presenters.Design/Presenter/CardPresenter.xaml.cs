using H.Providers.Mvvm;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace H.Presenters.Design
{
    [Display(Name = "卡片")]
    public class CardPresenter : TitlePresenter
    {
        public CardPresenter()
        {
            Title = "总计：";
            Text = "80.34%";
            Height = 80.0;
        }
        public override void LoadDefault()
        {
            base.LoadDefault();
            FromColor = Colors.Orange;
            ToColor = Colors.OrangeRed;
            DropShadowEffectOpacity = 0.5;
            Height = 80;
            Width = 200;
            Padding = new Thickness(10);
            Margin = new Thickness(10);
            CornerRadius = new CornerRadius(5);
            Foreground = Brushes.White;
            TitleForeground = Brushes.White;
            FontSize = 25;
            TitleFontSize = 15;
            HorizontalContentAlignment = HorizontalAlignment.Center;
            VerticalContentAlignment = VerticalAlignment.Center;
            Orientation = Orientation.Horizontal;
        }
        private Color _fromColor;
        [Display(Name = "起始颜色", GroupName = "常用,样式")]
        public Color FromColor
        {
            get { return _fromColor; }
            set
            {
                _fromColor = value;
                RaisePropertyChanged();
            }
        }

        private Color _toColor;
        [Display(Name = "终止颜色", GroupName = "常用,样式")]
        public Color ToColor
        {
            get { return _toColor; }
            set
            {
                _toColor = value;
                RaisePropertyChanged();
            }
        }

        private double _dropShadowEffectOpacity;
        [Display(Name = "阴影透明度", GroupName = "常用,样式")]
        public double DropShadowEffectOpacity
        {
            get { return _dropShadowEffectOpacity; }
            set
            {
                _dropShadowEffectOpacity = value;
                RaisePropertyChanged();
            }
        }


        private CornerRadius _CornerRadius;
        [Display(Name = "圆角", GroupName = "常用,样式")]
        public CornerRadius CornerRadius
        {
            get { return _CornerRadius; }
            set
            {
                _CornerRadius = value;
                RaisePropertyChanged();
            }
        }

        private Orientation _orientation;
        [Display(Name = "对齐方式", GroupName = "常用,样式")]
        public Orientation Orientation
        {
            get { return _orientation; }
            set
            {
                _orientation = value;
                RaisePropertyChanged();
            }
        }
    }
}
