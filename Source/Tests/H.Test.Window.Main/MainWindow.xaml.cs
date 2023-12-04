using H.Data.Test;
using H.Windows.Dialog;
using System.Windows;

namespace H.Test.Window.Main
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
                if (x is System.Windows.Window window)
                    window.SizeToContent = SizeToContent.Height;
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
