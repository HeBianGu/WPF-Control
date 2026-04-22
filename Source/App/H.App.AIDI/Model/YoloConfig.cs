// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.App.AIDI.Base;
using H.Controls.Form.Attributes;
using H.Controls.Form.PropertyItem.Attribute;
using H.Controls.Form.PropertyItem.Base;
using H.Controls.Form.PropertyItem.ComboBoxPropertyItems;
using H.Controls.Form.PropertyItem.TextPropertyItems;
using H.Controls.Form.PropertyItems;
using H.Mvvm.ViewModels.Base;
using Newtonsoft.Json;
using System.Text.Json;
using YamlDotNet.Serialization;

namespace H.App.AIDI.Model;
/// <summary>
/// YOLOv8 训练配置实体类
/// </summary>
public class YoloConfig : BindableBase
{
    // ============ 基本参数 ============

    /// <summary>
    /// 任务类型：detect（检测）、segment（分割）、pose（姿态）
    /// </summary>
    [JsonProperty("task")]
    [YamlMember(Alias = "task")]
    [DefaultValue("detect")]
    [Display(Name = "任务类型", GroupName = "基本参数", Description = "任务类型：detect（检测）、segment（分割）、pose（姿态）")]
    [GetMethodNameSource(nameof(GetTasks))]
    [PropertyItem(typeof(ComboBoxPropertyItem))]
    [PropertyViewItem(typeof(TextPropertyViewItem))]
    [DisplayMemberPath("Value")]
    [SelectedValuePath("Key")]
    public string Task { get; set; } = "detect";
    public IEnumerable<SelectSourceItem<string>> GetTasks()
    {
        yield return new SelectSourceItem<string>("detect", "detect（检测）");
        yield return new SelectSourceItem<string>("segment", "segment（分割）- 不可用");
        yield return new SelectSourceItem<string>("pose", "pose（姿态）- 不可用");
    }
    /// <summary>
    /// 模式：train（训练）、val（验证）、predict（预测）、export（导出）
    /// </summary>
    [JsonProperty("mode")]
    [YamlMember(Alias = "mode")]
    [DefaultValue("train")]
    [Display(Name = "模式", GroupName = "基本参数", Description = "模式：train（训练）、val（验证）、predict（预测）、export（导出）")]
    [GetMethodNameSource(nameof(GetModes))]
    [PropertyItem(typeof(ComboBoxPropertyItem))]
    [PropertyViewItem(typeof(TextPropertyViewItem))]
    [DisplayMemberPath("Value")]
    [SelectedValuePath("Key")]
    public string Mode { get; set; } = "train";

    public IEnumerable<SelectSourceItem<string>> GetModes()
    {
        yield return new SelectSourceItem<string>("train", "train（训练）");
        yield return new SelectSourceItem<string>("val", "val（验证）- 不可用");
        yield return new SelectSourceItem<string>("predict", "predict（预测）- 不可用");
        yield return new SelectSourceItem<string>("export", "export（导出,）- 不可用");
    }

    //模型文件 参数量 体积 速度  精度(mAP50-95)   适用场景
    //yolov8n.pt  ~3.2M	最小	⚡⚡⚡ 最快 最低  移动设备、实时检测
    //yolov8s.pt  ~11.2M	小	⚡⚡ 快 中等  边缘设备、一般应用
    //yolov8m.pt  ~25.9M	中	⚡ 中 较好  服务器、平衡需求
    //yolov8l.pt  ~43.7M	大 较慢  好 高性能服务器
    //yolov8x.pt ~68.2M	最大	🐢 最慢	🏆 最好 研究、最高精度
    /// <summary>
    /// 模型文件路径或名称（如：yolov8n.pt, yolov8s.pt）
    /// </summary>
    [JsonProperty("model")]
    [YamlMember(Alias = "model")]
    //[DefaultValue("yolov8n.pt")]
    [Display(Name = "模型文件", GroupName = "基本参数", Description = "模型文件路径或名称（如：yolov8n.pt, yolov8s.pt）")]
    [DisplayMemberPath("Value")]
    [SelectedValuePath("Key")]
    [GetMethodNameSource(nameof(GetModels))]
    [PropertyItem(typeof(ComboBoxPropertyItem))]
    [PropertyViewItem(typeof(TextPropertyViewItem))]
    public string Model { get; set; } = "yolov8n.pt";
    public IEnumerable<SelectSourceItem<string>> GetModels()
    {
        yield return new SelectSourceItem<string>("yolov8n.pt", "[ yolov8n.pt ] 体积:最小 速度:最快 精度:最低 适用场景:移动设备、实时检测");
        yield return new SelectSourceItem<string>("yolov8s.pt", "[ yolov8s.pt ] 体积:小 速度:快 精度:中等  适用场景:边缘设备、一般应用");
        yield return new SelectSourceItem<string>("yolov8m.pt", "[ yolov8m.pt ] 体积:中 速度:中 精度:较好  适用场景:服务器、平衡需求");
        yield return new SelectSourceItem<string>("yolov8l.pt", "[ yolov8l.pt ] 体积:大 速度:较慢 精度:好 适用场景:高性能服务器");
        yield return new SelectSourceItem<string>("yolov8x.pt", "[ yolov8x.pt ] 体积:最大 最慢: 精度:最好 适用场景:最好 研究、最高精度");
    }

    /// <summary>
    /// 数据集配置文件路径
    /// </summary>
    [JsonProperty("data")]
    [YamlMember(Alias = "data")]
    //[DefaultValue("data.yaml")]
    [Display(Name = "数据集配置", GroupName = "基本参数", Description = "数据集配置文件路径")]
    [ReadOnly(true)]
    public string Data { get; set; } = "data.yaml";

    /// <summary>
    /// 训练轮次
    /// </summary>
    [JsonProperty("epochs")]
    [YamlMember(Alias = "epochs")]
    [DefaultValue(100)]
    [Display(Name = "训练轮次", GroupName = "基本参数", Description = "训练轮次")]
    [PropertyItem(typeof(Int32SliderTextPropertyItem))]
    [Range(1, 10000)]
    public int Epochs { get; set; } = 100;

    /// <summary>
    /// 训练时间（小时），null 表示不限制
    /// </summary>
    [JsonProperty("time")]
    [YamlMember(Alias = "time")]
    [DefaultValue(null)]
    [Display(Name = "训练时间", GroupName = "基本参数", Description = "训练时间（小时），null 表示不限制")]
    [PropertyItem(typeof(DoubleSliderTextPropertyItem))]
    [Range(0.1, 1000)]
    [TypeConverter(typeof(NullableDoubleConverter))]
    public double? Time { get; set; }

