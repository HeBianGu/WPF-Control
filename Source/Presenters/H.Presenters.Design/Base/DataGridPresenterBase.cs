// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Collections;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace H.Presenters.Design.Base;

public abstract class DataGridPresenterBase : DesignPresenter
{
    public override void LoadDefault()
    {
        base.LoadDefault();
        this.GridHeaderBackground = Brushes.Black;
        this.GridHeaderForeground = Brushes.White;
        this.HorizontalGridLinesBrush = Brushes.LightGray;
        this.VerticalGridLinesBrush = Brushes.LightGray;
        this.GridForeground = Brushes.Black;
        this.GridBackground = Brushes.White;
        this.AlternatingRowBackground = Brushes.WhiteSmoke;
        this.ColumnHeaderHeight = 40;
        this.RowHeight = 35;
        this.GridBorderBrush = Brushes.LightGray;
        this.ColumnHorizontalContentAlignment = HorizontalAlignment.Left;
        this.CellHorizontalContentAlignment = HorizontalAlignment.Left;
    }

    private Brush _gridForeground;
    [Display(Name = "表格文本颜色", GroupName = "样式")]
    public Brush GridForeground
    {
        get { return _gridForeground; }
        set
        {
            _gridForeground = value;
            RaisePropertyChanged();
        }
    }


    private Brush _gridBackground;
    [Display(Name = "表格背景色", GroupName = "样式")]
    public Brush GridBackground
    {
        get { return _gridBackground; }
        set
        {
            _gridBackground = value;
            RaisePropertyChanged();
        }
    }


    private Brush _gridHeaderBackground;
    [Display(Name = "列头背景色", GroupName = "样式")]
    public Brush GridHeaderBackground
    {
        get { return _gridHeaderBackground; }
        set
        {
            _gridHeaderBackground = value;
            RaisePropertyChanged();
        }
    }


    private Brush _gridHeaderForeground;
    [Display(Name = "列头文本色", GroupName = "样式")]
    public Brush GridHeaderForeground
    {
        get { return _gridHeaderForeground; }
        set
        {
            _gridHeaderForeground = value;
            RaisePropertyChanged();
        }
    }

    private Brush _gridborderBrush;
    [Display(Name = "表格边框颜色", GroupName = "样式")]
    public Brush GridBorderBrush
    {
        get { return _gridborderBrush; }
        set
        {
            _gridborderBrush = value;
            RaisePropertyChanged();
        }
    }

    private Brush _alternatingRowBackground;
    [Display(Name = "表格换行色", GroupName = "样式")]
    public Brush AlternatingRowBackground
    {
        get { return _alternatingRowBackground; }
        set
        {
            _alternatingRowBackground = value;
            RaisePropertyChanged();
        }
    }


    private Brush _verticalGridLinesBrush;
    [Display(Name = "垂直分割线色", GroupName = "样式")]
    public Brush VerticalGridLinesBrush
    {
        get { return _verticalGridLinesBrush; }
        set
        {
            _verticalGridLinesBrush = value;
            RaisePropertyChanged();
        }
    }


    private Brush _horizontalGridLinesBrush;
    [Display(Name = "水平分割线色", GroupName = "样式")]
    public Brush HorizontalGridLinesBrush
    {
        get { return _horizontalGridLinesBrush; }
        set
        {
            _horizontalGridLinesBrush = value;
            RaisePropertyChanged();
        }
    }


    private HorizontalAlignment _columnHorizontalContentAlignment;
    [Display(Name = "列头水平停靠", GroupName = "常用,样式")]
    public HorizontalAlignment ColumnHorizontalContentAlignment
    {
        get { return _columnHorizontalContentAlignment; }
        set
        {
            _columnHorizontalContentAlignment = value;
            RaisePropertyChanged();
        }
    }

    private HorizontalAlignment _cellHorizontalContentAlignment;
    [Display(Name = "单元水平停靠", GroupName = "常用,样式")]
    public HorizontalAlignment CellHorizontalContentAlignment
    {
        get { return _cellHorizontalContentAlignment; }
        set
        {
            _cellHorizontalContentAlignment = value;
            RaisePropertyChanged();
        }
    }

    private int _columnHeaderHeight;
    [Display(Name = "表格列高", GroupName = "常用,样式")]
    public int ColumnHeaderHeight
    {
        get { return _columnHeaderHeight; }
        set
        {
            _columnHeaderHeight = value;
            RaisePropertyChanged();
        }
    }

    private int _rowHeight;
    [Display(Name = "表格行高", GroupName = "常用,样式")]
    public int RowHeight
    {
        get { return _rowHeight; }
        set
        {
            _rowHeight = value;
            RaisePropertyChanged();
        }
    }


    private IEnumerable _itemsSource;
    [System.Text.Json.Serialization.JsonIgnore]

    [XmlIgnore]
    [Browsable(false)]
    public IEnumerable ItemsSource
    {
        get { return _itemsSource; }
        set
        {
            _itemsSource = value;
            RaisePropertyChanged();
        }
    }

    private ObservableCollection<ColumnPropertyInfo> _columnPropertyInfos = new ObservableCollection<ColumnPropertyInfo>();
    [System.Text.Json.Serialization.JsonIgnore]

    [XmlIgnore]
    [ReadOnly(true)]
    [Display(Name = "列头设置", GroupName = "常用,数据")]
    public ObservableCollection<ColumnPropertyInfo> ColumnPropertyInfos
    {
        get { return _columnPropertyInfos; }
        set
        {
            _columnPropertyInfos = value;
            RaisePropertyChanged();
        }
    }

    public void Refresh()
    {
        this.ColumnPropertyInfos = new ObservableCollection<ColumnPropertyInfo>(this.ColumnPropertyInfos);
    }
}
