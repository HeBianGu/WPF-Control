using H.Controls.Diagram.Presenter.DiagramDatas.Base;
using H.Mvvm.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace H.Test.Diagram
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
    }

    public class MainViewModel : Bindable
    {
        private IFlowableDiagramData _diagramData;
        public IFlowableDiagramData DiagramData
        {
            get { return _diagramData; }
            set
            {
                _diagramData = value;
                RaisePropertyChanged();
            }
        }
    }
}