    /// <summary>
    /// 早停耐心值，多少个epoch没有改善则停止
    /// </summary>
    [JsonProperty("patience")]
    [YamlMember(Alias = "patience")]
    [DefaultValue(100)]
    [Display(Name = "早停耐心值", GroupName = "基本参数", Description = "早停耐心值，多少个epoch没有改善则停止")]
    [PropertyItem(typeof(Int32SliderTextPropertyItem))]
    [Range(1, 1000)]
    public int Patience { get; set; } = 100;

    /// <summary>
    /// 批量大小
    /// </summary>
    [JsonProperty("batch")]
    [YamlMember(Alias = "batch")]
    [DefaultValue(16)]
    [Display(Name = "批量大小", GroupName = "基本参数", Description = "批量大小")]
    [PropertyItem(typeof(Int32SliderTextPropertyItem))]
    [Range(1, 1024)]
    public int Batch { get; set; } = 16;

    /// <summary>
    /// 输入图像大小
    /// </summary>
    [JsonProperty("imgsz")]
    [YamlMember(Alias = "imgsz")]
    [DefaultValue(640)]
    [Display(Name = "图像尺寸", GroupName = "基本参数", Description = "输入图像大小")]
    [PropertyItem(typeof(Int32SliderTextPropertyItem))]
    [Range(32, 4096)]
    public int ImageSize { get; set; } = 640;

    /// <summary>
    /// 是否保存模型
    /// </summary>
    [JsonProperty("save")]
    [YamlMember(Alias = "save")]
    [DefaultValue(true)]
    [Display(Name = "保存模型", GroupName = "基本参数", Description = "是否保存模型")]
    [TextValueConverter(typeof(YoloConfigBoolenValueConverter))]
    [ReadOnly(true)]
    public bool Save { get; set; } = true;

    /// <summary>
    /// 保存检查点的周期，-1表示只保存最佳和最后
    /// </summary>
    [JsonProperty("save_period")]
    [YamlMember(Alias = "save_period")]
    [DefaultValue(-1)]
    [Display(Name = "保存周期", GroupName = "基本参数", Description = "保存检查点的周期，-1表示只保存最佳和最后")]
    public int SavePeriod { get; set; } = -1;

    /// <summary>
    /// 是否缓存数据集
    /// </summary>
    [JsonProperty("cache")]
    [YamlMember(Alias = "cache")]
    [DefaultValue(false)]
    [Display(Name = "缓存数据集", GroupName = "基本参数", Description = "是否缓存数据集")]
    [TextValueConverter(typeof(YoloConfigBoolenValueConverter))]
    public bool Cache { get; set; } = false;

    // ============ 设备参数 ============

    /// <summary>
    /// 训练设备：cpu, 0, 0,1,2,3 或 cpu
    /// </summary>
    [JsonProperty("device")]
    [YamlMember(Alias = "device")]
    [DefaultValue("cpu")]
    [Display(Name = "训练设备", GroupName = "设备参数", Description = "训练设备：cpu, 0, 0,1,2,3 或 cpu")]
    public string Device { get; set; } = "cpu";

    /// <summary>
    /// 数据加载工作线程数
    /// </summary>
    [JsonProperty("workers")]
    [YamlMember(Alias = "workers")]
    [DefaultValue(8)]
    [Display(Name = "工作线程数", GroupName = "设备参数", Description = "数据加载工作线程数")]
    [PropertyItem(typeof(Int32SliderTextPropertyItem))]
    [Range(0, 128)]
    public int Workers { get; set; } = 8;

    // ============ 项目参数 ============

    /// <summary>
    /// 项目保存目录
    /// </summary>
    [JsonProperty("project")]
    [YamlMember(Alias = "project")]
    [DefaultValue(null)]
    [Display(Name = "项目目录", GroupName = "项目参数", Description = "项目保存目录")]
    [ReadOnly(true)]
    public string Project { get; set; }

    /// <summary>
    /// 实验名称
    /// </summary>
    [JsonProperty("name")]
    [YamlMember(Alias = "name")]
    [DefaultValue(null)]
    [Display(Name = "实验名称", GroupName = "项目参数", Description = "实验名称")]
    [ReadOnly(true)]
    public string Name { get; set; }

    /// <summary>
    /// 是否覆盖现有目录
    /// </summary>
    [JsonProperty("exist_ok")]
    [YamlMember(Alias = "exist_ok")]
    [DefaultValue(false)]
    [Display(Name = "覆盖现有目录", GroupName = "项目参数", Description = "是否覆盖现有目录")]
    [TextValueConverter(typeof(YoloConfigBoolenValueConverter))]
    [ReadOnly(true)]
    public bool ExistOk { get; set; } = false;

    /// <summary>
    /// 是否使用预训练权重
    /// </summary>
    [JsonProperty("pretrained")]
    [YamlMember(Alias = "pretrained")]
    [DefaultValue(true)]
    [Display(Name = "预训练权重", GroupName = "项目参数", Description = "是否使用预训练权重")]
    [TextValueConverter(typeof(YoloConfigBoolenValueConverter))]
    public bool Pretrained { get; set; } = true;

    // ============ 优化器参数 ============

    /// <summary>
    /// 优化器：SGD, Adam, AdamW, NAdam, RAdam, RMSProp
    /// </summary>
    [JsonProperty("optimizer")]
    [YamlMember(Alias = "optimizer")]
    [DefaultValue("auto")]
    [Display(Name = "优化器", GroupName = "优化器参数", Description = "优化器：SGD, Adam, AdamW, NAdam, RAdam, RMSProp")]
    [GetMethodNameSource(nameof(GetOptimizers))]
    [PropertyItem(typeof(ComboBoxPropertyItem))]
    [PropertyViewItem(typeof(TextPropertyViewItem))]
    public string Optimizer { get; set; } = "auto";

    public IEnumerable<string> GetOptimizers()
    {
        yield return "auto";
        yield return "SGD";
        yield return "Adam";
        yield return "AdamW";
        yield return "NAdam";
        yield return "RAdam";
        yield return "RMSProp";
    }
    /// <summary>
    /// 是否输出详细信息
    /// </summary>
    [JsonProperty("verbose")]
    [YamlMember(Alias = "verbose")]
    [DefaultValue(true)]
    [Display(Name = "详细输出", GroupName = "优化器参数", Description = "是否输出详细信息")]
    [TextValueConverter(typeof(YoloConfigBoolenValueConverter))]
    public bool Verbose { get; set; } = true;

