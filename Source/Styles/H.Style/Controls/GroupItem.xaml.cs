namespace H.Styles.Controls;

public class GroupItemKeys
{
    public static ComponentResourceKey Default => new ComponentResourceKey(typeof(GroupItemKeys), "S.GroupItem.Default");
}

public class GroupStyleSelector : StyleSelector
{
    public Style SingleDefaultCategoryItemGroupStyle
    {
        get;
        set;
    }
    public Style ItemGroupStyle
    {
        get;
        set;
    }

    public override Style SelectStyle(object item, DependencyObject container)
    {
        CollectionViewGroup group = item as CollectionViewGroup;
        // Category is not "Misc" => use regular ItemGroupStyle
        if ((group.Name != null) && !group.Name.Equals(CategoryAttribute.Default.Category))
            return this.ItemGroupStyle;

        // Category is "Misc"
        while (container != null)
        {
            container = VisualTreeHelper.GetParent(container);
            if (container is ItemsControl)
                break;
        }

        ItemsControl itemsControl = container as ItemsControl;
        if (itemsControl != null)
        {
            // Category is "Misc" and this is the only category => use SingleDefaultCategoryItemGroupContainerStyle
            if ((itemsControl.Items.Count > 0) && (itemsControl.Items.Groups.Count == 1))
                return this.SingleDefaultCategoryItemGroupStyle;
        }

        // Category is "Misc" and this is NOT the only category => use regular ItemGroupStyle
        return this.ItemGroupStyle;
    }
}
