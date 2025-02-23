namespace H.Controls.Diagram.Extensions.OpenCV;
[Display(Name = "识别连通区域", GroupName = "基础检测", Order = 0)]
public class RenderBlobs : DetectorActionNodeDataBase
{
    private bool _useRectangle = true;
    [Display(Name = "绘制矩形", GroupName = "数据")]
    public bool UseRectangle
    {
        get { return _useRectangle; }
        set
        {
            _useRectangle = value;
            DispatcherRaisePropertyChanged();
        }
    }

    protected override IFlowableResult Refresh()
    {
        var preMat = this._preMat;
        var cc = Cv2.ConnectedComponentsEx(preMat);
        if (cc.LabelCount <= 1)
        {
            return Error("没有识别出联通区域");
        }
        var labelview = preMat.EmptyClone();
        cc.RenderBlobs(labelview);

        if (UseRectangle)
        {
            foreach (var blob in cc.Blobs.Skip(1))
            {
                labelview.Rectangle(blob.Rect, Scalar.Red);
            }
        }

        RefreshMatToView(labelview);
        return base.Refresh();
    }
}
