using H.Services.Common;
using H.Mvvm;
using System.Threading;

namespace H.Controls.Step
{
    public class ShowVerticalStepCommand : MarkupCommandBase
    {
        public int Count { get; set; } = 5;
        public double Width { get; set; } = 80;
        public override async void Execute(object parameter)
        {
            StepPresenter presenter = new StepPresenter();
            presenter.Orientation = System.Windows.Controls.Orientation.Vertical;
            for (int i = 0; i < this.Count; i++)
            {
                presenter.Collection.Add(new StepItemVerticalPresenter()
                {
                    DisplayName = i.ToString(),
                    Width = this.Width,
                });
            }
            await IocMessage.Dialog.ShowAction(presenter, x => x.DialogButton = DialogButton.None, (d, p) =>
            {
                foreach (var item in p.Collection)
                {
                    item.Message = "正在加载...";
                    item.State = StepState.Running;
                    for (int i = 0; i < 100; i++)
                    {
                        item.Percent = i;
                        Thread.Sleep(20);
                    }
                    item.Message = "加载完成";
                    item.State = StepState.Success;
                }
                Thread.Sleep(2000);
                return true;
            });
        }
    }
}
