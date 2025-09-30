// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.Diagram.Presenter.DiagramTemplates;

//public class XmlDiagramTemplateData
//{
//    //public XmlDiagramData DiagramData { get; set; }

//    //public string Name { get; set; }

//    //public XmlClassData Datas;

//    public XmlDiagramTemplateData(DiagramTemplate template)
//    {
//        XmlClassData xmlClassData = new XmlClassData(template);
//        this.Data = xmlClassData;
//    }
//    public XmlDiagramTemplateData()
//    {

//    }

//    public XmlClassData Data { get; set; }
//}

public class DiagramTemplateGroup : BindableBase
{
    public DiagramTemplateGroup(IEnumerable<DiagramTemplate> collection)
    {
        this.Collection = collection.ToObservable();
    }

    public string Name { get; set; }

    private ObservableCollection<DiagramTemplate> _collection = new ObservableCollection<DiagramTemplate>();
    /// <summary> 说明  </summary>
    public ObservableCollection<DiagramTemplate> Collection
    {
        get { return _collection; }
        set
        {
            _collection = value;
            RaisePropertyChanged();
        }
    }

    private DiagramTemplate _selectedItem;
    /// <summary> 说明  </summary>
    public DiagramTemplate SelectedItem
    {
        get { return _selectedItem; }
        set
        {
            _selectedItem = value;
            RaisePropertyChanged();
        }
    }

}
