// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Mvvm;
using H.Services.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace H.Controls.Diagram.Flowables;

public interface IFlowableDiagram
{
    void Reset();
    Task<string> Start();
    void Stop();
}

public class FlowableDiagram : Diagram, IFlowableDiagram
{
    public FlowableDiagram()
    {

        {
            CommandBinding binding = new CommandBinding(DiagramCommands.Start);
            binding.Executed += async (l, k) =>
            {
                string message = await this.Start();
                if (!string.IsNullOrEmpty(message))
                    IocMessage.Notify?.ShowInfo(message);
            };
            binding.CanExecute += (l, k) =>
            {
                k.CanExecute = this.State.CanStart();
            };
            this.CommandBindings.Add(binding);
        }

        {
            CommandBinding binding = new CommandBinding(DiagramCommands.Stop);
            binding.Executed += (l, k) =>
            {
                this.Stop();
            };
            binding.CanExecute += (l, k) =>
            {
                k.CanExecute = this.State.CanStop();
            };
            this.CommandBindings.Add(binding);
        }

        {
            CommandBinding binding = new CommandBinding(DiagramCommands.Reset);
            binding.Executed += (l, k) =>
            {
                this.Reset();
            };
            binding.CanExecute += (l, k) =>
            {
                k.CanExecute = this.State.CanReset();
            };
            this.CommandBindings.Add(binding);
        }

        {
            CommandBinding binding = new CommandBinding(DiagramCommands.Clear);
            binding.Executed += (l, k) =>
            {
                this.Clear();
            };
            binding.CanExecute += (l, k) =>
            {
                k.CanExecute = this.State.CanClear();
            };
            this.CommandBindings.Add(binding);
        }
    }

    [Display(Name = "启用开始点接口检查", GroupName = "流程控制")]
    public bool UseStartNodeOnly
    {
        get { return (bool)GetValue(UseStartNodeOnlyProperty); }
        set { SetValue(UseStartNodeOnlyProperty, value); }
    }
    public static readonly DependencyProperty UseStartNodeOnlyProperty =
        DependencyProperty.Register("UseStartNodeOnly", typeof(bool), typeof(FlowableDiagram), new FrameworkPropertyMetadata(default(bool)));

    [Display(Name = "执行方式", GroupName = "流程控制")]
    public DiagramFlowableMode FlowableMode
    {
        get { return (DiagramFlowableMode)GetValue(FlowableModeProperty); }
        set { SetValue(FlowableModeProperty, value); }
    }


    public static readonly DependencyProperty FlowableModeProperty =
        DependencyProperty.Register("FlowableMode", typeof(DiagramFlowableMode), typeof(FlowableDiagram), new FrameworkPropertyMetadata(DiagramFlowableMode.Link));

    [Display(Name = "执行状态", GroupName = "流程控制")]
    public DiagramFlowableState State
    {
        get { return (DiagramFlowableState)GetValue(StateProperty); }
        set { SetValue(StateProperty, value); }
    }


    public static readonly DependencyProperty StateProperty =
        DependencyProperty.Register("State", typeof(DiagramFlowableState), typeof(FlowableDiagram), new FrameworkPropertyMetadata(default(DiagramFlowableState)));

    [Display(Name = "执行时节点自动选中", GroupName = "流程控制")]
    public bool UseFlowableSelectToRunning
    {
        get { return (bool)GetValue(UseFlowableSelectToRunningProperty); }
        set { SetValue(UseFlowableSelectToRunningProperty, value); }
    }

    public static readonly DependencyProperty UseFlowableSelectToRunningProperty =
        DependencyProperty.Register("UseFlowableSelectToRunning", typeof(bool), typeof(FlowableDiagram), new FrameworkPropertyMetadata(false));

    [Display(Name = "执行时节点自动缩放", GroupName = "流程控制")]
    public DiagramFlowableZoomMode FlowableZoomMode
    {
        get { return (DiagramFlowableZoomMode)GetValue(FlowableZoomModeProperty); }
        set { SetValue(FlowableZoomModeProperty, value); }
    }

    public static readonly DependencyProperty FlowableZoomModeProperty =
        DependencyProperty.Register("FlowableZoomMode", typeof(DiagramFlowableZoomMode), typeof(FlowableDiagram), new FrameworkPropertyMetadata(default(DiagramFlowableZoomMode)));

    public static readonly RoutedEvent InvokingPartRoutedEvent =
        EventManager.RegisterRoutedEvent("InvokingPart", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(FlowableDiagram));
    public event RoutedEventHandler InvokingPart
    {
        add { this.AddHandler(InvokingPartRoutedEvent, value); }
        remove { this.RemoveHandler(InvokingPartRoutedEvent, value); }
    }

    protected void OnInvokingPart(Part part)
    {
        RoutedEventArgs<Part> args = new RoutedEventArgs<Part>(InvokingPartRoutedEvent, this, part);
        this.RaiseEvent(args);
    }

    public void Stop() => this.Nodes.Stop();

    public async Task<string> Start()
    {
        return await this.Nodes.Start(this.FlowableMode, x => this.State = x, x =>
          {
              this.OnInvokingPart(x);
              if (this.FlowableZoomMode == DiagramFlowableZoomMode.Rect)
                  this.ZoomTo(x.Bound);
              else if (this.FlowableZoomMode == DiagramFlowableZoomMode.Center)
              {
                  Point point = x.Bound.GetCenter();
                  //zoombox.ZoomToCenter(part.Bound.BottomRight);
              }
              if (this.UseFlowableSelectToRunning)
                  x.IsSelected = true;
          });
    }

    public void Reset()
    {
        this.Nodes.Reset();
        this.State = DiagramFlowableState.None;
    }
}
