using H.Mvvm.ViewModels.Base;
using Newtonsoft.Json;

namespace H.Extensions.NewtonsoftJson.Jsonable;

public abstract class NewtonsoftJsonBindableBase : DisplayBindableBase, IJsonable
{
    public virtual void ReadJson(JsonReader reader, JsonSerializer serializer, object existingValue)
    {
        serializer.Populate(reader, this);
    }

    public virtual void WriteJson(JsonWriter writer, JsonSerializer serializer)
    {
        serializer.Serialize(writer, this);
    }

    //protected virtual IEnumerable<PropertyInfo> GetJsonPropertyInfos(JsonSerializer serializer)
    //{
    //    var result = this.GetType().GetProperties().Where(x => x.GetCustomAttribute<JsonIgnoreAttribute>() == null);
    //    return result.Where(x => !typeof(ICommand).IsAssignableFrom(x.PropertyType));
    //}
}
