// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Extensions.TextJsonable;
using H.Services.Common.Serialize.Meta;

namespace H.Presenters.Common;

public abstract class MetaSettingPresenterBase : DisplayBindableBase, IMetaSetting
{
    public abstract void Load();

    public abstract bool Save(out string message);

    protected virtual IMetaSettingService GetMetaSettingService() => new TextJsonMetaSettingService();

    public virtual async Task<bool?> ShowAsync()
    {
        this.Load();
        var r = await IocMessage.Dialog.Show(this);
        if (r != true)
            return r;
        return this.Save(out _);
    }
}

public class MetaSettingPresenter<T> : MetaSettingPresenterBase
{
    private T _Data;
    public T Data
    {
        get { return _Data; }
        set
        {
            _Data = value;
            RaisePropertyChanged();
        }
    }

    public override void Load()
    {
        this.Data = this.GetMetaSettingService().Deserilize<T>(this.ID);
    }

    public override bool Save(out string message)
    {
        this.GetMetaSettingService().Serilize(this.Data, this.ID);
        message = null;
        return true;
    }
}
