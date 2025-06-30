// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Styles.Controls;

public class DataGridKeys
{
    public static ComponentResourceKey Default => new ComponentResourceKey(typeof(DataGridKeys), "S.DataGrid.Default");
}

public class DataGridRowKeys
{
    public static ComponentResourceKey Default => new ComponentResourceKey(typeof(DataGridRowKeys), "S.DataGridRow.Default");
}

public class DataGridCheckBoxColumn : System.Windows.Controls.DataGridCheckBoxColumn
{
    public DataGridCheckBoxColumn()
    {
        this.ElementStyle = null;
        this.EditingElementStyle = null;
    }
}

public class DataGridTextColumn : System.Windows.Controls.DataGridTextColumn
{
    public DataGridTextColumn()
    {
        this.ElementStyle = null;
        this.EditingElementStyle = null;
    }
}

public class DataGridComboBoxColumn : System.Windows.Controls.DataGridComboBoxColumn
{
    public DataGridComboBoxColumn()
    {
        this.ElementStyle = null;
        this.EditingElementStyle = null;
    }
}

public class DataGridHyperlinkColumn : System.Windows.Controls.DataGridHyperlinkColumn
{
    public DataGridHyperlinkColumn()
    {
        this.ElementStyle = null;
        this.EditingElementStyle = null;
    }
}
