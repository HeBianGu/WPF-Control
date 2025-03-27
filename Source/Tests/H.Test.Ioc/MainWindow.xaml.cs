global using H.Iocable;
global using H.Services.Logger;
using H.Modules.About;
using H.Services.Common;
using H.Services.Message;
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

namespace H.Test.Ioc
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

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var test = H.Iocable.Ioc.Services.GetService<ITest>();
            await IocMessage.ShowDialogMessage(test.GetType().Name);
        }
    }

    public interface ITest
    {
    }

    public class MyTest : ITest
    {

    }
}
