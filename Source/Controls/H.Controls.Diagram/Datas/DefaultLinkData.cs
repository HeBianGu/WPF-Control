// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control



using H.Mvvm.ViewModels.Base;
using System;

namespace H.Controls.Diagram.Datas;

/// <summary>
/// 默认端口要显示的效果
/// </summary>
public class DefaultLinkData : BindableBase, ILinkData
{
    private string _message;
    /// <summary> 说明  </summary>
    public string Message
    {
        get { return _message; }
        set
        {
            _message = value;
            RaisePropertyChanged("Message");
        }
    }

    public string FromNodeID { get; set; }

    public string ToNodeID { get; set; }

    public string FromPortID { get; set; }

    public string ToPortID { get; set; }

    public void ApplayStyleTo(ILinkData node)
    {

    }
}
