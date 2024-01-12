using System.Windows.Media;

namespace H.Controls.TagBox
{
    public interface ITag
    {
        Brush Background { get; set; }
        string Description { get; set; }
        string Name { get; set; }
        string GroupName { get; set; }
        int Order { get; set; }
    }
}