    /// <summary>
    /// 随机种子
    /// </summary>
    [JsonProperty("seed")]
    [YamlMember(Alias = "seed")]
    [DefaultValue(0)]
    [Display(Name = "随机种子", GroupName = "优化器参数", Description = "随机种子")]
    public int Seed { get; set; } = 0;

    /// <summary>
    /// 是否启用确定性训练
    /// </summary>
    [JsonProperty("deterministic")]
    [YamlMember(Alias = "deterministic")]
    [DefaultValue(true)]
    [Display(Name = "确定性训练", GroupName = "优化器参数", Description = "是否启用确定性训练")]
    [TextValueConverter(typeof(YoloConfigBoolenValueConverter))]
    public bool Deterministic { get; set; } = true;

    /// <summary>
    /// 是否单类别训练
    /// </summary>
    [JsonProperty("single_cls")]
    [YamlMember(Alias = "single_cls")]
    [DefaultValue(false)]
    [Display(Name = "单类别训练", GroupName = "优化器参数", Description = "是否单类别训练")]
    [TextValueConverter(typeof(YoloConfigBoolenValueConverter))]
    public bool SingleClass { get; set; } = false;

    /// <summary>
    /// 是否矩形训练
    /// </summary>
    [JsonProperty("rect")]
    [YamlMember(Alias = "rect")]
    [DefaultValue(false)]
    [Display(Name = "矩形训练", GroupName = "优化器参数", Description = "是否矩形训练")]
    [TextValueConverter(typeof(YoloConfigBoolenValueConverter))]
    public bool Rectangular { get; set; } = false;

    /// <summary>
    /// 是否使用余弦学习率调度
    /// </summary>
    [JsonProperty("cos_lr")]
    [YamlMember(Alias = "cos_lr")]
    [DefaultValue(false)]
    [Display(Name = "余弦学习率", GroupName = "优化器参数", Description = "是否使用余弦学习率调度")]
    [TextValueConverter(typeof(YoloConfigBoolenValueConverter))]
    public bool CosineLR { get; set; } = false;

    /// <summary>
    /// 关闭马赛克增强的轮次
    /// </summary>
    [JsonProperty("close_mosaic")]
    [YamlMember(Alias = "close_mosaic")]
    [DefaultValue(10)]
    [Display(Name = "关闭马赛克轮次", GroupName = "优化器参数", Description = "关闭马赛克增强的轮次")]
    [PropertyItem(typeof(Int32SliderTextPropertyItem))]
    [Range(0, 100)]
    public int CloseMosaic { get; set; } = 10;

    /// <summary>
    /// 是否恢复训练
    /// </summary>
    [JsonProperty("resume")]
    [YamlMember(Alias = "resume")]
    [DefaultValue(false)]
    [Display(Name = "恢复训练", GroupName = "优化器参数", Description = "是否恢复训练")]
    [TextValueConverter(typeof(YoloConfigBoolenValueConverter))]
    public bool Resume { get; set; } = false;

    /// <summary>
    /// 是否使用自动混合精度
    /// </summary>
    [JsonProperty("amp")]
    [YamlMember(Alias = "amp")]
    [DefaultValue(true)]
    [Display(Name = "自动混合精度", GroupName = "优化器参数", Description = "是否使用自动混合精度")]
    [TextValueConverter(typeof(YoloConfigBoolenValueConverter))]
    public bool MixedPrecision { get; set; } = true;

    /// <summary>
    /// 训练数据比例
    /// </summary>
    [JsonProperty("fraction")]
    [YamlMember(Alias = "fraction")]
    [DefaultValue(1.0)]
    [Display(Name = "训练数据比例", GroupName = "优化器参数", Description = "训练数据比例")]
    [PropertyItem(typeof(DoubleSliderTextPropertyItem))]
    [Range(0.1, 1.0)]
    public double Fraction { get; set; } = 1.0;

    // ============ 调试参数 ============

    /// <summary>
    /// 是否分析模型性能
    /// </summary>
    [JsonProperty("profile")]
    [YamlMember(Alias = "profile")]
    [DefaultValue(false)]
    [Display(Name = "性能分析", GroupName = "调试参数", Description = "是否分析模型性能")]
    [TextValueConverter(typeof(YoloConfigBoolenValueConverter))]
    public bool Profile { get; set; } = false;

    /// <summary>
    /// 冻结层数
    /// </summary>
    [JsonProperty("freeze")]
    [YamlMember(Alias = "freeze")]
    [DefaultValue(null)]
    [Display(Name = "冻结层数", GroupName = "调试参数", Description = "冻结层数")]
    [PropertyItem(typeof(Int32SliderTextPropertyItem))]
    [Range(0, 100)]
    [TypeConverter(typeof(NullableInt32Converter))]
    public int? Freeze { get; set; }

    /// <summary>
    /// 是否多尺度训练
    /// </summary>
    [JsonProperty("multi_scale")]
    [YamlMember(Alias = "multi_scale")]
    [DefaultValue(false)]
    [Display(Name = "多尺度训练", GroupName = "调试参数", Description = "是否多尺度训练")]
    [TextValueConverter(typeof(YoloConfigBoolenValueConverter))]
    public bool MultiScale { get; set; } = false;

    /// <summary>
    /// 是否编译模型（Torch 2.0+）
    /// </summary>
    [JsonProperty("compile")]
    [YamlMember(Alias = "compile")]
    [DefaultValue(false)]
    [Display(Name = "编译模型", GroupName = "调试参数", Description = "是否编译模型（Torch 2.0+）")]
    [TextValueConverter(typeof(YoloConfigBoolenValueConverter))]
    public bool Compile { get; set; } = false;

    // ============ 掩码相关参数 ============

    /// <summary>
    /// 掩码重叠（仅分割任务）
    /// </summary>
    [JsonProperty("overlap_mask")]
    [YamlMember(Alias = "overlap_mask")]
    [DefaultValue(true)]
    [Display(Name = "掩码重叠", GroupName = "掩码参数", Description = "掩码重叠（仅分割任务）")]
    [TextValueConverter(typeof(YoloConfigBoolenValueConverter))]
    public bool OverlapMask { get; set; } = true;

    /// <summary>
    /// 掩码下采样比率
    /// </summary>
    [JsonProperty("mask_ratio")]
    [YamlMember(Alias = "mask_ratio")]
    [DefaultValue(4)]
    [Display(Name = "掩码下采样比率", GroupName = "掩码参数", Description = "掩码下采样比率")]
    [PropertyItem(typeof(Int32SliderTextPropertyItem))]
    [Range(1, 16)]
    public int MaskRatio { get; set; } = 4;

