// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.Form.PropertyItem.Attribute;

namespace H.Components.VisionDiagrams.OpenCV.NodeDatas.Basic;
[Icon(FontIcons.Color)]
[Display(Name = "重复图片", GroupName = "基础函数", Description = "会改变整个图像的像素数量和分辨率", Order = 3)]
public class Repeat : OpenCVNodeDataBase, IPreprocessingGroupableNodeData
{

    //private int _ny = 2;
    //[PropertyItem(typeof(Int32SliderTextPropertyItem))]
    //[DefaultValue(2)]
    //[Range(1, 10)]
    //[Display(Name = "Y重复个数", GroupName = VisionTabNames.RunParameters)]

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

    private IExpressionKey _nyIntExpression;
    [Tab(VisionTabNames.RunParameters)]
    [GetMethodNameSource(nameof(GetIntFromExpressionKeys))]
    [PropertyItem(typeof(InputExpressionComboBoxTextPropertyItem<int>))]
    [Display(Name = "Y重复个数", GroupName = VisionTabNames.RunParameters)]

    public IExpressionKey nyIntExpression
    {
        get { return _nyIntExpression; }
        set
        {
            _nyIntExpression = value;
            RaisePropertyChanged();
        }
    }

    private IExpressionKey _nxIntExpression;
    [Tab(VisionTabNames.RunParameters)]
    [GetMethodNameSource(nameof(GetIntFromExpressionKeys))]
    [PropertyItem(typeof(InputExpressionComboBoxTextPropertyItem<int>))]
    [Display(Name = "X重复个数", GroupName = VisionTabNames.RunParameters)]

    public IExpressionKey nxIntExpression
    {
        get { return _nxIntExpression; }
        set
        {
            _nxIntExpression = value;
            RaisePropertyChanged();
        }
    }

    public override IEnumerable<IExpression> GetDefaultValueExpressions(Predicate<object> predicate = null)
    {
        yield return new VarExpression<int>(2, "默认重复个数");
    }

    //private int _nx = 2;
    //[PropertyItem(typeof(Int32SliderTextPropertyItem))]
    //[DefaultValue(2)]
    //[Range(1, 10)]
    //[Display(Name = "X重复个数", GroupName = VisionTabNames.RunParameters)]

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
        var ry = this.GetExpressionValue<int>(this.nyIntExpression);
        if (!ry.success)
            return this.Error(fromImage.Clone(), "获取Y重复个数失败");
        {
            ny = ry.value;
        }
        int nx = 1;
        var rx = this.GetExpressionValue<int>(this.nxIntExpression);
        if (!rx.success)
            return this.Error(fromImage.Clone(), "获取X重复个数失败");
        nx = rx.value;
        Mat result = new Mat();
        Cv2.Repeat(fromImage, nx <= 0 ? 1 : nx, ny <= 0 ? 1 : ny, result);
        return this.OK(result);
    }
}
