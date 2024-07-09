
using H.Extensions.Attach;
using H.Extensions.Setting;
using H.Services.Common;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace H.Modules.Guide
{
    [Display(Name = "登录页面", GroupName = SettingGroupNames.GroupSystem, Description = "登录页面设置的信息")]
    public class GuideOptions : IocOptionInstance<GuideOptions>
    {
        private bool _useOnLoad;
        [DefaultValue(true)]
        [Display(Name = "启动时显示新手向导")]
        public bool UseOnLoad
        {
            get { return _useOnLoad; }
            set
            {
                _useOnLoad = value;
                RaisePropertyChanged();
            }
        }

        public override bool Load(out string message)
        {
            message = null;
            var r = Application.Current.Dispatcher.Invoke(() =>
              {
                  return base.Load(out string message);
              });

            if (this.UseOnLoad)
            {
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.SystemIdle, new Action(() =>
                          {
                              Cattach.SetIsGuide(Application.Current.MainWindow, true);
                          }));

                this.UseOnLoad = false;
            }
            return r;
        }
        private double _textMaxWidth = 300.0;
        [Range(100.0, 800.0)]
        [Display(Name = "详情最大宽度")]
        public double TextMaxWidth
        {
            get { return _textMaxWidth; }
            set
            {
                _textMaxWidth = value;
                RaisePropertyChanged();
            }
        }

        private Color _coverColor = Colors.Black;
        [Display(Name = "遮盖背景")]
        public Color CoverColor
        {
            get { return _coverColor; }
            set
            {
                _coverColor = value;
                RaisePropertyChanged();
            }
        }

        private double _coverOpacity = 0.6;
        [Range(0.0, 1.0)]
        [Display(Name = "遮盖背景")]
        public double CoverOpacity
        {
            get { return _coverOpacity; }
            set
            {
                _coverOpacity = value;
                RaisePropertyChanged();
            }
        }

        public override void LoadDefault()
        {
            base.LoadDefault();
            //this.Stroke = Application.Current.FindResource(BrushKeys.Orange) as Brush;
        }


        private Brush _stroke = Brushes.Orange;
        [Display(Name = "线条颜色")]
        public Brush Stroke
        {
            get { return _stroke; }
            set
            {
                _stroke = value;
                RaisePropertyChanged();
            }
        }


        private double _strokeThickness = 1;
        [Display(Name = "线条厚度")]
        public double StrokeThickness
        {
            get { return _strokeThickness; }
            set
            {
                _strokeThickness = value;
                RaisePropertyChanged();
            }
        }


        private DoubleCollection _strokeDashArray = new DoubleCollection(new double[] { 1.0, 1.0 });
        [Display(Name = "线条虚线")]
        public DoubleCollection StrokeDashArray
        {
            get { return _strokeDashArray; }
            set
            {
                _strokeDashArray = value;
                RaisePropertyChanged();
            }
        }

        private int _animationDuration = 100;
        [Display(Name = "动画间隔")]
        public int AnimationDuration
        {
            get { return _animationDuration; }
            set
            {
                _animationDuration = value;
                RaisePropertyChanged();
            }
        }

        //public override bool Invoke(out string message)
        //{
        //    Application.Current.Dispatcher.Invoke(() =>
        //    {
        //        var use = Cattach.GetIsGuide(Application.Current.MainWindow);
        //        Cattach.SetIsGuide(Application.Current.MainWindow, !use);
        //    });

        //    message = null;
        //    return true;
        //}

    }
}
