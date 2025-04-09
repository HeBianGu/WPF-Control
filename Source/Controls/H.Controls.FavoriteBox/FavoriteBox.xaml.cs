global using H.Extensions.Tree;
global using H.Mvvm;
global using H.Mvvm.ViewModels;
global using H.Mvvm.ViewModels.Tree;
global using System.Collections.Generic;
global using System.Linq;
global using System.Windows;
global using System.Windows.Controls;
global using System.Windows.Input;
global using H.Extensions.Common;
global using H.Mvvm.Commands;

namespace H.Controls.FavoriteBox
{
    public class FavoriteBox : TreeView
    {
        static FavoriteBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FavoriteBox), new FrameworkPropertyMetadata(typeof(FavoriteBox)));
        }

        public FavoriteBox()
        {

            CommandBinding bingding = new CommandBinding(Commands.Clear);
            bingding.Executed += (s, e) =>
            {
                this.SelectNone();
            };
            bingding.CanExecute += (l, k) =>
            {
                k.CanExecute = this.SelectedItem != null;
            };
            this.CommandBindings.Add(bingding);

            IocFavoriteService.Instance.CollectionChanged += (l, k) =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    this.RefreshData();
                });

            };
            this.RefreshData();
        }


        private void RefreshData()
        {
            this.ItemsSource = new FavoriteTree().GetTreeNodes().ToList();
        }

        public string GroupName
        {
            get { return (string)GetValue(GroupNameProperty); }
            set { SetValue(GroupNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GroupNameProperty =
            DependencyProperty.Register("GroupName", typeof(string), typeof(FavoriteBox), new FrameworkPropertyMetadata(default(string), (d, e) =>
            {
                FavoriteBox control = d as FavoriteBox;

                if (control == null) return;

                if (e.OldValue is string o)
                {

                }

                if (e.NewValue is string n)
                {

                }
                //control.RefreshData();
            }));


        public string SelectedFavoritePath
        {
            get { return (string)GetValue(SelectedFavoritePathProperty); }
            set { SetValue(SelectedFavoritePathProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedFavoritePathProperty =
            DependencyProperty.Register("SelectedFavoritePath", typeof(string), typeof(FavoriteBox), new FrameworkPropertyMetadata(default(string), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | // Flags
                                           FrameworkPropertyMetadataOptions.Journal, (d, e) =>
            {
                FavoriteBox control = d as FavoriteBox;

                if (control == null) return;

                if (e.OldValue is string o)
                {

                }

                if (e.NewValue is string n)
                {
                    if (control.ItemsSource is IEnumerable<ITreeNode> tree)
                    {
                        var find = tree.Where<IFavoriteItem>(x => x.Path == n).FirstOrDefault();
                        if (find != null)
                        {
                            find.IsSelected = true;
                            //var item = control.GetChild<TreeViewItem>(x => x.DataContext == find);
                            //if (item != null)
                            //{
                            //    item.IsSelected = true;
                            //}
                        }
                        else
                        {
                            control.SelectNone();
                        }
                    }
                }
                else
                {
                    control.SelectNone();
                }
            }));

        protected override void OnSelectedItemChanged(RoutedPropertyChangedEventArgs<object> e)
        {
            base.OnSelectedItemChanged(e);
            if (e.NewValue is IModelBindable viewModel && viewModel.GetModel() is IFavoriteItem favorite)
                this.SelectedFavoritePath = favorite.Path;
            else
                this.SelectedFavoritePath = null;
        }

    }
}
