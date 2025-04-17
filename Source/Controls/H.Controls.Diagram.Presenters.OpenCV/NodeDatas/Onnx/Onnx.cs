
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

[Display(Name = "Onnx模型检测", GroupName = "多目标检测", Description = "目标检测", Order = 0)]
public class Onnx : OnnxOpenCVNodeDataBase
{
    public override void LoadDefault()
    {
        base.LoadDefault();
        //this.CfgFilePath = this.GetDataPath("Data\\Yolov3\\yolov3-tiny.cfg");
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

        // 模型参数
        Size inputSize = new Size(320, 320);  // 模型输入尺寸
        float confThreshold = 0.6f;           // 置信度阈值
        float nmsThreshold = 0.3f;            // NMS 阈值
        int topK = 5000;                      // 保留的最大检测数

        // 1. 加载图像
        Mat image = from.Mat;
        if (image.Empty())
        {
            Console.WriteLine("Failed to load image!");
            return this.Error(image);
        }

        // 2. 调整图像尺寸（保持宽高比，填充黑边）
        Mat resized = new Mat();
        double scale = Math.Min((double)inputSize.Width / image.Width, (double)inputSize.Height / image.Height);
        Cv2.Resize(image, resized, Size.Zero, scale, scale, InterpolationFlags.Linear);

        // 计算填充
        int padW = inputSize.Width - resized.Width;
        int padH = inputSize.Height - resized.Height;
        Cv2.CopyMakeBorder(resized, resized,
            top: padH / 2,
            bottom: padH - padH / 2,
            left: padW / 2,
            right: padW - padW / 2,
            BorderTypes.Constant,
            value: new Scalar(0, 0, 0));  // 填充黑边

        // 3. 生成Blob（归一化到[0,1]）
        Mat blob = CvDnn.BlobFromImage(
            resized,
            scaleFactor: 1.0 / 255.0,    // 归一化
            size: inputSize,              // 已经是320x320，实际不会缩放
            mean: new Scalar(0, 0, 0),    // 不减去均值
            swapRB: true,                 // BGR -> RGB（如果模型需要RGB）
            crop: false
        );

        // 4. 加载ONNX模型
        Net net = CvDnn.ReadNetFromOnnx(this.OnnxFilePath);
        net.SetPreferableBackend(Backend.OPENCV);  // backendId=0（OpenCV默认）
        net.SetPreferableTarget(Target.CPU);       // targetId=0（CPU）

        // 5. 输入Blob并推理
        net.SetInput(blob);
        Mat output = net.Forward();  // 输出维度通常是 [1, num_detections, 6]

        // 6. 解析输出
        List<Rect> boxes = new List<Rect>();
        List<float> confidences = new List<float>();
        List<int> classIds = new List<int>();

        for (int i = 0; i < output.Size(1); i++)
        {
            float confidence = output.At<float>(0, i, 1);
            if (confidence > confThreshold)
            {
                // 获取边界框坐标（相对320x320的坐标）
                float x1 = output.At<float>(0, i, 2) * inputSize.Width;
                float y1 = output.At<float>(0, i, 3) * inputSize.Height;
                float x2 = output.At<float>(0, i, 4) * inputSize.Width;
                float y2 = output.At<float>(0, i, 5) * inputSize.Height;

                // 转换到原始图像坐标（考虑填充和缩放）
                x1 = (x1 - padW / 2) / (float)scale;
                y1 = (y1 - padH / 2) / (float)scale;
                x2 = (x2 - padW / 2) / (float)scale;
                y2 = (y2 - padH / 2) / (float)scale;

                // 确保坐标不越界
                x1 = Math.Max(0, x1);
                y1 = Math.Max(0, y1);
                x2 = Math.Min(image.Width, x2);
                y2 = Math.Min(image.Height, y2);

                boxes.Add(new Rect((int)x1, (int)y1, (int)(x2 - x1), (int)(y2 - y1)));
                confidences.Add(confidence);
                classIds.Add((int)output.At<float>(0, i, 0));
            }
        }

        // 7. 应用NMS（OpenCV的NMSBoxes）
        CvDnn.NMSBoxes(boxes, confidences, confThreshold, nmsThreshold, out int[] indices, topK);

        // 8. 绘制检测框
        for (int i = 0; i < indices.Length; i++)
        {
            int idx = indices[i];
            Rect box = boxes[idx];
            Cv2.Rectangle(image, box, new Scalar(0, 255, 0), 2);
            Cv2.PutText(image, $"Class {classIds[idx]}: {confidences[idx]:F2}",
                new Point(box.X, box.Y - 5),
                HersheyFonts.HersheySimplex, 0.5, new Scalar(0, 0, 255), 1);
        }

        // 9. 显示结果
        Cv2.ImShow("Output", image);
        Cv2.WaitKey(0);
        return this.OK(image);

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


    Net LoadModel()
    {
        string modelPath = this.OnnxFilePath;
        // 检查模型文件是否存在
        if (!File.Exists(modelPath))
        {
            throw new FileNotFoundException($"ONNX model file not found: {modelPath}");
        }

        try
        {
            // 加载模型
            var net = CvDnn.ReadNetFromOnnx(modelPath);
            // 检查是否加载成功
            if (net.Empty())
            {
                throw new InvalidDataException("Failed to load ONNX model");
            }

            //  ToDo：尝试设置CUDA加速
            //if (Cuda.CudaEnabled)
            //{
            //    net.SetPreferableBackend(Net.Backend.CUDA);
            //    net.SetPreferableTarget(Net.Target.CUDA);
            //    Console.WriteLine("Using CUDA acceleration");
            //}
            //else
            //{
            //    net.SetPreferableBackend(Net.Backend.OPENCV);
            //    net.SetPreferableTarget(Net.Target.CPU);
            //    Console.WriteLine("Using CPU mode");
            //}

            // 打印模型信息
            Console.WriteLine($"Model loaded successfully. Layers: {net.GetLayerNames().Length}");
            return net;
        }
        catch (Exception ex)
        {
            throw new Exception($"Model loading failed: {ex.Message}", ex);
        }
    }

    Mat PreprocessImage(Mat image, Net net)
    {
        // 检查输入图像有效性
        if (image.Empty())
        {
            throw new ArgumentException("Input image is empty");
        }

        // 获取模型输入信息
        var inputNames = net.GetUnconnectedOutLayersNames();
        if (inputNames.Length == 0)
        {
            throw new Exception("Model has no input layers");
        }

        try
        {
            // 转换颜色空间 (BGR → RGB)
            Mat rgbImage = new Mat();
            Cv2.Resize(image, rgbImage, new Size(inputWidth, inputHeight), 0, 0);
            Cv2.CvtColor(rgbImage, rgbImage, ColorConversionCodes.BGR2RGB);

            // 创建blob (根据模型要求调整参数)
            Mat blob = CvDnn.BlobFromImage(
                rgbImage,
                scaleFactor: 1.0 / 255.0,          // 归一化系数
                size: new Size(inputWidth, inputHeight), // 输入尺寸
                mean: new Scalar(0, 0, 0),  // 均值 (ImageNet标准)
                swapRB: false,                      // 已转为RGB所以不需要交换
                crop: true                         // 中心裁剪
              );             // 输出数据类型

            return blob;
        }
        catch (Exception ex)
        {
            throw new Exception($"Image preprocessing failed: {ex.Message}", ex);
        }
    }


    Mat[] RunInference(Net net, Mat blob)
    {
        try
        {
            // 设置输入
            net.SetInput(blob);

            // 获取输出层名称
            var outNames = net.GetUnconnectedOutLayersNames();

            //outNames = net.GetLayerNames();
            if (outNames.Length == 0)
            {
                throw new Exception("Model has no output layers");
            }
            // 前向传播
            //Mat[] outputs = new Mat[outNames.Length];
            // 4. 初始化输出容器（关键修正）
            Mat[] outputs = new Mat[outNames.Length];
            for (int i = 0; i < outputs.Length; i++)
            {
                outputs[i] = new Mat(); // 显式初始化
            }
            net.Forward(outputs, outNames);

            return outputs;
        }
        catch (Exception ex)
        {
            throw new Exception($"Inference failed: {ex.Message}", ex);
        }
    }

    class DetectionResult
    {
        public int ClassId { get; set; }
        public float Confidence { get; set; }
        public Rect BoundingBox { get; set; }
    }

    DetectionResult[] PostprocessDetections(Mat[] outputs, Mat originalImage)
    {
        try
        {
            List<DetectionResult> results = new List<DetectionResult>();

            // 假设输出格式为[1, 25200, 85] (YOLO格式)
            var output = outputs[0];
            var data = new float[output.Total()];
            output.GetArray(out data); // 获取输出数据

            int dimensions = 85; // 类别数 + 5 (x,y,w,h,conf)
            int rows = (int)(data.Length / dimensions);

            // 原始图像尺寸
            float widthFactor = (float)originalImage.Width / inputWidth;
            float heightFactor = (float)originalImage.Height / inputHeight;

            // 解析检测结果
            for (int i = 0; i < rows; i++)
            {
                float confidence = data[i * dimensions + 4];

                if (confidence < confidenceThreshold)
                    continue;

                // 获取类别概率
                Span<float> classes = new Span<float>(data, i * dimensions + 5, 80);
                int classId = classes.ToArray().ToList().IndexOf(classes.ToArray().Max());
                float maxClassScore = classes[classId];

                // 计算最终置信度
                float finalConfidence = confidence * maxClassScore;
                if (finalConfidence < confidenceThreshold)
                    continue;

                // 解析边界框
                float centerX = data[i * dimensions + 0] * widthFactor;
                float centerY = data[i * dimensions + 1] * heightFactor;
                float width = data[i * dimensions + 2] * widthFactor;
                float height = data[i * dimensions + 3] * heightFactor;

                // 转换为矩形坐标
                int left = (int)(centerX - width / 2);
                int top = (int)(centerY - height / 2);

                results.Add(new DetectionResult
                {
                    ClassId = classId,
                    Confidence = finalConfidence,
                    BoundingBox = new Rect(left, top, (int)width, (int)height)
                });
            }

            // 非极大值抑制 (NMS)
            return ApplyNMS(results.ToArray());
        }
        catch (Exception ex)
        {
            throw new Exception($"Postprocessing failed: {ex.Message}", ex);
        }
    }

    DetectionResult[] ApplyNMS(DetectionResult[] detections)
    {
        // 按置信度排序
        var orderedDetections = detections.OrderByDescending(d => d.Confidence).ToArray();

        List<DetectionResult> filteredDetections = new List<DetectionResult>();

        for (int i = 0; i < orderedDetections.Length; i++)
        {
            var current = orderedDetections[i];
            if (current == null) continue;

            filteredDetections.Add(current);

            for (int j = i + 1; j < orderedDetections.Length; j++)
            {
                if (orderedDetections[j] == null) continue;

                // 计算IoU
                float iou = CalculateIoU(current.BoundingBox, orderedDetections[j].BoundingBox);
                if (iou > this.NmsThreshold)
                {
                    orderedDetections[j] = null; // 抑制该检测
                }
            }
        }

        return filteredDetections.ToArray();
    }

    float CalculateIoU(Rect a, Rect b)
    {
        Rect intersection = a & b;
        float intersectionArea = intersection.Width * intersection.Height;
        float unionArea = a.Width * a.Height + b.Width * b.Height - intersectionArea;

        return intersectionArea / unionArea;
    }

    void VisualizeResults(Mat image, DetectionResult[] results, string[] classLabels)
    {
        // 定义颜色表
        Scalar[] colors = new Scalar[]
        {
        new Scalar(255, 0, 0),
        new Scalar(0, 255, 0),
        new Scalar(0, 0, 255),
        new Scalar(255, 255, 0),
        new Scalar(255, 0, 255)
        };

        foreach (var result in results)
        {
            // 随机选择颜色
            var color = colors[result.ClassId % colors.Length];

            // 绘制边界框
            Cv2.Rectangle(image, result.BoundingBox, color, 2);

            // 绘制标签和置信度
            string label = $"{classLabels[result.ClassId]}: {result.Confidence * 100:F1}%";
            Cv2.PutText(
                image,
                label,
                new Point(result.BoundingBox.X, result.BoundingBox.Y - 5),
                HersheyFonts.HersheySimplex,
                0.5,
                color,
                1);
        }

        // 显示结果
        Cv2.ImShow("Detection Results", image);
        Cv2.WaitKey(0);
    }
}
