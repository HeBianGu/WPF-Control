//using H.Mvvm;
//using H.Mvvm.Attributes;
//using System.ComponentModel.DataAnnotations;

//namespace H.Services.Common
//{
//    [Icon("\xE75A")]
//    [Display(Name = "新增功能", Description = "显示新增功能向导")]
//    public class ShowNewGuideCommand : DisplayMarkupCommandBase
//    {
//        public override async Task ExecuteAsync(object parameter)
//        {
//            var version = Assembly.GetEntryAssembly().GetName().Version.ToString();
//            await Ioc<IGuideService>.Instance.Show(version);
//        }
//    }
//}
