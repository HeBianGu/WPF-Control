using System.Globalization;
using System.Windows.Controls;

namespace H.Data.Test
{
    public class StudentValidationRule : ValidationRule
    {
        public override System.Windows.Controls.ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value is Student student)
                return student.Age > 30 ? System.Windows.Controls.ValidationResult.ValidResult : new System.Windows.Controls.ValidationResult(false, "Age不应该大于50");
            return System.Windows.Controls.ValidationResult.ValidResult;
        }
    }
}
