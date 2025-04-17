
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

[Display(Name = "Onnx模型检测（测试）", GroupName = "多目标检测", Description = "目标检测", Order = 0)]
public class Onnx : OnnxOpenCVNodeDataBase
{
    public override void LoadDefault()
    {
        base.LoadDefault();
        //this.CfgFilePath = this.GetDataPath("Data\\Yolov3\\yolov3-tiny.cfg");

        this.NameFilePath = this.GetDataPath("Data\\Yolov3\\coco.names");
    }

    private string _onnxFilePath;
    [Required]
    [Display(Name = "Onnx文件路径", GroupName = "数据")]
    [PropertyItem(typeof(OpenFileDialogPropertyItem))]
    public string OnnxFilePath
    {
        get { return _onnxFilePath; }
        set
        {
            _onnxFilePath = value;
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
        if (!File.Exists(this.OnnxFilePath))
        {
            var r = await System.Windows.Application.Current.Dispatcher.Invoke(async () =>
            {
                return await IocMessage.Form?.ShowEdit(this, null, null, x =>
                 {
                     x.UsePropertyNames = $"{nameof(OnnxFilePath)}";
                 });
            });

            if (r != true)
                return this.Error("训练模型不存在：https://pjreddie.com/media/files/yolov3.weights 请先下载");
        }
        return await base.BeforeInvokeAsync(previors, diagram);
    }

    // 模型路径和输入输出配置
    const string modelPath = "model.onnx";
    const string configPath = "config.txt"; // 可选，包含类别标签等
    const int inputWidth = 320;  // 模型期望的输入宽度
    const int inputHeight = 320; // 模型期望的输入高度
    const float confidenceThreshold = 0.6f; // 置信度阈值
    const float nmsThreshold = 0.3f;       // 非极大值抑制阈值
    protected override FlowableResult<Mat> Invoke(ISrcImageNodeData srcImageNodeData, IOpenCVNodeData from, IFlowableDiagramData diagram)
    {
        // 初始化模型
        var model = new OnnxModelHelper(
            modelPath: this.OnnxFilePath,
            inputSize: new Size(320, 320),
            backend: Backend.OPENCV,  // 可选：Backend.OpenCV / Backend.CUDA
            target: Target.CPU      // 可选：Target.CPU / Target.CUDA
        );

        // 加载图像
        Mat image = from.Mat.Clone();

        // 推理
        Mat output = model.Infer(image);

        // 后处理（根据模型调整）
        var detections = OnnxModelHelper.Postprocess(output, confThreshold: 0.6f);

        // 可视化
        foreach (var det in detections)
        {
            float x1 = det.At<float>(0, 2);
            float y1 = det.At<float>(0, 3);
            float x2 = det.At<float>(0, 4);
            float y2 = det.At<float>(0, 5);
            Cv2.Rectangle(image, new Rect((int)x1, (int)y1, (int)(x2 - x1), (int)(y2 - y1)), new Scalar(0, 255, 0), 2);
        }
        //Cv2.ImShow("Result", image);
        //Cv2.WaitKey(0);
        return this.OK(image);

        //string[] lables = File.ReadAllLines(this.NameFilePath).ToArray();

        //Net net = CvDnn.ReadNetFromOnnx(this.OnnxFilePath);
        ////读入模型和设置
        //net.SetPreferableBackend(Backend.OPENCV); // 3:DNN_BACKEND_OPENCV 
        //net.SetPreferableTarget(0); //dnn target cpu
        ////var org = Cv2.ImRead(this.FilePath);

        //Mat org = from.Mat;
        ////生成blob, 块尺寸可以是320/416/608
        //Mat blob = CvDnn.BlobFromImage(org, 1.0 / 255, new Size(320, 320), new Scalar(), true, false);
        //// 输入数据
        //net.SetInput(blob);
        ////获得输出层名
        //string[] outNames = net.GetUnconnectedOutLayersNames();
        ////转换成 Mat[]
        //Mat[] outs = outNames.Select(_ => new Mat()).ToArray();
        //net.Forward(outs, outNames);
        //Mat output = net.Forward();  // 输出维度通常是 [1, num_detections, 6]
        ////Mat output = net.Forward();
        ////从输出中获得最佳的结果
        ////GetBestResult(outs, org, threshold, nmsThreshold);
        ////for nms
        //List<int> classIds = new List<int>();
        //List<float> confidences = new List<float>();
        //List<float> probabilities = new List<float>();
        //List<Rect2d> boxes = new List<Rect2d>();

        //int w = org.Width;
        //int h = org.Height;
        //const int prefix = 5;   //分类概率

        //foreach (Mat prob in outs)
        //{
        //    for (int i = 0; i < prob.Rows; i++)
        //    {
        //        float confidence = prob.At<float>(i, 4);
        //        if (confidence > this.Threshold) //置信度大于阈值
        //        {
        //            //获得识别概率
        //            Cv2.MinMaxLoc(prob.Row(i).ColRange(prefix, prob.Cols), out _, out Point max);
        //            int classes = max.X;
        //            float probability = prob.At<float>(i, classes + prefix);

        //            if (probability > this.Threshold) //概率大于阈值
        //            {
        //                float centerX = prob.At<float>(i, 0) * w;
        //                float centerY = prob.At<float>(i, 1) * h;
        //                float width = prob.At<float>(i, 2) * w;
        //                float height = prob.At<float>(i, 3) * h;

        //                //准备nms(非极大值抑制)数据
        //                classIds.Add(classes);
        //                confidences.Add(confidence);
        //                probabilities.Add(probability);
        //                boxes.Add(new Rect2d(centerX, centerY, width, height));
        //            }
        //        }
        //    }
        //}

        ////nms(非极大值抑制)提取分数最高的
        ////去除重叠和低置信度的目标框
        //CvDnn.NMSBoxes(boxes, confidences, this.Threshold, this.NmsThreshold, out int[] indices);

        //foreach (int i in indices)
        //{
        //    //画出目标方框并标注置信度和分类标签
        //    Rect2d box = boxes[i];
        //    //Draw(image, classIds[i], confidences[i], probabilities[i], box.X, box.Y, box.Width, box.Height);
        //    //标签字符串
        //    int classes = classIds[i];

        //    string label = string.Format("{0} {1:0.0}%", lables[classIds[i]], probabilities[i] * 100);
        //    double x1 = box.X - box.Width / 2 < 0 ? 0 : box.X - box.Width / 2;
        //    //画方框
        //    org.Rectangle(new Point(x1, box.Y - box.Height / 2), new Point(box.X + box.Width / 2, box.Y + box.Height / 2), Scalar.Red, 1);

        //    //标签字符大小
        //    Size textSize = Cv2.GetTextSize(label, HersheyFonts.HersheyTriplex, 0.5, 1, out int baseline);
        //    //画标签背景框
        //    Cv2.Rectangle(org, new Rect(new Point(x1, box.Y - box.Height / 2 - textSize.Height - baseline),
        //        new Size(textSize.Width, textSize.Height + baseline)), Scalar.Red, Cv2.FILLED);
        //    Cv2.PutText(org, label, new Point(x1, box.Y - box.Height / 2 - baseline), HersheyFonts.HersheyTriplex, 0.5, Scalar.White);
        //}
        //return this.OK(org);


        //// 模型参数
        //Size inputSize = new Size(320, 320);  // 模型输入尺寸
        //float confThreshold = 0.6f;           // 置信度阈值
        //float nmsThreshold = 0.3f;            // NMS 阈值
        //int topK = 5000;                      // 保留的最大检测数

        //// 1. 加载图像
        //Mat image = from.Mat;
        //if (image.Empty())
        //{
        //    Console.WriteLine("Failed to load image!");
        //    return this.Error(image);
        //}

        //// 2. 调整图像尺寸（保持宽高比，填充黑边）
        //Mat resized = new Mat();
        //double scale = Math.Min((double)inputSize.Width / image.Width, (double)inputSize.Height / image.Height);
        //Cv2.Resize(image, resized, Size.Zero, scale, scale, InterpolationFlags.Linear);

        //// 计算填充
        //int padW = inputSize.Width - resized.Width;
        //int padH = inputSize.Height - resized.Height;
        //Cv2.CopyMakeBorder(resized, resized,
        //    top: padH / 2,
        //    bottom: padH - padH / 2,
        //    left: padW / 2,
        //    right: padW - padW / 2,
        //    BorderTypes.Constant,
        //    value: new Scalar(0, 0, 0));  // 填充黑边

        //// 3. 生成Blob（归一化到[0,1]）
        //Mat blob = CvDnn.BlobFromImage(
        //    resized,
        //    scaleFactor: 1.0 / 255.0,    // 归一化
        //    size: inputSize,              // 已经是320x320，实际不会缩放
        //    mean: new Scalar(0, 0, 0),    // 不减去均值
        //    swapRB: true,                 // BGR -> RGB（如果模型需要RGB）
        //    crop: false
        //);

        //// 4. 加载ONNX模型
        //Net net = CvDnn.ReadNetFromOnnx(this.OnnxFilePath);
        //net.SetPreferableBackend(Backend.OPENCV);  // backendId=0（OpenCV默认）
        //net.SetPreferableTarget(Target.CPU);       // targetId=0（CPU）

        //// 5. 输入Blob并推理
        //net.SetInput(blob);
        //Mat output = net.Forward();  // 输出维度通常是 [1, num_detections, 6]

        //// 6. 解析输出
        //List<Rect> boxes = new List<Rect>();
        //List<float> confidences = new List<float>();
        //List<int> classIds = new List<int>();

        //for (int i = 0; i < output.Size(1); i++)
        //{
        //    float confidence = output.At<float>(0, i, 1);
        //    if (confidence > confThreshold)
        //    {
        //        // 获取边界框坐标（相对320x320的坐标）
        //        float x1 = output.At<float>(0, i, 2) * inputSize.Width;
        //        float y1 = output.At<float>(0, i, 3) * inputSize.Height;
        //        float x2 = output.At<float>(0, i, 4) * inputSize.Width;
        //        float y2 = output.At<float>(0, i, 5) * inputSize.Height;

        //        // 转换到原始图像坐标（考虑填充和缩放）
        //        x1 = (x1 - padW / 2) / (float)scale;
        //        y1 = (y1 - padH / 2) / (float)scale;
        //        x2 = (x2 - padW / 2) / (float)scale;
        //        y2 = (y2 - padH / 2) / (float)scale;

        //        // 确保坐标不越界
        //        x1 = Math.Max(0, x1);
        //        y1 = Math.Max(0, y1);
        //        x2 = Math.Min(image.Width, x2);
        //        y2 = Math.Min(image.Height, y2);

        //        boxes.Add(new Rect((int)x1, (int)y1, (int)(x2 - x1), (int)(y2 - y1)));
        //        confidences.Add(confidence);
        //        classIds.Add((int)output.At<float>(0, i, 0));
        //    }
        //}

        //// 7. 应用NMS（OpenCV的NMSBoxes）
        //CvDnn.NMSBoxes(boxes, confidences, confThreshold, nmsThreshold, out int[] indices, topK);

        //// 8. 绘制检测框
        //for (int i = 0; i < indices.Length; i++)
        //{
        //    int idx = indices[i];
        //    Rect box = boxes[idx];
        //    Cv2.Rectangle(image, box, new Scalar(0, 255, 0), 2);
        //    Cv2.PutText(image, $"Class {classIds[idx]}: {confidences[idx]:F2}",
        //        new Point(box.X, box.Y - 5),
        //        HersheyFonts.HersheySimplex, 0.5, new Scalar(0, 0, 255), 1);
        //}

        //// 9. 显示结果
        ////Cv2.ImShow("Output", image);
        ////Cv2.WaitKey(0);
        //return this.OK(image);

        //// 1. 加载模型
        ////var net = CvDnn.ReadNetFromOnnx("resnet50.onnx");
        //var net = CvDnn.ReadNetFromOnnx(this.OnnxFilePath);
        ////// 2. 加载图像
        ////Mat image = Cv2.ImRead("cat.jpg", ImreadModes.Color);
        //Mat image = from.Mat;
        //// 3. 预处理
        //Mat blob = CvDnn.BlobFromImage(
        //    image,
        //    1.0 / 255.0,
        //    new Size(224, 224),
        //    new Scalar(0.485, 0.456, 0.406),
        //    true,
        //    false
        //);

        //// 4. 推理
        //net.SetInput(blob);
        //Mat output = net.Forward();

        //// 5. 后处理
        //float[] probabilities = new float[output.Total()];
        //output.GetArray(out probabilities);

        //// 获取top-5结果
        //var top5 = probabilities
        //    .Select((p, i) => new { Index = i, Probability = p })
        //    .OrderByDescending(x => x.Probability)
        //    .Take(5);

        //// 加载类别标签
        //string[] labels = File.ReadAllLines("imagenet_classes.txt");

        //// 输出结果
        //foreach (var item in top5)
        //{
        //    Console.WriteLine($"{labels[item.Index]}: {item.Probability * 100:F2}%");
        //}

        //// 1. 加载模型
        //var net = LoadModel();

        //// 2. 加载类别标签
        //var inputNames = net.GetUnconnectedOutLayersNames();
        ////string[] classLabels = File.ReadAllLines(configPath);
        //string[] classLabels = inputNames;
        //// 3. 加载测试图像
        ////Mat image = Cv2.ImRead("test.jpg", ImreadModes.Color);
        //Mat image = from.Mat;
        //if (image.Empty())
        //{
        //    throw new Exception("Failed to load test image");
        //}

        //// 4. 预处理
        //Mat blob = PreprocessImage(image, net);

        ////Mat rgbImage = new Mat();
        ////Cv2.Resize(image, rgbImage, new Size(inputWidth, inputHeight), 0, 0);
        ////Cv2.CvtColor(rgbImage, rgbImage, ColorConversionCodes.BGR2RGB);

        //// 5. 执行推理
        //var outputs = RunInference(net, blob);

        //// 6. 后处理
        //var detections = PostprocessDetections(outputs, image);

        //// 7. 可视化结果

        //VisualizeResults(image.Clone(), detections, classLabels);

        //// 8. 保存结果
        //Cv2.ImWrite("result.jpg", image);
        //return this.OK(image);
    }
}

public class OnnxModelHelper
{
    private Net _net;
    private Size _inputSize;
    private Scalar _mean = new Scalar(0, 0, 0);
    private double _std = 1.0 / 255.0;
    private bool _swapRB = true; // OpenCV 默认 BGR，ONNX 通常需要 RGB

