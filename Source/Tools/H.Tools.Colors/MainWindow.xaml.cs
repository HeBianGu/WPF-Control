using H.Extensions.Color;
using H.Mvvm.Commands;
using H.Mvvm.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace H.Tools.Colors;
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

    private ObservableCollection<Color> _colors = new ObservableCollection<Color>();
    public ObservableCollection<Color> Colors
    {
        get { return _colors; }
        set
        {
            _colors = value;
            RaisePropertyChanged();
        }
    }

    private ObservableCollection<Color> _dColors = new ObservableCollection<Color>();
    public ObservableCollection<Color> DColors
    {
        get { return _dColors; }
        set
        {
            _dColors = value;
            RaisePropertyChanged();
        }
    }

    private string _Value = "#0F1F28";
    public string Value
    {
        get { return _Value; }
        set
        {
            _Value = value;
            RaisePropertyChanged();
        }
    }


    public RelayCommand CreateCommand => new RelayCommand(x =>
    {
        HsbaColor hsbaColor = new HsbaColor("#0F1F28");
        hsbaColor = new HsbaColor(this.Value);
        //ColorFactory.CreateDepthB(0, 255, 10, 0.5).ToList().ForEach(x => Colors.Add(x));

        string str = "<Color x:Key=\"{ComponentResourceKey ResourceId=S.Color.Dark.{0}, TypeInTargetAssembly={x:Type ColorKeys}}\">{1}</Color>";

        Action<string, string> log = (s, k) =>
        {
            System.Diagnostics.Debug.WriteLine("<Color x:Key=\"{ComponentResourceKey ResourceId=S.Color.Dark." + s + ", TypeInTargetAssembly={x:Type ColorKeys}}\">" + k + "</Color>");
        };

        int index = 0;
        if (hsbaColor.B > 0)
        {
            System.Diagnostics.Debug.WriteLine("深色");
            for (double i = hsbaColor.B; i > 0; i = i - hsbaColor.B / 28.0)
            {
                var c = new HsbaColor(hsbaColor.H, hsbaColor.S, i, hsbaColor.A);
                this.DColors.Add(c.Color);
                log.Invoke(index.ToString(), c.HexString);
                index++;
            }
        }


        if (hsbaColor.B < 1)
        {
            System.Diagnostics.Debug.WriteLine("浅色");
            index = 0;
            for (double i = hsbaColor.B; i <= 1; i = i + (1 - hsbaColor.B) / 28.0)
            {
                var c = new HsbaColor(hsbaColor.H, hsbaColor.S, i, hsbaColor.A);
                this.Colors.Add(c.Color);
                log.Invoke(index.ToString(), c.HexString);
                index++;
            }
        }

    });
}