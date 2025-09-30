// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.Diagram.Presenter.DiagramTemplates;

[Icon(FontIcons.Preview)]
[Display(Name = "选择模板")]
public class DiagramTemplates : DisplayBindableBase, IDiagramTempaltes
{
    private ObservableCollection<IDiagramTemplate> _collection = new ObservableCollection<IDiagramTemplate>();
    public ObservableCollection<IDiagramTemplate> Collection
    {
        get { return _collection; }
        set
        {
            _collection = value;
            RaisePropertyChanged();
            this.SelectedDiagramTemplate = value?.FirstOrDefault();
        }
    }

    private IDiagramTemplate _selectedDiagramTemplate;
    [JsonIgnore]
    public IDiagramTemplate SelectedDiagramTemplate
    {
        get { return _selectedDiagramTemplate; }
        set
        {
            _selectedDiagramTemplate = value;
            RaisePropertyChanged();
        }
    }

}
