// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace H.Mvvm
{

    public class DisplayCommand : RelayCommand, IDisplayCommand
    {
        public DisplayCommand(Action<object> action) : base(action)
        {

        }

        public DisplayCommand(Action<IRelayCommand, object> action) : base(action)
        {

        }

        public DisplayCommand(Action<object> execute, Predicate<object> canExecute) : base(execute, canExecute)
        {

        }

        public DisplayCommand(Action<IRelayCommand, object> execute, Func<IRelayCommand, object, bool> canExecute) : base(execute, canExecute)
        {

        }

        private string _description;
        [System.Text.Json.Serialization.JsonIgnore]
        
        [System.Xml.Serialization.XmlIgnore]
        [Browsable(false)]
        public virtual string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                RaisePropertyChanged();
            }
        }


        private int _order;
        [System.Text.Json.Serialization.JsonIgnore]
        
        [System.Xml.Serialization.XmlIgnore]
        [Browsable(false)]
        public virtual int Order
        {
            get { return _order; }
            set
            {
                _order = value;
                RaisePropertyChanged();
            }
        }
    }
}
