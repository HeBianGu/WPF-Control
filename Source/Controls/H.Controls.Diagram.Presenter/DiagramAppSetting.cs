// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using H.Extensions.Setting;

namespace H.Controls.Diagram.Presenter;

[Display(Name = "应用参数", GroupName = SettingGroupNames.GroupApp)]
public class DiagramAppSetting : LazySettableInstance<DiagramAppSetting>
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
