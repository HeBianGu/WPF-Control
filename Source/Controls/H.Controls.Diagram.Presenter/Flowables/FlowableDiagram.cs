// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control
using H.Mvvm;
using H.Services.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace H.Controls.Diagram.Presenter.Flowables;

public interface IFlowableDiagram
{
    void Reset();
    Task<bool?> Start();
    void Stop();
}

//public class FlowableDiagram : Diagram, IFlowableDiagram
//{
//    public FlowableDiagram()
//    {

//        {
//            CommandBinding binding = new CommandBinding(DiagramCommands.Start);
//            binding.Executed += async (l, k) =>
//            {
//                await this.Start();
//            };
//            binding.CanExecute += (l, k) =>
//            {
//                k.CanExecute = this.State.CanStart();
//            };
//            this.CommandBindings.Add(binding);
//        }

//        {
//            CommandBinding binding = new CommandBinding(DiagramCommands.Stop);
//            binding.Executed += (l, k) =>
//            {
//                this.Stop();
//            };
//            binding.CanExecute += (l, k) =>
//            {
//                k.CanExecute = this.State.CanStop();
//            };
//            this.CommandBindings.Add(binding);
//        }

//        {
//            CommandBinding binding = new CommandBinding(DiagramCommands.Reset);
//            binding.Executed += (l, k) =>
//            {
//                this.Reset();
//            };
//            binding.CanExecute += (l, k) =>
//            {
//                k.CanExecute = this.State.CanReset();
//            };
//            this.CommandBindings.Add(binding);
//        }

//        {
//            CommandBinding binding = new CommandBinding(DiagramCommands.Clear);
//            binding.Executed += (l, k) =>
//            {
//                this.Clear();
//            };
//            binding.CanExecute += (l, k) =>
//            {
//                k.CanExecute = this.State.CanClear();
//            };
//            this.CommandBindings.Add(binding);
//        }
//    }

//    [Display(Name = "启用开始点接口检查", GroupName = "流程控制")]
//    public bool UseStartNodeOnly
//    {
//        get { return (bool)GetValue(UseStartNodeOnlyProperty); }
//        set { SetValue(UseStartNodeOnlyProperty, value); }
//    }
//    public static readonly DependencyProperty UseStartNodeOnlyProperty =
//        DependencyProperty.Register("UseStartNodeOnly", typeof(bool), typeof(FlowableDiagram), new FrameworkPropertyMetadata(default(bool)));

//    [Display(Name = "执行方式", GroupName = "流程控制")]
//    public DiagramFlowableMode FlowableMode
//    {
//        get { return (DiagramFlowableMode)GetValue(FlowableModeProperty); }
//        set { SetValue(FlowableModeProperty, value); }
//    }


//    public static readonly DependencyProperty FlowableModeProperty =
//        DependencyProperty.Register("FlowableMode", typeof(DiagramFlowableMode), typeof(FlowableDiagram), new FrameworkPropertyMetadata(DiagramFlowableMode.Link));

//    [Display(Name = "执行状态", GroupName = "流程控制")]
//    public DiagramFlowableState State
//    {
//        get { return (DiagramFlowableState)GetValue(StateProperty); }
//        set { SetValue(StateProperty, value); }
//    }


//    public static readonly DependencyProperty StateProperty =
//        DependencyProperty.Register("State", typeof(DiagramFlowableState), typeof(FlowableDiagram), new FrameworkPropertyMetadata(default(DiagramFlowableState)));

//    [Display(Name = "执行时节点自动选中", GroupName = "流程控制")]
//    public bool UseFlowableSelectToRunning
//    {
//        get { return (bool)GetValue(UseFlowableSelectToRunningProperty); }
//        set { SetValue(UseFlowableSelectToRunningProperty, value); }
//    }

//    public static readonly DependencyProperty UseFlowableSelectToRunningProperty =
//        DependencyProperty.Register("UseFlowableSelectToRunning", typeof(bool), typeof(FlowableDiagram), new FrameworkPropertyMetadata(false));

