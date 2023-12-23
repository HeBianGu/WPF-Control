using H.Extensions.ValueConverter;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace H.Controls.TagBox
{
    public class TagWhereByGroupConverter : MarkupMultiValueConverterBase
    {
        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] is IEnumerable<ITag> tags && values[1] is string groupName)
                return tags.Where(x => x.GroupName == groupName);
            return values[0];
        }
    }
}
