// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Components.Visions.OpenCV.NodeDatas.Basic;

//[Icon(FontIcons.Color)]
//[Display(Name = "自然指数(Exp)运算", GroupName = "基础函数", Description = "对每个像素执行自然指数(Exp)运算", Order = 3)]
//public class Exp : OpenCVNodeDataBase, IPreprocessingGroupableNodeData
//{
//    protected override FlowableResult<IMatImage> Invoke(Mat fromImage)
//    {
//        Mat mat32 = new Mat();
//        from.Mat.ConvertTo(mat32, MatType.CV_32F); // 确保数据类型正确
//        Mat result = new Mat();
//        Cv2.Exp(mat32,result);
//        return this.OK(result);
//    }
//}

//[Icon(FontIcons.Color)]
//[Display(Name = "对数(Log)运算", GroupName = "基础函数", Description = "对每个像素执行对数(Log)运算", Order = 3)]
//public class Log : OpenCVNodeDataBase, IPreprocessingGroupableNodeData
//{
//    protected override FlowableResult<IMatImage> Invoke(Mat fromImage)
//    {
//        Mat result = new Mat();
//        Cv2.Log(from.Mat, result);
//        return this.OK(result);
//    }
//}

[Icon(FontIcons.Color)]
[Display(Name = "归一化运算", GroupName = "基础函数", Description = "将图像的像素值归一化到指定范围", Order = 3)]
public class Normalize : OpenCVNodeDataBase, IPreprocessingGroupableNodeData
{
    protected override FlowableResult<IMatImage> Invoke(Mat fromImage)
    {
        Mat result = new Mat();
        Cv2.Normalize(fromImage, result);
        return this.OK(result);
    }
}

//[Icon(FontIcons.Color)]
//[Display(Name = "逆运算", GroupName = "基础函数", Description = "用于求解矩阵的逆矩阵。常用的分解方法包括LU分解、SVD分解等", Order = 3)]
//public class Invert : OpenCVNodeDataBase, IPreprocessingGroupableNodeData
//{
//    private DecompTypes _DecompTypes;
//    [Display(Name = "方式", GroupName = VisionPropertyGroupNames.RunParameters)]
//    public DecompTypes DecompTypes
//    {
//        get { return _DecompTypes; }
//        set
//        {
//            _DecompTypes = value;
//            RaisePropertyChanged();
//            this.UpdateInvokeCurrent();
//        }
//    }
//    protected override FlowableResult<IMatImage> Invoke(Mat fromImage)
//    {
//        Mat result = new Mat();
//        Cv2.Invert(from.Mat, result,this.DecompTypes);
//        return this.OK(result);
//    }
//}

//[Icon(FontIcons.Color)]
//[Display(Name = "离散傅里叶变换", GroupName = "基础函数", Description = "将输入的图像或信号从时域转换为频域", Order = 3)]
//public class Dft : OpenCVNodeDataBase, IPreprocessingGroupableNodeData
//{
//    private DftFlags _DftFlags;
//    [Display(Name = "方式", GroupName = VisionPropertyGroupNames.RunParameters)]
//    public DftFlags DftFlags
//    {
//        get { return _DftFlags; }
//        set
//        {
//            _DftFlags = value;
//            RaisePropertyChanged();
//            this.UpdateInvokeCurrent();
//        }
//    }
//    protected override FlowableResult<IMatImage> Invoke(Mat fromImage)
//    {
//        Mat result = new Mat();
//        Cv2.Dft(from.Mat, result, this.DftFlags);
//        return this.OK(result);
//    }
//}

//[Icon(FontIcons.Color)]
//[Display(Name = "离散傅里叶逆变换", GroupName = "基础函数", Description = "将输入的图像或信号从时域转换为频域", Order = 3)]
//public class Idft : OpenCVNodeDataBase, IPreprocessingGroupableNodeData
//{
//    private DftFlags _DftFlags;
//    [Display(Name = "方式", GroupName = VisionPropertyGroupNames.RunParameters)]
//    public DftFlags DftFlags
//    {
//        get { return _DftFlags; }
//        set
//        {
//            _DftFlags = value;
//            RaisePropertyChanged();
//            this.UpdateInvokeCurrent();
//        }
//    }
//    protected override FlowableResult<IMatImage> Invoke(Mat fromImage)
//    {
//        Mat result = new Mat();
//        Cv2.Idft(from.Mat, result, this.DftFlags);
//        return this.OK(result);
//    }
//}

