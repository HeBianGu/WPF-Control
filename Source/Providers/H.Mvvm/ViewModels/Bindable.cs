using System;
using System.ComponentModel;
using System.Linq;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace H.Mvvm
{
    public abstract class Bindable : BindableBase
    {
        [Browsable(false)]
        [System.Text.Json.Serialization.JsonIgnore]
        
        [System.Xml.Serialization.XmlIgnore]
        public RelayCommand RelayCommand { get; set; }

        [Browsable(false)]
        [System.Text.Json.Serialization.JsonIgnore]
        
        [System.Xml.Serialization.XmlIgnore]
        public RelayCommand LoadedCommand => new RelayCommand(Loaded);

        [Browsable(false)]
        [System.Text.Json.Serialization.JsonIgnore]
        
        [System.Xml.Serialization.XmlIgnore]
        public RelayCommand CallMethodCommand { get; set; }


        //[Browsable(false)]
        //[XmlIgnore]
        //public ILogService Logger => ServiceRegistry.Instance.GetInstance<ILogService>();

        protected virtual void RelayMethod(object obj)
        {

        }

        public Bindable()
        {
            this.RelayCommand = new RelayCommand(RelayMethod);
            this.CallMethodCommand = new RelayCommand(CallMethod);
            RelayMethod("init");
        }

        protected virtual void Loaded(object obj)
        {

        }

        protected virtual void CallMethod(object obj)
        {
            string methodName = obj?.ToString();

            System.Reflection.MethodInfo method = GetType().GetMethod(methodName);

            if (method == null)
            {
                throw new ArgumentException("no found method :" + method);
            }

            object[] parameters = method.GetParameters().Select(l => l.RawDefaultValue is DBNull ? null : l.RawDefaultValue).ToArray();

            method.Invoke(this, parameters);
        }
    }

}
