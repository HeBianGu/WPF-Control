// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase






namespace HeBianGu.Diagram.OpenCV
{



    [Display(Name = "反转黑白", GroupName = "基础函数", Description = "二指图片的效果反转既黑色变白色，白色变黑色", Order = 0)]
    public class BitwiseNot : OpenCVNodeData
    {
        public override IFlowableResult Invoke(Part previors, Node current)
        {
            if (this._preMat == null)
                return this.Error("数据源为空");
            var src = this.GetFromData(current).Mat;
            Cv2.BitwiseNot(src, src);
            this.Mat = src;
            this.RefreshMatToView();
            return base.Invoke(previors, current);
        }
    }
}
