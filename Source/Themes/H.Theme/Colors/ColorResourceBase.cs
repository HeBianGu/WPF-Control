namespace H.Themes.Colors;

public abstract class ColorResourceBase : IColorResource
{
    public ColorResourceBase()
    {
        var display = TypeDescriptor.GetAttributes(this).OfType<DisplayAttribute>().FirstOrDefault();
        this.Name = display?.Name ?? this.GetType().Name;
        this.GroupName = display?.GroupName ?? null;
        this.Order = display?.Order ?? 0;
        this.Prompt = display?.Prompt ?? null;
        this.Description = display?.Description ?? null;
    }

    public bool IsDark { get; set; }
    public string Name { get; }
    public abstract ResourceDictionary Resource { get; }
    public string GroupName { get; }
    public string Prompt { get; set; }
    public int Order { get; set; }
    public string Description { get; set; }
    public override string ToString()
    {
        return this.Name;
    }
}
