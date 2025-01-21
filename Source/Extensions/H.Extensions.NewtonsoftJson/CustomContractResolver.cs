using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows.Markup;

namespace H.Extensions.NewtonsoftJson
{

    public class CustomContractResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty property = base.CreateProperty(member, memberSerialization);
            // 忽略属性
            var textIgnored = member.GetCustomAttribute<System.Text.Json.Serialization.JsonIgnoreAttribute>();
            var xmlIgnored = member.GetCustomAttribute<System.Xml.Serialization.XmlIgnoreAttribute>();
            property.ShouldDeserialize = x => textIgnored == null && xmlIgnored == null;
            property.ShouldSerialize = x => textIgnored == null && xmlIgnored == null;
            return property;
        }
    }


}