    /// <summary>
    /// Dropout率
    /// </summary>
    [JsonProperty("dropout")]
    [YamlMember(Alias = "dropout")]
    [DefaultValue(0.0f)]
    [Display(Name = "Dropout率", GroupName = "掩码参数", Description = "Dropout率")]
    [PropertyItem(typeof(FloatSliderTextPropertyItem))]
    [Range(0.0, 1.0)]
    public float Dropout { get; set; } = 0.0f;

    // ============ 验证参数 ============

    /// <summary>
    /// 是否启用验证
    /// </summary>
    [JsonProperty("val")]
    [YamlMember(Alias = "val")]
    [DefaultValue(true)]
    [Display(Name = "启用验证", GroupName = "验证参数", Description = "是否启用验证")]
    [TextValueConverter(typeof(YoloConfigBoolenValueConverter))]
    [ReadOnly(true)]
    public bool Validate { get; set; } = true;

    /// <summary>
    /// 验证集分割
    /// </summary>
    [JsonProperty("split")]
    [YamlMember(Alias = "split")]
    [DefaultValue("val")]
    [Display(Name = "验证集分割", GroupName = "验证参数", Description = "验证集分割")]
    [ReadOnly(true)]
    public string Split { get; set; } = "val";

    /// <summary>
    /// 是否保存JSON格式的验证结果
    /// </summary>
    [JsonProperty("save_json")]
    [YamlMember(Alias = "save_json")]
    [DefaultValue(false)]
    [Display(Name = "保存JSON结果", GroupName = "验证参数", Description = "是否保存JSON格式的验证结果")]
    [TextValueConverter(typeof(YoloConfigBoolenValueConverter))]
    public bool SaveJson { get; set; } = false;

    /// <summary>
    /// 检测置信度阈值
    /// </summary>
    [JsonProperty("conf")]
    [YamlMember(Alias = "conf")]
    [DefaultValue(null)]
    [Display(Name = "置信度阈值", GroupName = "验证参数", Description = "检测置信度阈值")]
    [PropertyItem(typeof(FloatSliderTextPropertyItem))]
    [Range(0.0, 1.0)]
    [TypeConverter(typeof(NullableFloatConverter))]
    public float? ConfidenceThreshold { get; set; }

    /// <summary>
    /// NMS的IoU阈值
    /// </summary>
    [JsonProperty("iou")]
    [YamlMember(Alias = "iou")]
    [DefaultValue(0.7f)]
    [Display(Name = "IoU阈值", GroupName = "验证参数", Description = "NMS的IoU阈值")]
    [PropertyItem(typeof(FloatSliderTextPropertyItem))]
    [Range(0.0, 1.0)]
    public float IoUThreshold { get; set; } = 0.7f;

    /// <summary>
    /// 每张图像最大检测数量
    /// </summary>
    [JsonProperty("max_det")]
    [YamlMember(Alias = "max_det")]
    [DefaultValue(300)]
    [Display(Name = "最大检测数", GroupName = "验证参数", Description = "每张图像最大检测数量")]
    [PropertyItem(typeof(Int32SliderTextPropertyItem))]
    [Range(1, 10000)]
    public int MaxDetections { get; set; } = 300;

    /// <summary>
    /// 是否使用半精度推理
    /// </summary>
    [JsonProperty("half")]
    [YamlMember(Alias = "half")]
    [DefaultValue(false)]
    [Display(Name = "半精度推理", GroupName = "验证参数", Description = "是否使用半精度推理")]
    [TextValueConverter(typeof(YoloConfigBoolenValueConverter))]
    public bool HalfPrecision { get; set; } = false;

    /// <summary>
    /// 是否使用OpenCV DNN
    /// </summary>
    [JsonProperty("dnn")]
    [YamlMember(Alias = "dnn")]
    [DefaultValue(false)]
    [Display(Name = "OpenCV DNN", GroupName = "验证参数", Description = "是否使用OpenCV DNN")]
    [TextValueConverter(typeof(YoloConfigBoolenValueConverter))]
    public bool Dnn { get; set; } = false;

    /// <summary>
    /// 是否保存可视化图表
    /// </summary>
    [JsonProperty("plots")]
    [YamlMember(Alias = "plots")]
    [DefaultValue(true)]
    [Display(Name = "保存可视化图表", GroupName = "验证参数", Description = "是否保存可视化图表")]
    [TextValueConverter(typeof(YoloConfigBoolenValueConverter))]
    public bool Plots { get; set; } = true;

    // ============ 预测参数 ============

    /// <summary>
    /// 预测源（图片/视频路径、摄像头ID等）
    /// </summary>
    [JsonProperty("source")]
    [YamlMember(Alias = "source")]
    [DefaultValue(null)]
    [Display(Name = "预测源", GroupName = "预测参数", Description = "预测源（图片/视频路径、摄像头ID等）")]
    public string Source { get; set; }

    /// <summary>
    /// 视频帧采样间隔
    /// </summary>
    [JsonProperty("vid_stride")]
    [YamlMember(Alias = "vid_stride")]
    [DefaultValue(1)]
    [Display(Name = "视频帧采样间隔", GroupName = "预测参数", Description = "视频帧采样间隔")]
    [PropertyItem(typeof(Int32SliderTextPropertyItem))]
    [Range(1, 100)]
    public int VideoStride { get; set; } = 1;

    /// <summary>
    /// 是否使用流缓冲区
    /// </summary>
    [JsonProperty("stream_buffer")]
    [YamlMember(Alias = "stream_buffer")]
    [DefaultValue(false)]
    [Display(Name = "流缓冲区", GroupName = "预测参数", Description = "是否使用流缓冲区")]
    [TextValueConverter(typeof(YoloConfigBoolenValueConverter))]
    public bool StreamBuffer { get; set; } = false;

    /// <summary>
    /// 是否可视化特征图
    /// </summary>
    [JsonProperty("visualize")]
    [YamlMember(Alias = "visualize")]
    [DefaultValue(false)]
    [Display(Name = "可视化特征图", GroupName = "预测参数", Description = "是否可视化特征图")]
    [TextValueConverter(typeof(YoloConfigBoolenValueConverter))]
    public bool Visualize { get; set; } = false;

    /// <summary>
    /// 是否在推理时使用数据增强
    /// </summary>
    [JsonProperty("augment")]
    [YamlMember(Alias = "augment")]
    [DefaultValue(false)]
    [Display(Name = "推理数据增强", GroupName = "预测参数", Description = "是否在推理时使用数据增强")]
    [TextValueConverter(typeof(YoloConfigBoolenValueConverter))]
    public bool Augment { get; set; } = false;

