
namespace H.Controls.TagBox;

public interface ITagOptions
{
    string SplitChars { get; set; }
    ObservableCollection<Tag> Tags { get; set; }
}