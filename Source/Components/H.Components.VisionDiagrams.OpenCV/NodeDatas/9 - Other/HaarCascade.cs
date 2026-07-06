// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using H.Extensions.TypeConverter;

namespace H.Components.VisionDiagrams.OpenCV.NodeDatas.Other;

//[Display(Name = "人脸检测(HAAR)", GroupName = "人脸检测", Description = "HAAR 级联分类器是 OpenCV 中基于机器学习的目标检测方法", Order = 0)]
//public class HaarCascade : CascadeClassifierOpenCVNodeDataBase
//{
//    private HaarType _haarType = HaarType.FrontalFace;
//    [DefaultValue(HaarType.FrontalFace)]
//    [Tab(VisionTabNames.RunParameters)]
//    [Display(Name = "检测类型", GroupName = VisionTabNames.RunParameters)]
//    public HaarType HaarType
//    {
//        get { return _haarType; }
//        set
//        {
//            _haarType = value;
//            RaisePropertyChanged();
//            this.Invoke();
//        }
//    }

//    protected override FlowableResult<IMatImage> Invoke(Mat fromImage)
//    {
//        string dataPath = this.GetDataPathByName();
//        // Load the cascades
//        CascadeClassifier haarCascade = new CascadeClassifier(dataPath);
//        // Detect faces
//        Tuple<Mat, OpenCvSharp.Rect[]> result = DetectFace(haarCascade, fromImage);
//        this.MatchingCountResult = result.Item2.Count();
//        return this.OK(result.Item1, result.Item2.ToResultPresenter(), this.MatchingCountResult.ToDetectSuccessMessage());
//    }

//    private string GetDataPathByName()
//    {
//        if (this.HaarType == HaarType.Eye)
//            return CascadeData.Eye.ToDataPath();
//        if (this.HaarType == HaarType.FrontalFace)
//            return CascadeData.Frontalface.ToDataPath();
//        if (this.HaarType == HaarType.Profileface)
//            return CascadeData.Profileface.ToDataPath();
//        if (this.HaarType == HaarType.FullBody)
//            return CascadeData.Fullbody.ToDataPath();
//        if (this.HaarType == HaarType.LeftEye)
//            return CascadeData.Lefteye.ToDataPath();
//        if (this.HaarType == HaarType.RightEye)
//            return CascadeData.Righteye.ToDataPath();
//        if (this.HaarType == HaarType.LowerBody)
//            return CascadeData.Lowerbody.ToDataPath();
//        if (this.HaarType == HaarType.UpperBody)
//            return CascadeData.Upperbody.ToDataPath();
//        if (this.HaarType == HaarType.Smile)
//            return CascadeData.Smile.ToDataPath();
//        if (this.HaarType == HaarType.Eyeglass)
//            return CascadeData.Eyeglasses.ToDataPath();
//        if (this.HaarType == HaarType.FrontalcatFace)
//            return CascadeData.Frontalcatface.ToDataPath();
//        return this.HaarType == HaarType.LicencePlate
//            ? CascadeData.Licence_plate.ToDataPath()
//            : this.HaarType == HaarType.RussianPlate
//            ? CascadeData.Russian_plate_number.ToDataPath()
//            : throw new ArgumentException("没有识别参数");
//    }
//}

//[TypeConverter(typeof(DisplayEnumConverter))]
//public enum HaarType
//{
//    [Display(Name = "左眼和右眼")]
//    Eye = 0,
//    [Display(Name = "正脸")]
//    FrontalFace,
//    [Display(Name = "侧脸")]
//    Profileface,
//    [Display(Name = "全身")]
//    FullBody,
//    [Display(Name = "左眼")]
//    LeftEye,
//    [Display(Name = "右眼")]
//    RightEye,
//    [Display(Name = "下半身")]
//    LowerBody,
//    [Display(Name = "上半身")]
//    UpperBody,
//    [Display(Name = "嘴部")]
//    Smile,
//    [Display(Name = "墨镜")]
//    Eyeglass,
//    [Display(Name = "猫脸")]
//    FrontalcatFace,
//    [Display(Name = "证件")]
//    LicencePlate,
//    [Display(Name = "字母车牌")]
//    RussianPlate

//}
