// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.App.AIDI.Base;
using H.App.AIDI.Model;
using H.Mvvm.Commands;
using H.Services.Message;

namespace H.App.AIDI.Presenters;
[Display(Name = "②训练管理", Description = "管理数据的训练和处理", GroupName = "流程")]
public class TrainManagerTabPresenter : TabPresenterBase
{
    public TrainManagerTabPresenter(IModulePresenter modulePresenter) : base(modulePresenter)
    {

    }
    private YoloConfig _YoloConfig = new YoloConfig();
    public YoloConfig YoloConfig
    {
        get { return _YoloConfig; }
        set
        {
            _YoloConfig = value;
            RaisePropertyChanged();
        }
    }

    private bool _UseAdvancedParameters;
    public bool UseAdvancedParameters
    {
        get { return _UseAdvancedParameters; }
        set
        {
            _UseAdvancedParameters = value;
            RaisePropertyChanged();
        }
    }

    public RelayCommand ShowTrainingAssistantCommand => new RelayCommand(async x =>
    {
        var presenter = new TrainingAssistantSelectPresenter();
        var r = await IocMessage.Dialog.Show(presenter);
        if (r != true)
            return;
        r = await IocMessage.Dialog.Show(presenter.SelectedTrainingAssistantItemPresenter);
        if (r != true)
            return;
        var rs = presenter.SelectedTrainingAssistantItemPresenter.Invoke(this.ModulePresenter);
        if (!rs.success)
            IocMessage.Snack.ShowError(rs.message);
    });

}
