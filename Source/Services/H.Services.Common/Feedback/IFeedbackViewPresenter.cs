namespace H.Services.Common.Feedback;

public interface IFeedbackViewPresenter
{
    string Contact { get; set; }
    string Text { get; set; }
    string Title { get; set; }
    ObservableCollection<string> Files { get; set; }
}
