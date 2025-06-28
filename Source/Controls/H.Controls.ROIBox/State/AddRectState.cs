// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Windows.Input;

namespace H.Controls.ROIBox.State
{
    [Obsolete]
    public class AddRectState : IState
    {
        private readonly ROIBox _box;
        public AddRectState(ROIBox box)
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
            if (e.LeftButton != MouseButtonState.Pressed)
                return;
            var point = e.GetPosition(sender as FrameworkElement);
            this.UpdateRect(point);
        }

        public void MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton != MouseButton.Left)
                return;
            this._mouseDown = e.GetPosition(sender as FrameworkElement);
        }

        void UpdateRect(Point to)
        {
            if (this._mouseDown == null)
                return;
            this._box.Rect = new Rect(this._mouseDown.Value, to);
        }

        void Clear()
        {
            this._mouseDown = null;
        }
    }
}
