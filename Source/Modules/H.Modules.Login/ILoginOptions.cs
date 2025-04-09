namespace H.Modules.Login;

public interface ILoginOptions
{
    string AdminName { get; set; }
    string AdminPassword { get; set; }
    string LastPassword { get; set; }
    string LastUserName { get; set; }
    string Product { get; set; }
    double ProductFontSize { get; set; }
    bool Remember { get; set; }
    bool UseVisitor { get; set; }

    bool Load(out string message);
    void LoadDefault();
    bool Save(out string message);
}