# NewtonsoftJson 序列化二次开发文档

**适用项目：** `H.Extensions.NewtonsoftJson`、项目配置、Diagram、Project、缓存和业务数据持久化  
**核心类型：** `NewtonsoftJsonSerializerService`、`NewtonsoftJsonOptions`、`JsonSerializerSettings`、`CustomContractResolver`、`TypeConverterJsonConverter`、`IJsonable`  
**相关能力：** JSON 序列化、反序列化、多态类型、对象引用、循环引用、WPF 类型、枚举、日期、自定义转换器、兼容错误处理

本文介绍框架中 `H.Extensions.NewtonsoftJson` 的注册、序列化服务、`JsonSerializerSettings` 默认配置，以及这些配置用于处理的具体问题和限制。

---

## 1. 模块定位

`H.Extensions.NewtonsoftJson` 是框架基于 `Newtonsoft.Json` 的序列化扩展。它主要解决 WPF 应用持久化时常见的问题：

- 节点、连线、项目数据等多态对象的真实类型恢复。
- 共享对象和循环引用的保存与恢复。
- WPF 常用类型通过 `TypeConverter` 转换为文本。
- 枚举按名称而不是数字保存。
- 日期时间格式统一。
- `ICommand`、`JsonIgnore`、`XmlIgnore` 属性不参与保存。
- 旧配置存在局部不兼容时记录日志并尽量继续加载。
- 特殊类型通过 `IJsonable` 完全接管读写过程。

模块通常用于：

```text
业务配置 / 项目文件 / Diagram 流程定义 / 模板 / 缓存 / 设置项
```

不建议将默认引用设置直接用于不可信的网络 JSON。

---

## 2. 服务注册与使用

### 2.1 注册 `IJsonSerializerService`

```csharp
protected override void ConfigureServices(IServiceCollection services)
{
    services.AddNewtonsoftJsonSerializerService();
}
```

注册方法：

```csharp
public static IServiceCollection AddNewtonsoftJsonSerializerService(
    this IServiceCollection services,
    Action<INewtonsoftJsonOptions> setupAction = null)
{
    services.AddOptions();
    services.TryAdd(ServiceDescriptor.Singleton<IJsonSerializerService, NewtonsoftJsonSerializerService>());
    if (setupAction != null)
        services.Configure(new Action<NewtonsoftJsonOptions>(setupAction));
    return services;
}
```

默认注册为单例：

```csharp
IJsonSerializerService → NewtonsoftJsonSerializerService
```

### 2.2 注入使用

```csharp
public class WorkflowStorageService
{
    private readonly IJsonSerializerService _serializer;

    public WorkflowStorageService(IJsonSerializerService serializer)
    {
        _serializer = serializer;
    }

    public string Save(WorkflowDefinition definition)
    {
        return _serializer.SerializeObject(definition);
    }

    public WorkflowDefinition Load(string json)
    {
        return (WorkflowDefinition)_serializer.DeserializeObject(json, typeof(WorkflowDefinition));
    }
}
```

### 2.3 直接使用具体服务

```csharp
var serializer = new NewtonsoftJsonSerializerService();
string json = serializer.SerializeObject(value);
var result = (MyType)serializer.DeserializeObject(json, typeof(MyType));
```

推荐优先使用 `IJsonSerializerService` 注入，方便单元测试替换实现，也方便后续切换序列化器。

---

## 3. 两种内置序列化设置

模块提供两种设置创建方法：

| 方法 | 主要用途 | 多态 / 引用配置 |
|---|---|---|
| `CreateDefaultSerializerSettings()` | 普通配置、较简单 DTO、受限数据交换。 | 不保留引用，不写入 `$type`。 |
| `CreateReferenceSerializerSettings()` | 框架默认设置，项目、图形、流程等复杂对象图。 | 保留对象引用，处理循环引用，写入类型信息。 |

### 3.1 默认服务使用引用设置

`NewtonsoftJsonOptions.LoadDefault()`：

```csharp
public override void LoadDefault()
{
    base.LoadDefault();
    this.JsonSerializerSettings = this.CreateReferenceSerializerSettings();
}
```

`NewtonsoftJsonSerializerService` 使用：

```csharp
protected virtual JsonSerializerSettings GetSerializerSettings()
{
    return NewtonsoftJsonOptions.Instance.JsonSerializerSettings;
}
```

