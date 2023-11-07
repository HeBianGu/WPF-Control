using System;
using System.Text;
using System.Threading.Tasks;

namespace H.Extensions.ViewModel
{
    internal class Class1
    {
    }


    public class LazyInstance<T> where T : new()
    {
        public static T Instance = new Lazy<T>(() => new T()).Value;
    }
}
