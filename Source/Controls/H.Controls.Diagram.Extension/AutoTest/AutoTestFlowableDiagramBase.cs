
//using H.Services.Common;
//using H.Mvvm;



//using System.Collections.ObjectModel;
//using System.ComponentModel;
//using System.ComponentModel.DataAnnotations;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Xml.Serialization;

//namespace H.Controls.Diagram.Extension
//{
//    public abstract class AutoTestFlowableDiagramBase : FlowableDiagramBase
//    {
//        public AutoTestFlowableDiagramBase()
//        {
//            //_autoTestResultPresenter = new AutoTestResultPresenter(this);
//        }

//        private bool _isSaved;

//        public override Task<bool?> Start(Node startNode)
//        {
//            _isSaved = false;
//            return base.Start(startNode);
//        }

//        [XmlIgnore]
//         [Display(Name = "数据统计", GroupName = "操作", Order = 8, Description = "点击此功能，查看运行结果数据统计")]
//        public virtual RelayCommand ShowResultCommand => new RelayCommand(async (s, e) =>
//        {
//            await ShowResult();
//        });

//        protected virtual async Task<bool> ShowResult()
//        {
//            //return IocMessage.Dialog.Show(AutoTestResultPresenter, null, "数据统计", x =>
//            //{
//            //    x.Margin = new Thickness(20);
//            //    x.Padding = new Thickness(10);
//            //    x.MinHeight = 500;
//            //    x.VerticalAlignment = VerticalAlignment.Stretch;
//            //    x.HorizontalContentAlignment = HorizontalAlignment.Stretch;
//            //});

//            var r= await IocMessage.Dialog.Show(AutoTestResultPresenter);
//            returntrue;
//        }

//        [XmlIgnore]
//         [Display(Name = "分析报表", GroupName = "操作", Order = 8, Description = "点击此功能，查看运行结果数据统计")]
//        public virtual RelayCommand ShowReportCommand => new RelayCommand(async (s, e) =>
//        {
//            await ShowReport();
//        });

//        protected virtual async Task<bool> ShowReport()
//        {
//            //return await IocMessage.Dialog.ShowClose(new AutoTestReportViewPresenter(), null, "分析报表", x =>
//            //{
//            //    x.Margin = new Thickness(20);
//            //    x.Padding = new Thickness(10);
//            //    x.VerticalAlignment = VerticalAlignment.Stretch;
//            //    x.HorizontalAlignment = HorizontalAlignment.Stretch;
//            //});

//             var r= await IocMessage.Dialog.Show(new AutoTestReportViewPresenter());
//            return r == true;
//        }

//        [XmlIgnore]
//         [Display(Name = "参数设置", GroupName = "操作", Order = 9, Description = "点击此功能，进行相关参数设置")]
//        public virtual RelayCommand SettingCommand => new RelayCommand(async (s, e) =>
//        {
//            await ShowSetting();
//        });


//        protected virtual Task<bool> ShowSetting()
//        {
//            IocMessage.Snack?.ShowInfo("未实现该功能");
//            return Task.FromResult(true);
//        }

//        //[XmlIgnore]
//        // [Display(Name = "保存结果", GroupName = "操作", Order = 6, Description = "点击此功能，进行相关参数设置", Icon = "\xe8f4")]
//        //public virtual RelayCommand SaveResultCommand => new RelayCommand(async (s, e) =>
//        //{
//        //    await IocMessage.Dialog.ShowWait(async () =>
//        //      {
//        //          ats_dd_result result = new ats_dd_result();
//        //          result.Diagram = ToXmlString();
//        //          result.Result = this.State == DiagramFlowableState.Success ? "合格" : "不合格";
//        //          await AutoTestResultPresenter.Add(result);
//        //          IocMessage.Snack?.ShowInfo("保存完成，请在数据统计中查看");
//        //          return true;
//        //      });
//        //    _isSaved = true;

//        //}, (x, e) => _isSaved == false && (State == DiagramFlowableState.Success || State == DiagramFlowableState.Error));

//        //private AutoTestResultPresenter _autoTestResultPresenter;
//        //[XmlIgnore]
//        //public AutoTestResultPresenter AutoTestResultPresenter
//        //{
//        //    get { return _autoTestResultPresenter; }
//        //    set
//        //    {
//        //        _autoTestResultPresenter = value;
//        //        RaisePropertyChanged();
//        //    }
//        //}
//    }
//}
