// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.ComponentModel.DataAnnotations;
using System.Windows.Input;
using H.Common.Attributes;
using H.Controls.ShapeBox.Shapes;
using H.Controls.ShapeBox.Shapes.Base;
using H.Controls.ShapeBox.State.Base;
using H.Extensions.FontIcon;
using H.Services.Message;

namespace H.Controls.ShapeBox.State
{
    public enum HitHandleType
    {
        None,
        TopLeft,
        TopRight,
        BottomLeft,
        BottomRight,
        Top,
        Bottom,
        Left,
        Right,
        Area
    }

    [Icon(FontIcons.Crop)]
    [Display(Name = "绘制ROI")]
    public class ROIRectState : PreviewShapeStateBase
    {
        private Point? _mouseDown;
        private HitHandleType HitHandleType = HitHandleType.None;
        private Rect _downRect;

        public ROIRectState()
        {
            this._rOIRectShape = this.GetShapeStyleSetting()?.Create<ROIRectShape>() ?? new ROIRectShape();
        }

        private ROIRectShape _rOIRectShape = new ROIRectShape();
        protected ROIRectShape ROIRectShape => this._rOIRectShape;

        public override IShapeStyleSetting GetShapeStyleSetting()
        {
            return ROIRectStateStyleSetting.Instance;
        }
        public override void MouseLeave(object sender, MouseEventArgs e)
        {
            this.Clear();
        }

        public override void MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Clear();
        }

        protected override IPreviewShape GetPreviewShape()
        {
            return this._rOIRectShape;
        }

        public override void MouseMove(object sender, MouseEventArgs e)
        {
            base.MouseMove(sender, e);
            var p = e.GetPosition(sender as FrameworkElement);
            var cursor = this.GetCursor(p);
            this.SetCursor(cursor);

            if (e.LeftButton != MouseButtonState.Pressed)
                return;
            this.UpdateRect(p);
        }

        private Cursor GetCursor(Point p)
        {
            if (this.HitPoint(this._rOIRectShape.Rect.TopLeft, p))
                return Cursors.SizeNWSE;

            if (this.HitPoint(this._rOIRectShape.Rect.BottomRight, p))
                return Cursors.SizeNWSE;

            if (this.HitPoint(this._rOIRectShape.Rect.TopRight, p))
                return Cursors.SizeNESW;

            if (this.HitPoint(this._rOIRectShape.Rect.BottomLeft, p))
                return Cursors.SizeNESW;
            var center = new Point(this._rOIRectShape.Rect.Left + this._rOIRectShape.Rect.Width / 2, this._rOIRectShape.Rect.Top + this._rOIRectShape.Rect.Height / 2);

            if (this.HitPoint(new Point(this._rOIRectShape.Rect.Left, center.Y), p))
                return Cursors.SizeWE;
            if (this.HitPoint(new Point(this._rOIRectShape.Rect.Right, center.Y), p))
                return Cursors.SizeWE;

            if (this.HitPoint(new Point(center.X, this._rOIRectShape.Rect.Top), p))
                return Cursors.SizeNS;
            if (this.HitPoint(new Point(center.X, this._rOIRectShape.Rect.Bottom), p))
                return Cursors.SizeNS;
            if (this._rOIRectShape.Rect.Contains(p))
                return Cursors.SizeAll;
            return Cursors.Cross;
        }

        public override void MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton != MouseButton.Left)
                return;
            e.Handled = true;
            var p = e.GetPosition(sender as FrameworkElement);
            this._mouseDown = p;
            this._downRect = this._rOIRectShape.Rect;
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
            var d = 0.5 * this._rOIRectShape.HandleLength / this.View.Scale;
            var v = targetPoint - position;
            return Math.Abs(v.X) < d && Math.Abs(v.Y) < d;
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
            targetRect.Intersect(new Rect(0, 0, this.View.Size.Width, this.View.Size.Height));
            this._rOIRectShape.Rect = targetRect;
            this.OnRectChanged();
        }

        protected virtual void OnRectChanged()
        {
            this.DrawStateShape(this._rOIRectShape);
        }

        protected override void Clear()
        {
            this._mouseDown = null;
            this.HitHandleType = HitHandleType.None;
        }

        public override void Exit()
        {
            base.Exit();
            this.Clear();
            this.ROIRectShape.Rect = Rect.Empty;
            this.DrawStateShape();
        }

        public override void Enter()
        {
            this.GetShapeStyleSetting()?.SaveTo(this._rOIRectShape);
            this.DrawStateShape(this._rOIRectShape);
        }

        public override void ScaleChanged()
        {
            this.DrawStateShape(this._rOIRectShape);
        }

        public override async void ShowEdit()
        {
            await IocMessage.Form?.ShowTabEdit(this._rOIRectShape, x => x.Title = this.Name);
            this.DrawStateShape(this._rOIRectShape);
            this.GetShapeStyleSetting()?.LoadBy(this._rOIRectShape);
        }
    }
}
