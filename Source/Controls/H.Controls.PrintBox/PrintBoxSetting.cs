using System.ComponentModel.DataAnnotations;
using H.Providers.Ioc;
using H.Extensions.Setting;

namespace H.Controls.PrintBox
{
    [Display(Name = "打印设置", GroupName = SettingGroupNames.GroupControl)]
    public class PrintBoxSetting : Setting<PrintBoxSetting>
    {
        private double _printableAreaWidth;
        public double PrintableAreaWidth
        {
            get { return _printableAreaWidth; }
            set
            {
                _printableAreaWidth = value;
                RaisePropertyChanged();
            }
        }
    }
}