    /// <summary>
    /// 是否使用类别无关的NMS
    /// </summary>
    [JsonProperty("agnostic_nms")]
    [YamlMember(Alias = "agnostic_nms")]
    [DefaultValue(false)]
    [Display(Name = "类别无关NMS", GroupName = "预测参数", Description = "是否使用类别无关的NMS")]
    [TextValueConverter(typeof(YoloConfigBoolenValueConverter))]
    public bool AgnosticNMS { get; set; } = false;

    /// <summary>
    /// 仅检测指定类别
    /// </summary>
    [JsonProperty("classes")]
    [YamlMember(Alias = "classes")]
    [DefaultValue(null)]
    [Display(Name = "指定类别", GroupName = "预测参数", Description = "仅检测指定类别")]
    public List<int> Classes { get; set; }

    /// <summary>
    /// 是否使用高分辨率掩码
    /// </summary>
    [JsonProperty("retina_masks")]
    [YamlMember(Alias = "retina_masks")]
    [DefaultValue(false)]
    [Display(Name = "高分辨率掩码", GroupName = "预测参数", Description = "是否使用高分辨率掩码")]
    [TextValueConverter(typeof(YoloConfigBoolenValueConverter))]
    public bool RetinaMasks { get; set; } = false;

    /// <summary>
    /// 是否提取特征向量
    /// </summary>
    [JsonProperty("embed")]
    [YamlMember(Alias = "embed")]
    [DefaultValue(null)]
    [Display(Name = "提取特征向量", GroupName = "预测参数", Description = "是否提取特征向量")]
    public List<int> Embed { get; set; }

    /// <summary>
    /// 是否实时显示结果
    /// </summary>
    [JsonProperty("show")]
    [YamlMember(Alias = "show")]
    [DefaultValue(false)]
    [Display(Name = "实时显示结果", GroupName = "预测参数", Description = "是否实时显示结果")]
    [TextValueConverter(typeof(YoloConfigBoolenValueConverter))]
    public bool Show { get; set; } = false;

    /// <summary>
    /// 是否保存视频帧
    /// </summary>
    [JsonProperty("save_frames")]
    [YamlMember(Alias = "save_frames")]
    [DefaultValue(false)]
    [Display(Name = "保存视频帧", GroupName = "预测参数", Description = "是否保存视频帧")]
    [TextValueConverter(typeof(YoloConfigBoolenValueConverter))]
    public bool SaveFrames { get; set; } = false;

    // ============ 保存选项 ============

    /// <summary>
    /// 是否保存标注文件
    /// </summary>
    [JsonProperty("save_txt")]
    [YamlMember(Alias = "save_txt")]
    [DefaultValue(false)]
    [Display(Name = "保存标注文件", GroupName = "保存选项", Description = "是否保存标注文件")]
    [TextValueConverter(typeof(YoloConfigBoolenValueConverter))]
    public bool SaveTxt { get; set; } = false;

    /// <summary>
    /// 是否保存置信度
    /// </summary>
    [JsonProperty("save_conf")]
    [YamlMember(Alias = "save_conf")]
    [DefaultValue(false)]
    [Display(Name = "保存置信度", GroupName = "保存选项", Description = "是否保存置信度")]
    [TextValueConverter(typeof(YoloConfigBoolenValueConverter))]
    public bool SaveConf { get; set; } = false;

    /// <summary>
    /// 是否保存裁剪的检测结果
    /// </summary>
    [JsonProperty("save_crop")]
    [YamlMember(Alias = "save_crop")]
    [DefaultValue(false)]
    [Display(Name = "保存裁剪结果", GroupName = "保存选项", Description = "是否保存裁剪的检测结果")]
    [TextValueConverter(typeof(YoloConfigBoolenValueConverter))]
    public bool SaveCrop { get; set; } = false;

    /// <summary>
    /// 是否显示标注
    /// </summary>
    [JsonProperty("show_labels")]
    [YamlMember(Alias = "show_labels")]
    [DefaultValue(true)]
    [Display(Name = "显示标注", GroupName = "保存选项", Description = "是否显示标注")]
    [TextValueConverter(typeof(YoloConfigBoolenValueConverter))]
    public bool ShowLabels { get; set; } = true;

    /// <summary>
    /// 是否显示置信度
    /// </summary>
    [JsonProperty("show_conf")]
    [YamlMember(Alias = "show_conf")]
    [DefaultValue(true)]
    [Display(Name = "显示置信度", GroupName = "保存选项", Description = "是否显示置信度")]
    [TextValueConverter(typeof(YoloConfigBoolenValueConverter))]
    public bool ShowConf { get; set; } = true;

    /// <summary>
    /// 是否显示边界框
    /// </summary>
    [JsonProperty("show_boxes")]
    [YamlMember(Alias = "show_boxes")]
    [DefaultValue(true)]
    [Display(Name = "显示边界框", GroupName = "保存选项", Description = "是否显示边界框")]
    [TextValueConverter(typeof(YoloConfigBoolenValueConverter))]
    public bool ShowBoxes { get; set; } = true;

    /// <summary>
    /// 边界框线宽，null表示自动
    /// </summary>
    [JsonProperty("line_width")]
    [YamlMember(Alias = "line_width")]
    [DefaultValue(null)]
    [Display(Name = "边界框线宽", GroupName = "保存选项", Description = "边界框线宽，null表示自动")]
    [PropertyItem(typeof(Int32SliderTextPropertyItem))]
    [Range(1, 10)]
    [TypeConverter(typeof(NullableInt32Converter))]
    public int? LineWidth { get; set; }

    // ============ 导出参数 ============

    /// <summary>
    /// 导出格式：torchscript, onnx, openvino, engine, coreml, saved_model, pb, tflite, edgetpu, tfjs, paddle
    /// </summary>
    [JsonProperty("format")]
    [YamlMember(Alias = "format")]
    [DefaultValue("torchscript")]
    [Display(Name = "导出格式", GroupName = "导出参数", Description = "导出格式：torchscript, onnx, openvino, engine, coreml, saved_model, pb, tflite, edgetpu, tfjs, paddle")]
    [GetMethodNameSource(nameof(GetFormats))]
    [PropertyItem(typeof(ComboBoxPropertyItem))]
    [PropertyViewItem(typeof(TextPropertyViewItem))]
    public string Format { get; set; } = "torchscript";
    public IEnumerable<string> GetFormats()
    {
        yield return "torchscript";
        yield return "onnx";
        yield return "openvino";
        yield return "engine";
        yield return "coreml";
        yield return "saved_model";
        yield return "pb";
        yield return "tflite";
        yield return "edgetpu";
        yield return "tfjs";
        yield return "paddle";
    }

