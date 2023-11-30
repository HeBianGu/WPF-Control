// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using System.Collections.ObjectModel;
#if NET
#endif 
using System.Windows;
using System.Windows.Input;

namespace H.Extensions.Behvaiors
{
    public class MouseOverHitTestAdornerBehavior : HitTestAdornerBehavior
    {
        public ObservableCollection<object> Parameters { get; } = new ObservableCollection<object>();

        public static bool GetIsMouseOver(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsMouseOverProperty);
        }

        public static void SetIsMouseOver(DependencyObject obj, bool value)
        {
            obj.SetValue(IsMouseOverProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty IsMouseOverProperty =
            DependencyProperty.RegisterAttached("IsMouseOver", typeof(bool), typeof(MouseOverHitTestAdornerBehavior), new PropertyMetadata(default(bool), OnIsMouseOverChanged));

        static public void OnIsMouseOverChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d as DependencyObject;

            bool n = (bool)e.NewValue;

            bool o = (bool)e.OldValue;
        }

        protected override void OnAttached()
        {
            AssociatedObject.MouseMove += AssociatedObject_MouseMove;
        }

        private void AssociatedObject_MouseMove(object sender, MouseEventArgs e)
        {
            if (AdornerType == null)
                return;
            if (AdornerVisual == null)
                AdornerVisual = AssociatedObject;
            Point point = e.GetPosition(AssociatedObject);
            var visualHit = AssociatedObject.HitTest<UIElement>(point, x => GetIsHitTest(x));
            if (visualHit == null)
            {
                Clear();
            }
            else
            {
                if (visualHit != _temp)
                    Clear();
                AddAdorner(visualHit);
                SetIsMouseOver(visualHit, true);
            }
            _temp = visualHit;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.MouseMove -= AssociatedObject_MouseMove;
            Clear();
        }

        protected override void Clear()
        {
            if (_temp != null)
                SetIsMouseOver(_temp, false);
            base.Clear();
        }
    }
}



