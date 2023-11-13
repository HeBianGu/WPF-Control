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


        public bool UseAdd
        {
            get { return (bool)GetValue(UseAddProperty); }
            set { SetValue(UseAddProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UseAddProperty =
            DependencyProperty.Register("UseAdd", typeof(bool), typeof(RepositoryBox), new FrameworkPropertyMetadata(default(bool), (d, e) =>
            {
                RepositoryBox control = d as RepositoryBox;

                if (control == null) return;

                if (e.OldValue is bool o)
                {

                }

                if (e.NewValue is bool n)
                {

                }

            }));


        public bool UseDelete
        {
            get { return (bool)GetValue(UseDeleteProperty); }
            set { SetValue(UseDeleteProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UseDeleteProperty =
            DependencyProperty.Register("UseDelete", typeof(bool), typeof(RepositoryBox), new FrameworkPropertyMetadata(default(bool), (d, e) =>
            {
                RepositoryBox control = d as RepositoryBox;

                if (control == null) return;

                if (e.OldValue is bool o)
                {

                }

                if (e.NewValue is bool n)
                {

                }

            }));


        public bool UseCheckAll
        {
            get { return (bool)GetValue(UseCheckAllProperty); }
            set { SetValue(UseCheckAllProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UseCheckAllProperty =
            DependencyProperty.Register("UseCheckAll", typeof(bool), typeof(RepositoryBox), new FrameworkPropertyMetadata(default(bool), (d, e) =>
            {
                RepositoryBox control = d as RepositoryBox;

                if (control == null) return;

                if (e.OldValue is bool o)
                {

                }

                if (e.NewValue is bool n)
                {

                }

            }));


        public bool UseClear
        {
            get { return (bool)GetValue(UseClearProperty); }
            set { SetValue(UseClearProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UseClearProperty =
            DependencyProperty.Register("UseClear", typeof(bool), typeof(RepositoryBox), new FrameworkPropertyMetadata(default(bool), (d, e) =>
            {
                RepositoryBox control = d as RepositoryBox;

                if (control == null) return;

                if (e.OldValue is bool o)
                {

                }

                if (e.NewValue is bool n)
                {

                }

            }));


        public bool UseEdit
        {
            get { return (bool)GetValue(UseEditProperty); }
            set { SetValue(UseEditProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UseEditProperty =
            DependencyProperty.Register("UseEdit", typeof(bool), typeof(RepositoryBox), new FrameworkPropertyMetadata(default(bool), (d, e) =>
            {
                RepositoryBox control = d as RepositoryBox;

                if (control == null) return;

                if (e.OldValue is bool o)
                {

                }

                if (e.NewValue is bool n)
                {

                }

            }));


        public bool UseExport
        {
            get { return (bool)GetValue(UseExportProperty); }
            set { SetValue(UseExportProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UseExportProperty =
            DependencyProperty.Register("UseExport", typeof(bool), typeof(RepositoryBox), new FrameworkPropertyMetadata(default(bool), (d, e) =>
            {
                RepositoryBox control = d as RepositoryBox;

                if (control == null) return;

                if (e.OldValue is bool o)
                {

                }

                if (e.NewValue is bool n)
                {

                }

            }));


        public bool UseView
        {
            get { return (bool)GetValue(UseViewProperty); }
            set { SetValue(UseViewProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UseViewProperty =
            DependencyProperty.Register("UseView", typeof(bool), typeof(RepositoryBox), new FrameworkPropertyMetadata(default(bool), (d, e) =>
            {
                RepositoryBox control = d as RepositoryBox;

                if (control == null) return;

                if (e.OldValue is bool o)
                {

                }

                if (e.NewValue is bool n)
                {

                }

            }));


        public bool UseDeleteChecked
        {
            get { return (bool)GetValue(UseDeleteCheckedProperty); }
            set { SetValue(UseDeleteCheckedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UseDeleteCheckedProperty =
            DependencyProperty.Register("UseDeleteChecked", typeof(bool), typeof(RepositoryBox), new FrameworkPropertyMetadata(default(bool), (d, e) =>
            {
                RepositoryBox control = d as RepositoryBox;

                if (control == null) return;

                if (e.OldValue is bool o)
                {

                }

                if (e.NewValue is bool n)
                {

                }

            }));


        public bool UseSave
        {
            get { return (bool)GetValue(UseSaveProperty); }
            set { SetValue(UseSaveProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UseSaveProperty =
            DependencyProperty.Register("UseSave", typeof(bool), typeof(RepositoryBox), new FrameworkPropertyMetadata(default(bool), (d, e) =>
            {
                RepositoryBox control = d as RepositoryBox;

                if (control == null) return;

                if (e.OldValue is bool o)
                {

                }

                if (e.NewValue is bool n)
                {

                }

            }));


        public bool UseNextPage
        {
            get { return (bool)GetValue(UseNextPageProperty); }
            set { SetValue(UseNextPageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UseNextPageProperty =
            DependencyProperty.Register("UseNextPage", typeof(bool), typeof(RepositoryBox), new FrameworkPropertyMetadata(default(bool), (d, e) =>
            {
                RepositoryBox control = d as RepositoryBox;

                if (control == null) return;

                if (e.OldValue is bool o)
                {

                }

                if (e.NewValue is bool n)
                {

                }

            }));


        public bool UsePreviousPage
        {
            get { return (bool)GetValue(UsePreviousPageProperty); }
            set { SetValue(UsePreviousPageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UsePreviousPageProperty =
            DependencyProperty.Register("UsePreviousPage", typeof(bool), typeof(RepositoryBox), new FrameworkPropertyMetadata(default(bool), (d, e) =>
            {
                RepositoryBox control = d as RepositoryBox;

                if (control == null) return;

                if (e.OldValue is bool o)
                {

                }

                if (e.NewValue is bool n)
                {

                }

            }));



    }
}
