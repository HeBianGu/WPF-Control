// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")
using H.Extensions.Common;
using H.Extensions.NewtonsoftJson;
using H.Services.AppPath;
using H.Services.Serializable;
using System.IO;

namespace H.Components.Modbus;

public class SerializableModbusDataService : ModbusDataService, ISerializableModbusDataService
{
    private NewtonsoftJsonSerializerService _serializerService = new NewtonsoftJsonSerializerService();

    public bool Load(out string message)
    {
        try
        {
            var datas = this._serializerService.Load<ModbusTcpDatas>(this.GetFilePath());
            this.Collection = datas ?? new ModbusTcpDatas();
            message = null;
            return true;
        }
        catch (Exception ex)
        {
            message = ex.Message;
            IocLog.Instance?.Error(ex);
            return true;
        }
    }

    protected string GetFilePath()
    {
        string path = Path.Combine(AppPaths.Instance.Data, "ModbusDatas.json");
        if (!Directory.Exists(Path.GetDirectoryName(path)))
            Directory.CreateDirectory(Path.GetDirectoryName(path));
        return path;
    }

    public bool Save(out string message)
    {
        try
        {
            this.Stop().Wait();
            this._serializerService.Save(this.GetFilePath(), this.Collection);
            message = null;
            return true;
        }
        catch (Exception ex)
        {
            message = ex.Message;
            IocLog.Instance?.Error(ex);
            return true;
        }

    }
}

