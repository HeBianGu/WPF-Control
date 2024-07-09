// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using H.Mvvm;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace H.Controls.Panel
{
    public abstract class PanelBase : System.Windows.Controls.Panel
    {
        public PanelBase()
        {
            //  Do ：下一项
            {
                CommandBinding binding = new CommandBinding(Commands.Next);

                this.CommandBindings.Add(binding);

                binding.Executed += (l, k) => this.Next();
            }

            //  Do ：上一项
            {
                CommandBinding binding = new CommandBinding(Commands.Prev);

                this.CommandBindings.Add(binding);

                binding.Executed += (l, k) => this.Previous();
            }

            this.SizeChanged += (l, k) =>
              {
                  this.InvalidateArrange();

              };

        }

        protected virtual void Next()
        {
            this.StartIndex = this.StartIndex + 1;

            this.StartIndex = this.StartIndex > this.Children.Count - 1 ? 0 : this.StartIndex;

            //System.Diagnostics.Debug.WriteLine(this.StartIndex);

            //  Do ：刷新
            this.InvalidateArrange();
        }

        protected virtual void Previous()
        {
            this.StartIndex = this.StartIndex - 1;

            this.StartIndex = this.StartIndex < 0 ? this.Children.Count - 1 : this.StartIndex;

            //  Do ：刷新
            this.InvalidateArrange();
        }

        public int StartIndex
        {
            get { return (int)GetValue(StartIndexProperty); }
            set { SetValue(StartIndexProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StartIndexProperty =
            DependencyProperty.Register("StartIndex", typeof(int), typeof(PanelBase), new PropertyMetadata(0, (d, e) =>
            {
                PanelBase control = d as PanelBase;

                if (control == null) return;

                //int config = e.NewValue as int;

                //  Do ：刷新
                control.InvalidateArrange();

            }));


        public int DisplayCount
        {
            get { return (int)GetValue(DisplayCountProperty); }
            set { SetValue(DisplayCountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DisplayCountProperty =
            DependencyProperty.Register("DisplayCount", typeof(int), typeof(PanelBase), new PropertyMetadata(-1, (d, e) =>
             {
                 PanelBase control = d as PanelBase;

                 if (control == null) return;

                 //int config = e.NewValue as int;

                 control.InvalidateArrange();

             }));


        protected List<UIElement> GetChildren()
        {
            int count = this.DisplayCount < 0 || this.DisplayCount > this.Children.Count ? this.Children.Count : this.DisplayCount;

            //  Do ：重新排序
            UIElement[] orders = new UIElement[this.Children.Count];

            for (int i = 0; i < this.Children.Count; i++)
            {
                int index = this.StartIndex + i;

                index = index > this.Children.Count - 1 ? index - this.Children.Count : index;

                orders[i] = this.Children[index];

                orders[i].Arrange(new Rect(new Point(0, 0), orders[i].DesiredSize));

                //orders[i].Arrange(new Rect(0, 0, 0, 0));

                //orders[i].Opacity = 0.0;
            }

            List<UIElement> result = orders?.ToList()?.Take(count)?.ToList();


            //result.ForEach(l => l.Opacity = 1);

            return result;
        }

    }
}
