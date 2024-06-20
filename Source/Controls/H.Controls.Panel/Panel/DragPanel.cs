// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;
using System.Collections;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace H.Controls.Panel
{
    public partial class DragPanel : System.Windows.Controls.Panel
    {
        #region private vars
        private Size _calculatedSize;
        private bool _isNotFirstArrange = false;
        private int columns, rows;
        #endregion
        static DragPanel()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DragPanel), new FrameworkPropertyMetadata(typeof(DragPanel)));
        }

        public DragPanel() : base()
        {
            this.AddHandler(Mouse.MouseMoveEvent, new MouseEventHandler(OnMouseMove), false);
            this.MouseLeftButtonUp += OnMouseUp;
            this.LostMouseCapture += OnLostMouseCapture;
        }

        private UIElement GetChildThatHasMouseOver()
        {
            return GetParent(Mouse.DirectlyOver as DependencyObject, (ve) => this.Children.Contains(ve as UIElement)) as UIElement;
        }

        private Point GetItemVisualPoint(UIElement element)
        {
            TransformGroup group = (TransformGroup)element.RenderTransform;
            TranslateTransform trans = (TranslateTransform)group.Children[0];

            return new Point(trans.X, trans.Y);
        }

        private int GetIndexFromPoint(double x, double y)
        {
            int columnIndex = (int)Math.Truncate(x / this.itemContainterWidth);
            int rowIndex = (int)Math.Truncate(y / this.itemContainterHeight);
            return columns * rowIndex + columnIndex;
        }

        private int GetIndexFromPoint(Point p)
        {
            return GetIndexFromPoint(p.X, p.Y);
        }

        #region dependency properties		
        public static readonly DependencyProperty ItemsWidthProperty = DependencyProperty.Register(
                  "ItemsWidth",
                  typeof(double),
                  typeof(DragPanel),
                  new FrameworkPropertyMetadata(150d));

        public static readonly DependencyProperty ItemsHeightProperty = DependencyProperty.Register(
                  "ItemsHeight",
                  typeof(double),
                  typeof(DragPanel),
                  new FrameworkPropertyMetadata(60d));

        public static readonly DependencyProperty ItemSeparationProperty = DependencyProperty.Register(
                 "ItemSeparation",
                 typeof(Thickness),
                 typeof(DragPanel),
                 new FrameworkPropertyMetadata());

        // Using a DependencyProperty as the backing store for AnimationMilliseconds.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AnimationMillisecondsProperty = DependencyProperty.Register("AnimationMilliseconds", typeof(int), typeof(DragPanel), new FrameworkPropertyMetadata(200));

        public static readonly DependencyProperty SwapCommandProperty = DependencyProperty.Register(
                  "SwapCommand",
                  typeof(ICommand),
                  typeof(DragPanel),
                  new FrameworkPropertyMetadata(null));

        #endregion

        #region properties
        public double ItemsWidth
        {
            get { return (double)GetValue(ItemsWidthProperty); }
            set { SetValue(ItemsWidthProperty, value); }
        }
        public double ItemsHeight
        {
            get { return (double)GetValue(ItemsHeightProperty); }
            set { SetValue(ItemsHeightProperty, value); }
        }
        public Thickness ItemSeparation
        {
            get { return (Thickness)this.GetValue(ItemSeparationProperty); }
            set { this.SetValue(ItemSeparationProperty, value); }
        }
        public int AnimationMilliseconds
        {
            get { return (int)GetValue(AnimationMillisecondsProperty); }
            set { SetValue(AnimationMillisecondsProperty, value); }
        }
        private double itemContainterHeight
        {
            get { return this.ItemSeparation.Top + this.ItemsHeight + this.ItemSeparation.Bottom; }
        }
        private double itemContainterWidth
        {
            get { return this.ItemSeparation.Left + this.ItemsWidth + this.ItemSeparation.Right; }
        }
        public ICommand SwapCommand
        {
            get { return (ICommand)GetValue(SwapCommandProperty); }
            set { SetValue(SwapCommandProperty, value); }
        }
        #endregion

        #region transformation things		
        private void AnimateAll()
        {
            //Apply exactly the same algorithm, but instide of Arrange a call AnimateTo method
            double colPosition = 0;
            double rowPosition = 0;
            foreach (UIElement child in this.Children)
            {
                if (child != this._draggedElement)
                    AnimateTo(child, colPosition + this.ItemSeparation.Left, rowPosition + this.ItemSeparation.Top, _isNotFirstArrange ? this.AnimationMilliseconds : 0);
                //drag will locate dragged element
                colPosition += this.itemContainterWidth;
                if (colPosition + 1 > _calculatedSize.Width)
                {
                    colPosition = 0;
                    rowPosition += this.itemContainterHeight;
                }
            }
        }

        private void AnimateTo(UIElement child, double x, double y, int duration)
        {
            TransformGroup group = (TransformGroup)child.RenderTransform;
            TranslateTransform trans = (TranslateTransform)group.Children.First((groupElement) => groupElement is TranslateTransform);
            trans.BeginAnimation(TranslateTransform.XProperty, MakeAnimation(x, duration));
            trans.BeginAnimation(TranslateTransform.YProperty, MakeAnimation(y, duration));
        }

        private DoubleAnimation MakeAnimation(double to, int duration)
        {
            DoubleAnimation anim = new DoubleAnimation(to, TimeSpan.FromMilliseconds(duration));
            anim.AccelerationRatio = 0.2;
            anim.DecelerationRatio = 0.7;
            return anim;
        }
        #endregion

        #region measure
        protected override Size MeasureOverride(Size availableSize)
        {
            Size itemContainerSize = new Size(this.itemContainterWidth, this.itemContainterHeight);
            int count = 0;  //for not call it again
            foreach (UIElement child in this.Children)
            {
                child.Measure(itemContainerSize);
                count++;
            }
            if (availableSize.Width < this.itemContainterWidth)
                _calculatedSize = new Size(this.itemContainterWidth, count * this.itemContainterHeight);  //the size of nX1
            else
            {
                columns = (int)Math.Truncate(availableSize.Width / this.itemContainterWidth);
                rows = count / columns;
                if (count % columns != 0)
                    rows++;
                _calculatedSize = new Size(columns * this.itemContainterWidth, rows * this.itemContainterHeight);
            }
            return _calculatedSize;
        }
        #endregion

        #region arrange
        protected override Size ArrangeOverride(Size finalSize)
        {
            Size _finalItemSize = new Size(this.ItemsWidth, this.ItemsHeight);
            //if is animated then arrange elements to 0,0, and then put them on its location using the transform
            foreach (UIElement child in this.InternalChildren)
            {
                // If this is the first time we've seen this child, add our transforms
                if (child.RenderTransform as TransformGroup == null)
                {
                    child.RenderTransformOrigin = new Point(0.5, 0.5);
                    TransformGroup group = new TransformGroup();
                    child.RenderTransform = group;
                    group.Children.Add(new TranslateTransform());
                }
                //locate all children in 0,0 point//TODO: use infinity and then scale each element to items size
                child.Arrange(new Rect(new Point(0, 0), _finalItemSize));       //when use transformations change to childs.DesireSize
            }
            AnimateAll();

            if (!_isNotFirstArrange)
                _isNotFirstArrange = true;

            return _calculatedSize;
        }
        #endregion

        #region Static
        //this can be an extension method
        public static DependencyObject GetParent(DependencyObject o, Func<DependencyObject, bool> matchFunction)
        {
            DependencyObject t = o;
            do
            {
                t = VisualTreeHelper.GetParent(t);
            } while (t != null && !matchFunction.Invoke(t));
            return t;
        }
        #endregion

        //TODO: Add IsEditing property
        //TODO: Add Scale transform to items for fill items area
    }

    public partial class DragPanel
    {
        #region const drag
        private const double mouseDif = 2d;
        private const int mouseTimeDif = 25;
        #endregion

        #region private
        private UIElement __draggedElement;

        public UIElement _draggedElement
        {
            get { return __draggedElement; }
            set
            {
                __draggedElement = value;
            }
        }

        private int _draggedIndex;
        private bool _firstScrollRequest = true;
        private ScrollViewer _scrollContainer;

        private ScrollViewer scrollViewer
        {
            get
            {
                if (_firstScrollRequest && _scrollContainer == null)
                {
                    _firstScrollRequest = false;
                    _scrollContainer = (ScrollViewer)GetParent(this, (ve) => ve is ScrollViewer);
                }
                return _scrollContainer;
            }
        }
        #endregion

        #region private drag
        private double _lastMousePosX;
        private double _lastMousePosY;
        private int _lastMouseMoveTime;
        private double _x;
        private double _y;
        private Rect _rectOnDrag;
        #endregion


        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && this._draggedElement == null && !this.IsMouseCaptured)
                StartDrag(e);
            else if (this._draggedElement != null)
                OnDragOver(e);
        }

        private void OnDragOver(MouseEventArgs e)
        {
            Point mousePos = Mouse.GetPosition(this);
            double difX = mousePos.X - _lastMousePosX;
            double difY = mousePos.Y - _lastMousePosY;

            int timeDif = e.Timestamp - _lastMouseMoveTime;
            if ((Math.Abs(difX) > mouseDif || Math.Abs(difY) > mouseDif) && timeDif > mouseTimeDif)
            {
                //this lines is for keepn draged item inside control bounds
                DoScroll();

                if (_x + difX < _rectOnDrag.Location.X)
                    _x = 0;
                else if (this.ItemsWidth + _x + difX > _rectOnDrag.Location.X + _rectOnDrag.Width)
                    _x = _rectOnDrag.Location.X + _rectOnDrag.Width - this.ItemsWidth;
                else if (mousePos.X > _rectOnDrag.Location.X && mousePos.X < _rectOnDrag.Location.X + _rectOnDrag.Width)
                    _x += difX;
                if (_y + difY < _rectOnDrag.Location.Y)
                    _y = 0;
                else if (this.ItemsHeight + _y + difY > _rectOnDrag.Location.Y + _rectOnDrag.Height)
                    _y = _rectOnDrag.Location.Y + _rectOnDrag.Height - this.ItemsHeight;
                else if (mousePos.Y > _rectOnDrag.Location.Y && mousePos.Y < _rectOnDrag.Location.Y + _rectOnDrag.Height)
                    _y += difY;
                //lines ends

                AnimateTo(this._draggedElement, _x, _y, 0);
                _lastMousePosX = mousePos.X;
                _lastMousePosY = mousePos.Y;
                _lastMouseMoveTime = e.Timestamp;
                SwapElement(_x + this.ItemsWidth / 2, _y + this.ItemsHeight / 2);
            }
        }

        private void StartDrag(MouseEventArgs e)
        {
            Point mousePos = Mouse.GetPosition(this);
            this._draggedElement = GetChildThatHasMouseOver();
            if (this._draggedElement == null)
                return;
            _draggedIndex = this.Children.IndexOf(this._draggedElement);
            _rectOnDrag = VisualTreeHelper.GetDescendantBounds(this);
            Point p = GetItemVisualPoint(this._draggedElement);
            _x = p.X;
            _y = p.Y;
            SetZIndex(this._draggedElement, 1000);
            _lastMousePosX = mousePos.X;
            _lastMousePosY = mousePos.Y;
            _lastMouseMoveTime = e.Timestamp;
            this.InvalidateArrange();
            e.Handled = true;
            this.CaptureMouse();
        }

        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            if (this.IsMouseCaptured)
                ReleaseMouseCapture();
        }

        private void SwapElement(double x, double y)
        {
            int index = GetIndexFromPoint(x, y);

            if (index == _draggedIndex || index < 0) return;

            if (index >= this.Children.Count)
                index = this.Children.Count - 1;

            int[] parameter = new int[] { _draggedIndex, index };

            if (this.SwapCommand != null && this.SwapCommand.CanExecute(parameter))
            {
                this.SwapCommand.Execute(parameter);

                this._draggedElement = this.Children[index];

                //this is bcause after changing the collection the element is other			
                FillNewDraggedChild(this._draggedElement);

                _draggedIndex = index;
            }
            else
            {
                //  Do ：设置更新ListBox数据
                if (this.AutoSwapItems(_draggedIndex, index))
                {
                    this._draggedElement = this.Children[index];

                    //this is bcause after changing the collection the element is other			
                    FillNewDraggedChild(this._draggedElement);

                    _draggedIndex = index;
                }
            }

            this.InvalidateArrange();
        }

        private bool AutoSwapItems(int oldIndex, int newIndex)
        {
            ItemsControl list = ItemsControl.GetItemsOwner(this);

            if (list == null) return false;

            object elementSource = list.Items[newIndex];

            object dragged = list.Items[oldIndex];

            if (list.ItemsSource is IList l)
            {
                l.Remove(dragged);
                l.Insert(newIndex, dragged);
            }
            else
            {
                list.Items.Remove(dragged);
                list.Items.Insert(newIndex, dragged);
            }


            //if (oldIndex > newIndex)
            //{
            //    list.Items.Remove(dragged);
            //    list.Items.Insert(newIndex, dragged);
            //}
            //else
            //{
            //    list.Items.Remove(dragged);
            //    list.Items.Insert(newIndex, dragged);
            //}

            return true;

        }

        private void FillNewDraggedChild(UIElement child)
        {
            if (child.RenderTransform as TransformGroup == null)
            {
                child.RenderTransformOrigin = new Point(0.5, 0.5);
                TransformGroup group = new TransformGroup();
                child.RenderTransform = group;
                group.Children.Add(new TranslateTransform());
            }
            SetZIndex(child, 1000);
            AnimateTo(child, _x, _y, 0);            //need relocate the element
        }

        private void OnLostMouseCapture(object sender, MouseEventArgs e)
        {
            FinishDrag();
        }

        private void FinishDrag()
        {
            if (this._draggedElement != null)
            {
                SetZIndex(this._draggedElement, 0);
                this._draggedElement = null;
                this.InvalidateArrange();
            }
        }

        private void DoScroll()
        {
            if (this.scrollViewer != null)
            {
                Point position = Mouse.GetPosition(this.scrollViewer);
                double scrollMargin = Math.Min(this.scrollViewer.FontSize * 2, this.scrollViewer.ActualHeight / 2);

                if (position.X >= this.scrollViewer.ActualWidth - scrollMargin &&
                    this.scrollViewer.HorizontalOffset < this.scrollViewer.ExtentWidth - this.scrollViewer.ViewportWidth)
                {
                    this.scrollViewer.LineRight();
                }
                else if (position.X < scrollMargin && this.scrollViewer.HorizontalOffset > 0)
                {
                    this.scrollViewer.LineLeft();
                }
                else if (position.Y >= this.scrollViewer.ActualHeight - scrollMargin &&
                    this.scrollViewer.VerticalOffset < this.scrollViewer.ExtentHeight - this.scrollViewer.ViewportHeight)
                {
                    this.scrollViewer.LineDown();
                }
                else if (position.Y < scrollMargin && this.scrollViewer.VerticalOffset > 0)
                {
                    this.scrollViewer.LineUp();
                }
            }
        }
    }

}
