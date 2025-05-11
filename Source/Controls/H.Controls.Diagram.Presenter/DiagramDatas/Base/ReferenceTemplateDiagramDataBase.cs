// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.Diagram.Presenter.DiagramDatas.Base;

public abstract class ReferenceTemplateDiagramDataBase : ThemeDigramDataBase, IReferenceTemplateDiagramData
{
    private ObservableCollection<FlowableDiagramTemplateNodeData> _referenceTemplateNodeDatas = new ObservableCollection<FlowableDiagramTemplateNodeData>();
    [System.Text.Json.Serialization.JsonIgnore]

    [XmlIgnore]
    public ObservableCollection<FlowableDiagramTemplateNodeData> ReferenceTemplateNodeDatas
    {
        get { return _referenceTemplateNodeDatas; }
        set
        {
            _referenceTemplateNodeDatas = value;
            RaisePropertyChanged();
        }
    }

    public RelayCommand DeleteReferenceTemplateCommand => new RelayCommand(e =>
    {
        if (e is FlowableDiagramTemplateNodeData template)
            this.DeleteReferenceTemplate(template);
    });

    public void DeleteReferenceTemplate(FlowableDiagramTemplateNodeData data)
    {
        //IEnumerable<Node> finds = this.Nodes.Where(x =>
        //{
        //    return x.Content is FlowableDiagramTemplateNodeData f ? f.FilePath == data.FilePath : false;
        //});
        //if (finds != null && finds.Count() > 0)
        //{
        //    bool? r = await IocMessage.Dialog.Show("当前已添加对应节点，删除将删除节点，是否确定删除？");
        //    if (r != true) return;

        //    foreach (Node item in finds.ToList())
        //    {
        //        item.Delete();
        //    }
        //}

        //this.ReferenceTemplateNodeDatas.Remove(data);
    }
}
