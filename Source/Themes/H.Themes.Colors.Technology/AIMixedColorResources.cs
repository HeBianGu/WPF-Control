// Copyright (c) HeBianGu Authors. All Rights Reserved.
// Author: HeBianGu
// Github: https://github.com/HeBianGu/WPF-Control
// Document: https://hebiangu.github.io/WPF-Control-Docs
// QQ:908293466 Group:971261058
// bilibili: https://space.bilibili.com/370266611
// Licensed under the MIT License (the "License")

using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace H.Themes.Colors.Technology;

[Display(Name = "模型锻造", GroupName = "AI混合配色", Description = "深灰黑、电蓝、橙色、绿色混合，适合模型训练和训练日志", Order = 1100, Prompt = "混合")]
public class ModelForgeColorResource : ResxColorResourceBase
{
    public ModelForgeColorResource() => this.IsDark = true;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Technology;component/ModelForge.xaml") };
}

[Display(Name = "张量流光", GroupName = "AI混合配色", Description = "深蓝黑、青色、紫色、琥珀混合，适合算法可视化和特征图展示", Order = 1101, Prompt = "混合")]
public class TensorFlowColorResource : ResxColorResourceBase
{
    public TensorFlowColorResource() => this.IsDark = true;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Technology;component/TensorFlow.xaml") };
}

[Display(Name = "神经绽放", GroupName = "AI混合配色", Description = "深紫黑、粉紫、青蓝、柔绿混合，适合 AI 展示页和可视化首页", Order = 1102, Prompt = "混合")]
public class NeuralBloomColorResource : ResxColorResourceBase
{
    public NeuralBloomColorResource() => this.IsDark = true;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Technology;component/NeuralBloom.xaml") };
}

[Display(Name = "推理夜航", GroupName = "AI混合配色", Description = "深空蓝、蓝色、青色、黄色混合，适合推理服务监控和实时检测大屏", Order = 1103, Prompt = "混合")]
public class InferenceNightColorResource : ResxColorResourceBase
{
    public InferenceNightColorResource() => this.IsDark = true;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Technology;component/InferenceNight.xaml") };
}
