using H.Extensions.Color;
using H.Mvvm.Commands;
using H.Mvvm.ViewModels.Base;
using H.Themes.Colors;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace H.Tools.Colors;

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

        //string str = "<Color x:Key=\"{ComponentResourceKey ResourceId=S.Color.Dark.{0}, TypeInTargetAssembly={x:Type ColorKeys}}\">{1}</Color>";

        Action<string, string> log = (s, k) =>
        {
            System.Diagnostics.Debug.WriteLine("<Color x:Key=\"{ComponentResourceKey ResourceId=" + s + ", TypeInTargetAssembly={x:Type ColorKeys}}\">" + k + "</Color>");
        };
        double p = 0.9;//值越小，颜色变化起始颜色变化越快
        double s = hsbaColor.S / 28.0;
        if (hsbaColor.B > 0)
        {
            this.DColors.Clear();
            System.Diagnostics.Debug.WriteLine("深色");
            double b = hsbaColor.B / 28.0;
            for (int i = 0; i <= 28; i++)
            {
                var bs = Math.Pow(i / 28.0, p) * hsbaColor.B;
                var c = new HsbaColor(hsbaColor.H, hsbaColor.S - i * s, hsbaColor.B - bs, hsbaColor.A);
                this.DColors.Add(c.Color);
                log.Invoke($"S.Color.Dark.{i.ToString()}", c.HexString);
                var ks = GetKeyByIndex(i);
                foreach (var k in ks)
                {
                    log.Invoke(k, c.HexString);
                }
            }
        }


        if (hsbaColor.B < 1)
        {
            this.Colors.Clear();
            System.Diagnostics.Debug.WriteLine("浅色");
            double b = (1 - hsbaColor.B) / 28.0;
            for (int i = 0; i <= 28; i++)
            {
                var bs = Math.Pow(i / 28.0, p) * (1 - hsbaColor.B);
                var c = new HsbaColor(hsbaColor.H, hsbaColor.S - i * s, hsbaColor.B + bs, hsbaColor.A);
                this.Colors.Add(c.Color);
                log.Invoke($"S.Color.Dark.{i.ToString()}", c.HexString);
                var ks = GetKeyByIndex(i);
                foreach (var k in ks)
                {
                    log.Invoke(k, c.HexString);
                }
            }
        }
    });


    public IEnumerable<string> GetKeyByIndex(int index)
    {
        if (index == 0)
            yield return ColorKeys.Background.ResourceId.ToString();
        if (index == 1)
            yield return ColorKeys.AlternatingRowBackground.ResourceId.ToString();
        if (index == 1)
            yield return ColorKeys.CaptionBackground.ResourceId.ToString();
        if (index == 2)
            yield return ColorKeys.MouseOver.ResourceId.ToString();
        if (index == 3)
            yield return ColorKeys.BorderBrushAssist.ResourceId.ToString();
        if (index == 4)
            yield return ColorKeys.Selected.ResourceId.ToString();
        if (index == 5)
            yield return ColorKeys.BorderBrush.ResourceId.ToString();
        if (index == 6)
            yield return ColorKeys.BorderBrushTitle.ResourceId.ToString();
        if (index == 12)
            yield return ColorKeys.ForegroundAssist.ResourceId.ToString();
        if (index == 24)
            yield return ColorKeys.CaptionForeground.ResourceId.ToString();
        if (index == 24)
            yield return ColorKeys.Foreground.ResourceId.ToString();
        if (index == 28)
            yield return ColorKeys.ForegroundTitle.ResourceId.ToString();
        if (index == 28)
            yield return ColorKeys.ForegroundSelect.ResourceId.ToString();
    }

}