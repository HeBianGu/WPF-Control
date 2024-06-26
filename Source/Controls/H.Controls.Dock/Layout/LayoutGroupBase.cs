﻿








using System;
using System.Xml.Serialization;

namespace H.Controls.Dock.Layout
{
    /// <summary>
    /// Provides a minimal implementation for a layout model that can inform subscribers via event
    /// if and when their children collection or tree of children has changed.
    /// </summary>
    [Serializable]
    public abstract class LayoutGroupBase : LayoutElement
    {
        #region Events

        /// <summary>Raise an event to inform supscribers that the children collection down the tree of this object has changed.</summary>
        [field: NonSerialized]
        [field: XmlIgnore]
        public event EventHandler ChildrenCollectionChanged;

        /// <summary>
        /// Implements an event to make parents update their children up the tree.
        /// Otherwise, they will not be redrawn.
        /// </summary>
        [field: NonSerialized]
        [field: XmlIgnore]
        public event EventHandler<ChildrenTreeChangedEventArgs> ChildrenTreeChanged;

        #endregion Events

        #region Internal Methods

        /// <summary>
        /// Raises an event to make parents update their children up the tree.
        /// Otherwise, they will not be redrawn.
        /// </summary>
        internal void RaiseChildrenTreeChanged()
        {
            OnChildrenTreeChanged(ChildrenTreeChange.DirectChildrenChanged);
            LayoutGroupBase parentGroup = this.Parent as LayoutGroupBase;
            if (parentGroup != null)
                parentGroup.RaiseChildrenTreeChanged();
        }

        /// <summary>Raise an event to inform supscribers that the children collection down the tree of this object has changed.</summary>
        protected virtual void OnChildrenCollectionChanged()
        {
            if (ChildrenCollectionChanged != null)
                ChildrenCollectionChanged(this, EventArgs.Empty);
        }

        /// <summary>Provides an opportuntiy for inheriting classes to execute custom code when the <see cref="ChildrenTreeChange"/> event is raised.</summary>
        /// <param name="change"></param>
        protected virtual void OnChildrenTreeChanged(ChildrenTreeChange change)
        {
            if (ChildrenTreeChanged != null)
                ChildrenTreeChanged(this, new ChildrenTreeChangedEventArgs(change));
        }

        protected void NotifyChildrenTreeChanged(ChildrenTreeChange change)
        {
            OnChildrenTreeChanged(change);
            LayoutGroupBase parentGroup = this.Parent as LayoutGroupBase;
            if (parentGroup != null)
                parentGroup.NotifyChildrenTreeChanged(ChildrenTreeChange.TreeChanged);
        }

        #endregion Internal Methods
    }
}