// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Common.Attributes;

namespace H.Services.Common.Serialize.Meta;

public interface IMetaSetting
{
    string ID { get; set; }
    void Load();
    bool Save(out string message);
}

public abstract class MetaSettingBase : IMetaSetting
{
    private readonly IMetaSettingService _json;
    protected MetaSettingBase()
    {
        this._json = this.CreateMetaSettingService();
        this.ID = this.GetType().Name;
    }
    public string ID { get; set; } = "2FC39B7F-8C77-437B-A984-3EBDC28FF5F2";
    protected IMetaSettingService Json => this._json;
    protected abstract IMetaSettingService CreateMetaSettingService();
    public abstract void Load();
    public abstract bool Save(out string message);
}

public abstract class MetaSetting<T> : MetaSettingBase
{
    public T Data { get; set; }
    public override void Load()
    {
        var dts = this.Json.Deserilize<T>(this.ID);
        if (dts == null)
            return;
        this.Data = dts;
    }

    public override bool Save(out string message)
    {
        try
        {
            this.Json.Serilize(this.Data, this.ID);
        }
        catch (System.Exception ex)
        {
            message = ex.Message;
            return false;
        }
        message = null;
        return true;
    }
}
