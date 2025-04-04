
global using H.Controls.Form;
global using H.Controls.Form.PropertyItem.TextPropertyItems;
global using OpenCvSharp.Dnn;
global using System.Collections.Generic;
global using System.IO;
using H.Services.Common;
using System.Threading.Tasks;

namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Yolovs;

/// <summary>
/// https://pjreddie.com/darknet/yolo/
/// https://www.pudn.com/news/6228dd3e9ddf223e1ad298f1.html
/// https://github.com/pjreddie/darknet
/// https://pjreddie.com/darknet/yolo/
/// </summary>

[Display(Name = "Yolov3模型检测", GroupName = "多目标检测", Description = "目标检测", Order = 0)]
public class Yolov3 : YolovOpenCVNodeDataBase
{
    public override void LoadDefault()
    {
        base.LoadDefault();
        //this.CfgFilePath = this.GetDataPath("Data\\Yolov3\\yolov3.cfg");
        this.CfgFilePath = this.GetDataPath("Data\\Yolov3\\yolov3-tiny.cfg");
        this.NameFilePath = this.GetDataPath("Data\\Yolov3\\coco.names");
        //this.WeightFilePath = @"D:\\Download\\YoloWrapper-WPF-master\\YoloWrapper-WPF-master\\DeepLearning\\Assets\\Weights\\yolov3.weights";
        //this.WeightFilePath = @"D:\Download\yolov3-tiny.weights";
    }

    private string _cfgFilePath;
    [Required]
    [Display(Name = "Cfg路径", GroupName = "数据")]
    [PropertyItem(typeof(OpenFileDialogPropertyItem))]
    public string CfgFilePath
    {
        get { return _cfgFilePath; }
        set
        {
            _cfgFilePath = value;
            RaisePropertyChanged();
        }
    }

    private string _weightFilePath;
    [Required]
    [Display(Name = "Weight路径", GroupName = "数据")]
    [PropertyItem(typeof(OpenFileDialogPropertyItem))]
    public string WeightFilePath
    {
        get { return _weightFilePath; }
        set
        {
            _weightFilePath = value;
            RaisePropertyChanged();
        }
    }

    private string _nameFilePath;
    [Required]
    [Display(Name = "Name路径", GroupName = "数据")]
    [PropertyItem(typeof(OpenFileDialogPropertyItem))]
    public string NameFilePath
    {
        get { return _nameFilePath; }
        set
        {
            _nameFilePath = value;
            RaisePropertyChanged();
        }
    }

    private float _threshold = 0.5f;
    [DefaultValue(0.5f)]
    [Display(Name = "置信度阈值", GroupName = "数据")]
    public float Threshold
    {
        get { return _threshold; }
        set
        {
            _threshold = value;
            RaisePropertyChanged();
        }
    }

    private float _nmsThreshold = 0.3f;
    [DefaultValue(0.3f)]
    [Display(Name = "nms阈值", GroupName = "数据")]
    public float NmsThreshold
    {
        get { return _nmsThreshold; }
        set
        {
            _nmsThreshold = value;
            RaisePropertyChanged();
        }
    }

    protected override async Task<IFlowableResult> BeforeInvokeAsync(IFlowableLinkData previors, IFlowableDiagramData diagram)
    {
        if (!File.Exists(this.WeightFilePath) || !File.Exists(this.CfgFilePath) || !File.Exists(this.NameFilePath))
        {
            var r = await System.Windows.Application.Current.Dispatcher.Invoke(async () =>
            {
                return await IocMessage.Form?.ShowEdit(this, null, null, x =>
                 {
                     x.UsePropertyNames = $"{nameof(WeightFilePath)},{nameof(CfgFilePath)},{nameof(NameFilePath)},";
                 });
            });

            if (r != true)
                return this.Error("训练模型不存在：https://pjreddie.com/media/files/yolov3.weights 请先下载");
        }
        return await base.BeforeInvokeAsync(previors, diagram);
    }

