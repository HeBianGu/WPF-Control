// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Components.Visions.OpenCV.NodeDatas.Basic;
[Icon(FontIcons.Color)]
[Display(Name = "色彩变换", GroupName = "基础函数", Description = "设置图片颜色", Order = 2)]
public class CvtColor : OpenCVNodeDataBase, IPreprocessingGroupableNodeData
{
    private ColorConversionCodes _colorConversionCode = ColorConversionCodes.BGR2GRAY;

    [DefaultValue(ColorConversionCodes.BGR2GRAY)]
    [Display(Name = "转换规则", GroupName = VisionPropertyGroupNames.RunParameters)]
    public ColorConversionCodes ColorConversionCode
    {
        get { return _colorConversionCode; }
        set
        {
            _colorConversionCode = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private int _dstCn = 0;
    [DefaultValue(0)]
    [Display(Name = "通道数", GroupName = VisionPropertyGroupNames.RunParameters)]
    public int DstCn
    {
        get { return _dstCn; }
        set
        {
            _dstCn = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    protected override FlowableResult<IMatImage> Invoke(Mat fromImage)
    {
        Mat mat = fromImage.CvtColor(this.ColorConversionCode, this.DstCn);
        return this.OK(mat);
    }

    public override void Clear()
    {
        base.Clear();
    }
}