    /// <summary>
    /// 是否导出Keras格式
    /// </summary>
    [JsonProperty("keras")]
    [YamlMember(Alias = "keras")]
    [DefaultValue(false)]
    [Display(Name = "导出Keras格式", GroupName = "导出参数", Description = "是否导出Keras格式")]
    [TextValueConverter(typeof(YoloConfigBoolenValueConverter))]
    public bool Keras { get; set; } = false;

    /// <summary>
    /// 是否优化模型
    /// </summary>
    [JsonProperty("optimize")]
    [YamlMember(Alias = "optimize")]
    [DefaultValue(false)]
    [Display(Name = "优化模型", GroupName = "导出参数", Description = "是否优化模型")]
    [TextValueConverter(typeof(YoloConfigBoolenValueConverter))]
    public bool Optimize { get; set; } = false;

    /// <summary>
    /// 是否量化到int8
    /// </summary>
    [JsonProperty("int8")]
    [YamlMember(Alias = "int8")]
    [DefaultValue(false)]
    [Display(Name = "INT8量化", GroupName = "导出参数", Description = "是否量化到int8")]
    [TextValueConverter(typeof(YoloConfigBoolenValueConverter))]
    public bool Int8 { get; set; } = false;

    /// <summary>
    /// 是否动态批次/维度
    /// </summary>
    [JsonProperty("dynamic")]
    [YamlMember(Alias = "dynamic")]
    [DefaultValue(false)]
    [Display(Name = "动态批次", GroupName = "导出参数", Description = "是否动态批次/维度")]
    [TextValueConverter(typeof(YoloConfigBoolenValueConverter))]
    public bool Dynamic { get; set; } = false;

    /// <summary>
    /// 是否简化模型
    /// </summary>
    [JsonProperty("simplify")]
    [YamlMember(Alias = "simplify")]
    [DefaultValue(true)]
    [Display(Name = "简化模型", GroupName = "导出参数", Description = "是否简化模型")]
    [TextValueConverter(typeof(YoloConfigBoolenValueConverter))]
    public bool Simplify { get; set; } = true;

    /// <summary>
    /// ONNX opset版本
    /// </summary>
    [JsonProperty("opset")]
    [YamlMember(Alias = "opset")]
    [DefaultValue(null)]
    [Display(Name = "ONNX Opset版本", GroupName = "导出参数", Description = "ONNX opset版本")]
    [PropertyItem(typeof(Int32SliderTextPropertyItem))]
    [Range(7, 18)]
    [TypeConverter(typeof(NullableInt32Converter))]
    public int? Opset { get; set; }

    /// <summary>
    /// TensorRT工作空间大小（GB）
    /// </summary>
    [JsonProperty("workspace")]
    [YamlMember(Alias = "workspace")]
    [DefaultValue(4)]
    [Display(Name = "TensorRT工作空间", GroupName = "导出参数", Description = "TensorRT工作空间大小（GB）")]
    [PropertyItem(typeof(Int32SliderTextPropertyItem))]
    [Range(1, 64)]
    [TypeConverter(typeof(NullableInt32Converter))]
    public int? Workspace { get; set; } = 4;

    /// <summary>
    /// 是否在导出时包含NMS
    /// </summary>
    [JsonProperty("nms")]
    [YamlMember(Alias = "nms")]
    [DefaultValue(false)]
    [Display(Name = "包含NMS", GroupName = "导出参数", Description = "是否在导出时包含NMS")]
    [TextValueConverter(typeof(YoloConfigBoolenValueConverter))]
    public bool IncludeNMS { get; set; } = false;

    // ============ 超参数 ============

    /// <summary>
    /// 初始学习率
    /// </summary>
    [JsonProperty("lr0")]
    [YamlMember(Alias = "lr0")]
    [DefaultValue(0.01f)]
    [Display(Name = "初始学习率", GroupName = "超参数", Description = "初始学习率")]
    [PropertyItem(typeof(FloatSliderTextPropertyItem))]
    [Range(0.00001, 1.0)]
    public float InitialLearningRate { get; set; } = 0.01f;

    /// <summary>
    /// 最终学习率因子
    /// </summary>
    [JsonProperty("lrf")]
    [YamlMember(Alias = "lrf")]
    [DefaultValue(0.01f)]
    [Display(Name = "最终学习率因子", GroupName = "超参数", Description = "最终学习率因子")]
    [PropertyItem(typeof(FloatSliderTextPropertyItem))]
    [Range(0.00001, 1.0)]
    public float FinalLearningRateFactor { get; set; } = 0.01f;

    /// <summary>
    /// SGD动量
    /// </summary>
    [JsonProperty("momentum")]
    [YamlMember(Alias = "momentum")]
    [DefaultValue(0.937f)]
    [Display(Name = "SGD动量", GroupName = "超参数", Description = "SGD动量")]
    [PropertyItem(typeof(FloatSliderTextPropertyItem))]
    [Range(0.0, 1.0)]
    public float Momentum { get; set; } = 0.937f;

    /// <summary>
    /// 权重衰减
    /// </summary>
    [JsonProperty("weight_decay")]
    [YamlMember(Alias = "weight_decay")]
    [DefaultValue(0.0005f)]
    [Display(Name = "权重衰减", GroupName = "超参数", Description = "权重衰减")]
    [PropertyItem(typeof(FloatSliderTextPropertyItem))]
    [Range(0.0, 0.01)]
    public float WeightDecay { get; set; } = 0.0005f;

    /// <summary>
    /// 热身轮次
    /// </summary>
    [JsonProperty("warmup_epochs")]
    [YamlMember(Alias = "warmup_epochs")]
    [DefaultValue(3.0f)]
    [Display(Name = "热身轮次", GroupName = "超参数", Description = "热身轮次")]
    [PropertyItem(typeof(FloatSliderTextPropertyItem))]
    [Range(0.0, 10.0)]
    public float WarmupEpochs { get; set; } = 3.0f;

    /// <summary>
    /// 热身动量
    /// </summary>
    [JsonProperty("warmup_momentum")]
    [YamlMember(Alias = "warmup_momentum")]
    [DefaultValue(0.8f)]
    [Display(Name = "热身动量", GroupName = "超参数", Description = "热身动量")]
    [PropertyItem(typeof(FloatSliderTextPropertyItem))]
    [Range(0.0, 1.0)]
    public float WarmupMomentum { get; set; } = 0.8f;

