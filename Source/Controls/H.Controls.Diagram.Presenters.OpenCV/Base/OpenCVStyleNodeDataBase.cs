global using H.Controls.Diagram.Presenter.NodeDatas;
using System.Windows.Controls;

namespace H.Controls.Diagram.Presenters.OpenCV.Base;

public abstract class OpenCVStyleNodeDataBase : FlowableNodeData
{
    private bool _useLeft;
    [DefaultValue(true)]
    [Display(Name = "启用左侧", GroupName = "样式")]
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
    [Display(Name = "启用右侧侧", GroupName = "样式")]
    public bool UseRight
    {
        get { return _useRight; }
        set
        {
            _useRight = value;
            RaisePropertyChanged();
        }
    }

    private double _flagLength;
    [DefaultValue(10)]
    [Display(Name = "标签宽度", GroupName = "样式")]
    public double FlagLength
    {
        get { return _flagLength; }
        set
        {
            _flagLength = value;
            RaisePropertyChanged();
        }
    }

    public override void LoadDefault()
    {
        base.LoadDefault();
        this.Width = 180;
        this.Height = 60;
    }

    protected override IEnumerable<IPortData> CreatePortDatas()
    {
        {
            IPortData port = CreatePortData();
            port.Dock = Dock.Top;
            port.PortType = PortType.Input;
            yield return port;
        }
        {
            IPortData port = CreatePortData();
            port.Dock = Dock.Bottom;
            port.PortType = PortType.OutPut;
            yield return port;
        }
    }
}