
using H.Controls.Dock.Layout;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace H.Controls.Dock.Controls
{
    /// <summary>
    /// Implements a <see cref="LayoutAnchorablePaneControl"/> drop target
    /// on which other items (<see cref="LayoutAnchorable"/>) can be dropped.
    /// </summary>
    internal class AnchorablePaneDropTarget : DropTarget<LayoutAnchorablePaneControl>
    {
        #region fields

        private LayoutAnchorablePaneControl _targetPane;
        private int _tabIndex = -1;

        #endregion fields

        #region Constructors

        /// <summary>
        /// Class constructor from parameters without a specific tabindex as dock position.
        /// </summary>
        /// <param name="paneControl"></param>
        /// <param name="detectionRect"></param>
        /// <param name="type"></param>
        internal AnchorablePaneDropTarget(LayoutAnchorablePaneControl paneControl,
                                                                     Rect detectionRect,
                                                                     DropTargetType type)
            : base(paneControl, detectionRect, type)
        {
            _targetPane = paneControl;
        }

        /// <summary>
        /// Class constructor from parameters with a specific tabindex as dock position.
        /// This constructor can be used to drop an anchorable at a specific tab index.
        /// </summary>
        /// <param name="paneControl"></param>
        /// <param name="detectionRect"></param>
        /// <param name="type"></param>
        /// <param name="tabIndex"></param>
        internal AnchorablePaneDropTarget(LayoutAnchorablePaneControl paneControl,
                                          Rect detectionRect,
                                          DropTargetType type,
                                          int tabIndex)
            : base(paneControl, detectionRect, type)
        {
            _targetPane = paneControl;
            _tabIndex = tabIndex;
        }

        #endregion Constructors

        #region Overrides

        /// <summary>
        /// Method is invoked to complete a drag & drop operation with a (new) docking position
        /// by docking of the <paramref name="floatingWindow"/> into this drop target.
        /// </summary>
        /// <param name="floatingWindow"></param>
        protected override void Drop(LayoutAnchorableFloatingWindow floatingWindow)
        {
            ILayoutAnchorablePane targetModel = _targetPane.Model as ILayoutAnchorablePane;
            LayoutAnchorable anchorableActive = floatingWindow.Descendents().OfType<LayoutAnchorable>().FirstOrDefault();

            switch (this.Type)
            {
                case DropTargetType.AnchorablePaneDockBottom:

                    #region DropTargetType.AnchorablePaneDockBottom

                    {
                        ILayoutGroup parentModel = targetModel.Parent as ILayoutGroup;
                        ILayoutOrientableGroup parentModelOrientable = targetModel.Parent as ILayoutOrientableGroup;
                        int insertToIndex = parentModel.IndexOfChild(targetModel);

                        if (parentModelOrientable.Orientation != System.Windows.Controls.Orientation.Vertical &&
                            parentModel.ChildrenCount == 1)
                            parentModelOrientable.Orientation = System.Windows.Controls.Orientation.Vertical;

                        if (parentModelOrientable.Orientation == System.Windows.Controls.Orientation.Vertical)
                        {
                            LayoutAnchorablePaneGroup layoutAnchorablePaneGroup = floatingWindow.RootPanel;
                            if (layoutAnchorablePaneGroup != null &&
                                (layoutAnchorablePaneGroup.Children.Count == 1 ||
                                    layoutAnchorablePaneGroup.Orientation == System.Windows.Controls.Orientation.Vertical))
                            {
                                ILayoutAnchorablePane[] anchorablesToMove = layoutAnchorablePaneGroup.Children.ToArray();
                                for (int i = 0; i < anchorablesToMove.Length; i++)
                                    parentModel.InsertChildAt(insertToIndex + 1 + i, anchorablesToMove[i]);
                            }
                            else
                                parentModel.InsertChildAt(insertToIndex + 1, floatingWindow.RootPanel);
                        }
                        else
                        {
                            ILayoutPositionableElement targetModelAsPositionableElement = targetModel as ILayoutPositionableElement;
                            LayoutAnchorablePaneGroup newOrientedPanel = new LayoutAnchorablePaneGroup()
                            {
                                Orientation = System.Windows.Controls.Orientation.Vertical,
                                DockWidth = targetModelAsPositionableElement.DockWidth,
                                DockHeight = targetModelAsPositionableElement.DockHeight,
                            };

                            parentModel.InsertChildAt(insertToIndex, newOrientedPanel);
                            newOrientedPanel.Children.Add(targetModel);
                            newOrientedPanel.Children.Add(floatingWindow.RootPanel);
                        }
                    }
                    break;

                #endregion DropTargetType.AnchorablePaneDockBottom

                case DropTargetType.AnchorablePaneDockTop:

                    #region DropTargetType.AnchorablePaneDockTop

                    {
                        ILayoutGroup parentModel = targetModel.Parent as ILayoutGroup;
                        ILayoutOrientableGroup parentModelOrientable = targetModel.Parent as ILayoutOrientableGroup;
                        int insertToIndex = parentModel.IndexOfChild(targetModel);

                        if (parentModelOrientable.Orientation != System.Windows.Controls.Orientation.Vertical &&
                            parentModel.ChildrenCount == 1)
                            parentModelOrientable.Orientation = System.Windows.Controls.Orientation.Vertical;

                        if (parentModelOrientable.Orientation == System.Windows.Controls.Orientation.Vertical)
                        {
                            LayoutAnchorablePaneGroup layoutAnchorablePaneGroup = floatingWindow.RootPanel;
                            if (layoutAnchorablePaneGroup != null &&
                                (layoutAnchorablePaneGroup.Children.Count == 1 ||
                                    layoutAnchorablePaneGroup.Orientation == System.Windows.Controls.Orientation.Vertical))
                            {
                                ILayoutAnchorablePane[] anchorablesToMove = layoutAnchorablePaneGroup.Children.ToArray();
                                for (int i = 0; i < anchorablesToMove.Length; i++)
                                    parentModel.InsertChildAt(insertToIndex + i, anchorablesToMove[i]);
                            }
                            else
                                parentModel.InsertChildAt(insertToIndex, floatingWindow.RootPanel);
                        }
                        else
                        {
                            ILayoutPositionableElement targetModelAsPositionableElement = targetModel as ILayoutPositionableElement;
                            LayoutAnchorablePaneGroup newOrientedPanel = new LayoutAnchorablePaneGroup()
                            {
                                Orientation = System.Windows.Controls.Orientation.Vertical,
                                DockWidth = targetModelAsPositionableElement.DockWidth,
                                DockHeight = targetModelAsPositionableElement.DockHeight,
                            };

                            parentModel.InsertChildAt(insertToIndex, newOrientedPanel);
                            //the floating window must be added after the target modal as it could be raise a CollectGarbage call
                            newOrientedPanel.Children.Add(targetModel);
                            newOrientedPanel.Children.Insert(0, floatingWindow.RootPanel);
                        }
                    }
                    break;

                #endregion DropTargetType.AnchorablePaneDockTop

                case DropTargetType.AnchorablePaneDockLeft:

                    #region DropTargetType.AnchorablePaneDockLeft

                    {
                        ILayoutGroup parentModel = targetModel.Parent as ILayoutGroup;
                        ILayoutOrientableGroup parentModelOrientable = targetModel.Parent as ILayoutOrientableGroup;
                        int insertToIndex = parentModel.IndexOfChild(targetModel);

                        if (parentModelOrientable.Orientation != System.Windows.Controls.Orientation.Horizontal &&
                            parentModel.ChildrenCount == 1)
                            parentModelOrientable.Orientation = System.Windows.Controls.Orientation.Horizontal;

                        if (parentModelOrientable.Orientation == System.Windows.Controls.Orientation.Horizontal)
                        {
                            LayoutAnchorablePaneGroup layoutAnchorablePaneGroup = floatingWindow.RootPanel;
                            if (layoutAnchorablePaneGroup != null &&
                                (layoutAnchorablePaneGroup.Children.Count == 1 ||
                                    layoutAnchorablePaneGroup.Orientation == System.Windows.Controls.Orientation.Horizontal))
                            {
                                ILayoutAnchorablePane[] anchorablesToMove = layoutAnchorablePaneGroup.Children.ToArray();
                                for (int i = 0; i < anchorablesToMove.Length; i++)
                                    parentModel.InsertChildAt(insertToIndex + i, anchorablesToMove[i]);
                            }
                            else
                                parentModel.InsertChildAt(insertToIndex, floatingWindow.RootPanel);
                        }
                        else
                        {
                            ILayoutPositionableElement targetModelAsPositionableElement = targetModel as ILayoutPositionableElement;
                            LayoutAnchorablePaneGroup newOrientedPanel = new LayoutAnchorablePaneGroup()
                            {
                                Orientation = System.Windows.Controls.Orientation.Horizontal,
                                DockWidth = targetModelAsPositionableElement.DockWidth,
                                DockHeight = targetModelAsPositionableElement.DockHeight,
                            };

                            parentModel.InsertChildAt(insertToIndex, newOrientedPanel);
                            //the floating window must be added after the target modal as it could be raise a CollectGarbage call
                            newOrientedPanel.Children.Add(targetModel);
                            newOrientedPanel.Children.Insert(0, floatingWindow.RootPanel);
                        }
                    }
                    break;

                #endregion DropTargetType.AnchorablePaneDockLeft

                case DropTargetType.AnchorablePaneDockRight:

                    #region DropTargetType.AnchorablePaneDockRight

                    {
                        ILayoutGroup parentModel = targetModel.Parent as ILayoutGroup;
                        ILayoutOrientableGroup parentModelOrientable = targetModel.Parent as ILayoutOrientableGroup;
                        int insertToIndex = parentModel.IndexOfChild(targetModel);

                        if (parentModelOrientable.Orientation != System.Windows.Controls.Orientation.Horizontal &&
                            parentModel.ChildrenCount == 1)
                            parentModelOrientable.Orientation = System.Windows.Controls.Orientation.Horizontal;

                        if (parentModelOrientable.Orientation == System.Windows.Controls.Orientation.Horizontal)
                        {
                            LayoutAnchorablePaneGroup layoutAnchorablePaneGroup = floatingWindow.RootPanel;
                            if (layoutAnchorablePaneGroup != null &&
                                (layoutAnchorablePaneGroup.Children.Count == 1 ||
                                    layoutAnchorablePaneGroup.Orientation == System.Windows.Controls.Orientation.Horizontal))
                            {
                                ILayoutAnchorablePane[] anchorablesToMove = layoutAnchorablePaneGroup.Children.ToArray();
                                for (int i = 0; i < anchorablesToMove.Length; i++)
                                    parentModel.InsertChildAt(insertToIndex + 1 + i, anchorablesToMove[i]);
                            }
                            else
                                parentModel.InsertChildAt(insertToIndex + 1, floatingWindow.RootPanel);
                        }
                        else
                        {
                            ILayoutPositionableElement targetModelAsPositionableElement = targetModel as ILayoutPositionableElement;
                            LayoutAnchorablePaneGroup newOrientedPanel = new LayoutAnchorablePaneGroup()
                            {
                                Orientation = System.Windows.Controls.Orientation.Horizontal,
                                DockWidth = targetModelAsPositionableElement.DockWidth,
                                DockHeight = targetModelAsPositionableElement.DockHeight,
                            };

                            parentModel.InsertChildAt(insertToIndex, newOrientedPanel);
                            newOrientedPanel.Children.Add(targetModel);
                            newOrientedPanel.Children.Add(floatingWindow.RootPanel);
                        }
                    }
                    break;

                #endregion DropTargetType.AnchorablePaneDockRight

                case DropTargetType.AnchorablePaneDockInside:

                    #region DropTargetType.AnchorablePaneDockInside

                    {
                        LayoutAnchorablePane paneModel = targetModel as LayoutAnchorablePane;
                        LayoutAnchorablePaneGroup layoutAnchorablePaneGroup = floatingWindow.RootPanel;

                        int i = _tabIndex == -1 ? 0 : _tabIndex;
                        foreach (LayoutAnchorable anchorableToImport in
                            layoutAnchorablePaneGroup.Descendents().OfType<LayoutAnchorable>().ToArray())
                        {
                            paneModel.Children.Insert(i, anchorableToImport);
                            i++;
                        }
                    }
                    break;

                    #endregion DropTargetType.AnchorablePaneDockInside
            }

            anchorableActive.IsActive = true;

            base.Drop(floatingWindow);
        }

        /// <summary>
        /// Gets a <see cref="Geometry"/> that is used to highlight/preview the docking position
        /// of this drop target for a <paramref name="floatingWindowModel"/> being docked inside an
        /// <paramref name="overlayWindow"/>.
        /// </summary>
        /// <param name="overlayWindow"></param>
        /// <param name="floatingWindowModel"></param>
        /// <returns>The geometry of the preview/highlighting WPF figure path.</returns>
        public override Geometry GetPreviewPath(OverlayWindow overlayWindow,
                                                LayoutFloatingWindow floatingWindowModel)
        {
            switch (this.Type)
            {
                case DropTargetType.AnchorablePaneDockBottom:
                    {
                        Rect targetScreenRect = this.TargetElement.GetScreenArea();
                        targetScreenRect.Offset(-overlayWindow.Left, -overlayWindow.Top);

                        targetScreenRect.Offset(0.0, targetScreenRect.Height / 2.0);
                        targetScreenRect.Height /= 2.0;

                        return new RectangleGeometry(targetScreenRect);
                    }

                case DropTargetType.AnchorablePaneDockTop:
                    {
                        Rect targetScreenRect = this.TargetElement.GetScreenArea();
                        targetScreenRect.Offset(-overlayWindow.Left, -overlayWindow.Top);

                        targetScreenRect.Height /= 2.0;

                        return new RectangleGeometry(targetScreenRect);
                    }

                case DropTargetType.AnchorablePaneDockLeft:
                    {
                        Rect targetScreenRect = this.TargetElement.GetScreenArea();
                        targetScreenRect.Offset(-overlayWindow.Left, -overlayWindow.Top);

                        targetScreenRect.Width /= 2.0;

                        return new RectangleGeometry(targetScreenRect);
                    }

                case DropTargetType.AnchorablePaneDockRight:
                    {
                        Rect targetScreenRect = this.TargetElement.GetScreenArea();
                        targetScreenRect.Offset(-overlayWindow.Left, -overlayWindow.Top);

                        targetScreenRect.Offset(targetScreenRect.Width / 2.0, 0.0);
                        targetScreenRect.Width /= 2.0;

                        return new RectangleGeometry(targetScreenRect);
                    }

                case DropTargetType.AnchorablePaneDockInside:
                    {
                        Rect targetScreenRect = this.TargetElement.GetScreenArea();
                        targetScreenRect.Offset(-overlayWindow.Left, -overlayWindow.Top);

                        if (_tabIndex == -1)
                            return new RectangleGeometry(targetScreenRect);
                        else
                        {
                            Rect translatedDetectionRect = new Rect(this.DetectionRects[0].TopLeft, this.DetectionRects[0].BottomRight);
                            translatedDetectionRect.Offset(-overlayWindow.Left, -overlayWindow.Top);

                            PathFigure pathFigure = new PathFigure();
                            pathFigure.StartPoint = targetScreenRect.TopLeft;
                            pathFigure.Segments.Add(new LineSegment() { Point = new Point(targetScreenRect.Left, translatedDetectionRect.Top) });
                            pathFigure.Segments.Add(new LineSegment() { Point = translatedDetectionRect.TopLeft });
                            pathFigure.Segments.Add(new LineSegment() { Point = translatedDetectionRect.BottomLeft });
                            pathFigure.Segments.Add(new LineSegment() { Point = translatedDetectionRect.BottomRight });
                            pathFigure.Segments.Add(new LineSegment() { Point = translatedDetectionRect.TopRight });
                            pathFigure.Segments.Add(new LineSegment() { Point = new Point(targetScreenRect.Right, translatedDetectionRect.Top) });
                            pathFigure.Segments.Add(new LineSegment() { Point = targetScreenRect.TopRight });
                            pathFigure.IsClosed = true;
                            pathFigure.IsFilled = true;
                            pathFigure.Freeze();

                            return new PathGeometry(new PathFigure[] { pathFigure });
                        }
                    }
            }

            return null;
        }

        #endregion Overrides
    }
}