    /// <summary>
    /// 热身偏置学习率
    /// </summary>
    [JsonProperty("warmup_bias_lr")]
    [YamlMember(Alias = "warmup_bias_lr")]
    [DefaultValue(0.1f)]
    [Display(Name = "热身偏置学习率", GroupName = "超参数", Description = "热身偏置学习率")]
    [PropertyItem(typeof(FloatSliderTextPropertyItem))]
    [Range(0.0, 1.0)]
    public float WarmupBiasLR { get; set; } = 0.1f;

    // ============ 损失权重 ============

    /// <summary>
    /// 边界框损失权重
    /// </summary>
    [JsonProperty("box")]
    [YamlMember(Alias = "box")]
    [DefaultValue(7.5f)]
    [Display(Name = "边界框损失权重", GroupName = "损失权重", Description = "边界框损失权重")]
    [PropertyItem(typeof(FloatSliderTextPropertyItem))]
    [Range(0.0, 100.0)]
    public float BoxLossWeight { get; set; } = 7.5f;

    /// <summary>
    /// 分类损失权重
    /// </summary>
    [JsonProperty("cls")]
    [YamlMember(Alias = "cls")]
    [DefaultValue(0.5f)]
    [Display(Name = "分类损失权重", GroupName = "损失权重", Description = "分类损失权重")]
    [PropertyItem(typeof(FloatSliderTextPropertyItem))]
    [Range(0.0, 100.0)]
    public float ClassLossWeight { get; set; } = 0.5f;

    /// <summary>
    /// DFL损失权重
    /// </summary>
    [JsonProperty("dfl")]
    [YamlMember(Alias = "dfl")]
    [DefaultValue(1.5f)]
    [Display(Name = "DFL损失权重", GroupName = "损失权重", Description = "DFL损失权重")]
    [PropertyItem(typeof(FloatSliderTextPropertyItem))]
    [Range(0.0, 100.0)]
    public float DFLLossWeight { get; set; } = 1.5f;

    /// <summary>
    /// 姿态损失权重（仅姿态检测）
    /// </summary>
    [JsonProperty("pose")]
    [YamlMember(Alias = "pose")]
    [DefaultValue(12.0f)]
    [Display(Name = "姿态损失权重", GroupName = "损失权重", Description = "姿态损失权重（仅姿态检测）")]
    [PropertyItem(typeof(FloatSliderTextPropertyItem))]
    [Range(0.0, 100.0)]
    public float PoseLossWeight { get; set; } = 12.0f;

    /// <summary>
    /// 关键点损失权重
    /// </summary>
    [JsonProperty("kobj")]
    [YamlMember(Alias = "kobj")]
    [DefaultValue(1.0f)]
    [Display(Name = "关键点损失权重", GroupName = "损失权重", Description = "关键点损失权重")]
    [PropertyItem(typeof(FloatSliderTextPropertyItem))]
    [Range(0.0, 100.0)]
    public float KeypointLossWeight { get; set; } = 1.0f;

    /// <summary>
    /// 名义批量大小
    /// </summary>
    [JsonProperty("nbs")]
    [YamlMember(Alias = "nbs")]
    [DefaultValue(64)]
    [Display(Name = "名义批量大小", GroupName = "损失权重", Description = "名义批量大小")]
    [PropertyItem(typeof(Int32SliderTextPropertyItem))]
    [Range(1, 1024)]
    public int NominalBatchSize { get; set; } = 64;

    // ============ 数据增强参数 ============

    /// <summary>
    /// HSV色相增强（分数）
    /// </summary>
    [JsonProperty("hsv_h")]
    [YamlMember(Alias = "hsv_h")]
    [DefaultValue(0.015f)]
    [Display(Name = "HSV色相增强", GroupName = "数据增强", Description = "HSV色相增强（分数）")]
    [PropertyItem(typeof(FloatSliderTextPropertyItem))]
    [Range(0.0, 1.0)]
    public float HSVHue { get; set; } = 0.015f;

    /// <summary>
    /// HSV饱和度增强（分数）
    /// </summary>
    [JsonProperty("hsv_s")]
    [YamlMember(Alias = "hsv_s")]
    [DefaultValue(0.7f)]
    [Display(Name = "HSV饱和度增强", GroupName = "数据增强", Description = "HSV饱和度增强（分数）")]
    [PropertyItem(typeof(FloatSliderTextPropertyItem))]
    [Range(0.0, 1.0)]
    public float HSVSaturation { get; set; } = 0.7f;

    /// <summary>
    /// HSV明度增强（分数）
    /// </summary>
    [JsonProperty("hsv_v")]
    [YamlMember(Alias = "hsv_v")]
    [DefaultValue(0.4f)]
    [Display(Name = "HSV明度增强", GroupName = "数据增强", Description = "HSV明度增强（分数）")]
    [PropertyItem(typeof(FloatSliderTextPropertyItem))]
    [Range(0.0, 1.0)]
    public float HSVValue { get; set; } = 0.4f;

    /// <summary>
    /// 旋转角度（度）
    /// </summary>
    [JsonProperty("degrees")]
    [YamlMember(Alias = "degrees")]
    [DefaultValue(0.0f)]
    [Display(Name = "旋转角度", GroupName = "数据增强", Description = "旋转角度（度）")]
    [PropertyItem(typeof(FloatSliderTextPropertyItem))]
    [Range(0.0, 180.0)]
    public float Rotation { get; set; } = 0.0f;

    /// <summary>
    /// 平移（分数）
    /// </summary>
    [JsonProperty("translate")]
    [YamlMember(Alias = "translate")]
    [DefaultValue(0.1f)]
    [Display(Name = "平移", GroupName = "数据增强", Description = "平移（分数）")]
    [PropertyItem(typeof(FloatSliderTextPropertyItem))]
    [Range(0.0, 1.0)]
    public float Translation { get; set; } = 0.1f;

    /// <summary>
    /// 缩放（分数）
    /// </summary>
    [JsonProperty("scale")]
    [YamlMember(Alias = "scale")]
    [DefaultValue(0.5f)]
    [Display(Name = "缩放", GroupName = "数据增强", Description = "缩放（分数）")]
    [PropertyItem(typeof(FloatSliderTextPropertyItem))]
    [Range(0.0, 1.0)]
    public float Scale { get; set; } = 0.5f;

    /// <summary>
    /// 剪切（度）
    /// </summary>
    [JsonProperty("shear")]
    [YamlMember(Alias = "shear")]
    [DefaultValue(0.0f)]
    [Display(Name = "剪切", GroupName = "数据增强", Description = "剪切（度）")]
    [PropertyItem(typeof(FloatSliderTextPropertyItem))]
    [Range(0.0, 180.0)]
    public float Shear { get; set; } = 0.0f;