因此默认 `NewtonsoftJsonSerializerService` 采用 `CreateReferenceSerializerSettings()`。

### 3.2 使用普通设置的服务

```csharp
public class DefaultNewtonsoftJsonSerializerService : NewtonsoftJsonSerializerService
{
    protected override JsonSerializerSettings GetSerializerSettings()
    {
        return NewtonsoftJsonOptions.Instance.CreateDefaultSerializerSettings();
    }
}
```

当数据没有循环引用、无需保存派生类型，或者 JSON 需要和其他系统交换时，可使用 `DefaultNewtonsoftJsonSerializerService`。

---

## 4. `CreateDefaultSerializerSettings()` 默认配置

框架普通设置：

```csharp
public JsonSerializerSettings CreateDefaultSerializerSettings()
{
    var setting = new JsonSerializerSettings
    {
        NullValueHandling = NullValueHandling.Ignore,
        DefaultValueHandling = DefaultValueHandling.Ignore,
        Formatting = Formatting.Indented,
        ContractResolver = new CustomContractResolver(),
        Converters =
        {
            new TypeConverterJsonConverter(),
            new EnumConverter(),
            new DateTimeConverter(),
            new JsonableJsonConverter()
        },
        Error = (sender, args) =>
        {
            IocLog.Error($"Json兼容错误: {args.ErrorContext.Path}");
            IocLog.Error(args.ErrorContext.Error.Message);
            args.ErrorContext.Handled = true;
        }
    };
    return setting;
}
```

以下章节分别说明每项配置。

---

## 5. `NullValueHandling.Ignore`

配置：

```csharp
NullValueHandling = NullValueHandling.Ignore
```

作用：序列化时忽略值为 `null` 的属性。

对象：

```csharp
public class UserOptions
{
    public string Name { get; set; }
    public string Description { get; set; }
}

var value = new UserOptions
{
    Name = "管理员",
    Description = null
};
```

输出：

```json
{
  "Name": "管理员"
}
```

解决的问题：

- 减少配置文件冗余。
- 避免大量无意义的 `null` 属性。
- 让手工维护的 JSON 更易阅读。

注意：`null` 被忽略意味着反序列化时无法区分“文件中显式保存了 `null`”和“属性未写入”。如果业务需要表达“清空值”，不要依赖该差异，应该使用明确状态字段或自定义转换器。

---

## 6. `DefaultValueHandling.Ignore`

配置：

```csharp
DefaultValueHandling = DefaultValueHandling.Ignore
```

作用：序列化时忽略默认值。

常见默认值：

| 类型 | 默认值 |
|---|---|
| `bool` | `false` |
| 数值类型 | `0` |
| `enum` | 第一个枚举值或 `0` |
| 引用类型 | `null` |

对象：

```csharp
public class DiagramOptions
{
    public bool ShowGrid { get; set; }
    public int GridSize { get; set; }
    public string Name { get; set; }
}
```

如果：

```csharp
var options = new DiagramOptions
{
    ShowGrid = false,
    GridSize = 0,
    Name = "流程图"
};
```

输出通常仅保留：

```json
{
  "Name": "流程图"
}
```

解决的问题：

- 项目配置文件体积更小。
- 默认值变化后，旧 JSON 中没有显式配置的字段可以自动使用新代码默认值。
- 减少无意义的默认字段噪声。

### 6.1 与 `[DefaultValue]` 配合

框架注释说明：

```csharp
// 默认值不保存；加了 DefaultValue 后按 DefaultValue 关联。
```

例如：

```csharp
public class ViewOptions
{
    [DefaultValue(24)]
    public int IconSize { get; set; } = 24;
}
```

配置为默认值 `24` 时可忽略保存；设置为其他值时才保存。

注意：如果 `false`、`0`、空枚举值本身具有业务意义，忽略默认值可能不适合。此时可在特定类型或特定属性中使用单独的 `JsonSerializerSettings`，或通过 JSON 特性和自定义 ContractResolver 调整策略。

---

## 7. `Formatting.Indented`

配置：

```csharp
Formatting = Formatting.Indented
```

作用：输出缩进格式 JSON。

输出示例：

```json
{
  "Name": "主流程",
  "Nodes": [
    {
      "Title": "开始"
    }
  ]
}
```

解决的问题：

- 项目文件、配置文件可读性更好。
- 便于 Git diff 查看字段变化。
- 便于用户排查和手工修正配置。

