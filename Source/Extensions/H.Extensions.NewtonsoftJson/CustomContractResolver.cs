using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;
using System.Windows.Input;

namespace H.Extensions.NewtonsoftJson;


public class CustomContractResolver : DefaultContractResolver
{
    protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
    {
        JsonProperty property = base.CreateProperty(member, memberSerialization);
        // 忽略属性
        var textIgnored = member.GetCustomAttribute<System.Text.Json.Serialization.JsonIgnoreAttribute>();
        var textInclude = member.GetCustomAttribute<System.Text.Json.Serialization.JsonIncludeAttribute>();
        var xmlIgnored = member.GetCustomAttribute<System.Xml.Serialization.XmlIgnoreAttribute>();

        Predicate<object> predicate = x =>
        {
            if (textIgnored != null && textInclude == null)
                return false;
            if (xmlIgnored != null)
                return false;
            if (member is PropertyInfo propertyInfo)
                return !typeof(ICommand).IsAssignableFrom(propertyInfo.PropertyType);
            return true;
        };

        property.ShouldDeserialize = predicate;
        property.ShouldSerialize = predicate;
        return property;
    }
}
