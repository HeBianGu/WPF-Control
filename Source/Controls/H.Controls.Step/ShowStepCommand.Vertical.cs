// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.Step
{
    public class ShowVerticalStepCommand : ShowMessageDialogCommandBase
    {
        public int Count { get; set; } = 5;

        public double StepLineWidth { get; set; } = 80;

        public override async Task ExecuteAsync(object parameter)
        {
            StepPresenter presenter = new StepPresenter();
            presenter.Orientation = System.Windows.Controls.Orientation.Vertical;
            for (int i = 0; i < this.Count; i++)
            {
                presenter.Collection.Add(new StepItemVerticalPresenter()
                {
                    DisplayName = i.ToString(),
                    Width = this.StepLineWidth,
                });
            }
            await IocMessage.Dialog.ShowAction(presenter, x =>
            {
                x.DialogButton = DialogButton.None;
                x.Padding = new System.Windows.Thickness(10, 10, 0, 10);
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