代价：文件比压缩 JSON 略大。对于应用配置和项目文件，这是合理取舍；高频网络传输数据应使用 `Formatting.None`。

---

## 8. `CustomContractResolver`

框架使用：

```csharp
ContractResolver = new CustomContractResolver()
```

核心逻辑：

```csharp
protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
{
    JsonProperty property = base.CreateProperty(member, memberSerialization);
    var textIgnored = member.GetCustomAttribute<JsonIgnoreAttribute>();
    var textInclude = member.GetCustomAttribute<JsonIncludeAttribute>();
    var xmlIgnored = member.GetCustomAttribute<XmlIgnoreAttribute>();

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
```

### 8.1 支持 `System.Text.Json` 的忽略标记

```csharp
[System.Text.Json.Serialization.JsonIgnore]
public string RuntimeState { get; set; }
```

即使当前使用 Newtonsoft.Json，这个属性也会被忽略。

解决的问题：同一个模型可同时被 `System.Text.Json` 和 `Newtonsoft.Json` 使用，无需重复增加两套忽略标记。

### 8.2 `JsonInclude` 优先于 `JsonIgnore`

规则：

```csharp
if (textIgnored != null && textInclude == null)
    return false;
```

如果同时标记 `[JsonIgnore]` 和 `[JsonInclude]`，`JsonInclude` 使该成员仍可参与当前 Resolver 的读写判断。

使用时应保持标记语义清晰；除非存在跨序列化器兼容需求，否则不建议同时使用二者。

### 8.3 支持 `XmlIgnore`

```csharp
[XmlIgnore]
public object RuntimeOnlyValue { get; set; }
```

该属性也会被 NewtonsoftJson 设置忽略。

解决的问题：已有 XML 序列化模型迁移到 JSON 时，运行时属性无需重新逐个增加 JSON 忽略标记。

### 8.4 自动忽略 `ICommand`

```csharp
public ICommand SaveCommand { get; }
```

`ICommand` 属性不会参与序列化和反序列化。

解决的问题：

- 避免保存命令对象、委托、闭包和 UI 服务引用。
- 避免序列化 WPF 命令导致循环引用或异常。
- 让 Presenter / ViewModel 可以同时包含业务数据和命令。

建议：所有命令、服务、运行时缓存、UI 引用都应使用忽略特性或定义为不参与持久化的属性。

---

## 9. `TypeConverterJsonConverter`

框架注册：

```csharp
new TypeConverterJsonConverter()
```

该转换器会查找：

```csharp
TypeDescriptor.GetConverter(objectType)
```

如果类型的 `TypeConverter` 同时支持：

```csharp
converter.CanConvertFrom(typeof(string))
converter.CanConvertTo(typeof(string))
```

则按字符串读写。

### 9.1 可处理的 WPF 类型

许多 WPF 类型带有 `TypeConverter`，例如：

- `Point`
- `Size`
- `Rect`
- `Thickness`
- `CornerRadius`
- `Color`
- `Brush` 的部分实现
- `FontFamily`
- `GridLength`
- 其他支持字符串转换的类型

例如：

```csharp
public class NodeData
{
    public Point Location { get; set; }
    public Thickness Margin { get; set; }
}
```

可能保存为类似：

```json
{
  "Location": "120,80",
  "Margin": "5,10,5,10"
}
```

解决的问题：WPF 结构类型通常不适合默认 JSON 对象结构，使用字符串表达可使配置更紧凑、可读，并能通过 WPF 自己的转换规则恢复。

### 9.2 对 `Freezable` 的处理

反序列化后：

```csharp
if (r is Freezable freezable && freezable.CanFreeze)
    freezable.Freeze();
```

作用：对可冻结的 WPF 对象冻结，提高线程安全性并降低运行时变更风险。

### 9.3 Dispatcher 对象的处理

序列化 `DispatcherObject` 时：

```csharp
dispatcherObject.Dispatcher.Invoke(() =>
{
    writer.WriteValue(converter.ConvertToInvariantString(value));
});
```

作用：在对象所属 Dispatcher 线程上读取 WPF 对象，避免跨线程访问 WPF 依赖对象时出现线程访问异常。

### 9.4 不参与该转换器的类型

以下类型被排除：

```csharp
objectType.IsPrimitive
objectType.IsEnum
objectType == typeof(string)
objectType == typeof(DateTime)
```

