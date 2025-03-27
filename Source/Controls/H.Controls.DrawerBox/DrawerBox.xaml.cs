// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Common.Transitionable;
using H.Extensions.Animations;
using H.Services.Common;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace H.Controls.DrawerBox
{

    [TemplatePart(Name = "PART_DrawerTemplate")]
    [TemplatePart(Name = "PART_ToggleTemplate")]
    public class DrawerBox : Control, ITransitionHostable
    {
        static DrawerBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DrawerBox), new FrameworkPropertyMetadata(typeof(DrawerBox)));
        }

        private Control _drawer;
        private Control _toggle;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this._drawer = this.Template.FindName("PART_DrawerTemplate", this) as Control;
            this._toggle = this.Template.FindName("PART_ToggleTemplate", this) as Control;
            this.Show();
        }


        public bool IsExpanded
        {
            get { return (bool)GetValue(IsExpandedProperty); }
            set { SetValue(IsExpandedProperty, value); }
        }

        public static readonly DependencyProperty IsExpandedProperty =
            DependencyProperty.Register("IsExpanded", typeof(bool), typeof(DrawerBox), new FrameworkPropertyMetadata(default(bool)));


        public ControlTemplate ToggleTemplate
        {
            get { return (ControlTemplate)GetValue(ToggleTemplateProperty); }
            set { SetValue(ToggleTemplateProperty, value); }
        }
        public static readonly DependencyProperty ToggleTemplateProperty =
            DependencyProperty.Register("ToggleTemplate", typeof(ControlTemplate), typeof(DrawerBox), new FrameworkPropertyMetadata(default(ControlTemplate)));


        public ControlTemplate DrawerTemplate
        {
            get { return (ControlTemplate)GetValue(DrawerTemplateProperty); }
            set { SetValue(DrawerTemplateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DrawerTemplateProperty =
            DependencyProperty.Register("DrawerTemplate", typeof(ControlTemplate), typeof(DrawerBox), new FrameworkPropertyMetadata(default(ControlTemplate)));
        public ITransitionable Transitionable { get; set; } = new OpacityTransitionable();

        public async void Show()
        {
            await this.Show(this._drawer);
            await this.Close(this._toggle);
            this.IsExpanded = true;
        }

        public async void Close()
        {
            await this.Show(this._toggle);
            await this.Close(this._drawer);
            this.IsExpanded = false;
        }
    }

}
