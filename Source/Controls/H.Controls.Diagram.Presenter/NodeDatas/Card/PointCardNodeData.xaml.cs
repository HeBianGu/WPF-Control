namespace H.Controls.Diagram.Presenter.NodeDatas.Card;

public class PointCardNodeData : CardNodeData
{
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
        this.FlagLength = 15;
    }
}