    protected override FlowableResult<Mat> Invoke(ISrcImageNodeData srcImageNodeData, IOpenCVNodeData from, IFlowableDiagramData diagram)
    {
        string[] lables = File.ReadAllLines(this.NameFilePath).ToArray();

        Net net = CvDnn.ReadNetFromDarknet(this.CfgFilePath, this.WeightFilePath);
        //读入模型和设置
        net.SetPreferableBackend(Backend.OPENCV); // 3:DNN_BACKEND_OPENCV 
        net.SetPreferableTarget(0); //dnn target cpu
        //var org = Cv2.ImRead(this.FilePath);

        Mat org = from.Mat;
        //生成blob, 块尺寸可以是320/416/608
        Mat blob = CvDnn.BlobFromImage(org, 1.0 / 255, new Size(320, 320), new Scalar(), true, false);
        // 输入数据
        net.SetInput(blob);
        //获得输出层名
        string[] outNames = net.GetUnconnectedOutLayersNames();
        //转换成 Mat[]
        Mat[] outs = outNames.Select(_ => new Mat()).ToArray();
        net.Forward(outs, outNames);
        //从输出中获得最佳的结果
        //GetBestResult(outs, org, threshold, nmsThreshold);
        //for nms
        List<int> classIds = new List<int>();
        List<float> confidences = new List<float>();
        List<float> probabilities = new List<float>();
        List<Rect2d> boxes = new List<Rect2d>();

        int w = org.Width;
        int h = org.Height;
        const int prefix = 5;   //分类概率

        foreach (Mat prob in outs)
        {
            for (int i = 0; i < prob.Rows; i++)
            {
                float confidence = prob.At<float>(i, 4);
                if (confidence > this.Threshold) //置信度大于阈值
                {
                    //获得识别概率
                    Cv2.MinMaxLoc(prob.Row(i).ColRange(prefix, prob.Cols), out _, out Point max);
                    int classes = max.X;
                    float probability = prob.At<float>(i, classes + prefix);

                    if (probability > this.Threshold) //概率大于阈值
                    {
                        float centerX = prob.At<float>(i, 0) * w;
                        float centerY = prob.At<float>(i, 1) * h;
                        float width = prob.At<float>(i, 2) * w;
                        float height = prob.At<float>(i, 3) * h;

                        //准备nms(非极大值抑制)数据
                        classIds.Add(classes);
                        confidences.Add(confidence);
                        probabilities.Add(probability);
                        boxes.Add(new Rect2d(centerX, centerY, width, height));
                    }
                }
            }
        }

        //nms(非极大值抑制)提取分数最高的
        //去除重叠和低置信度的目标框
        CvDnn.NMSBoxes(boxes, confidences, this.Threshold, this.NmsThreshold, out int[] indices);

        foreach (int i in indices)
        {
            //画出目标方框并标注置信度和分类标签
            Rect2d box = boxes[i];
            //Draw(image, classIds[i], confidences[i], probabilities[i], box.X, box.Y, box.Width, box.Height);
            //标签字符串
            int classes = classIds[i];

            string label = string.Format("{0} {1:0.0}%", lables[classIds[i]], probabilities[i] * 100);
            double x1 = box.X - box.Width / 2 < 0 ? 0 : box.X - box.Width / 2;
            //画方框
            org.Rectangle(new Point(x1, box.Y - box.Height / 2), new Point(box.X + box.Width / 2, box.Y + box.Height / 2), Scalar.Red, 1);

            //标签字符大小
            Size textSize = Cv2.GetTextSize(label, HersheyFonts.HersheyTriplex, 0.5, 1, out int baseline);
            //画标签背景框
            Cv2.Rectangle(org, new Rect(new Point(x1, box.Y - box.Height / 2 - textSize.Height - baseline),
                new Size(textSize.Width, textSize.Height + baseline)), Scalar.Red, Cv2.FILLED);
            Cv2.PutText(org, label, new Point(x1, box.Y - box.Height / 2 - baseline), HersheyFonts.HersheyTriplex, 0.5, Scalar.White);
        }
        return this.OK(org);
    }
}

//class Program
//{

//    static string[] Labels;
//    static void Main()
//    {
//        const string Cfg = @"h:\csharp\yolov3\yolov3.cfg";
//        const string Weight = @"h:\csharp\yolov3\yolov3.weights";
//        const string Names = @"h:\csharp\yolov3\coco.names";

//        const float threshold = 0.5f;       //置信度阈值
//        const float nmsThreshold = 0.3f;    //nms 阈值

//        //读入标签
//        Labels = File.ReadAllLines(Names).ToArray();

