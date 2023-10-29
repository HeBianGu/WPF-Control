// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase



using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace HeBianGu.Controls.Form
{
    public interface IPropertyViewPresenterOption 
    {

    }

    //[Display(Name = "编辑属性",Description = "显示编辑属性消息功能")]
    //public class PropertyGridViewPresenter : ServiceMvpSettingBase<PropertyGridViewPresenter, IPropertyGridViewPresenter>, IPropertyGridViewPresenter, IPropertyViewPresenterOption
    //{
    //    private object _data;
    //    [XmlIgnore]
    //    [Browsable(false)]
    //    public object Data
    //    {
    //        get { return _data; }
    //        set
    //        {
    //            _data = value;
    //            RaisePropertyChanged();
    //        }
    //    }
    //}

    //[Displayer(Name = "查看详情", GroupName = SystemSetting.GroupSystem, Icon = Icons.Message, Description = "显示查看详情消息功能")]
    //public class PropertyViewViewPresenter : ServiceMvpSettingBase<PropertyViewViewPresenter, IPropertyViewViewPresenter>, IPropertyViewViewPresenter, IPropertyViewPresenterOption
    //{
    //    private object _data;
    //    [XmlIgnore]
    //    [Browsable(false)]
    //    public object Data
    //    {
    //        get { return _data; }
    //        set
    //        {
    //            _data = value;
    //            RaisePropertyChanged();
    //        }
    //    }
    //}
}
