// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Text.Json;

namespace H.Services.Serializable;

public class JsonCloneService : ICloneService
{
    public object Clone(object o)
    {
        string txt = JsonSerializer.Serialize(o);
        return JsonSerializer.Deserialize(txt, o.GetType());
    }
}