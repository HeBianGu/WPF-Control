global using H.Mvvm.Commands;
namespace H.Presenters.Design.Base;

public class CommandsDesignPresenterBase : DesignPresenter, IDesignPresenter
{
    [Display(Name = "恢复默认")]
    public RelayCommand DefaultCommand => new RelayCommand(e =>
    {
        if (e is string project)
        {

        }
    });

    [Display(Name = "保存模板")]
    public RelayCommand SaveTempateCommand => new RelayCommand(e =>
    {
        if (e is string project)
        {

        }
    });
}
