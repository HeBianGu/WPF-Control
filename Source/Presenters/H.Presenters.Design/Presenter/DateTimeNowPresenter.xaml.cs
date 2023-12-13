using H.Providers.Mvvm;
using System;
using System.ComponentModel.DataAnnotations;

namespace H.Presenters.Design
{
    [Displayer(Name = "当前日期")]
    public class DateTimeNowPresenter : TitlePresenter
    {
        public DateTimeNowPresenter()
        {
            Title = "当前日期：";
            Text = DateTime.Now.ToString(Format);
        }
        public override void LoadDefault()
        {
            base.LoadDefault();
        }

        private string _format = "yyyy-MM-dd HH:mm:ss";
        [Display(Name = "日期格式", GroupName = "常用,样式")]
        public string Format
        {
            get { return _format; }
            set
            {
                _format = value;
                RaisePropertyChanged();
            }
        }

    }
}
