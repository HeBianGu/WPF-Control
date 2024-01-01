// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase



using H.Providers.Ioc;
using H.Providers.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace H.Modules.Project
{
    public partial class ProjectBox : ListBox
    {
        static ProjectBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ProjectBox), new FrameworkPropertyMetadata(typeof(ProjectBox)));
        }

        public ProjectBox()
        {
            {
                CommandBinding commandBinding = new CommandBinding(Commands.Delete);
                commandBinding.Executed += async (l, k) =>
                {
                    if (k.Parameter is IProjectItem project)
                    {
                        var r = await IocMessage.Dialog.Show("确定要删除？");
                        if (r != true) return;
                        IocProject.Instance.Delete(x => x == project);
                        IocProject.Instance.Save(out string message);
                    }
                };
                this.CommandBindings.Add(commandBinding);
            }

            {
                CommandBinding commandBinding = new CommandBinding(Commands.Open);
                commandBinding.Executed += (l, k) =>
                {
                    if (k.Parameter is IProjectItem project)
                    {
                        IocProject.Instance.Current = project;
                        IocProject.Instance.Save(out string message);
                    }
                };
                commandBinding.CanExecute += (l, k) =>
                {
                    k.CanExecute = k.Parameter is IProjectItem project ? project != IocProject.Instance.Current : false;
                };
                this.CommandBindings.Add(commandBinding);
            }

            {
                CommandBinding commandBinding = new CommandBinding(Commands.New);
                commandBinding.Executed += async (l, k) =>
                {
                    var project = IocProject.Instance.Create();
                    var r = await IocMessage.Form.ShowEdit(project, null, null, null, "新建工程");
                    if (r != true)
                        return;
                    IocProject.Instance.Add(project);
                    IocProject.Instance.Current = project;
                };
                this.CommandBindings.Add(commandBinding);
            }

            {
                CommandBinding commandBinding = new CommandBinding(Commands.Refresh);
                commandBinding.Executed += (l, k) =>
                {
                    this.Refresh();
                };
                this.CommandBindings.Add(commandBinding);
            }
        }

        public IEnumerable<IProjectItem> Projects
        {
            get { return (IEnumerable<IProjectItem>)GetValue(ProjectsProperty); }
            set { SetValue(ProjectsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ProjectsProperty =
            DependencyProperty.Register("Projects", typeof(IEnumerable<IProjectItem>), typeof(ProjectBox), new FrameworkPropertyMetadata(default(IEnumerable<IProjectItem>), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (d, e) =>
             {
                 ProjectBox control = d as ProjectBox;

                 if (control == null) return;


                 if (e.OldValue is INotifyCollectionChanged o)
                 {
                     o.CollectionChanged -= control.Config_CollectionChanged;
                 }

                 if (e.NewValue is INotifyCollectionChanged n)
                 {
                     n.CollectionChanged -= control.Config_CollectionChanged;
                     n.CollectionChanged += control.Config_CollectionChanged;
                 }


                 control.Refresh();
             }));

        private void Config_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this?.Refresh();
        }

        public IProjectItem SelectedProject
        {
            get { return (ProjectItem)GetValue(SelectedProjectProperty); }
            set { SetValue(SelectedProjectProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedProjectProperty =
            DependencyProperty.Register("SelectedProject", typeof(IProjectItem), typeof(ProjectBox), new FrameworkPropertyMetadata(default(IProjectItem), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (d, e) =>
             {
                 ProjectBox control = d as ProjectBox;

                 if (control == null) return;

                 IProjectItem config = e.NewValue as IProjectItem;

             }));

        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            base.OnSelectionChanged(e);

            this.SelectedProject = (this.SelectedItem as ProjectItemViewModel)?.Model;
        }


        public Func<IProjectItem, string> GroupBy
        {
            get { return this.Dispatcher.Invoke(() => (Func<IProjectItem, string>)GetValue(GroupByProperty)); }
            set { SetValue(GroupByProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GroupByProperty =
            DependencyProperty.Register("GroupBy", typeof(Func<ProjectItem, string>), typeof(ProjectBox), new FrameworkPropertyMetadata(default(Func<ProjectItem, string>), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (d, e) =>
               {
                   ProjectBox control = d as ProjectBox;

                   if (control == null) return;

                   Func<IProjectItem, string> config = e.NewValue as Func<IProjectItem, string>;

               }));


        public void Refresh()
        {
            var projects = this.Dispatcher.Invoke(() =>
              {
                  return this.Projects;
              });

            if (projects == null)
                return;

            var order = projects.OrderBy(l => !l.IsFixed).ThenBy(l => l.UpdateTime);

            IEnumerable<IGrouping<string, IProjectItem>> groups = order.GroupBy(this.GroupBy ?? new Func<IProjectItem, string>(l =>
                    {
                        if (l.IsFixed) return "已固定";
                        if (l.UpdateTime.Date == DateTime.Now.Date) return "今天";
                        return "更早";
                    }));

            ObservableCollection<ProjectItemViewModel> models = new ObservableCollection<ProjectItemViewModel>();

            var list = groups.ToList();
            list.Sort((x, y) =>
            {
                if (x.Key == y.Key) return 0;
                if (x.Key == "已固定") return -1;
                if (x.Key == "更早") return 1;
                return 0;
            });

            foreach (IGrouping<string, IProjectItem> group in list)
            {
                foreach (IProjectItem item in group)
                {
                    models.Add(new ProjectItemViewModel(item) { GroupName = group.Key });
                }
            }

            Application.Current.Dispatcher.Invoke(() =>
            {
                //Do ：分组
                ICollectionView vw = CollectionViewSource.GetDefaultView(models);
                vw.GroupDescriptions.Clear();
                vw.GroupDescriptions.Add(new PropertyGroupDescription("GroupName"));

                //vw.SortDescriptions.Clear(); 
                //vw.SortDescriptions.Add(new SortDescription());
            });
            this.Dispatcher.Invoke(() =>
            {
                this.ItemsSource = models;
            });
        }
    }
}
