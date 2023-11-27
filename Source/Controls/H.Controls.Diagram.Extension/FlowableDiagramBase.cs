



using H.Providers.Ioc;
using H.Providers.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace H.Controls.Diagram.Extension
{
    public abstract class FlowableDiagramBase : DiagramBase, IFlowableDiagram
    {
        protected override IEnumerable<Node> LoadNodes(IEnumerable<INodeData> nodeDatas, IEnumerable<ILinkData> linkDatas)
        {
            DiagramFlowableDataSourceConverter converter = new DiagramFlowableDataSourceConverter(nodeDatas, linkDatas);
            return converter.NodeSource;
        }

        private DiagramFlowableState _state = DiagramFlowableState.None;
        /// <summary> 说明  </summary>
        public DiagramFlowableState State
        {
            get { return _state; }
            set
            {
                _state = value;
                RaisePropertyChanged("State");
            }
        }

        private DiagramFlowableMode _startMode = DiagramFlowableMode.Port;
        [Display(Name = "运行模式", Order = 0, GroupName = "数据")]
        public DiagramFlowableMode StartMode
        {
            get { return _startMode; }
            set
            {
                _startMode = value;
                RaisePropertyChanged();
            }
        }

         [Display(Name = "开始", GroupName = "操作", Order = 0)]
        public RelayCommand StartCommand => new RelayCommand(async (s, e) =>
        {
            var start = this.GetStartNode();
            if (start == null)
            {
                return;
            }
            s.IsBusy = true;
            if (AppSetting.Instance.UseAutoShowLog)
            {
                AppSetting.Instance.ShowLog = true;
                //WindowMessageViewPresenter.Instance.IsVisible = true;
            }
            this.State = DiagramFlowableState.Running;
            bool? b = await Start(start);
            this.State = b == null ? DiagramFlowableState.Canceled : b == true ? DiagramFlowableState.Success : DiagramFlowableState.Error;
            IocMessage.Snack?.ShowInfo(b == null ? "用户取消" : b == true ? "运行成功" : "运行失败");
            //Commander.InvalidateRequerySuggested();
            s.IsBusy = false;
            await Task.Delay(2000).ContinueWith(x =>
             {
                 //AppSetting.Instance.ShowLog = false;
                 //WindowMessageViewPresenter.Instance.IsVisible = false;

             });
        }, (s, e) => State != DiagramFlowableState.Running && this.State != DiagramFlowableState.Canceling);


         [Display(Name = "停止", GroupName = "操作", Order = 0)]
        public RelayCommand StopCommand => new RelayCommand((s, e) =>
        {
            var parts = this.Nodes.SelectMany(x => x.GetParts());
            foreach (var part in parts)
            {
                var data = part.GetContent<IFlowable>();
                {
                    if (data != null)
                    {
                        if (data.State == FlowableState.Running || data.State == FlowableState.Wait)
                        {
                            data.State = FlowableState.Canceling;
                        }
                    }
                }
                if (part.State == FlowableState.Running || part.State == FlowableState.Wait)
                {
                    part.State = FlowableState.Canceling;
                }
            }
        }, (s, e) => State == DiagramFlowableState.Running && this.State != DiagramFlowableState.Canceling);

         [Display(Name = "重置", GroupName = "操作", Order = 0)]
        public RelayCommand CancelCommand => new RelayCommand((s, e) =>
        {
            var parts = this.Nodes.SelectMany(x => x.GetParts());
            foreach (var part in parts)
            {
                part.State = FlowableState.Ready;
                if (part.Content is IFlowable flowable)
                    flowable.State = FlowableState.Ready;
            }
            this.State = DiagramFlowableState.None;
        }, (s, e) => State != DiagramFlowableState.None && this.State != DiagramFlowableState.Canceling);


        public RelayCommand StartNodeCommand => new RelayCommand(async (s, e) =>
        {
            Node start = e as Node;
            if (start == null)
                return;
            s.IsBusy = true;
            if (AppSetting.Instance.UseAutoShowLog)
            {
                AppSetting.Instance.ShowLog = true;
                //WindowMessageViewPresenter.Instance.IsVisible = true;
            }
            this.State = DiagramFlowableState.Running;
            bool? b = await Start(start);
            this.State = b == null ? DiagramFlowableState.Canceled : b == true ? DiagramFlowableState.Success : DiagramFlowableState.Error;
            IocMessage.Snack?.ShowInfo(b == null ? "用户取消" : b == true ? "运行成功" : "运行失败");
            //Commander.InvalidateRequerySuggested();
            s.IsBusy = false;
            await Task.Delay(2000).ContinueWith(x =>
            {
                if (AppSetting.Instance.UseAutoShowLog)
                {
                    AppSetting.Instance.ShowLog = false;
                    //WindowMessageViewPresenter.Instance.IsVisible = false;
                }
            });
        });

        protected virtual Node GetStartNode()
        {
            var parts = this.Nodes.SelectMany(x => x.GetParts());
            foreach (var part in parts)
            {
                part.State = FlowableState.Wait;
                if (part.Content is IFlowable flowable)
                    flowable.State = FlowableState.Wait;
            }
            var starts = this.Nodes.Where(x => x.GetFromNodes().Count == 0 && x.GetContent<FlowableNodeData>()?.UseStart == true);

            if (starts == null || starts.Count() == 0)
            {
                IocMessage.Snack?.ShowInfo("未找到起始节点,请添加UseStart节点");
                return null;
            }

            if (starts.Count() > 1)
            {
                IocMessage.Snack?.ShowInfo("查询到多个节点");
                return null;
            }

            return starts.FirstOrDefault();
        }

        public virtual async Task<bool?> Start(Node startNode)
        {
            Action<Part> builder = x =>
            {
                if (AppSetting.Instance.UseAutoLocator)
                    this.LocateCenter(x);
                if (AppSetting.Instance.UseAutoSelect)
                    x.IsSelected = true;
                if (AppSetting.Instance.UseAutoScaleTo)
                    this.LocateRect(x);
            };
            if (this.StartMode == DiagramFlowableMode.Link)
                return await startNode.StartLink(builder);
            if (this.StartMode == DiagramFlowableMode.Port)
                return await startNode.StartPort(builder);
            return await startNode.StartNode(builder);
        }

        public async Task<bool?> Start()
        {
            var start = this.GetStartNode();
            if (start == null)
            {
                this.Message = "没有找到开始节点";
                return false;
            }
            var b = await Start(start);
            this.State = b == null ? DiagramFlowableState.Canceled : b == true ? DiagramFlowableState.Success : DiagramFlowableState.Error;
            return b;
        }

        private string _message;
        [Browsable(false)]
        [XmlIgnore]
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                RaisePropertyChanged();
            }
        }

    }
}
