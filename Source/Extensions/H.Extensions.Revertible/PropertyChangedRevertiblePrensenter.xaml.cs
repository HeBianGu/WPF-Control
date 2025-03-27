namespace H.Extensions.Revertible;

public abstract class PropertyChangedRevertiblePrensenterBase
{

}
public class PropertyChangedRevertiblePrensenter<T> : PropertyChangedRevertiblePrensenterBase
{
    public PropertyChangedRevertiblePrensenter(string perpertyName, T oldValue, T newValue)
    {
        this.PropertyName = perpertyName;
        this.OldValue = oldValue;
        this.NewValue = newValue;
    }
    public string PropertyName { get; set; }
    public T OldValue { get; set; }
    public T NewValue { get; set; }
}
