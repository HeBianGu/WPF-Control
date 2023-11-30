

using H.Providers.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Serialization;

namespace H.Controls.Diagram.Extension
{
    public class FlowableNodeData : TextNodeData, IFlowableNode
    {
        [XmlIgnore]
         [Display(Name = "开始", GroupName = "操作")]
        public RelayCommand StartCommand => new RelayCommand(async (s, e) =>
        {
            if (e is Node part)
            {
                if (part.GetParent<Diagram>().DataContext is FlowableDiagramBase flowable)
                {
                    await flowable.Start(part);
                }
            }
        }, (s, e) => this.UseStart && this.State != FlowableState.Running && this.State != FlowableState.Canceling);

        private FlowableState _state = FlowableState.Ready;
        [Browsable(false)]
        public FlowableState State
        {
            get { return _state; }
            set
            {
                _state = value;
                RaisePropertyChanged("State");
            }
        }

        private bool _useAnimation = true;
        [XmlIgnore]
        [Browsable(false)]
        public bool UseAnimation
        {
            get { return _useAnimation; }
            set
            {
                _useAnimation = value;
                RaisePropertyChanged();
            }
        }

        private bool _useStart;
        [XmlIgnore]
        [Browsable(false)]
        public bool UseStart
        {
            get { return _useStart; }
            set
            {
                _useStart = value;
                RaisePropertyChanged();
            }
        }

        private bool _useInvoke;
        [XmlIgnore]
        [Browsable(false)]
        public bool UseInvoke
        {
            get { return _useInvoke; }
            set
            {
                _useInvoke = value;
                RaisePropertyChanged();
            }
        }

        private string _message;
        //[XmlIgnore]
        [Browsable(false)]
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                RaisePropertyChanged("Message");
            }
        }

        private bool _isBuzy;
        [XmlIgnore]
        [Browsable(false)]
        public bool IsBuzy
        {
            get { return _isBuzy; }
            set
            {
                _isBuzy = value;
                RaisePropertyChanged();
            }
        }


        private bool _useInfoLogger = true;
        [XmlIgnore]
        [Browsable(false)]
        public bool UseInfoLogger
        {
            get { return _useInfoLogger; }
            set
            {
                _useInfoLogger = value;
                RaisePropertyChanged();
            }
        }

        private Exception _exception;
        /// <summary> 说明  </summary>
        [Browsable(false)]
        [XmlIgnore]
        public Exception Exception
        {
            get { return _exception; }
            set
            {
                _exception = value;
                RaisePropertyChanged("Exception");
            }
        }
        [XmlIgnore]
        [Browsable(false)]
        protected Random Random { get; } = new Random();

        protected virtual IFlowableResult OK(string message = "运行成功")
        {
            this.Message = message;
            return new FlowableResult(message) { State = FlowableResultState.OK };
        }

        protected virtual IFlowableResult Error(string message = "运行错误")
        {
            this.Message = message;
            return new FlowableResult(message) { State = FlowableResultState.Error };
        }

        [XmlIgnore]
        [Display(Name = "执行")]
        public RelayCommand InvokeCommand => new RelayCommand(async l => await this.TryInvokeAsync(null, null));

        public virtual IFlowableResult Invoke(Part previors, Node current)
        {
            Thread.Sleep(AppSetting.Instance.FlowSleepMillisecondsTimeout);
            if (AppSetting.Instance.UseMock)
            {
                if (this.Random.Next(0, 19) == 1)
                    return this.Error("模拟仿真一个错误信息");
                return this.OK("模拟仿真一个成功信息");
            }
            return this.OK("运行成功");
        }

        public virtual async Task<IFlowableResult> InvokeAsync(Part previors, Node current)
        {
            return await Task.Run(() =>
            {
                return this.Invoke(previors, current);
            });
        }
        public virtual async Task<IFlowableResult> TryInvokeAsync(Part previors, Node current)
        {
            try
            {
                this.Clear();
                this.State = FlowableState.Running;
                this.IsBuzy = true;
                if (this.UseInfoLogger)
                    IocLog.Instance?.Info($"正在执行<{this.GetType().Name}>:{this.Text}");
                IFlowableResult result = await InvokeAsync(previors, current);
                if (this.UseInfoLogger)
                    IocLog.Instance?.Info(result.State == FlowableResultState.Error ? $"运行错误<{this.GetType().Name}>:{this.Text} {result.Message}" : $"执行完成<{this.GetType().Name}>:{this.Text} {result.Message}");
                this.State = result.State == FlowableResultState.OK ? FlowableState.Success : FlowableState.Error;
                return result;
            }
            catch (Exception ex)
            {
                this.State = FlowableState.Error;
                this.Exception = ex;
                this.Message = ex.Message;
                IocLog.Instance?.Error($"执行错误<{this.GetType().Name}>:{this.Name} {this.Message}");
                IocLog.Instance?.Error(ex);
                return this.Error(ex.Message);
            }
            finally
            {
                this.IsBuzy = false;
            }
        }

        public virtual void Clear()
        {
        }

        public override ILinkData CreateLinkData()
        {
            return new FlowableLinkData() { FromNodeID = this.ID };
        }

        public override IPortData CreatePortData()
        {
            return new FlowablePortData(this.ID, PortType.Both);
        }
        public virtual void Dispose()
        {

        }

        protected T GetFromData<T>(Node current)
        {
            var from = current.GetFromNodes().FirstOrDefault();
            if (from == null)
                return default(T);
            return from.GetContent<T>();
        }
    }

    public class StartFlowableNodeData : FlowableNodeData
    {
        public StartFlowableNodeData()
        {
            this.UseStart = true;
        }
    }
}
