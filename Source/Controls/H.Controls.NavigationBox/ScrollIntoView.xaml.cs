// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Collections;
using System.Windows;
using System.Windows.Controls;

namespace H.Controls.NavigationBox
{
    /// <summary> 用于绑定数据的导航效果 </summary>
    [TemplatePart(Name = "PART_NAVIGATION", Type = typeof(ListBox))]
    [TemplatePart(Name = "PART_CONTAINT", Type = typeof(ListBox))]
    public partial class ScrollIntoView : ContentControl
    {
        public static ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(ScrollIntoView), "S.ScrollIntoView.Default");

        private ListBox _navigation;
        private ListBox _contain;

        static ScrollIntoView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ScrollIntoView), new FrameworkPropertyMetadata(typeof(ScrollIntoView)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this._navigation = this.Template.FindName("PART_NAVIGATION", this) as ListBox;


            this._contain = this.Template.FindName("PART_CONTAINT", this) as ListBox;

            this._navigation.SelectionChanged += (l, k) =>
              {
                  this._contain.ScrollIntoView(this._navigation.SelectedItem);
              };
        }

        public DataTemplate NavigationDataTemplate
        {
            get { return (DataTemplate)GetValue(NavigationDataTemplateProperty); }
            set { SetValue(NavigationDataTemplateProperty, value); }
        }


        public static readonly DependencyProperty NavigationDataTemplateProperty =
            DependencyProperty.Register("NavigationDataTemplate", typeof(DataTemplate), typeof(ScrollIntoView), new PropertyMetadata(default(DataTemplate), (d, e) =>
             {
                 ScrollIntoView control = d as ScrollIntoView;

                 if (control == null) return;

                 DataTemplate config = e.NewValue as DataTemplate;

             }));


        public DataTemplate ContainDataTemplate
        {
            get { return (DataTemplate)GetValue(ContainDataTemplateProperty); }
            set { SetValue(ContainDataTemplateProperty, value); }
        }


        public static readonly DependencyProperty ContainDataTemplateProperty =
            DependencyProperty.Register("ContainDataTemplate", typeof(DataTemplate), typeof(ScrollIntoView), new PropertyMetadata(default(DataTemplate), (d, e) =>
             {
                 ScrollIntoView control = d as ScrollIntoView;

                 if (control == null) return;

                 DataTemplate config = e.NewValue as DataTemplate;

             }));


        public IEnumerable Source
        {
            get { return (IEnumerable)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }


        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(IEnumerable), typeof(ScrollIntoView), new PropertyMetadata(default(IEnumerable), (d, e) =>
             {
                 ScrollIntoView control = d as ScrollIntoView;

                 if (control == null) return;

                 IEnumerable config = e.NewValue as IEnumerable;

             }));
    }

}
