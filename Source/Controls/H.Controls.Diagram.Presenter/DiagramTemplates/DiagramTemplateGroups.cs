// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.Diagram.Presenter.DiagramTemplates;

public class DiagramTemplateGroups : BindableBase
{
    public DiagramTemplateGroups(IEnumerable<DiagramTemplateGroup> collection)
    {
        this.Collection = collection.ToObservable();
    }

    public string Name { get; set; }

    private ObservableCollection<DiagramTemplateGroup> _collection = new ObservableCollection<DiagramTemplateGroup>();
    /// <summary> 说明  </summary>
    public ObservableCollection<DiagramTemplateGroup> Collection
    {
        get { return _collection; }
        set
        {
            _collection = value;
            RaisePropertyChanged();
        }
    }
}
