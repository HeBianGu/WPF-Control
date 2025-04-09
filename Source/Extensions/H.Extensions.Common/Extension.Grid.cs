// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace H.Extensions.Common;

public static class GridExtension
{
    public static bool HitTestRow(this Grid grid, out int row)
    {
        Point p = Mouse.GetPosition(grid);
        System.Diagnostics.Debug.WriteLine(p);
        return grid.HitTestRow(p, out row);
    }

    public static bool HitTestColumn(this Grid grid, out int column)
    {
        Point p = Mouse.GetPosition(grid);
        System.Diagnostics.Debug.WriteLine(p);
        return grid.HitTestColumn(p, out column);
    }

    public static bool HitTestRow(this Grid grid, Point point, out int row)
    {
        row = 0;
        if (point.Y < 0)
        {
            row = 0;
            return false;
        }
        if (point.Y > grid.ActualHeight)
        {
            row = grid.RowDefinitions.Count;
            return false;
        }

        for (int i = 0; i < grid.RowDefinitions.Count; i++)
        {
            if (grid.RowDefinitions[i].Offset > point.Y)
                return true;
            row = i;
        }
        row = grid.RowDefinitions.Count;
        return true;
    }

    public static bool HitTestColumn(this Grid grid, Point point, out int column)
    {
        column = 0;
        if (point.Y < 0)
        {
            column = 0;
            return false;
        }
        if (point.Y > grid.ActualWidth)
        {
            column = grid.ColumnDefinitions.Count;
            return false;
        }

        for (int i = 0; i < grid.ColumnDefinitions.Count; i++)
        {
            if (grid.ColumnDefinitions[i].Offset > point.X)
                return true;
            column = i;
        }
        column = grid.ColumnDefinitions.Count;
        return true;
    }

    public static bool HitTestRowColumn(this Grid grid, Point point, out int row, out int column)
    {
        bool r = true;
        if (grid.HitTestRow(point, out row))
            r = false;
        if (grid.HitTestColumn(point, out column))
            r = false;
        return r;
    }
}
