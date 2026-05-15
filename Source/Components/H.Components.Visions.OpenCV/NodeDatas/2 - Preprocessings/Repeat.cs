// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Components.Visions.OpenCV.NodeDatas.Basic;
[Icon(FontIcons.Color)]
[Display(Name = "重复图片", GroupName = "基础函数", Description = "会改变整个图像的像素数量和分辨率", Order = 3)]
public class Repeat : OpenCVNodeDataBase, IPreprocessingGroupableNodeData
{

    //private int _ny = 2;
    //[PropertyItem(typeof(Int32SliderTextPropertyItem))]
    //[DefaultValue(2)]
    //[Range(1, 10)]
    //[Display(Name = "Y重复个数", GroupName = VisionPropertyGroupNames.RunParameters)]

    //public int ny
    //{
    //    get { return _ny; }
    //    set
    //    {
    //        _ny = value;

    //        RaisePropertyChanged();
    //        this.UpdateInvokeCurrent();
    //    }
    //}

    private NodeDataExpression _nyIntExpression = new ConstNodeDataExpression<int>(2, "默认");
    [GetMethodNameSource(nameof(GetIntFromExpressions))]
    [PropertyItem(typeof(ExpressionComboBoxPropertyItem))]
    [Display(Name = "Y重复个数", GroupName = VisionPropertyGroupNames.RunParameters)]

    public NodeDataExpression nyIntExpression
    {
        get { return _nyIntExpression; }
        set
        {
            _nyIntExpression = value;
            RaisePropertyChanged();
        }
    }

    private NodeDataExpression _nxIntExpression = new ConstNodeDataExpression<int>(2, "默认");
    [GetMethodNameSource(nameof(GetIntFromExpressions))]
    [PropertyItem(typeof(ExpressionComboBoxPropertyItem))]
    [Display(Name = "X重复个数", GroupName = VisionPropertyGroupNames.RunParameters)]

    public NodeDataExpression nxIntExpression
    {
        get { return _nxIntExpression; }
        set
        {
            _nxIntExpression = value;
            RaisePropertyChanged();
        }
    }

    public override IEnumerable<NodeDataExpression> GetDefaultValueExpressions(Predicate<object> predicate = null)
    {
        yield return new ConstNodeDataExpression<int>(2, "默认重复个数");
    }

    //private int _nx = 2;
    //[PropertyItem(typeof(Int32SliderTextPropertyItem))]
    //[DefaultValue(2)]
    //[Range(1, 10)]
    //[Display(Name = "X重复个数", GroupName = VisionPropertyGroupNames.RunParameters)]

    //public int nx
    //{
    //    get { return _nx; }
    //    set
    //    {
    //        _nx = value;
    //        RaisePropertyChanged();
    //        this.UpdateInvokeCurrent();
    //    }
    //}

    protected override FlowableResult<IMatImage> Invoke(Mat fromImage)
    {
        int ny = 1;
        if (this.TryGetExpressionValue<int>(this.nyIntExpression, out int y))
        {
            ny = y;
        }
        int nx = 1;
        if (this.TryGetExpressionValue<int>(this.nxIntExpression, out int x))
        {
            nx = x;
        }
        Mat result = new Mat();
        Cv2.Repeat(fromImage, nx <= 0 ? 1 : nx, ny <= 0 ? 1 : ny, result);
        return this.OK(result);
    }
}
