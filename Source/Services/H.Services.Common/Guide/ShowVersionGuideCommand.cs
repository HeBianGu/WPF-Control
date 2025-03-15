//using H.Mvvm;
//using H.Mvvm.Attributes;
//using System.ComponentModel.DataAnnotations;

//namespace H.Services.Common
//{
//    [Icon("\xE75A")]
//    [Display(Name = "版本新增功能", Description = "显示版本新增功能向导")]
//    public class ShowVersionGuideCommand : DisplayMarkupCommandBase
//    {
//        public string Version { get; set; }
//        public override async Task ExecuteAsync(object parameter)
//        {
//            var version = Assembly.GetEntryAssembly().GetName().Version.ToString();
//            await Ioc<IGuideService>.Instance.Show(Version??parameter?.ToString());
//        }
//    }
//}
