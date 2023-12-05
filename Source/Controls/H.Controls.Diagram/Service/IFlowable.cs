// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;

namespace H.Controls.Diagram
{
    public interface IFlowable : IDisposable
    {
        bool UseInfoLogger { get; set; }
        //IFlowable FromFlowable { get; }
        FlowableState State { get; set; }
        void Clear();
    }

    public static class FlowableExtension
    {
        //public static T GetFlowable<T>(this Node flowable, Predicate<T> predicate = null) where T : class, IFlowable
        //{
        //    //if (flowable.FromFlowable == null) return null;

        //    //T t = flowable.FromFlowable as T;

        //    //if (t != null)
        //    //    if (predicate?.Invoke(t) != false) return t;

        //    //return flowable.FromFlowable.GetFlowable<T>(predicate);

        //    return null;
        //}
    }
}
