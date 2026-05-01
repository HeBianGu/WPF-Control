// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Mvvm.ViewModels.Base;

namespace H.Controls.ShapeBox.Presenters;
public interface IToolTipShapePresenter
{

}
public class ToolTipShapePresenter : BindableBase, IToolTipShapePresenter
{
    public ToolTipShapePresenter()
    {

    }

    public ToolTipShapePresenter(IEnumerable<IMouseOverShape> shapes)
    {
        this.Shapes = new ObservableCollection<IMouseOverShape>(shapes);
    }

    private ObservableCollection<IMouseOverShape> _Shapes = new ObservableCollection<IMouseOverShape>();
    public ObservableCollection<IMouseOverShape> Shapes
    {
        get { return _Shapes; }
        set
        {
            _Shapes = value;
            RaisePropertyChanged();
        }
    }

}