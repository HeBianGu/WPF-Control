// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control
global using System.Collections.Generic;
global using System.Collections.ObjectModel;
global using System.Collections.Specialized;
global using System.ComponentModel;
global using System.Linq;
global using System.Windows;
global using System.Windows.Controls;
global using System.Windows.Data;
global using System.Windows.Input;
using System.Windows.Threading;

namespace H.Modules.Project;

public partial class ProjectBox : ListBox
{
    static ProjectBox()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(ProjectBox), new FrameworkPropertyMetadata(typeof(ProjectBox)));
    }
    public ProjectBox()
    {
        {
            CommandBinding commandBinding = new CommandBinding(H.Mvvm.Commands.Commands.Delete);
            commandBinding.Executed += async (l, k) =>
            {
                if (k.Parameter is IProjectItem project)
                {
                    await IocProject.Instance.ShowDeleteProject(project);
                }
            };
            this.CommandBindings.Add(commandBinding);
        }

        {
            CommandBinding commandBinding = new CommandBinding(H.Mvvm.Commands.Commands.Edit);
            commandBinding.Executed += async (l, k) =>
            {
                if (k.Parameter is IProjectItem project)
                {
                    await IocProject.Instance.ShowEidtProject(project);
                }
            };
            this.CommandBindings.Add(commandBinding);
        }

        {
            CommandBinding commandBinding = new CommandBinding(H.Mvvm.Commands.Commands.Open);
            commandBinding.Executed += async (l, k) =>
            {
                if (k.Parameter is IProjectItem project)
                {
                    await IocProject.Instance.ShowOpenProject(project);
                }
            };
            commandBinding.CanExecute += (l, k) =>
            {
                k.CanExecute = k.Parameter is IProjectItem project ? project != IocProject.Instance.Current : false;
            };
            this.CommandBindings.Add(commandBinding);
        }

        {
            CommandBinding commandBinding = new CommandBinding(H.Mvvm.Commands.Commands.New);
            commandBinding.Executed += async (l, k) =>
            {
                await IocProject.Instance.ShowNewProject();
            };
            this.CommandBindings.Add(commandBinding);
        }

        {
            CommandBinding commandBinding = new CommandBinding(H.Mvvm.Commands.Commands.Refresh);
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
        get { return (IProjectItem)GetValue(SelectedProjectProperty); }
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
        DependencyProperty.Register("GroupBy", typeof(Func<ProjectItem, string>), typeof(ProjectBox), new FrameworkPropertyMetadata(default(Func<ProjectItem, string>), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));


    public void Refresh()
    {
        IEnumerable<IProjectItem> projects = this.Dispatcher.Invoke(() =>
          {
              return this.Projects;
          });

        if (projects == null)
            return;

        IOrderedEnumerable<IProjectItem> order = projects.OrderBy(l => !l.IsFixed).ThenByDescending(l => l.UpdateTime);
        IEnumerable<IGrouping<string, IProjectItem>> groups = order.GroupBy(this.GroupBy ?? new Func<IProjectItem, string>(l =>
                {
                    if (l.IsFixed) return "已固定";
                    return l.UpdateTime.Date == DateTime.Now.Date ? "今天" : "更早";
                }));
        ObservableCollection<ProjectItemViewModel> models = new ObservableCollection<ProjectItemViewModel>();
        this.Dispatcher.Invoke(() =>
        {
            this.ItemsSource = models;
        });

        List<IGrouping<string, IProjectItem>> list = groups.ToList();
        list.Sort((x, y) =>
        {
            if (x.Key == y.Key) return 0;
            if (x.Key == "已固定") return -1;
            return x.Key == "更早" ? 1 : 0;
        });
        Application.Current.Dispatcher.Invoke(() =>
        {
            //Do ：分组
            ICollectionView vw = CollectionViewSource.GetDefaultView(models);
            vw.GroupDescriptions.Clear();
            vw.GroupDescriptions.Add(new PropertyGroupDescription("GroupName"));

            //vw.SortDescriptions.Clear(); 
            //vw.SortDescriptions.Add(new SortDescription());
        });
        foreach (IGrouping<string, IProjectItem> group in list)
        {
            foreach (IProjectItem item in group)
            {
                this.Dispatcher.BeginInvoke(DispatcherPriority.Input, new Action(() =>
                          {
                              models.Add(new ProjectItemViewModel(item) { GroupName = group.Key });
                          }));
            }
        }
    }
}
