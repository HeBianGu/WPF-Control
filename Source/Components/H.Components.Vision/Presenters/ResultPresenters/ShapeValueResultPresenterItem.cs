// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Components.Vision.Presenters.ResultPresenters;

public class ShapeValueResultPresenterItem<T> : ValueResultPresenterItem<T>, IRectangleResultItem, IShapeResultPresenterItem
{
    private readonly Rect _rect;
    private readonly IShape _shape;
    public ShapeValueResultPresenterItem(Rect rect)
    {
        this._rect = rect;
    }
    public ShapeValueResultPresenterItem(IShape shape, Rect rect, T value) : base(value)
    {
        this._rect = rect;
        this._shape = shape;
    }

    [Browsable(false)]
    public Rect Rect => this._rect;
    [Browsable(false)]
    public IShape Shape => this._shape;
}

