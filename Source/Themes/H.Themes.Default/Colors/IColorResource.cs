using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace H.Themes.Default
{
    public interface IColorResource
    {
        string Name { get; }
        ResourceDictionary Resource { get; }
    }
}
