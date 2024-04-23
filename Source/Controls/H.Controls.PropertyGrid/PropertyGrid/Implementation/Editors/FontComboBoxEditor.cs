// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Collections;
using System.Linq;
using System.Windows;
using System.Windows.Media;


namespace H.Controls.PropertyGrid
{
    public class FontComboBoxEditor : ComboBoxEditor
    {
        protected override IEnumerable CreateItemsSource(PropertyItem propertyItem)
        {
            if (propertyItem.PropertyType == typeof(FontFamily))
                return FontUtilities.Families.OrderBy(x => x.Source);
            else if (propertyItem.PropertyType == typeof(FontWeight))
                return FontUtilities.Weights;
            else if (propertyItem.PropertyType == typeof(FontStyle))
                return FontUtilities.Styles;
            else if (propertyItem.PropertyType == typeof(FontStretch))
                return FontUtilities.Stretches;

            return null;
        }
    }
}
