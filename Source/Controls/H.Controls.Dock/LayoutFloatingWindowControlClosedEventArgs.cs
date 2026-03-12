/************************************************************************
  AvalonDock

  Copyright (C) 2007-2013 Xceed Software Inc.

  This program is provided to you under the terms of the Microsoft Public
  License (Ms-PL) as published at https://opensource.org/licenses/MS-PL
************************************************************************/
using H.Controls.Dock.Controls;
using System;

namespace H.Controls.Dock
{
    public sealed class LayoutFloatingWindowControlClosedEventArgs : EventArgs
    {
        public LayoutFloatingWindowControlClosedEventArgs(LayoutFloatingWindowControl layoutFloatingWindowControl)
        {
            this.LayoutFloatingWindowControl = layoutFloatingWindowControl;
        }

        public LayoutFloatingWindowControl LayoutFloatingWindowControl { get; }
    }

}
