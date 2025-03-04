using H.Extensions.Setting;
using H.Services.Common;
using H.Mvvm;



#if NETFRAMEWORK
using System.Data.Entity;
#endif

#if NETCOREAPP
using Microsoft.Extensions.DependencyInjection;
using System;
#endif
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Serialization;

namespace H.DataBases.Share
{
    public abstract class DbSettableBase<T> : LazySettableInstance<T>, IDbSettable where T : new()
    {
        public abstract string GetConnect();

        [System.Text.Json.Serialization.JsonIgnore]
        [System.Xml.Serialization.XmlIgnore]
        [Browsable(false)]
        [Display(Name = "测试连接")]
        public InvokeCommand ConnectCommand => new InvokeCommand(async (s,e) =>
        {
            Tuple<bool, string> r = await Task.Run(() =>
              {
                  s.IsBusy = true;
                  s.Message = "正在连接...";

                  System.Collections.Generic.IEnumerable<IDbConnectService> inits = Ioc.Services.GetServices<ISplashLoad>().OfType<IDbConnectService>();
                  bool r = false;
                  foreach (IDbConnectService init in inits)
                  {
                      r = init.TryConnect(out string message);
                      if (r == false)
                      {
                          s.Message = message;
                          break;
                      }
                  }

                  Thread.Sleep(500);
                  s.IsBusy = false;
                  CommandManager.InvalidateRequerySuggested();
                  return Tuple.Create(r, s.Message);
              });

            if (r.Item1 == false)
                await IocMessage.Window.Show(r.Item2, x => x.Title = "连接失败");
        });

        [System.Text.Json.Serialization.JsonIgnore]
        
        [System.Xml.Serialization.XmlIgnore]
        [Browsable(false)]
        [Display(Name = "保存配置")]
        public RelayCommand SaveCommand => new RelayCommand(e=>
        {
            bool r = this.Save(out string message);
            if (r)
            {
                IocMessage.Window.Show("保存成功");
            }
            else
            {
                IocMessage.Window.Show(message, x => x.Title = "保存失败");
            }
        });
    }

}
