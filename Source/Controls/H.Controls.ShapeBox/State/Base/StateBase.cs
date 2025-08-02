// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.ShapeBox.Shapes.Base;
using H.Extensions.Mvvm.ViewModels.Base;
using System.Windows.Input;

namespace H.Controls.ShapeBox.State.Base
{

    public abstract class StateBase : DisplayBindableBase, IViewState
    {
        public IView View { get; set; }
        public virtual void MouseLeave(object sender, MouseEventArgs e)
        {

        }

        public virtual void MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        public virtual void MouseMove(object sender, MouseEventArgs e)
        {

        }


        public virtual void MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        protected virtual void Clear()
        {

        }

        protected virtual void Sumit()
        {

        }

        protected virtual IStateShapeView GetStateShapeView()
        {
            if (this.View is IStateShapeView stateShapeView)
                return stateShapeView;
            return null;
        }

        protected FrameworkElement GetElementView()
        {
            if (this.View is FrameworkElement frameworkElement)
                return frameworkElement;
            return null;
        }

        protected void SetCursor(Cursor cursor)
        {
            var element = this.GetElementView();
            if (element == null)
                return;
            element.Cursor = cursor;
        }

        protected void DrawStateShape(params IShape[] shapes)
        {
            this.GetStateShapeView()?.DrawStateShape(shapes);
        }

        public virtual void Cancel()
        {
            this.Clear();
            this.DrawStateShape();
        }

        public virtual void Enter()
        {

        }

        public virtual void Exit()
        {
            this.Cancel();
            this.View = null;
        }

        public virtual void ScaleChanged()
        {

        }
    }
}
