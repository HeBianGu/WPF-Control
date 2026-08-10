//// Copyright (c) HeBianGu Authors. All Rights Reserved. 
//// Author: HeBianGu 
//// Github: https://github.com/HeBianGu/WPF-Control 
//// Document: https://hebiangu.github.io/WPF-Control-Docs  
//// QQ:908293466 Group:971261058 
//// bilibili: https://space.bilibili.com/370266611 
//// Licensed under the MIT License (the "License")

//using H.Components.VisionDiagram.Base;
//using H.Controls.Form.PropertyItem.Attribute;
//using H.Controls.Form.PropertyItem.ComboBoxPropertyItems;

//namespace H.Components.VisionDiagram.NodeDatas;
//public abstract class NinePointSelectableVisionNodeData<T> : FromImageVisionNodeDataBase<T>, IVisionNodeData<T> where T : class, IVisionImage
//{
//    private INinePointCalibrationNodeData _ninePointCalibrationNodeData;

//    [GetMethodNameSource(nameof(GetNinePointCalibrationNodeDatas))]
//    [PropertyItem(typeof(ComboBoxPropertyItem))]
//    [Tab(VisionTabNames.BaseParameters)]
//    [Display(Name = "九点标定源", GroupName = VisionTabNames.BaseParameters, Description = "选择九点标定节点源")]
//    public INinePointCalibrationNodeData NinePointCalibrationNodeData
//    {
//        get { return _ninePointCalibrationNodeData; }
//        set
//        {
//            _ninePointCalibrationNodeData = value;
//            RaisePropertyChanged();
//        }
//    }

//    public IEnumerable<INinePointCalibrationNodeData> GetNinePointCalibrationNodeDatas()
//    {
//        return this.AllFromNodeDatas.OfType<INinePointCalibrationNodeData>();
//    }

//    protected INinePointCalibrationNodeData GetNinePointCalibrationNodeData()
//    {
//        if (this.NinePointCalibrationNodeData != null)
//            return this.NinePointCalibrationNodeData;

//        return this.GetNinePointCalibrationNodeDatas()?.FirstOrDefault();
//    }

//    protected Point? TryGetWorldPoint(Point pixelPoint)
//    {
//        if (this.GetNinePointCalibrationNodeData() is INinePointCalibrationNodeData calibration && calibration.IsValid)
//            return calibration.PixelToWorld(pixelPoint);

//        return null;
//    }
//}