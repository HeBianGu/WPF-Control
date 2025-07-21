// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Text.Json;
using H.Extensions.Encryption.String;
using System.Text.Json.Serialization;

namespace H.Modules.License.Provider
{
    public class EncriyptoDateTimeJsonConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var encryptedTicks = reader.GetString();
            var decryptedTicks = encryptedTicks.DecryptAES();
            return new DateTime(long.Parse(decryptedTicks));
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            var str = value.Ticks.ToString().EncryptAES();
            writer.WriteStringValue(str);
        }
    }
}