原因：

- 原始类型由 Newtonsoft.Json 原生处理。
- 枚举由 `EnumConverter` 处理。
- `DateTime` 由 `DateTimeConverter` 处理。
- `string` 无需转换。

### 9.5 排除特定 TypeConverter

若自定义 TypeConverter 不应参与 JSON 转换，可在 Converter 类型上增加：

```csharp
[IgnoreTypeConverterJsonConverter]
public class MyTypeConverter : TypeConverter
{
}
```

框架会跳过带有该标记的 Converter。

---

## 10. `EnumConverter`

框架枚举转换器：

```csharp
public class EnumConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return objectType.IsEnum;
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        string str = (string)reader.Value;
        return Enum.Parse(objectType, str);
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        writer.WriteValue(value.ToString());
    }
}
```

示例：

```csharp
public enum PortType
{
    Input = 1,
    OutPut = 2,
    Both = Input | OutPut
}
```

输出：

```json
{
  "PortType": "OutPut"
}
```

而不是：

```json
{
  "PortType": 2
}
```

解决的问题：

- JSON 可读性更好。
- 枚举成员的数值调整时，名称形式更稳定。
- Git diff 和手工调试更直观。

注意：重命名枚举成员会导致旧 JSON 无法按原名称解析。需要兼容历史文件时，应保留旧成员、实现自定义映射，或在反序列化前做 JSON 迁移。

---

## 11. `DateTimeConverter`

默认格式：

```csharp
private readonly string _format = "yyyy-MM-dd HH:mm:ss";
```

写入：

```csharp
writer.WriteValue(dateTime.ToString(_format));
```

读取：

```csharp
return DateTime.ParseExact(str, _format, null);
```

输出示例：

```json
{
  "UpdateTime": "2025-03-08 14:30:00"
}
```

解决的问题：

- 避免不同计算机区域设置造成日期格式不同。
- 配置文件和项目文件具有统一、可读的时间格式。
- 避免 Newtonsoft.Json 默认 ISO 格式在不同系统中的展示差异。

限制：

- 该转换器只处理 `DateTime`，不处理 `DateTime?`、`DateTimeOffset`。
- 不保存毫秒、时区和 `DateTimeKind`。
- JSON 中日期必须严格符合 `yyyy-MM-dd HH:mm:ss`；其他格式会进入错误处理逻辑。

如果业务需要审计精度、跨时区同步或 UTC 时间，应自定义支持 `DateTimeOffset` 的转换器，并保留 ISO 8601 格式。

---

## 12. `JsonableJsonConverter` 与 `IJsonable`

框架注册：

```csharp
new JsonableJsonConverter()
```

接口：

```csharp
public interface IJsonable
{
    void ReadJson(JsonReader reader, JsonSerializer serializer, object existingValue);
    void WriteJson(JsonWriter writer, JsonSerializer serializer);
}
```

转换器实现：

```csharp
public class JsonableJsonConverter : JsonConverter<IJsonable>
{
    public override IJsonable ReadJson(JsonReader reader, Type objectType, IJsonable existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null)
            return null;
        var jsonable = Activator.CreateInstance(objectType) as IJsonable;
        jsonable.ReadJson(reader, serializer, existingValue);
        return jsonable;
    }

    public override void WriteJson(JsonWriter writer, IJsonable value, JsonSerializer serializer)
    {
        value.WriteJson(writer, serializer);
    }
}
```

适用场景：

- 某个类型需要完全自定义 JSON 格式。
- 需要兼容多个历史版本 JSON。
- 需要把多个字段压缩为一个字段。
- 需要忽略临时状态并恢复默认状态。
- 需要根据 JSON 内容决定如何构建对象。

示例：

```csharp
public class VersionedOptions : IJsonable
{
    public int Version { get; set; } = 1;
    public string Name { get; set; }

    public void WriteJson(JsonWriter writer, JsonSerializer serializer)
    {
        writer.WriteStartObject();
        writer.WritePropertyName("version");
        writer.WriteValue(this.Version);
        writer.WritePropertyName("name");
        writer.WriteValue(this.Name);
        writer.WriteEndObject();
    }

    public void ReadJson(JsonReader reader, JsonSerializer serializer, object existingValue)
    {
        var obj = Newtonsoft.Json.Linq.JObject.Load(reader);
        this.Version = obj.Value<int?>("version") ?? 1;
        this.Name = obj.Value<string>("name");
    }
}
```

