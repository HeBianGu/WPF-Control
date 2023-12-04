using H.Data.Test;
using H.Windows.Dialog;
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

namespace H.Test.Identify
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogWindow.ShowPresenter("这是一个提示消息");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DialogWindow.ShowPresenter(Student.Random(), x =>
            {
                x.Width = 500;
                x.Height = 300;
                if (x is Window w)
                    w.SizeToContent = SizeToContent.Height;
            });
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            DialogWindow.ShowPresenter(Student.Randoms(), x =>
            {
                x.Width = 500;
                x.Height = 300;
            });

        }
    }
}
