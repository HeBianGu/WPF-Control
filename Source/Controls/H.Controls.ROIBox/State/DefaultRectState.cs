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

    public class DefaultRectState : IState
    {
        private readonly ROIBox _box;
        public DefaultRectState(ROIBox box)
        {
            this._box = box;
        }
        private Point? _mouseDown;
        private HitHandleType HitHandleType = HitHandleType.None;
        private Rect _downRect;
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
            var p = e.GetPosition(sender as FrameworkElement);
            var cursor = this.GetCursor(p);
            this._box.Cursor = cursor;

            if (e.LeftButton != MouseButtonState.Pressed)
                return;
            this.UpdateRect(p);
        }

        private Cursor GetCursor(Point p)
        {
            if (this.HitPoint(this._box.Rect.TopLeft, p))
                return Cursors.SizeNWSE;

            if (this.HitPoint(this._box.Rect.BottomRight, p))
                return Cursors.SizeNWSE;

            if (this.HitPoint(this._box.Rect.TopRight, p))
                return Cursors.SizeNESW;

            if (this.HitPoint(this._box.Rect.BottomLeft, p))
                return Cursors.SizeNESW;
            var center = new Point(this._box.Rect.Left + this._box.Rect.Width / 2, this._box.Rect.Top + this._box.Rect.Height / 2);

            if (this.HitPoint(new Point(this._box.Rect.Left, center.Y), p))
                return Cursors.SizeWE;
            if (this.HitPoint(new Point(this._box.Rect.Right, center.Y), p))
                return Cursors.SizeWE;

            if (this.HitPoint(new Point(center.X, this._box.Rect.Top), p))
                return Cursors.SizeNS;
            if (this.HitPoint(new Point(center.X, this._box.Rect.Bottom), p))
                return Cursors.SizeNS;
            if (this._box.Rect.Contains(p))
                return Cursors.SizeAll;
            return Cursors.Cross;
        }

        public void MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton != MouseButton.Left)
                return;
            var p = e.GetPosition(sender as FrameworkElement);
            this._mouseDown = p;
            this._downRect = this._box.Rect;
            if (this.HitPoint(this._downRect.TopLeft, p))
            {
                this.HitHandleType = HitHandleType.TopLeft;
                return;
            }

            if (this.HitPoint(this._downRect.BottomRight, p))
            {
                this.HitHandleType = HitHandleType.BottomRight;
                return;
            }

            if (this.HitPoint(this._downRect.TopRight, p))
            {
                this.HitHandleType = HitHandleType.TopRight;
                return;
            }

            if (this.HitPoint(this._downRect.BottomLeft, p))
            {
                this.HitHandleType = HitHandleType.BottomLeft;
                return;
            }

            var center = new Point(this._downRect.Left + this._downRect.Width / 2, this._downRect.Top + this._downRect.Height / 2);

            if (this.HitPoint(new Point(this._downRect.Left, center.Y), p))
            {
                this.HitHandleType = HitHandleType.Left;
                return;
            }
            if (this.HitPoint(new Point(this._downRect.Right, center.Y), p))
            {
                this.HitHandleType = HitHandleType.Right;
                return;
            }

            if (this.HitPoint(new Point(center.X, this._downRect.Top), p))
            {
                this.HitHandleType = HitHandleType.Top;
                return;
            }

            if (this.HitPoint(new Point(center.X, this._downRect.Bottom), p))
            {
                this.HitHandleType = HitHandleType.Bottom;
                return;
            }

            if (this._downRect.Contains(p))
            {
                this.HitHandleType = HitHandleType.Area;
                return;
            }
        }

        bool HitPoint(Point targetPoint, Point position)
        {
            var v = targetPoint - position;
            return Math.Abs(v.X) < this._box.HandleLength / 2 && Math.Abs(v.Y) < this._box.HandleLength;
        }

        void UpdateRect(Point to)
        {
            if (this._mouseDown == null)
                return;
            var targetRect = this._downRect;
            if (this.HitHandleType == HitHandleType.Area)
            {
                Rect rect = this._downRect;
                rect.Offset(to.X - this._mouseDown.Value.X, to.Y - this._mouseDown.Value.Y);
                targetRect = rect;
            }
            else if (this.HitHandleType == HitHandleType.TopLeft)
            {
                to.X = Math.Min(to.X, this._downRect.Right);
                to.Y = Math.Min(to.Y, this._downRect.Bottom);
                targetRect = new Rect(to, this._downRect.BottomRight);
            }
            else if (this.HitHandleType == HitHandleType.BottomRight)
            {
                to.X = Math.Max(to.X, this._downRect.Left);
                to.Y = Math.Max(to.Y, this._downRect.Top);
                targetRect = new Rect(this._downRect.TopLeft, to);
            }
            else if (this.HitHandleType == HitHandleType.TopRight)
            {
                to.X = Math.Max(to.X, this._downRect.Left);
                to.Y = Math.Min(to.Y, this._downRect.Bottom);

                var tl = new Point(this._downRect.Left, to.Y);
                var br = new Point(to.X, this._downRect.Bottom);

                targetRect = new Rect(tl, br);
            }
            else if (this.HitHandleType == HitHandleType.BottomLeft)
            {
                to.X = Math.Min(to.X, this._downRect.Right);
                to.Y = Math.Max(to.Y, this._downRect.Top);

                var tl = new Point(to.X, this._downRect.Top);
                var br = new Point(this._downRect.Right, to.Y);
                targetRect = new Rect(tl, br);
            }
            else if (this.HitHandleType == HitHandleType.Left)
            {
                to.X = Math.Min(to.X, this._downRect.Right);
                to.Y = this._downRect.Top;
                targetRect = new Rect(to, this._downRect.BottomRight);
            }
            else if (this.HitHandleType == HitHandleType.Right)
            {
                to.X = Math.Max(to.X, this._downRect.Left);
                to.Y = this._downRect.Bottom;
                targetRect = new Rect(this._downRect.TopLeft, to);
            }
            else if (this.HitHandleType == HitHandleType.Top)
            {
                to.X = this._downRect.Left;
                to.Y = Math.Min(to.Y, this._downRect.Bottom);
                targetRect = new Rect(to, this._downRect.BottomRight);
            }
            else if (this.HitHandleType == HitHandleType.Bottom)
            {
                to.X = this._downRect.Right;
                to.Y = Math.Max(to.Y, this._downRect.Top);
                targetRect = new Rect(this._downRect.TopLeft, to);
            }
            else
            {
                targetRect = new Rect(this._mouseDown.Value, to);
            }
            targetRect.Intersect(new Rect(0, 0, this._box.ImageSource.Width, this._box.ImageSource.Height));
            this._box.Rect = targetRect;
        }

        void Clear()
        {
            this._mouseDown = null;
            this.HitHandleType = HitHandleType.None;
        }
    }
}