    public OnnxModelHelper(string modelPath, Size inputSize, Backend backend = Backend.OPENCV, Target target = Target.CPU)
    {
        _net = CvDnn.ReadNetFromOnnx(modelPath);
        _net.SetPreferableBackend(backend);
        _net.SetPreferableTarget(target);
        _inputSize = inputSize;
    }

    public Mat Infer(Mat image)
    {
        // 1. 预处理（自动缩放 + 归一化）
        Mat blob = CvDnn.BlobFromImage(
            image,
            scaleFactor: _std,
            size: _inputSize,
            mean: _mean,
            swapRB: _swapRB,
            crop: false
        );

        // 2. 推理
        _net.SetInput(blob);
        Mat output = _net.Forward();

        return output;
    }

    // 可选：后处理（例如目标检测的 NMS）
    public static Mat[] Postprocess(Mat output, float confThreshold = 0.5f, float nmsThreshold = 0.4f)
    {
        // 这里根据你的模型输出格式自定义
        // 例如 YOLO 返回 [1, num_detections, 6]（class_id, conf, x1, y1, x2, y2）
        // 这里仅作示例，实际需调整
        List<Mat> results = new List<Mat>();
        for (int i = 0; i < output.Size(1); i++)
        {
            float conf = output.At<float>(0, i, 1);
            if (conf > confThreshold)
            {
                if (output.Rows > i)
                    results.Add(output.Row(i).Clone());
            }
        }
        return results.ToArray();
    }
}
