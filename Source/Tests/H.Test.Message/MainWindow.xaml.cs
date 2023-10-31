
using H.Data.Test;
using H.Providers.Ioc;
using H.Windows.Dialog;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace H.Test.Message
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var student = new Student();
            student.Age = 200;
            Predicate<Student> match = x =>
                {
                    if (x.Age > 100)
                    {
                        IocMessage.Dialog.Show("年龄输入不合法");
                        return false;
                    }
                    return true;
                };
            IocMessage.Form.ShowEdit(student, match);
        }
    }
}
