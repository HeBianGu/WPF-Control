
using H.Controls.Dock.Layout;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace H.Controls.Dock.Controls
{
    /// <summary>
    /// This control displays multiple <see cref="LayoutAnchorControl"/>s along the
    /// top, bottom, left, or right side of the <see cref="DockingManager"/>.
    /// </summary>
    public class LayoutAnchorGroupControl : System.Windows.Controls.Control, ILayoutControl
    {
        #region fields

        private ObservableCollection<LayoutAnchorControl> _childViews = new ObservableCollection<LayoutAnchorControl>();
        private LayoutAnchorGroup _model;

        #endregion fields

        #region Constructors

        static LayoutAnchorGroupControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LayoutAnchorGroupControl), new FrameworkPropertyMetadata(typeof(LayoutAnchorGroupControl)));
        }

        internal LayoutAnchorGroupControl(LayoutAnchorGroup model)
        {
            _model = model;
            CreateChildrenViews();

            _model.Children.CollectionChanged += (s, e) => OnModelChildrenCollectionChanged(e);
        }

        #endregion Constructors

        #region Properties

        public ObservableCollection<LayoutAnchorControl> Children
        {
            get
            {
                return _childViews;
            }
        }

        public ILayoutElement Model
        {
            get
            {
                return _model;
            }
        }

        #endregion Properties

        #region Private Methods

        private void CreateChildrenViews()
        {
            DockingManager manager = _model.Root.Manager;
            foreach (LayoutAnchorable childModel in _model.Children)
            {
                LayoutAnchorControl lac = new LayoutAnchorControl(childModel);
                lac.SetBinding(LayoutAnchorControl.TemplateProperty, new Binding(DockingManager.AnchorTemplateProperty.Name) { Source = manager });
                _childViews.Add(lac);
            }
        }

        private void OnModelChildrenCollectionChanged(System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove ||
                e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Replace)
            {
                if (e.OldItems != null)
                {
                    {
                        foreach (object childModel in e.OldItems)
                            _childViews.Remove(_childViews.First(cv => cv.Model == childModel));
                    }
                }
            }

            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Reset)
                _childViews.Clear();

            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add ||
                e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Replace)
            {
                if (e.NewItems != null)
                {
                    DockingManager manager = _model.Root.Manager;
                    int insertIndex = e.NewStartingIndex;
                    foreach (LayoutAnchorable childModel in e.NewItems)
                    {
                        LayoutAnchorControl lac = new LayoutAnchorControl(childModel);
                        lac.SetBinding(LayoutAnchorControl.TemplateProperty, new Binding(DockingManager.AnchorTemplateProperty.Name) { Source = manager });
                        _childViews.Insert(insertIndex++, lac);
                    }
                }
            }
        }

        #endregion Private Methods
    }
}