    /// <summary>
    /// 透视变换（分数）
    /// </summary>
    [JsonProperty("perspective")]
    [YamlMember(Alias = "perspective")]
    [DefaultValue(0.0f)]
    [Display(Name = "透视变换", GroupName = "数据增强", Description = "透视变换（分数）")]
    [PropertyItem(typeof(FloatSliderTextPropertyItem))]
    [Range(0.0, 1.0)]
    public float Perspective { get; set; } = 0.0f;

    /// <summary>
    /// 上下翻转概率
    /// </summary>
    [JsonProperty("flipud")]
    [YamlMember(Alias = "flipud")]
    [DefaultValue(0.0f)]
    [Display(Name = "上下翻转概率", GroupName = "数据增强", Description = "上下翻转概率")]
    [PropertyItem(typeof(FloatSliderTextPropertyItem))]
    [Range(0.0, 1.0)]
    public float FlipUD { get; set; } = 0.0f;

    /// <summary>
    /// 左右翻转概率
    /// </summary>
    [JsonProperty("fliplr")]
    [YamlMember(Alias = "fliplr")]
    [DefaultValue(0.5f)]
    [Display(Name = "左右翻转概率", GroupName = "数据增强", Description = "左右翻转概率")]
    [PropertyItem(typeof(FloatSliderTextPropertyItem))]
    [Range(0.0, 1.0)]
    public float FlipLR { get; set; } = 0.5f;

    /// <summary>
    /// BGR通道交换概率
    /// </summary>
    [JsonProperty("bgr")]
    [YamlMember(Alias = "bgr")]
    [DefaultValue(0.0f)]
    [Display(Name = "BGR通道交换概率", GroupName = "数据增强", Description = "BGR通道交换概率")]
    [PropertyItem(typeof(FloatSliderTextPropertyItem))]
    [Range(0.0, 1.0)]
    public float BGR { get; set; } = 0.0f;

    /// <summary>
    /// 马赛克增强概率
    /// </summary>
    [JsonProperty("mosaic")]
    [YamlMember(Alias = "mosaic")]
    [DefaultValue(1.0f)]
    [Display(Name = "马赛克增强概率", GroupName = "数据增强", Description = "马赛克增强概率")]
    [PropertyItem(typeof(FloatSliderTextPropertyItem))]
    [Range(0.0, 1.0)]
    public float Mosaic { get; set; } = 1.0f;

    /// <summary>
    /// MixUp增强概率
    /// </summary>
    [JsonProperty("mixup")]
    [YamlMember(Alias = "mixup")]
    [DefaultValue(0.0f)]
    [Display(Name = "MixUp增强概率", GroupName = "数据增强", Description = "MixUp增强概率")]
    [PropertyItem(typeof(FloatSliderTextPropertyItem))]
    [Range(0.0, 1.0)]
    public float Mixup { get; set; } = 0.0f;

    /// <summary>
    /// CutMix增强概率
    /// </summary>
    [JsonProperty("cutmix")]
    [YamlMember(Alias = "cutmix")]
    [DefaultValue(0.0f)]
    [Display(Name = "CutMix增强概率", GroupName = "数据增强", Description = "CutMix增强概率")]
    [PropertyItem(typeof(FloatSliderTextPropertyItem))]
    [Range(0.0, 1.0)]
    public float Cutmix { get; set; } = 0.0f;

    /// <summary>
    /// 复制粘贴增强概率
    /// </summary>
    [JsonProperty("copy_paste")]
    [YamlMember(Alias = "copy_paste")]
    [DefaultValue(0.0f)]
    [Display(Name = "复制粘贴增强概率", GroupName = "数据增强", Description = "复制粘贴增强概率")]
    [PropertyItem(typeof(FloatSliderTextPropertyItem))]
    [Range(0.0, 1.0)]
    public float CopyPaste { get; set; } = 0.0f;

    /// <summary>
    /// 复制粘贴模式：flip或keep
    /// </summary>
    [JsonProperty("copy_paste_mode")]
    [YamlMember(Alias = "copy_paste_mode")]
    [DefaultValue("flip")]
    [Display(Name = "复制粘贴模式", GroupName = "数据增强", Description = "复制粘贴模式：flip或keep")]
    public string CopyPasteMode { get; set; } = "flip";

    /// <summary>
    /// 自动增强方法：randaugment, autoaugment, augmix
    /// </summary>
    [JsonProperty("auto_augment")]
    [YamlMember(Alias = "auto_augment")]
    [DefaultValue("randaugment")]
    [Display(Name = "自动增强方法", GroupName = "数据增强", Description = "自动增强方法：randaugment, autoaugment, augmix")]
    [GetMethodNameSource(nameof(GetAutoAugments))]
    [PropertyItem(typeof(ComboBoxPropertyItem))]
    [PropertyViewItem(typeof(TextPropertyViewItem))]
    public string AutoAugment { get; set; } = "randaugment";
    public IEnumerable<string> GetAutoAugments()
    {
        yield return "randaugment";
        yield return "autoaugment";
        yield return "augmix";
    }

    /// <summary>
    /// 随机擦除概率
    /// </summary>
    [JsonProperty("erasing")]
    [YamlMember(Alias = "erasing")]
    [DefaultValue(0.4f)]
    [Display(Name = "随机擦除概率", GroupName = "数据增强", Description = "随机擦除概率")]
    [PropertyItem(typeof(FloatSliderTextPropertyItem))]
    [Range(0.0, 1.0)]
    public float RandomErasing { get; set; } = 0.4f;

    // ============ 其他参数 ============

    /// <summary>
    /// 配置文件路径
    /// </summary>
    [JsonProperty("cfg")]
    [YamlMember(Alias = "cfg")]
    [DefaultValue(null)]
    [Display(Name = "配置文件", GroupName = "其他参数", Description = "配置文件路径")]
    [ReadOnly(true)]
    public string ConfigFile { get; set; }

    /// <summary>
    /// 跟踪器配置文件
    /// </summary>
    [JsonProperty("tracker")]
    [YamlMember(Alias = "tracker")]
    [DefaultValue("botsort.yaml")]
    [Display(Name = "跟踪器配置", GroupName = "其他参数", Description = "跟踪器配置文件")]
    [ReadOnly(true)]
    public string Tracker { get; set; } = "botsort.yaml";

    /// <summary>
    /// 结果保存目录
    /// </summary>
    [JsonProperty("save_dir")]
    [YamlMember(Alias = "save_dir")]
    [DefaultValue("runs/detect")]
    [Display(Name = "保存目录", GroupName = "其他参数", Description = "结果保存目录")]
    [ReadOnly(true)]
    public string SaveDirectory { get; set; } = "runs/detect";
}

