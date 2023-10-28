using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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

namespace H.Test.Mvvm
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new User() { Name = "Name" };
        }
    }

    public class User : ObservableObject
    {
        private string name;

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }
    }

    public class MyViewModel : ObservableObject
    {
        public MyViewModel()
        {
            IncrementCounterCommand = new RelayCommand(IncrementCounter);
        }

        private int counter;

        public int Counter
        {
            get => counter;
            private set => SetProperty(ref counter, value);
        }

        public ICommand IncrementCounterCommand { get; }

        private void IncrementCounter() => Counter++;
    }
}
