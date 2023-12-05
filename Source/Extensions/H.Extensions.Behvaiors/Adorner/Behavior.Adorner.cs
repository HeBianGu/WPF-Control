// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using System;
#if NET
#endif 
using System.Windows;
using System.Windows.Documents;

namespace H.Extensions.Behvaiors
{

    //public class SelectedHitTestAdornerBehavior : HitTestAdornerBehavior
    //{
    //    public ObservableCollection<object> Parameters { get; } = new ObservableCollection<object>();

    //    public static bool GetIsSelected(DependencyObject obj)
    //    {
    //        return (bool)obj.GetValue(IsSelectedProperty);
    //    }

    //    public static void SetIsSelected(DependencyObject obj, bool value)
    //    {
    //        obj.SetValue(IsSelectedProperty, value);
    //    }

    //   
    //    public static readonly DependencyProperty IsSelectedProperty =
    //        DependencyProperty.RegisterAttached("IsSelected", typeof(bool), typeof(SelectedHitTestAdornerBehavior), new PropertyMetadata(default(bool), OnIsSelectedChanged));

    //    static public void OnIsSelectedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    //    {
    //        DependencyObject control = d as DependencyObject;

    //        bool n = (bool)e.NewValue;

    //        bool o = (bool)e.OldValue;
    //    }



    //    protected override void OnAttached()
    //    {
    //        AssociatedObject.AddHandler(UIElement.MouseLeftButtonDownEvent, new MouseButtonEventHandler(AssociatedObject_MouseLeftButtonDown), true);
    //    }
    //    protected override void OnDetaching()
    //    {
    //        AssociatedObject.RemoveHandler(UIElement.MouseLeftButtonDownEvent, new MouseButtonEventHandler(AssociatedObject_MouseLeftButtonDown));
    //        this.Clear();
    //    }

    //    private void AssociatedObject_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    //    {
    //        if (this.AdornerType == null)
    //            return;
    //        if (this.AdornerVisual == null)
    //            this.AdornerVisual = this.AssociatedObject;
    //        Point point = e.GetPosition(this.AssociatedObject);
    //        var visualHit = this.AssociatedObject.HitTest<UIElement>(point, x => SelectedHitTestAdornerBehavior.GetIsHitTest(x));
    //        if (visualHit == null)
    //        {
    //            this.Clear();
    //            if (_temp != null)
    //            {
    //                SelectedHitTestAdornerBehavior.SetIsSelected(_temp, false);
    //                Cattach.SetIsSelected(_temp, false);
    //            }
    //        }
    //        else
    //        {
    //            if (visualHit != _temp)
    //                this.Clear();
    //            this.AddAdorner(visualHit);
    //            SelectedHitTestAdornerBehavior.SetIsSelected(visualHit, true);
    //            Cattach.SetIsSelected(visualHit, true);
    //        }
    //        _temp = visualHit;
    //    }
    //}

    public class DragOverHitTestAdornerBehavior : HitTestAdornerBehavior
    {
        public Type AdornerDropErrorType
        {
            get { return (Type)GetValue(AdornerDropErrorTypeProperty); }
            set { SetValue(AdornerDropErrorTypeProperty, value); }
        }

        
        public static readonly DependencyProperty AdornerDropErrorTypeProperty =
            DependencyProperty.Register("AdornerDropErrorType", typeof(Type), typeof(DragOverHitTestAdornerBehavior), new FrameworkPropertyMetadata(default(Type), (d, e) =>
            {
                DragOverHitTestAdornerBehavior control = d as DragOverHitTestAdornerBehavior;

                if (control == null) return;

                if (e.OldValue is Type o)
                {

                }

                if (e.NewValue is Type n)
                {

                }

            }));

        protected override void OnAttached()
        {
            AssociatedObject.DragOver += AssociatedObject_DragOver;
            AssociatedObject.Drop += AssociatedObject_Drop;
            //AssociatedObject.DragEnter += AssociatedObject_DragEnter;
            //AssociatedObject.DragLeave += AssociatedObject_DragLeave;
        }


        public static bool GetIsPreviewing(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsPreviewingProperty);
        }

        public static void SetIsPreviewing(DependencyObject obj, bool value)
        {
            obj.SetValue(IsPreviewingProperty, value);
        }

       
        public static readonly DependencyProperty IsPreviewingProperty =
            DependencyProperty.RegisterAttached("IsPreviewing", typeof(bool), typeof(DragOverHitTestAdornerBehavior), new PropertyMetadata(default(bool), OnIsPreviewingChanged));

