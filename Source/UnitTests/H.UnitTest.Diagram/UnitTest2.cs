using H.Extensions.TextJsonable;
using H.Mvvm;
using H.Services.Serializable;
using System.Text.Json;

namespace H.UnitTest.Diagram;

[TestClass]
public class UnitTest2
{
    [TestMethod]
    public void TestMethod1()
    {
        var options = TextJsonOptions.GetDefaltOptions();

        //options.Converters.Add(new TextJsonableJsonConverter());
        options.Converters.Add(new ObjectJsonConverter());

        MyJsonBindable bindable = new MyJsonBindable() { Age = 10 };
        //bindable.Notify = new MyJsonBindable();
        //for (int i = 0; i < 10; i++)
        //{
        //    bindable.Collection.Add(i.ToString());
        //}
        //for (int i = 0; i < 10; i++)
        //{
        //    bindable.MyJsonBindables.Add(new MyJsonBindable() { Age = i });
        //}
        for (int i = 0; i < 10; i++)
        {
            bindable.Nofities.Add(new MyDisposable() { MyProperty = i });
        }
        //bindable.Values = Enumerable.Range(0, 5).ToList();
        var txt = JsonSerializer.Serialize(bindable, options);
        System.Diagnostics.Debug.WriteLine(txt);
        var v = JsonSerializer.Deserialize<MyJsonBindable>(txt, options);
        Assert.AreEqual(bindable.Age, v.Age);
    }
}
