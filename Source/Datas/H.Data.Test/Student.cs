using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Xml.Linq;

namespace H.Data.Test
{
    public class Student : ICloneable
    {
        [Display(Name = "姓名", GroupName = "基础信息")]
        [Required()]
        public string Name { get; set; }

        [Display(Name = "班级", GroupName = "基础信息")]
        [Required]
        public string Class { get; set; }

        [Display(Name = "地址", GroupName = "基础信息")]
        [Required]
        public string Address { get; set; }

        [Display(Name = "邮箱", GroupName = "基础信息")]
        [Required]
        public string Emall { get; set; }

        [Display(Name = "可用", GroupName = "其他信息")]
        [Required]
        public bool IsEnbled { get; set; }

        [Display(Name = "时间", GroupName = "其他信息")]
        [Required]
        public DateTime Time { get; set; }

        [Display(Name = "年龄", GroupName = "基础信息")]
        [Required]
        public int Age { get; set; }

        [Display(Name = "分数", GroupName = "成绩信息")]
        [Range(0.0, 150.0)]
        public double Score { get; set; }

        [Display(Name = "电话号码", GroupName = "基础信息")]
        [Required]
        [RegularExpression(@"^1[3|4|5|7|8][0-9]{9}$", ErrorMessage = "手机号码不合法！")]
        public string Tel { get; set; }

        private static Random random = new Random();
        private static string[] names = new string[] { "Lucy", "Jim", "Tom", "Lulu", "Angle", "Linda", "Summi" };
        private static string[] mails = new string[] { "@163.com", "@126.com", "@qq.com", "@gmail.com" };
        private static string[] address = new string[] { "浙江省 台州市 临海市", "广西壮族自治区 钦州市 钦北区", "重庆 重庆市 南岸区", "上海 上海市 闵行区", "香港特别行政区 香港岛 南区", "新疆维吾尔自治区 喀什地区 伽师县", "Summi" };
        public Student()
        {
            this.Name = names[random.Next(names.Count())];
            this.Class = random.Next(1, 6).ToString() + "班";
            this.Address = address[random.Next(address.Count())];
            this.Emall = random.Next(0, 99999999).ToString().PadLeft(8, '0') + mails[random.Next(mails.Count())];
            this.IsEnbled = random.Next(1, 3) == 2;
            this.Time = DateTime.Now.AddDays(random.Next(-50, 50));
            this.Age = random.Next(10, 50);
            this.Score = Math.Round(random.NextDouble() * 100, 2);
            this.Tel = "13" + random.Next(9) + random.Next(0, 99999999).ToString().PadLeft(8, '0');
        }

        public static Student Random()
        {
            Student student = new Student();
            student.Name = names[random.Next(names.Count())];
            student.Class = random.Next(1, 6).ToString() + "班";
            student.Address = address[random.Next(address.Count())];
            student.Emall = random.Next(0, 99999999).ToString().PadLeft(8, '0') + mails[random.Next(mails.Count())];
            student.IsEnbled = random.Next(1, 3) == 2;
            student.Time = DateTime.Now.AddDays(random.Next(-50, 50));
            student.Age = random.Next(10, 50);
            student.Score = Math.Round(random.NextDouble() * 100, 2);
            student.Tel = "13" + random.Next(9) + random.Next(0, 99999999).ToString().PadLeft(8, '0');
            return student;
        }

        public object Clone()
        {
            return Student.Random();
        }

        public static Students Randoms(int c = 100)
        {
            var ss = Enumerable.Range(0, c).Select(x => Student.Random()).ToList();
            return new Students(ss);
        }
    }

    public class Students : ObservableCollection<Student>
    {
        public Students()
        {

        }
        public Students(IEnumerable<Student> collection) : base(collection)
        {

        }
    }

    public class GetStudentsExtension : MarkupExtension
    {
        public int Count { get; set; } = 10;

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Enumerable.Range(0, this.Count).Select(x => new Student());
        }
    }

    public class GetStudentExtension : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Student.Random();
        }
    }

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
