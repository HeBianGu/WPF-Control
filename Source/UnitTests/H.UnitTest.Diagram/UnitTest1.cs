using H.Controls.Diagram.Datas;
using H.Controls.Diagram.Presenter.NodeDatas;
using H.Controls.Diagram.Presenters.OpenCV;
using H.Extensions.NewtonsoftJson;
using H.Extensions.NewtonsoftJson.Jsonable;
using H.Services.Serializable;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace H.UnitTest.Diagram;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void TestMethod1()
    {
        //OpenCVDiagramData openCVDiagramData = new OpenCVDiagramData();
        //ObservableCollection<INodeData> nodeDatas = new ObservableCollection<INodeData>();
        //nodeDatas.Add(new TextNodeData() { Text = "123456" });

        //openCVDiagramData.NodeDatas.Add(new TextNodeData() { Text = "123456" });
        ////NewtonsoftJsonSerializerService serializerService = new NewtonsoftJsonSerializerService();
        ////string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "1.json");
        ////serializerService.Save(path, nodeDatas);
        //var txt = JsonConvert.SerializeObject(openCVDiagramData, CreateSerializerSettings());
        //System.Diagnostics.Debug.WriteLine(txt);
    }

    private JsonSerializerSettings CreateSerializerSettings()
    {
        var setting = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All,
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore,//默认值不保存,如果不加DefaultValue则跟类型默认值关连，加了之后就会根据DefaultValue关联
            Formatting = Formatting.Indented,// 增加格式化选项
                                             // 以允许命名的浮点文字
                                             //NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals，
                                             //ReferenceLoopHandling = ReferenceLoopHandling.Ignore //忽略循环引用
            ContractResolver = new CustomContractResolver(), // 使用自定义的ContractResolver忽略字段
            Converters = {
                    new H.Extensions.NewtonsoftJson.TypeConverterJsonConverter(),
                    new H.Extensions.NewtonsoftJson.EnumConverter(),
                    new H.Extensions.NewtonsoftJson.DateTimeConverter() }//这部分序列化是会逻辑有问题用FilterBox测试

        };
        return setting;
    }
    [TestMethod]
    public void TestMethod2()
    {
        //OpenCVDiagramData openCVDiagramData = new OpenCVDiagramData();
        //ObservableCollection<INodeData> nodeDatas = new ObservableCollection<INodeData>();
        //nodeDatas.Add(new TextNodeData() { Text = "123456" });

        //openCVDiagramData.NodeDatas.Add(new TextNodeData() { Text = "123456" });
        ////NewtonsoftJsonSerializerService serializerService = new NewtonsoftJsonSerializerService();
        ////string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "1.json");
        ////serializerService.Save(path, nodeDatas);
        //var txt = JsonConvert.SerializeObject(openCVDiagramData, CreateSerializerSettings());
        //System.Diagnostics.Debug.WriteLine(txt);

        MyNodeDatas myNodeDatas = new MyNodeDatas();
        myNodeDatas.NodeDatas.Add(new TextNodeData() { Text = "123456" });
        myNodeDatas.NodeDatas.Add(new ImageNodeData() { Text = "123456" });



        var setting = new JsonSerializerSettings
        {
            //TypeNameHandling = TypeNameHandling.All,
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore,//默认值不保存,如果不加DefaultValue则跟类型默认值关连，加了之后就会根据DefaultValue关联
            Formatting = Formatting.Indented,// 增加格式化选项
                                             // 以允许命名的浮点文字
                                             //NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals，
                                             //ReferenceLoopHandling = ReferenceLoopHandling.Ignore //忽略循环引用
                                             //ContractResolver = new CustomContractResolver(), // 使用自定义的ContractResolver忽略字段
                                             //Converters = {
                                             //        new H.Extensions.NewtonsoftJson.TypeConverterJsonConverter(),
                                             //        new H.Extensions.NewtonsoftJson.EnumConverter(),
                                             //        new H.Extensions.NewtonsoftJson.DateTimeConverter() }//这部分序列化是会逻辑有问题用FilterBox测试

        };
        var txt = JsonConvert.SerializeObject(myNodeDatas, setting);
        System.Diagnostics.Debug.WriteLine(txt);
    }

    [TestMethod]
    public void TestMethod3()
    {
        MyJsonable myNodeDatas = new MyJsonable();
        myNodeDatas.Value = 1234556;
        var setting = new JsonSerializerSettings
        {
            //TypeNameHandling = TypeNameHandling.All,
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore,//默认值不保存,如果不加DefaultValue则跟类型默认值关连，加了之后就会根据DefaultValue关联
            Formatting = Formatting.Indented,// 增加格式化选项
                                             // 以允许命名的浮点文字
                                             //NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals，
                                             //ReferenceLoopHandling = ReferenceLoopHandling.Ignore //忽略循环引用
                                             //ContractResolver = new CustomContractResolver(), // 使用自定义的ContractResolver忽略字段
            Converters = {
                                                     new H.Extensions.NewtonsoftJson.JsonableJsonConverter()
            }//这部分序列化是会逻辑有问题用FilterBox测试

        };
        var txt = JsonConvert.SerializeObject(myNodeDatas, setting);
        System.Diagnostics.Debug.WriteLine(txt);
       var v=  JsonConvert.DeserializeObject<MyJsonable>(txt, setting);
    }

}

[JsonDerivedType(typeof(TextNodeData))]
public class MyNodeDatas
{
    public ObservableCollection<INodeData> NodeDatas { get; set; } = new ObservableCollection<INodeData>();
}