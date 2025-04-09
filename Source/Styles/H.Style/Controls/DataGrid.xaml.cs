namespace H.Styles.Controls;

public class DataGridKeys
{
    public static ComponentResourceKey Default => new ComponentResourceKey(typeof(DataGridKeys), "S.DataGrid.Default");
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
