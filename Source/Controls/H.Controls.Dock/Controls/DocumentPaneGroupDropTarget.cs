
using H.Controls.Dock.Layout;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace H.Controls.Dock.Controls
{
    /// <summary>
    /// Implements a <see cref="LayoutDocumentPaneGroupControl"/> drop target
    /// on which other items (<see cref="LayoutDocument"/>) can be dropped.
    /// </summary>
    internal class DocumentPaneGroupDropTarget : DropTarget<LayoutDocumentPaneGroupControl>
    {
        #region fields

        private LayoutDocumentPaneGroupControl _targetPane;

        #endregion fields

        #region Constructors

        /// <summary>
        /// Class contructor
        /// </summary>
        /// <param name="paneControl"></param>
        /// <param name="detectionRect"></param>
        /// <param name="type"></param>
        internal DocumentPaneGroupDropTarget(LayoutDocumentPaneGroupControl paneControl,
                                             Rect detectionRect,
                                             DropTargetType type)
            : base(paneControl, detectionRect, type)
        {
            _targetPane = paneControl;
        }

        #endregion Constructors

        #region Overrides

        /// <summary>
        /// Method is invoked to complete a drag & drop operation with a (new) docking position
        /// by docking of the LayoutDocument <paramref name="floatingWindow"/> into this drop target.
        /// </summary>
        /// <param name="floatingWindow"></param>
        protected override void Drop(LayoutDocumentFloatingWindow floatingWindow)
        {
            ILayoutPane targetModel = _targetPane.Model as ILayoutPane;

            switch (this.Type)
            {
                case DropTargetType.DocumentPaneGroupDockInside:

                    #region DropTargetType.DocumentPaneGroupDockInside

                    {
                        LayoutDocumentPaneGroup paneGroupModel = targetModel as LayoutDocumentPaneGroup;
                        LayoutDocumentPaneGroup paneModel = paneGroupModel;
                        LayoutDocumentPaneGroup sourceModel = floatingWindow.RootPanel;

                        paneModel.Children.Insert(0, sourceModel);
                    }
                    break;

                    #endregion DropTargetType.DocumentPaneGroupDockInside
            }

            base.Drop(floatingWindow);
        }

        /// <summary>
        /// Method is invoked to complete a drag & drop operation with a (new) docking position
        /// by docking of the LayoutAnchorable <paramref name="floatingWindow"/> into this drop target.
        /// </summary>
        /// <param name="floatingWindow"></param>
        protected override void Drop(LayoutAnchorableFloatingWindow floatingWindow)
        {
            ILayoutPane targetModel = _targetPane.Model as ILayoutPane;

            switch (this.Type)
            {
                case DropTargetType.DocumentPaneGroupDockInside:

                    #region DropTargetType.DocumentPaneGroupDockInside

                    {
                        LayoutDocumentPaneGroup paneGroupModel = targetModel as LayoutDocumentPaneGroup;
                        LayoutDocumentPane paneModel = paneGroupModel.Children[0] as LayoutDocumentPane;
                        LayoutAnchorablePaneGroup layoutAnchorablePaneGroup = floatingWindow.RootPanel;

                        int i = 0;
                        foreach (LayoutAnchorable anchorableToImport in layoutAnchorablePaneGroup.Descendents().OfType<LayoutAnchorable>().ToArray())
                        {
                            // BD: 18.07.2020 Remove that bodge and handle CanClose=false && CanHide=true in XAML
                            //anchorableToImport.SetCanCloseInternal(true);

                            paneModel.Children.Insert(i, anchorableToImport);
                            i++;
                        }
                    }
                    break;

                    #endregion DropTargetType.DocumentPaneGroupDockInside
            }

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
                case DropTargetType.DocumentPaneGroupDockInside:

                    #region DropTargetType.DocumentPaneGroupDockInside

                    {
                        Rect targetScreenRect = this.TargetElement.GetScreenArea();
                        targetScreenRect.Offset(-overlayWindow.Left, -overlayWindow.Top);

                        return new RectangleGeometry(targetScreenRect);
                    }

                    #endregion DropTargetType.DocumentPaneGroupDockInside
            }

            return null;
        }

        #endregion Overrides
    }
}
