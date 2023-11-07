using H.Extensions.ViewModel;
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

namespace H.Controls.RepositoryBox
{
    public class RepositoryBox : ListBox
    {
        static RepositoryBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RepositoryBox), new FrameworkPropertyMetadata(typeof(RepositoryBox)));
        }

        public Type Type
        {
            get { return (Type)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TypeProperty =
            DependencyProperty.Register("Type", typeof(Type), typeof(RepositoryBox), new FrameworkPropertyMetadata(default(Type), (d, e) =>
            {
                RepositoryBox control = d as RepositoryBox;

                if (control == null) return;

                if (e.OldValue is Type o)
                {

                }

                if (e.NewValue is Type n)
                {
                    control.RefreshType(n);
                }

            }));

        void RefreshType(Type type)
        {
            var gType = typeof(IRepositoryViewModel<>).MakeGenericType(type);
            this.DataContext = Ioc.GetService<IRepositoryViewModel>(gType, true);
        }

    }

    public class RepositoryBoxKeys
    {
        public static ComponentResourceKey ItemsControl => new ComponentResourceKey(typeof(RepositoryBoxKeys), "S.RepositoryBox.ItemsControl");
        public static ComponentResourceKey DataGrid => new ComponentResourceKey(typeof(RepositoryBoxKeys), "S.RepositoryBox.DataGrid");

    }
}
