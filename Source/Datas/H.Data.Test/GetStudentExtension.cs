using System;
using System.Windows.Markup;

namespace H.Data.Test
{
    public class GetStudentExtension : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Student.Random();
        }
    }
}
