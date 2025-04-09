// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Modules.Help.Base;

namespace H.Modules.Help.ReleaseVersions;

public class ReleaseVersionsService : ShowHelpServiceBase, IReleaseVersionsService
{
    public override void Show()
    {
        ReleaseVersionsOptions.Instance.Uri.ShowProcess();
    }
}