//        string videofile = @"h:\csharp\videos\vtest.avi";

//        var net = CvDnn.ReadNetFromDarknet(Cfg, Weight);
//        //读入模型和设置
//        net.SetPreferableBackend(3); // 3:DNN_BACKEND_OPENCV 
//        net.SetPreferableTarget(0); //dnn target cpu

//        Cv2.NamedWindow("Result", WindowMode.Normal);
//        VideoCapture capture = new VideoCapture(videofile);
//        while (true)
//        {
//            Mat org = new Mat();
//            capture.Read(org);
//            if (org.Empty())
//                break;
//            //生成blob, 块尺寸可以是320/416/608
//            var blob = CvDnn.BlobFromImage(org, 1.0 / 255, new Size(320, 320), new Scalar(), true, false);

//            // 输入数据
//            net.SetInput(blob);

//            //获得输出层名
//            var outNames = net.GetUnconnectedOutLayersNames();

//            //转换成 Mat[]
//            var outs = outNames.Select(_ => new Mat()).ToArray();

//            net.Forward(outs, outNames);

//            //从输出中获得最佳的结果
//            //GetBestResult(outs, org, threshold, nmsThreshold);
//            //for nms
//            var classIds = new List<int>();
//            var confidences = new List<float>();
//            var probabilities = new List<float>();
//            var boxes = new List<Rect2d>();

//            var w = org.Width;
//            var h = org.Height;
//            const int prefix = 5;   //分类概率

//            foreach (var prob in outs)
//            {
//                for (var i = 0; i < prob.Rows; i++)
//                {
//                    var confidence = prob.At<float>(i, 4);
//                    if (confidence > threshold) //置信度大于阈值
//                    {
//                        //获得识别概率
//                        Cv2.MinMaxLoc(prob.Row[i].ColRange(prefix, prob.Cols), out _, out Point max);
//                        var classes = max.X;
//                        var probability = prob.At<float>(i, classes + prefix);

//                        if (probability > threshold) //概率大于阈值
//                        {
//                            var centerX = prob.At<float>(i, 0) * w;
//                            var centerY = prob.At<float>(i, 1) * h;
//                            var width = prob.At<float>(i, 2) * w;
//                            var height = prob.At<float>(i, 3) * h;

//                            //准备nms(非极大值抑制)数据
//                            classIds.Add(classes);
//                            confidences.Add(confidence);
//                            probabilities.Add(probability);
//                            boxes.Add(new Rect2d(centerX, centerY, width, height));
//                        }
//                    }
//                }
//            }

//            //nms(非极大值抑制)提取分数最高的
//            //去除重叠和低置信度的目标框
//            CvDnn.NMSBoxes(boxes, confidences, threshold, nmsThreshold, out int[] indices);

//            foreach (var i in indices)
//            {
//                //画出目标方框并标注置信度和分类标签
//                var box = boxes[i];
//                //Draw(image, classIds[i], confidences[i], probabilities[i], box.X, box.Y, box.Width, box.Height);
//                //标签字符串
//                int classes = classIds[i];

//                var label = string.Format("{0} {1:0.0}%", Labels[classIds[i]], probabilities[i] * 100);
//                var x1 = (box.X - box.Width / 2) < 0 ? 0 : box.X - box.Width / 2;
//                //画方框
//                org.Rectangle(new Point(x1, box.Y - box.Height / 2), new Point(box.X + box.Width / 2, box.Y + box.Height / 2), Scalar.Red, 1);

//                //标签字符大小
//                var textSize = Cv2.GetTextSize(label, HersheyFonts.HersheyTriplex, 0.5, 1, out var baseline);
//                //画标签背景框
//                Cv2.Rectangle(org, new Rect(new Point(x1, box.Y - box.Height / 2 - textSize.Height - baseline),
//                    new Size(textSize.Width, textSize.Height + baseline)), Scalar.Red, Cv2.FILLED);
//                Cv2.PutText(org, label, new Point(x1, box.Y - box.Height / 2 - baseline), HersheyFonts.HersheyTriplex, 0.5, Scalar.White);
//            }
//            //显示结果
//            Cv2.ImShow("Result", org);
//            int key = Cv2.WaitKey(10);
//            if (key == 27)
//                break;
//        }
//    }

//}
