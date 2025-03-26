global using H.Services.Message.Dialog.Commands;
global using H.Services.Message;
global using H.Services.Message.Dialog;

namespace H.Controls.Step
{
    public class ShowStepCommand : ShowMessageDialogCommandBase
    {
        public int Count { get; set; } = 5;

        public double StepLineWidth { get; set; } = 80;
        public override async Task ExecuteAsync(object parameter)
        {
            StepPresenter presenter = new StepPresenter();
            for (int i = 0; i < this.Count; i++)
            {
                presenter.Collection.Add(new StepItemPresenter()
                {
                    DisplayName = i.ToString(),
                    Width = this.StepLineWidth,
                });
            }
            await IocMessage.Dialog.ShowAction(presenter, x =>
             {
                 x.DialogButton = DialogButton.None;
                 this.Invoke(x);
             }, (d, p) =>
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
