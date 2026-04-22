// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using System;
global using System.Collections.ObjectModel;
global using System.Windows;
global using System.Windows.Media;
global using System.Windows.Media.Imaging;
global using System.Windows.Threading;
using H.Controls.ShapeBox.Drawings;
using H.Controls.ShapeBox.Shapes.Base;
using H.Controls.ZoomBox;
using H.Extensions.Common;
using System.Linq;

namespace H.Controls.ShapeZoomBox;
public class ShapeZoomBox : Zoombox
{
    static ShapeZoomBox()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(ShapeZoomBox), new FrameworkPropertyMetadata(typeof(ShapeZoomBox)));
    }

    public ObservableCollection<IShape> Shapes
    {
        get { return (ObservableCollection<IShape>)GetValue(ShapesProperty); }
        set { SetValue(ShapesProperty, value); }
    }

    public static readonly DependencyProperty ShapesProperty =
        DependencyProperty.Register("Shapes", typeof(ObservableCollection<IShape>), typeof(ShapeZoomBox), new FrameworkPropertyMetadata(default(ObservableCollection<IShape>), (d, e) =>
        {
            ShapeZoomBox control = d as ShapeZoomBox;

            if (control == null) return;

            if (e.OldValue is ObservableCollection<IShape> o)
            {
                o.CollectionChanged -= control.Shapes_CollectionChanged;
            }

            if (e.NewValue is ObservableCollection<IShape> n)
            {
                n.CollectionChanged -= control.Shapes_CollectionChanged;
                n.CollectionChanged += control.Shapes_CollectionChanged;
            }
            control.ZoomToShapes();
        }));

    public void ZoomToShapes()
    {
        if (this.Shapes == null)
            return;
        var box = this.Shapes.OfType<IBoundingBoxShape>().GetBoundingBox();
        this.ZoomTo(box.GetPadding(2));
    }

    private void Shapes_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        this.ZoomToShapes();
    }
}
