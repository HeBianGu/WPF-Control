// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Modules.Help.Base;
using System.Diagnostics;

namespace H.Modules.Help.Support;

public class SupportService : ISupportService
{
    public void Show()
    {
        SupportOptions.Instance.Uri.ShowProcess();
    }
}
