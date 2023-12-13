
using H.Providers.Mvvm;
using System.ComponentModel.DataAnnotations;

namespace H.Presenters.Design
{
    [Displayer(Name = "属性列表", Icon = "\xe740")]
    public class PropertyGridDesignPresenter : CommandsDesignPresenterBase
    {
        private object _data;
        [Display(Name = "数据源", GroupName = "常用,数据")]
        public object Data
        {
            get { return _data; }
            set
            {
                _data = value;
                RaisePropertyChanged();
            }
        }
    }
}
