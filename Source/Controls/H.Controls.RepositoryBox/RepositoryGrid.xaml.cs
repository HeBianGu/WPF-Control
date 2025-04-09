using H.Extensions.DataBase.Repository;
using System;
using System.Windows;
using System.Windows.Controls;

namespace H.Controls.RepositoryBox
{
    public class RepositoryGrid : DataGrid
    {
        static RepositoryGrid()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RepositoryGrid), new FrameworkPropertyMetadata(typeof(RepositoryGrid)));
        }

        public Type Type
        {
            get { return (Type)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }


        public static readonly DependencyProperty TypeProperty =
            DependencyProperty.Register("Type", typeof(Type), typeof(RepositoryGrid), new FrameworkPropertyMetadata(default(Type), (d, e) =>
            {
                RepositoryGrid control = d as RepositoryGrid;

                if (control == null) return;

                if (e.OldValue is Type o)
                {

                }

                if (e.NewValue is Type n)
                {
                    control.RefreshType(n);
                }

            }));

        private void RefreshType(Type type)
        {
            Type gType = typeof(IRepositoryBindable<>).MakeGenericType(type);
            this.DataContext = Ioc.GetService<IRepositoryBindable>(gType, true);
        }


        public bool UseAdd
        {
            get { return (bool)GetValue(UseAddProperty); }
            set { SetValue(UseAddProperty, value); }
        }


        public static readonly DependencyProperty UseAddProperty =
            DependencyProperty.Register("UseAdd", typeof(bool), typeof(RepositoryGrid), new FrameworkPropertyMetadata(default(bool), (d, e) =>
            {
                RepositoryGrid control = d as RepositoryGrid;

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


        public static readonly DependencyProperty UseDeleteProperty =
            DependencyProperty.Register("UseDelete", typeof(bool), typeof(RepositoryGrid), new FrameworkPropertyMetadata(default(bool), (d, e) =>
            {
                RepositoryGrid control = d as RepositoryGrid;

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


        public static readonly DependencyProperty UseCheckAllProperty =
            DependencyProperty.Register("UseCheckAll", typeof(bool), typeof(RepositoryGrid), new FrameworkPropertyMetadata(default(bool), (d, e) =>
            {
                RepositoryGrid control = d as RepositoryGrid;

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


        public static readonly DependencyProperty UseClearProperty =
            DependencyProperty.Register("UseClear", typeof(bool), typeof(RepositoryGrid), new FrameworkPropertyMetadata(default(bool), (d, e) =>
            {
                RepositoryGrid control = d as RepositoryGrid;

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


        public static readonly DependencyProperty UseEditProperty =
            DependencyProperty.Register("UseEdit", typeof(bool), typeof(RepositoryGrid), new FrameworkPropertyMetadata(default(bool), (d, e) =>
            {
                RepositoryGrid control = d as RepositoryGrid;

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


        public static readonly DependencyProperty UseExportProperty =
            DependencyProperty.Register("UseExport", typeof(bool), typeof(RepositoryGrid), new FrameworkPropertyMetadata(default(bool), (d, e) =>
            {
                RepositoryGrid control = d as RepositoryGrid;

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


        public static readonly DependencyProperty UseViewProperty =
            DependencyProperty.Register("UseView", typeof(bool), typeof(RepositoryGrid), new FrameworkPropertyMetadata(default(bool), (d, e) =>
            {
                RepositoryGrid control = d as RepositoryGrid;

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


        public static readonly DependencyProperty UseDeleteCheckedProperty =
            DependencyProperty.Register("UseDeleteChecked", typeof(bool), typeof(RepositoryGrid), new FrameworkPropertyMetadata(default(bool), (d, e) =>
            {
                RepositoryGrid control = d as RepositoryGrid;

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


        public static readonly DependencyProperty UseSaveProperty =
            DependencyProperty.Register("UseSave", typeof(bool), typeof(RepositoryGrid), new FrameworkPropertyMetadata(default(bool), (d, e) =>
            {
                RepositoryGrid control = d as RepositoryGrid;

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


        public static readonly DependencyProperty UseNextPageProperty =
            DependencyProperty.Register("UseNextPage", typeof(bool), typeof(RepositoryGrid), new FrameworkPropertyMetadata(default(bool), (d, e) =>
            {
                RepositoryGrid control = d as RepositoryGrid;

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


        public static readonly DependencyProperty UsePreviousPageProperty =
            DependencyProperty.Register("UsePreviousPage", typeof(bool), typeof(RepositoryGrid), new FrameworkPropertyMetadata(default(bool), (d, e) =>
            {
                RepositoryGrid control = d as RepositoryGrid;

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
