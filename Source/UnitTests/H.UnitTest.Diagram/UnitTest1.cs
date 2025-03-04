using H.Controls.Diagram.Datas;
using H.Controls.Diagram.Presenter.NodeDatas;
using H.Controls.Diagram.Presenters.OpenCV;
using H.Extensions.NewtonsoftJson;
using H.Services.Serializable;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace H.UnitTest.Diagram;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void TestMethod1()
    {
        OpenCVDiagramData openCVDiagramData = new OpenCVDiagramData();
        ObservableCollection<INodeData> nodeDatas = new ObservableCollection<INodeData>();
        nodeDatas.Add(new TextNodeData() { Text = "123456" });

        openCVDiagramData.NodeDatas.Add(new TextNodeData() { Text = "123456" });
        //NewtonsoftJsonSerializerService serializerService = new NewtonsoftJsonSerializerService();
        //string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "1.json");
        //serializerService.Save(path, nodeDatas);
        var txt = JsonConvert.SerializeObject(openCVDiagramData, CreateSerializerSettings());
        System.Diagnostics.Debug.WriteLine(txt);
    }

    private JsonSerializerSettings CreateSerializerSettings()
    {
        var setting = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All,
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore,//Ĭ��ֵ������,�������DefaultValue�������Ĭ��ֵ����������֮��ͻ����DefaultValue����
            Formatting = Formatting.Indented,// ���Ӹ�ʽ��ѡ��
                                             // �����������ĸ�������
                                             //NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals��
                                             //ReferenceLoopHandling = ReferenceLoopHandling.Ignore //����ѭ������
            ContractResolver = new CustomContractResolver(), // ʹ���Զ����ContractResolver�����ֶ�
            Converters = {
                    new H.Extensions.NewtonsoftJson.TypeConverterJsonConverter(),
                    new H.Extensions.NewtonsoftJson.EnumConverter(),
                    new H.Extensions.NewtonsoftJson.DateTimeConverter() }//�ⲿ�����л��ǻ��߼���������FilterBox����

        };
        return setting;
    }
}