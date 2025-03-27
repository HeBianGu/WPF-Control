namespace H.Services.Message.IODialog;

public interface IIOFileDialogOption
{
    const string defaultFilter = "所有文件(*.*)|*.*";
    const string defaultTitle = "打开文件";
    const string defaultImageFilter = "图片文件(*.jpg;*.png;*.bmp)|*.jpg;*.png;*.bmp";
    const string defaultTextFilter = "文本文件(*.txt;*.csv)|*.txt;*.csv";
    const string defaultExcelFilter = "Excel文件(*.xlsx;*.xls)|*.xlsx;*.xls";
    const string defaultWordFilter = "Word文件(*.doc;*.docx)|*.doc;*.docx";
    const string defaultPdfFilter = "PDF文件(*.pdf)|*.pdf";
    const string defaultZipFilter = "压缩文件(*.zip;*.rar)|*.zip;*.rar";
    const string defaultVideoFilter = "视频文件(*.mp4;*.avi)|*.mp4;*.avi";
    const string defaultAudioFilter = "音频文件(*.mp3;*.wav)|*.mp3;*.wav";
    const string defaultXmlFilter = "XML文件(*.xml)|*.xml";
    const string defaultJsonFilter = "JSON文件(*.json)|*.json";
    string Filter { get; set; }
    string Title { get; set; }
    string InitialDirectory { get; set; }
    bool RestoreDirectory { get; set; }
}