//    [Display(Name = "执行时节点自动缩放", GroupName = "流程控制")]
//    public DiagramFlowableZoomMode FlowableZoomMode
//    {
//        get { return (DiagramFlowableZoomMode)GetValue(FlowableZoomModeProperty); }
//        set { SetValue(FlowableZoomModeProperty, value); }
//    }

//    public static readonly DependencyProperty FlowableZoomModeProperty =
//        DependencyProperty.Register("FlowableZoomMode", typeof(DiagramFlowableZoomMode), typeof(FlowableDiagram), new FrameworkPropertyMetadata(default(DiagramFlowableZoomMode)));

//    public static readonly RoutedEvent InvokingPartRoutedEvent =
//        EventManager.RegisterRoutedEvent("InvokingPart", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(FlowableDiagram));
//    public event RoutedEventHandler InvokingPart
//    {
//        add { this.AddHandler(InvokingPartRoutedEvent, value); }
//        remove { this.RemoveHandler(InvokingPartRoutedEvent, value); }
//    }


//    public static readonly RoutedEvent InvokedPartRoutedEvent =
//        EventManager.RegisterRoutedEvent("InvokedPart", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(FlowableDiagram));
//    public event RoutedEventHandler InvokedPart
//    {
//        add { this.AddHandler(InvokedPartRoutedEvent, value); }
//        remove { this.RemoveHandler(InvokedPartRoutedEvent, value); }
//    }

//    //protected void OnInvokedPart(Part part)
//    //{
//    //    var args = new RoutedEventArgs<Part>(InvokedPartRoutedEvent, this, part);
//    //    this.RaiseEvent(args);
//    //}

//    //protected void OnInvokingPart(Part part)
//    //{
//    //    var args = new RoutedEventArgs<Part>(InvokingPartRoutedEvent, this, part);
//    //    this.RaiseEvent(args);

//    //    this.FocusPart(part);
//    //}

//    //private void FocusPart(Part part)
//    //{
//    //    if (this.FlowableZoomMode == DiagramFlowableZoomMode.Rect)
//    //        this.ZoomTo(part.Bound);
//    //    else if (this.FlowableZoomMode == DiagramFlowableZoomMode.Center)
//    //    {
//    //        Point point = part.Bound.GetCenter();
//    //        //zoombox.ZoomToCenter(part.Bound.BottomRight);
//    //    }
//    //    if (this.UseFlowableSelectToRunning)
//    //        part.IsSelected = true;
//    //}

//    //public void Stop() => this.Nodes.Stop();

//    //public async Task<bool?> Start()
//    //{
//    //    Node node = this.Nodes.GetStartNode(out string message);
//    //    if (node == null)
//    //    {
//    //        this.Message = message;
//    //        IocMessage.Notify?.ShowInfo(message);
//    //        return false;
//    //    }
//    //    this.State = DiagramFlowableState.Running;
//    //    var b = await node.Start(this.FlowableMode, OnInvokingPart, OnInvokedPart);
//    //    this.State = b == null ? DiagramFlowableState.Canceled : b == true ? DiagramFlowableState.Success : DiagramFlowableState.Error;
//    //    this.Message = message;
//    //    H.Mvvm.Commands.InvalidateRequerySuggested();
//    //    if (!string.IsNullOrEmpty(message))
//    //    {
//    //        IocMessage.Notify?.ShowInfo(message);
//    //        return false;
//    //    }
//    //    return true;
//    //    //return await this.Nodes.Start(this.FlowableMode, 
//    //    //    x => this.State = x, 
//    //    //    OnInvokingPart, 
//    //    //    OnInvokedPart);
//    //}

//    //public void Reset()
//    //{
//    //    this.Nodes.Reset();
//    //    this.State = DiagramFlowableState.None;
//    //}
//}
