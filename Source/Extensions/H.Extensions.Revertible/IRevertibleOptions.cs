namespace H.Extensions.Revertible;

public interface IRevertibleOptions
{
    bool AutoInitializedOnCreate { get; set; }
    int Capacity { get; set; }
}