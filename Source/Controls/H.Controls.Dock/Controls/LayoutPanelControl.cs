
using H.Controls.Dock.Layout;
using System;
using System.Windows;
using System.Windows.Controls;

namespace H.Controls.Dock.Controls
{
    /// <summary>
    /// Implements a <see cref="Grid"/> based panel base class
    /// that hosts a <see cref="LayoutPanel"/> as its model.
    /// </summary>
    public class LayoutPanelControl : LayoutGridControl<ILayoutPanelElement>, ILayoutControl
    {
        #region fields

        private readonly LayoutPanel _model;

        #endregion fields

        #region Constructors

        internal LayoutPanelControl(LayoutPanel model)
            : base(model, model.Orientation)
        {
            _model = model;
        }

        #endregion Constructors

        #region Overrides

        protected override void OnFixChildrenDockLengths()
        {
            if (this.ActualWidth == 0.0 || this.ActualHeight == 0.0) return;

            #region Setup DockWidth/Height for children

            if (_model.Orientation == Orientation.Horizontal)
            {
                if (_model.ContainsChildOfType<LayoutDocumentPane, LayoutDocumentPaneGroup>())
                {
                    for (int i = 0; i < _model.Children.Count; i++)
                    {
                        if ((_model.Children[i] as ILayoutContainer) != null &&
                            ((_model.Children[i] as ILayoutContainer).IsOfType<LayoutDocumentPane, LayoutDocumentPaneGroup>() ||
                             (_model.Children[i] as ILayoutContainer).ContainsChildOfType<LayoutDocumentPane, LayoutDocumentPaneGroup>()))
                        {
                            // Keep set values (from XML for instance)
                            if (!(_model.Children[i] as ILayoutPositionableElement).DockWidth.IsStar) (_model.Children[i] as ILayoutPositionableElement).DockWidth = new GridLength(1.0, GridUnitType.Star);
                        }
                        else if ((_model.Children[i] as ILayoutPositionableElement) != null && (_model.Children[i] as ILayoutPositionableElement).DockWidth.IsStar)
                        {
                            ILayoutPositionableElementWithActualSize childPositionableModelWidthActualSize = _model.Children[i] as ILayoutPositionableElement as ILayoutPositionableElementWithActualSize;
                            double childDockMinWidth = (_model.Children[i] as ILayoutPositionableElement).CalculatedDockMinWidth();
                            double widthToSet = Math.Max(childPositionableModelWidthActualSize.ActualWidth, childDockMinWidth);

                            widthToSet = Math.Min(widthToSet, this.ActualWidth / 2.0);
                            widthToSet = Math.Max(widthToSet, childDockMinWidth);
                            (_model.Children[i] as ILayoutPositionableElement).DockWidth = new GridLength(widthToSet, GridUnitType.Pixel);
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < _model.Children.Count; i++)
                    {
                        ILayoutPositionableElement childPositionableModel = _model.Children[i] as ILayoutPositionableElement;
                        if (!childPositionableModel.DockWidth.IsStar)
                        {
                            // Keep set values (from XML for instance)
                            if (!childPositionableModel.DockWidth.IsStar) childPositionableModel.DockWidth = new GridLength(1.0, GridUnitType.Star);
                        }
                    }
                }
            }
            else // Vertical
            {
                if (_model.ContainsChildOfType<LayoutDocumentPane, LayoutDocumentPaneGroup>())
                {
                    for (int i = 0; i < _model.Children.Count; i++)
                    {
                        if (_model.Children[i] is ILayoutContainer childContainerModel &&
                            (childContainerModel.IsOfType<LayoutDocumentPane, LayoutDocumentPaneGroup>() ||
                             childContainerModel.ContainsChildOfType<LayoutDocumentPane, LayoutDocumentPaneGroup>()))
                        {
                            // Keep set values (from XML for instance)
                            if (!(_model.Children[i] as ILayoutPositionableElement).DockHeight.IsStar) (_model.Children[i] as ILayoutPositionableElement).DockHeight = new GridLength(1.0, GridUnitType.Star);
                        }
                        else if ((_model.Children[i] as ILayoutPositionableElement) != null && (_model.Children[i] as ILayoutPositionableElement).DockHeight.IsStar)
                        {
                            ILayoutPositionableElementWithActualSize childPositionableModelWidthActualSize = _model.Children[i] as ILayoutPositionableElement as ILayoutPositionableElementWithActualSize;
                            double childDockMinHeight = (_model.Children[i] as ILayoutPositionableElement).CalculatedDockMinHeight();
                            double heightToSet = Math.Max(childPositionableModelWidthActualSize.ActualHeight, childDockMinHeight);
                            heightToSet = Math.Min(heightToSet, this.ActualHeight / 2.0);
                            heightToSet = Math.Max(heightToSet, childDockMinHeight);
                            (_model.Children[i] as ILayoutPositionableElement).DockHeight = new GridLength(heightToSet, GridUnitType.Pixel);
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < _model.Children.Count; i++)
                    {
                        ILayoutPositionableElement childPositionableModel = _model.Children[i] as ILayoutPositionableElement;
                        if (!childPositionableModel.DockHeight.IsStar)
                        {
                            // Keep set values (from XML for instance)
                            if (!childPositionableModel.DockHeight.IsStar) childPositionableModel.DockHeight = new GridLength(1.0, GridUnitType.Star);
                        }
                    }
                }
            }

            #endregion Setup DockWidth/Height for children
        }

        #endregion Overrides
    }
}