注意：实现 `IJsonable` 时必须完整处理 JSON 读写，避免在 `WriteJson` 内对当前对象再次调用 `serializer.Serialize(writer, this)`，否则可能造成递归调用。

---

## 13. 错误处理与配置兼容性

默认设置：

```csharp
Error = (sender, args) =>
{
    IocLog.Error($"Json兼容错误: {args.ErrorContext.Path}");
    IocLog.Error(args.ErrorContext.Error.Message);
    args.ErrorContext.Handled = true;
}
```

行为：

1. 记录发生错误的 JSON 路径。
2. 记录异常消息。
3. 将错误标记为已处理。
4. 尝试继续反序列化其他字段。

可处理的问题：

- 旧配置中某个属性格式不正确。
- 枚举值已删除或拼写不一致。
- 日期格式不符合预期。
- 某些 WPF 类型转换失败。
- 项目升级后某个局部字段类型发生变化。

优点：单个字段损坏时，不会因为一个局部错误导致整个配置文件完全无法加载。

风险：如果被处理的字段是关键字段，应用可能在不完整状态下继续运行。

建议：

- 对关键配置在加载后进行业务校验。
- 记录完整日志并提示用户修复或恢复默认配置。
- 对版本化配置执行明确的数据迁移，不要长期依赖“吞掉异常”。

---

## 14. 引用设置：循环引用、共享引用和多态

`CreateReferenceSerializerSettings()`：

```csharp
public JsonSerializerSettings CreateReferenceSerializerSettings()
{
    var setting = this.CreateDefaultSerializerSettings();
    setting.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
    setting.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
    setting.TypeNameHandling = TypeNameHandling.All;
    return setting;
}
```

这三个配置组合用于复杂对象图。

---

## 15. `PreserveReferencesHandling.Objects`

配置：

```csharp
PreserveReferencesHandling = PreserveReferencesHandling.Objects
```

作用：保留对象身份，序列化时使用 `$id` 和 `$ref` 表达共享对象。

对象图：

```csharp
var shared = new NodeOptions { Name = "共享配置" };
var root = new WorkflowOptions
{
    First = shared,
    Second = shared
};
```

可能输出：

```json
{
  "$id": "1",
  "First": {
    "$id": "2",
    "Name": "共享配置"
  },
  "Second": {
    "$ref": "2"
  }
}
```

解决的问题：

- 同一个对象被多个属性或节点引用时，反序列化后仍保持为同一对象实例。
- 避免重复保存共享对象。
- 与循环引用配置配合使用。

适合：

- Diagram 节点之间共享配置对象。
- 父子对象互相引用。
- 复杂项目模型、树结构、图结构。

不适合：

- 需要给外部服务读取的简单 JSON。
- 用户期望手工维护的简洁配置。

---

## 16. `ReferenceLoopHandling.Serialize`

配置：

```csharp
ReferenceLoopHandling = ReferenceLoopHandling.Serialize
```

作用：允许序列化循环引用。通常与 `PreserveReferencesHandling.Objects` 一起使用。

循环对象：

```csharp
public class Parent
{
    public Child Child { get; set; }
}

public class Child
{
    public Parent Parent { get; set; }
}
```

如果：

```csharp
var parent = new Parent();
var child = new Child { Parent = parent };
parent.Child = child;
```

普通 JSON 序列化会因为对象图无限循环而失败；引用设置会用 `$id` / `$ref` 表达循环。

解决的问题：

- 父子双向导航属性。
- 图形节点、端口、连线之间的双向关联。
- 缓存和运行时模型中存在回指。

注意：更推荐的长期模型设计仍是保存 ID 关系，而不是保存大量运行时对象双向引用。例如 Diagram 的 `ILinkData` 使用 `FromNodeID`、`ToNodeID`、`FromPortID`、`ToPortID` 保存连接关系，这是更适合持久化的方式。

---

## 17. `TypeNameHandling.All`

配置：

```csharp
TypeNameHandling = TypeNameHandling.All
```

作用：为对象写入 `$type`，反序列化时恢复运行时具体类型。

对象：

```csharp
public abstract class NodeDataBase
{
    public string ID { get; set; }
}

public class StartNodeData : NodeDataBase
{
}

public class DecisionNodeData : NodeDataBase
{
}

public class WorkflowFile
{
    public List<NodeDataBase> Nodes { get; set; }
}
```

