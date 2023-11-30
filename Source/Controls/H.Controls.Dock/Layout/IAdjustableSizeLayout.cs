








using System.Windows;

namespace H.Controls.Dock.Layout
{
    public interface IAdjustableSizeLayout
    {
        void AdjustFixedChildrenPanelSizes(Size? parentSize = null);
    }
}