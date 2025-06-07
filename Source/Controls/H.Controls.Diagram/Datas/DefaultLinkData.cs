// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Mvvm.ViewModels.Base;

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