输出会包含类型信息：

```json
{
  "$type": "MyApp.WorkflowFile, MyApp",
  "Nodes": [
    {
      "$type": "MyApp.StartNodeData, MyApp",
      "ID": "start"
    },
    {
      "$type": "MyApp.DecisionNodeData, MyApp",
      "ID": "decision"
    }
  ]
}
```

解决的问题：

- 集合声明为基类或接口时，恢复每个元素的真实派生类型。
- 反序列化 `INodeData`、`ILinkData` 等多态对象。
- 项目配置中包含不同 Presenter、节点、条件和动作类型。

### 17.1 重要安全边界

`TypeNameHandling.All` **不应直接用于不可信 JSON**，例如：

- HTTP 请求体。
- 外部上传文件。
- 来自未知用户的 JSON。
- 未签名、未校验的共享项目文件。

原因：`$type` 会参与类型解析。错误配置或不安全 Binder 可能带来反序列化安全风险。

默认框架配置适合：

- 本机应用生成的配置文件。
- 受信任项目文件。
- 应用内部缓存。
- 已签名、验证来源的持久化数据。

对于不可信输入，应：

1. 使用 `CreateDefaultSerializerSettings()`。
2. 设置 `TypeNameHandling = TypeNameHandling.None`。
3. 使用明确 DTO，不反序列化到任意接口或抽象基类。
4. 必要时实现类型白名单 `ISerializationBinder`。
5. 在业务层验证字段、版本和权限。

### 17.2 稳定性限制

`$type` 通常包含程序集和类型全名。重命名命名空间、类名或程序集后，旧 JSON 可能无法加载。

框架提供了 `CompatibleSerializationBinder` 示例：

```csharp
public sealed class CompatibleSerializationBinder : ISerializationBinder
{
    public Type BindToType(string assemblyName, string typeName)
    {
        // if (typeName == "Old.Namespace.OldClass")
        //     return typeof(New.Namespace.NewClass);
        return Type.GetType($"{typeName}, {assemblyName}");
    }
}
```

当前默认设置未启用该 Binder。若项目需要长期兼容旧文件，应实现类型白名单和旧类型映射，并设置：

```csharp
settings.SerializationBinder = new MyCompatibleSerializationBinder();
```

---

## 18. 配置选择建议

| 场景 | 推荐设置 |
|---|---|
| 本地项目文件，含抽象基类、接口集合、复杂节点类型 | `CreateReferenceSerializerSettings()`，并确保文件可信。 |
| Diagram 节点、连线和工作流定义 | 引用设置或明确 DTO + 类型判别字段。 |
| 简单设置项、颜色、尺寸、位置 | `CreateDefaultSerializerSettings()`。 |
| 对外 API、网络消息 | 默认/专用安全设置，禁止 `TypeNameHandling.All`。 |
| 长期版本化配置 | 明确版本字段、DTO 迁移、兼容 Binder 或类型映射。 |
| UI Presenter 临时克隆 | `CloneByNewtonsoftJson()`，但注意引用设置和非持久化属性。 |

---

## 19. 深拷贝：`CloneByNewtonsoftJson`

扩展方法：

```csharp
public static T CloneByNewtonsoftJson<T>(this T t)
{
    var service = new NewtonsoftJsonSerializerService();
    return service.Clone(t);
}
```

使用：

```csharp
var copy = source.CloneByNewtonsoftJson();
```

适合：

- 编辑前备份配置。
- 创建可撤销快照。
- 复制 Diagram 节点定义。
- 表单编辑使用副本，提交后再应用。

注意：

- 被 `[JsonIgnore]`、`[XmlIgnore]` 或 `ICommand` 排除的属性不会被复制。
- `TypeNameHandling.All` 会保留多态类型，但只应处理受信任内存对象。
- 不是所有对象都适合 JSON 深拷贝，例如持有文件句柄、Dispatcher、数据库连接、事件订阅或 UI 控件的对象。

---

## 20. 自定义设置与服务

### 20.1 配置 `NewtonsoftJsonOptions`

`INewtonsoftJsonOptions` 暴露：

```csharp
public interface INewtonsoftJsonOptions
{
    JsonSerializerSettings JsonSerializerSettings { get; set; }
}
```

可在注册时替换设置：

