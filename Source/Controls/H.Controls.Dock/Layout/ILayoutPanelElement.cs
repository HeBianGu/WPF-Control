﻿








namespace H.Controls.Dock.Layout
{
    /// <summary>Interface definition for a <see cref="ILayoutElement"/> that supports a visibility property.</summary>
    public interface ILayoutPanelElement : ILayoutElement
    {
        /// <summary>Gets whether the <see cref="ILayoutElement"/> is currently visible or not.</summary>
        bool IsVisible { get; }
    }
}