        public static void OnIsPreviewingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d;

            bool n = (bool)e.NewValue;

            bool o = (bool)e.OldValue;
        }


        private void AssociatedObject_DragLeave(object sender, DragEventArgs e)
        {
            //this.Clear();
        }

        private void AssociatedObject_DragEnter(object sender, DragEventArgs e)
        {

        }

        protected virtual void Drop(object sender, DragEventArgs e)
        {
            if (_temp.GetContent() is IHitTestElementDrop drag)
            {
                if (drag.CanDrop(_temp, e))
                    drag.Drop(_temp, e);
            }
            else if (AssociatedObject is IHitTestElementDrop drop)
            {
                if (drop.CanDrop(_temp, e))
                    drop.Drop(_temp, e);
            }
        }

        private void AssociatedObject_Drop(object sender, DragEventArgs e)
        {
            Drop(sender, e);
            Clear();
        }

        private void AssociatedObject_DragOver(object sender, DragEventArgs e)
        {
            if (AdornerType == null)
                return;
            if (AdornerVisual == null)
                AdornerVisual = AssociatedObject;

            Point point = e.GetPosition(AssociatedObject);
            UIElement visualHit = AssociatedObject.HitTest<UIElement>(point, x =>
            {
                if (GetIsHitTest(x) == false)
                    return false;
                if (_temp.GetContent() is IHitTestElementDrop drop)
                    return drop.IsHitTest(x, e);
                return true;
            });
            if (visualHit == null)
            {
                Clear();
                if (AssociatedObject != _temp)
                {
                    DragEnter(AssociatedObject, e);
                    DragLeave(_temp, e);
                }
                _temp = AssociatedObject;
            }
            else
            {
                if (visualHit != _temp)
                {
                    Clear();
                    DragLeave(_temp, e);
                    DragEnter(visualHit, e);
                }
                AddAdorner(visualHit);
                _temp = visualHit;
            }

            if (_temp.GetContent() is IHitTestElementDrag drag)
            {
                drag.DragOver(_temp, e);
            }
        }

        protected virtual void DragEnter(UIElement element, DragEventArgs e)
        {
            if (element.GetContent() is IHitTestElementDrag newDrag)
            {
                newDrag.DragEnter(element, e);
            }
        }

        protected virtual void DragLeave(UIElement element, DragEventArgs e)
        {
            if (_temp.GetContent() is IHitTestElementDrag oldDrag)
            {
                oldDrag.DragLeave(_temp, e);
            }
        }

        protected override void OnDetaching()
        {
            AssociatedObject.DragOver -= AssociatedObject_DragOver;
            AssociatedObject.Drop -= AssociatedObject_Drop;
            //AssociatedObject.DragEnter -= AssociatedObject_DragEnter;
            //AssociatedObject.DragLeave -= AssociatedObject_DragLeave;
            Clear();
        }

        protected override bool CheckAdorner(UIElement elment)
        {
            return elment.GetAdorner(x => x.GetType() == AdornerType) == null;
        }

        protected override Adorner GetAdorner(UIElement elment)
        {
            Adorner adorner = Activator.CreateInstance(AdornerType, elment) as Adorner;
            object data = elment.GetDataContext();
            if (data is IHitTestElementDrag drag)
            {
                if (drag.CanDrop(elment, null))
                    adorner = drag.GetDragAdorner(elment);
                else
                    adorner = Activator.CreateInstance(AdornerDropErrorType, elment) as Adorner;
            }

            if (data is IGetDropAdorner gdrag)
            {
                return gdrag.GetDropAdorner(elment);
            }
            return adorner;
        }
    }

    public interface IHitTestElementDrag : IHitTestElementDrop, IGetDragAdorner
    {
        void DragEnter(UIElement element, DragEventArgs e);
        void DragLeave(UIElement element, DragEventArgs e);
        void DragOver(UIElement element, DragEventArgs e);
    }

    public interface IGetDropAdorner
    {
        Adorner GetDropAdorner(UIElement element);
        void RemoveDropAdorner(UIElement element);
    }

    public interface IGetDragAdorner
    {
        Adorner GetDragAdorner(UIElement element);
        void RemoveDragAdorner(UIElement element);
    }

    public interface IHitTestElementDrop : IGetDropAdorner
    {
        bool CanDrop(UIElement element, DragEventArgs e);
        void Drop(UIElement element, DragEventArgs e);
        bool IsHitTest(UIElement element, DragEventArgs e);
    }
}



