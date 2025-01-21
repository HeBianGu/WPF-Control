using System.Threading.Tasks;
using System.Windows;

namespace H.Services.Common
{
    public interface ITransitionable
    {
        Task Show(DependencyObject visual);

        Task Close(DependencyObject visual);
    }
}
