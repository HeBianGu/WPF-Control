// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Windows.Input;

namespace H.Controls.ShapeBox.State
{

    public class DefaultRectState : IState
    {
        private readonly ShapeBox _box;
        public DefaultRectState(ShapeBox box)
        {
            this._box = box;
        }
        private Point? _mouseDown;
        public void MouseLeave(object sender, MouseEventArgs e)
        {
            this.Clear();
        }

        public void MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Clear();
        }

        public void MouseMove(object sender, MouseEventArgs e)
        {

        }


        public void MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        void Clear()
        {
            this._mouseDown = null;
        }
    }
}
