// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace H.Controls.Step
{
    public class Step : ListBox
    {
        public static ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(Step), "S.StepState.Default");

        static Step()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Step), new FrameworkPropertyMetadata(typeof(Step)));
        }

        public void Build()
        {
            List<StepItemPresenter> stepItems = new List<StepItemPresenter>();

            stepItems.Add(new StepItemPresenter() { DisplayName = "1", Message = "准备开始" });
            stepItems.Add(new StepItemPresenter() { DisplayName = "2", Message = "步骤一" });
            stepItems.Add(new StepItemPresenter() { DisplayName = "3", Message = "步骤二" });
            stepItems.Add(new StepItemPresenter() { DisplayName = "4", Message = "步骤三" });
            stepItems.Add(new StepItemPresenter() { DisplayName = "5", Message = "步骤四" });
            stepItems.Add(new StepItemPresenter() { DisplayName = "6", Message = "步骤五" });
            stepItems.Add(new StepItemPresenter() { DisplayName = "7", Message = "完成" });

            this.ItemsSource = stepItems;
        }


        public double LineWidth
        {
            get { return (double)GetValue(LineWidthProperty); }
            set { SetValue(LineWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LineWidthProperty =
            DependencyProperty.Register("LineWidth", typeof(double), typeof(Step), new FrameworkPropertyMetadata(100.0, (d, e) =>
             {
                 Step control = d as Step;

                 if (control == null) return;

                 if (e.OldValue is double o)
                 {

                 }

                 if (e.NewValue is double n)
                 {

                 }

             }));

    }

}
