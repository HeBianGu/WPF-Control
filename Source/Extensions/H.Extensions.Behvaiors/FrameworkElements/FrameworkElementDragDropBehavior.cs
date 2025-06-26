// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Extensions.Behvaiors.FrameworkElements;

public class FrameworkElementDragDropFileBehaviorBase : Behavior<FrameworkElement>
{
    public static bool GetIsDragMouseOver(DependencyObject obj)
    {
        return (bool)obj.GetValue(IsDragEnterProperty);
    }

    public static void SetIsDragEnter(DependencyObject obj, bool value)
    {
        obj.SetValue(IsDragEnterProperty, value);
    }

    public static readonly DependencyProperty IsDragEnterProperty =
        DependencyProperty.RegisterAttached("IsDragEnter", typeof(bool), typeof(FrameworkElementDragDropFileBehaviorBase), new PropertyMetadata(default(bool), OnIsDragEnterChanged));

    static public void OnIsDragEnterChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        DependencyObject control = d as DependencyObject;

        bool n = (bool)e.NewValue;

        bool o = (bool)e.OldValue;
    }

    protected override void OnAttached()
    {
        this.AssociatedObject.AllowDrop = true;
        this.AssociatedObject.DragEnter += this.AssociatedObject_DragEnter;
        this.AssociatedObject.DragLeave += this.AssociatedObject_DragLeave;
        this.AssociatedObject.Drop += this.AssociatedObject_Drop;
    }

    private void AssociatedObject_Drop(object sender, DragEventArgs e)
    {
        if (e.Data.GetDataPresent(DataFormats.FileDrop))
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            this.DropCommand?.Execute(files);
        }
    }

    private void AssociatedObject_DragLeave(object sender, DragEventArgs e)
    {
        FrameworkElementDragDropFileBehaviorBase.SetIsDragEnter(this.AssociatedObject, false);
    }

    private void AssociatedObject_DragEnter(object sender, DragEventArgs e)
    {
        FrameworkElementDragDropFileBehaviorBase.SetIsDragEnter(this.AssociatedObject, true);
        // 检查拖入的数据是否包含文件
        if (e.Data.GetDataPresent(DataFormats.FileDrop))
        {
            e.Effects = DragDropEffects.Copy;
        }
        else
        {
            e.Effects = DragDropEffects.None;
        }
    }

    public ICommand DropCommand
    {
        get { return (ICommand)GetValue(DropCommandProperty); }
        set { SetValue(DropCommandProperty, value); }
    }

    public static readonly DependencyProperty DropCommandProperty =
        DependencyProperty.Register("DropCommand", typeof(ICommand), typeof(FrameworkElementDragDropFileBehaviorBase), new FrameworkPropertyMetadata(default(ICommand), (d, e) =>
        {
            FrameworkElementDragDropFileBehaviorBase control = d as FrameworkElementDragDropFileBehaviorBase;

            if (control == null) return;

            if (e.OldValue is ICommand o)
            {

            }

            if (e.NewValue is ICommand n)
            {

            }

        }));


    protected override void OnDetaching()
    {
        this.AssociatedObject.DragEnter -= this.AssociatedObject_DragEnter;
        this.AssociatedObject.DragLeave -= this.AssociatedObject_DragLeave;
    }
}

public class FrameworkElementDragDropFileBehavior : FrameworkElementDragDropFileBehaviorBase
{


}