```csharp
services.AddNewtonsoftJsonSerializerService(options =>
{
    options.JsonSerializerSettings = new JsonSerializerSettings
    {
        Formatting = Formatting.Indented,
        NullValueHandling = NullValueHandling.Ignore,
        TypeNameHandling = TypeNameHandling.None
    };
});
```

注意：替换整个设置对象时，应按需要重新添加框架的 `ContractResolver` 和 Converters，否则 `ICommand` 忽略、WPF `TypeConverter`、枚举与日期格式等默认行为将不再生效。

更稳妥的方式是从框架设置复制后调整：

```csharp
services.AddNewtonsoftJsonSerializerService(options =>
{
    var settings = NewtonsoftJsonOptions.Instance.CreateDefaultSerializerSettings();
    settings.TypeNameHandling = TypeNameHandling.None;
    options.JsonSerializerSettings = settings;
});
```

### 20.2 自定义序列化服务

```csharp
public class SafeJsonSerializerService : NewtonsoftJsonSerializerService
{
    protected override JsonSerializerSettings GetSerializerSettings()
    {
        var settings = NewtonsoftJsonOptions.Instance.CreateDefaultSerializerSettings();
        settings.TypeNameHandling = TypeNameHandling.None;
        return settings;
    }
}
```

注册：

```csharp
services.AddSingleton<IJsonSerializerService, SafeJsonSerializerService>();
```

如果已使用 `AddNewtonsoftJsonSerializerService()`，该方法采用 `TryAdd`，应确保自定义服务先注册，或直接使用明确的 `Replace` 策略。

---

## 21. 常见问题

### 反序列化后接口或抽象类无法创建

原因：普通设置没有保存真实派生类型。

处理：

- 对受信任项目文件使用引用设置的 `TypeNameHandling.All`。
- 或使用 DTO 类型判别字段和手动工厂。

### JSON 出现 `$id`、`$ref`、`$type`

这是引用设置的正常结果：

- `$id` / `$ref`：共享对象和循环引用。
- `$type`：运行时类型信息。

如果不需要这些字段，使用 `DefaultNewtonsoftJsonSerializerService` 或 `CreateDefaultSerializerSettings()`。

### WPF `Point`、`Thickness`、`Brush` 反序列化失败

检查：

1. 是否仍使用 `TypeConverterJsonConverter`。
2. JSON 字符串是否符合对应 WPF TypeConverter 格式。
3. 自定义 TypeConverter 是否支持不变区域字符串转换。
4. 是否误将类型标注为 `IgnoreTypeConverterJsonConverterAttribute`。

### 日期解析失败

默认日期格式必须是：

```text
yyyy-MM-dd HH:mm:ss
```

如需毫秒、UTC 或 `DateTimeOffset`，应替换 `DateTimeConverter`。

### 枚举值改名后旧项目打不开

默认 `EnumConverter` 使用枚举名称。应保留兼容枚举成员、实现枚举名称映射，或在读取 JSON 前迁移旧字段值。

### `ICommand` 没有保存

这是 `CustomContractResolver` 的设计行为。命令属于运行时行为，不应被持久化；请保存命令需要的业务参数，而不是命令实例。

### 某个字段错误但加载没有抛异常

默认 `Error` 回调会记录并处理局部错误。应检查日志中的：

```text
Json兼容错误: {JSON 路径}
```

并在加载后验证关键业务字段。

---

## 22. 二次开发建议

- 本地复杂项目模型可使用默认引用设置，但只加载受信任文件。
- 对外 JSON、网络请求、插件输入不要使用 `TypeNameHandling.All`。
- Presenter、节点和项目数据中使用 `[JsonIgnore]` / `[XmlIgnore]` 标记运行时属性。
- `ICommand`、服务引用、WPF 控件、窗口、Dispatcher 对象不应作为持久化业务状态。
- 日期、枚举和 WPF 结构类型的默认格式是框架约定，修改前评估旧文件兼容性。
- 长期保存的业务格式应增加 `Version` 字段，并明确设计数据迁移。
- 使用接口、抽象类集合时，要么使用受信任的类型信息，要么设计显式节点类型字段和工厂。
- 保存 Diagram 时优先保存节点 ID、端口 ID 和连线 ID 关系，不依赖运行时 UI 引用。
- JSON 深拷贝适合纯数据对象，不适合具有外部资源和 UI 生命周期的对象。
