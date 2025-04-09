namespace H.Mvvm.ViewModels.Base;

/// <summary>
/// 提供显示相关功能的可绑定基类。
/// </summary>
public abstract class DisplayBindableBase : CommandsBindableBase, IDable, IDisplayBindable
{
    /// <summary>
    /// 初始化 <see cref="DisplayBindableBase"/> 类的新实例。
    /// </summary>
    public DisplayBindableBase()
    {
        Type type = this.GetType();
        //this.Name = type.Name;
        DisplayAttribute display = type.GetCustomAttribute<DisplayAttribute>(true);
        if (display != null)
        {
            this.Name = display.Name ?? this.Name;
            this.GroupName = display.GroupName;
            this.Description = display.Description;
            int? od = display.GetOrder();
            if (od.HasValue)
                this.Order = od.Value;
            this.ShortName = display.ShortName;
        }
        IDAttribute id = type.GetCustomAttribute<IDAttribute>(true);
        this.ID = id?.ID ?? Guid.NewGuid().ToString();
        IconAttribute icon = type.GetCustomAttribute<IconAttribute>(true);
        this.Icon = icon?.Icon;
        LoadDefault();
    }

    /// <summary>
    /// 获取或设置一个值，指示对象是否已加载。
    /// </summary>
    [Browsable(false)]
    [System.Text.Json.Serialization.JsonIgnore]
    [System.Xml.Serialization.XmlIgnore]
    public bool IsLoaded { get; set; }

    /// <summary>
    /// 当对象加载完成时调用。
    /// </summary>
    /// <param name="obj">加载完成的对象。</param>
    protected override void Loaded(object obj)
    {
        base.Loaded(obj);
        this.IsLoaded = true;
    }

    private string _id;

    /// <summary>
    /// 获取或设置对象的唯一标识符。
    /// </summary>
    [Browsable(false)]
    public virtual string ID
    {
        get { return _id; }
        set
        {
            _id = value;
            RaisePropertyChanged();
        }
    }

    private string _name;

    /// <summary>
    /// 获取或设置对象的名称。
    /// </summary>
    [Browsable(false)]
    public virtual string Name
    {
        get { return _name; }
        set
        {
            _name = value;
            RaisePropertyChanged();
        }
    }

    private string _icon;

    /// <summary>
    /// 获取或设置对象的图标。
    /// </summary>
    [Browsable(false)]
    [Display(Name = "图标", GroupName = "常用")]
    public virtual string Icon
    {
        get { return _icon; }
        set
        {
            _icon = value;
            RaisePropertyChanged();
        }
    }

    private string _shortName;

    /// <summary>
    /// 获取或设置对象的简称。
    /// </summary>
    [System.Text.Json.Serialization.JsonIgnore]
    [System.Xml.Serialization.XmlIgnore]
    [Browsable(false)]
    public virtual string ShortName
    {
        get { return _shortName; }
        set
        {
            _shortName = value;
            RaisePropertyChanged();
        }
    }

    private string _groupName;

    /// <summary>
    /// 获取或设置对象的分组名称。
    /// </summary>
    [System.Text.Json.Serialization.JsonIgnore]
    [System.Xml.Serialization.XmlIgnore]
    [Browsable(false)]
    public virtual string GroupName
    {
        get { return _groupName; }
        set
        {
            _groupName = value;
            RaisePropertyChanged();
        }
    }

    private string _description;

    /// <summary>
    /// 获取或设置对象的描述。
    /// </summary>
    [System.Text.Json.Serialization.JsonIgnore]
    [System.Xml.Serialization.XmlIgnore]
    [Browsable(false)]
    public virtual string Description
    {
        get { return _description; }
        set
        {
            _description = value;
            RaisePropertyChanged();
        }
    }

    private int _order;

    /// <summary>
    /// 获取或设置对象的顺序。
    /// </summary>
    [System.Text.Json.Serialization.JsonIgnore]
    [System.Xml.Serialization.XmlIgnore]
    [Browsable(false)]
    public virtual int Order
    {
        get { return _order; }
        set
        {
            _order = value;
            RaisePropertyChanged();
        }
    }

    /// <summary>
    /// 获取加载默认值的命令。
    /// </summary>
    [System.Text.Json.Serialization.JsonIgnore]
    [System.Xml.Serialization.XmlIgnore]
    [Display(Name = "恢复默认")]
    [Browsable(false)]
    public virtual RelayCommand LoadDefaultCommand => new RelayCommand(x =>
    {
        LoadDefault();
    });

    /// <summary>
    /// 加载默认值。
    /// </summary>
    public virtual void LoadDefault()
    {
        PropertyInfo[] ps = GetType().GetProperties();
        foreach (PropertyInfo p in ps)
        {
            DefaultValueAttribute d = p.GetCustomAttribute<DefaultValueAttribute>();
            if (d == null) 
                continue;
            if (typeof(IConvertible).IsAssignableFrom(p.PropertyType))
            {
                object value = Convert.ChangeType(d.Value, p.PropertyType);
                p.SetValue(this, value);
            }
            else
            {
                p.SetValue(this, d.Value);
            }
        }
    }
}
