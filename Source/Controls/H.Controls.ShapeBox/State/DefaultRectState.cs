// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Windows.Input;
using H.Controls.ShapeBox.State.Base;

namespace H.Controls.ShapeBox.State
{

    public class DefaultRectState : StateBase
    {
        private readonly ShapeBox _box;
        public DefaultRectState(ShapeBox box)
        {
            this._box = box;
        }
        private Point? _mouseDown;
        public override void MouseLeave(object sender, MouseEventArgs e)
        {
            this.Clear();
        }

        public override void MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Clear();
        }

        public override void MouseMove(object sender, MouseEventArgs e)
        {

        }


        public override void MouseDown(object sender, MouseButtonEventArgs e)
        {

        }
        protected override void Clear()
        {
            this._mouseDown = null;
        }
    }
}
