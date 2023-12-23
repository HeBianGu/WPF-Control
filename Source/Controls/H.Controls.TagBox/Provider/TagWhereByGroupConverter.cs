using H.Extensions.ValueConverter;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace H.Controls.TagBox
{
    //public class TagWhereByGroupConverter : MarkupMultiValueConverterBase
    //{
    //    public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        if (values[0] is IEnumerable<ITag> tags)
    //        {
    //            if (values[1] is string groupName)
    //            {
    //                return tags.Where(x => x.GroupName == groupName);
    //            }
    //            else if (values[1] == null)
    //            {
    //                return tags.Where(x => x.GroupName == null);
    //            }
    //        }
    //        return values[0];
    //    }
    //}
}
