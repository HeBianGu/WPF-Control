// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using H.Extensions.Setting;
using H.Providers.Ioc;
using H.Themes.Default;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace H.Controls.Diagram.Extension
{
    [Display(Name = "绘图面板", GroupName = SettingGroupNames.GroupApp)]
    public class DiagramSetting : LazySettableInstance<DiagramSetting>
    {
        private Color _symbolDefaultBackground = (Application.Current.FindResource(BrushKeys.Foreground) as SolidColorBrush).Color;
        [Display(Name = "符号默认背景色")]
        public Color SymbolDefaultBackground
        {
            get { return _symbolDefaultBackground; }
            set
            {
                _symbolDefaultBackground = value;
                RaisePropertyChanged();
            }
        }


        private Color _nodeDataDefaultFill = (Application.Current.FindResource(BrushKeys.Background) as SolidColorBrush).Color;
        [Display(Name = "节点默认背景色")]
        public Color NodeDataDefaultFill
        {
            get { return _nodeDataDefaultFill; }
            set
            {
                _nodeDataDefaultFill = value;
                RaisePropertyChanged();
            }
        }

        private Color _nodeDataDefaultStroke = (Application.Current.FindResource(BrushKeys.Foreground) as SolidColorBrush).Color;
        [Display(Name = "节点默认前景色")]
        public Color NodeDataDefaultStroke
        {
            get { return _nodeDataDefaultStroke; }
            set
            {
                _nodeDataDefaultStroke = value;
                RaisePropertyChanged();
            }
        }

        private Color _nodeDataDefaultForeground = (Application.Current.FindResource(BrushKeys.Foreground) as SolidColorBrush).Color;
        [Display(Name = "节点默认字体色")]
        public Color NodeDataDefaultForeground
        {
            get { return _nodeDataDefaultForeground; }
            set
            {
                _nodeDataDefaultForeground = value;
                RaisePropertyChanged();
            }
        }


        private bool _useTop;
        [DefaultValue(true)]
        [Display(Name = "启用顶部刻度尺")]
        public bool UseTop
        {
            get { return _useTop; }
            set
            {
                _useTop = value;
                RaisePropertyChanged();
            }
        }


        private bool _useBottom;
        [DefaultValue(false)]
        [Display(Name = "启用底部刻度尺")]
        public bool UseBottom
        {
            get { return _useBottom; }
            set
            {
                _useBottom = value;
                RaisePropertyChanged();
            }
        }


        private bool _useLeft;
        [DefaultValue(true)]
        [Display(Name = "启用左侧刻度尺")]
        public bool UseLeft
        {
            get { return _useLeft; }
            set
            {
                _useLeft = value;
                RaisePropertyChanged();
            }
        }

        private bool _useRight;
        [DefaultValue(false)]
        [Display(Name = "启用右侧刻度尺")]
        public bool UseRight
        {
            get { return _useRight; }
            set
            {
                _useRight = value;
                RaisePropertyChanged();
            }
        }


        private int _largeSplit;
        [Range(10, 1000)]
        [DefaultValue(100)]
        [Display(Name = "刻度尺大间隔")]
        public int LargeSplit
        {
            get { return _largeSplit; }
            set
            {
                _largeSplit = value;
                RaisePropertyChanged();
            }
        }


        private int _smallSplit;
        [Range(1, 1000)]
        [DefaultValue(20)]
        [Display(Name = "刻度尺小间隔")]
        public int SmallSplit
        {
            get { return _smallSplit; }
            set
            {
                _smallSplit = value;
                RaisePropertyChanged();
            }
        }

        private bool _useText;
        [DefaultValue(true)]
        [Display(Name = "显示刻度尺文本")]
        public bool UseText
        {
            get { return _useText; }
            set
            {
                _useText = value;
                RaisePropertyChanged();
            }
        }


        private bool _useSmallSplitVerticalLine;
        [DefaultValue(true)]
        [Display(Name = "显示刻度尺垂直小网格线")]
        public bool UseSmallSplitVerticalLine
        {
            get { return _useSmallSplitVerticalLine; }
            set
            {
                _useSmallSplitVerticalLine = value;
                RaisePropertyChanged();
            }
        }


        private bool _useSmallSpliteHorizontalLine;
        [DefaultValue(true)]
        [Display(Name = "显示刻度尺水平小网格线")]
        public bool UseSmallSpliteHorizontalLine
        {
            get { return _useSmallSpliteHorizontalLine; }
            set
            {
                _useSmallSpliteHorizontalLine = value;
                RaisePropertyChanged();
            }
        }


        private bool _useSplitHorizontalLine;
        [DefaultValue(true)]
        [Display(Name = "显示刻度尺垂直大网格线")]
        public bool UseSplitHorizontalLine
        {
            get { return _useSplitHorizontalLine; }
            set
            {
                _useSplitHorizontalLine = value;
                RaisePropertyChanged();
            }
        }

        private bool _useSplitVerticalLine;
        [DefaultValue(true)]
        [Display(Name = "显示刻度尺水平大网格线")]
        public bool UseSplitVerticalLine
        {
            get { return _useSplitVerticalLine; }
            set
            {
                _useSplitVerticalLine = value;
                RaisePropertyChanged();
            }
        }


        private Brush _gridBackground;
        [DefaultValue(null)]
        [Display(Name = "刻度尺网格背景色")]
        public Brush GridBackground
        {
            get { return _gridBackground; }
            set
            {
                _gridBackground = value;
                RaisePropertyChanged();
            }
        }

        private Brush _gridLineBrush;
        [DefaultValue(null)]
        [Display(Name = "刻度尺网格线颜色")]
        public Brush GridLineBrush
        {
            get { return _gridLineBrush; }
            set
            {
                _gridLineBrush = value;
                RaisePropertyChanged();
            }
        }

        private Thickness _gridMargin;
        [Display(Name = "刻度尺网格边距")]
        public Thickness GridMargin
        {
            get { return _gridMargin; }
            set
            {
                _gridMargin = value;
                RaisePropertyChanged();
            }
        }
    }

    [Display(Name = "应用参数", GroupName = SettingGroupNames.GroupApp)]
    public class AppSetting : LazySettableInstance<AppSetting>
    {
        private bool _showLog;
        [Display(Name = "显示日志")]
        public bool ShowLog
        {
            get { return _showLog; }
            set
            {
                _showLog = value;
                RaisePropertyChanged();
            }
        }


        private bool _useAutoLocator = true;
        [Display(Name = "启用运行时自动定位到运行节点")]
        public bool UseAutoLocator
        {
            get { return _useAutoLocator; }
            set
            {
                _useAutoLocator = value;
                RaisePropertyChanged();
            }
        }

        private bool _useAutoScaleTo;
        [Display(Name = "启用运行时自动放大到运行节点")]
        public bool UseAutoScaleTo
        {
            get { return _useAutoScaleTo; }
            set
            {
                _useAutoScaleTo = value;
                RaisePropertyChanged();
            }
        }

        private bool _useAutoSelect;
        [Display(Name = "启用运行时自动选中到运行节点")]
        public bool UseAutoSelect
        {
            get { return _useAutoSelect; }
            set
            {
                _useAutoSelect = value;
                RaisePropertyChanged();
            }
        }


        private bool _useAutoShowLog = true;
        [Display(Name = "启用运行时自动显示日志")]
        public bool UseAutoShowLog
        {
            get { return _useAutoShowLog; }
            set
            {
                _useAutoShowLog = value;
                RaisePropertyChanged();
            }
        }


        private SelectionMode _propertySelectionMode;
        [Display(Name = "属性栏显示方式")]
        public SelectionMode PropertySelectionMode
        {
            get { return _propertySelectionMode; }
            set
            {
                _propertySelectionMode = value;
                RaisePropertyChanged();
            }
        }

        private bool _useMock = true;
        [Display(Name = "启用模拟仿真")]
        public bool UseMock
        {
            get { return _useMock; }
            set
            {
                _useMock = value;
                RaisePropertyChanged();
            }
        }

        private int _flowSleepMillisecondsTimeout;
        [DefaultValue(1000)]
        [Range(0, 10 * 1000)]
        [Display(Name = "流程运行延迟时间")]
        public int FlowSleepMillisecondsTimeout
        {
            get { return _flowSleepMillisecondsTimeout; }
            set
            {
                _flowSleepMillisecondsTimeout = value;
                RaisePropertyChanged();
            }
        }


    }
}
