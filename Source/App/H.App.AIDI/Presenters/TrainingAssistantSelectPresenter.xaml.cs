// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.App.AIDI.Base;
using H.Common.Attributes;
using H.Extensions.Common;
using H.Extensions.FontIcon;

namespace H.App.AIDI.Presenters;
[Icon(FontIcons.Add)]
[Display(Name = "模型训练助手")]
public class TrainingAssistantSelectPresenter : DisplayBindableBase
{
    public TrainingAssistantSelectPresenter()
    {
        var items = this.GetType().GetInstances<ITrainingAssistantItemPresenter>();
        this.TrainingAssistantItemPresenters = items.ToObservable();
        this.SelectedTrainingAssistantItemPresenter = items?.FirstOrDefault();
    }

    private ObservableCollection<ITrainingAssistantItemPresenter> _TrainingAssistantItemPresenters = new ObservableCollection<ITrainingAssistantItemPresenter>();

    public ObservableCollection<ITrainingAssistantItemPresenter> TrainingAssistantItemPresenters
    {
        get { return _TrainingAssistantItemPresenters; }
        set
        {
            _TrainingAssistantItemPresenters = value;
            RaisePropertyChanged();
        }
    }

    private ITrainingAssistantItemPresenter _SelectedTrainingAssistantItemPresenter;

    public ITrainingAssistantItemPresenter SelectedTrainingAssistantItemPresenter
    {
        get { return _SelectedTrainingAssistantItemPresenter; }
        set
        {
            _SelectedTrainingAssistantItemPresenter = value;
            RaisePropertyChanged();
        }
    }
}




