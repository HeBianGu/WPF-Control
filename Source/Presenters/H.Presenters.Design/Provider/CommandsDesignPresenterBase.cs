using H.Mvvm;
using System.ComponentModel.DataAnnotations;

namespace H.Presenters.Design
{
    public class CommandsDesignPresenterBase : DesignPresenter, IDesignPresenter
    {
        [Display(Name = "恢复默认")]
        public RelayCommand DefaultCommand => new RelayCommand((s, e) =>
        {
            if (e is string project)
            {

            }
        });

        [Display(Name = "保存模板")]
        public RelayCommand SaveTempateCommand => new RelayCommand((s, e) =>
        {
            if (e is string project)
            {

            }
        });
    }
}
