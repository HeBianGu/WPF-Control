// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.Diagram.Presenter.Expressions;

//步骤1：新建自定义类型
public class DemoExpressionClass
{
    //步骤2：定义一个属性，设置ResultParameters
    [Expressionable]
    [Display(Name = "示例：自定义整型", Description = "用来演示如何增加节点表达式参数")]
    public int IntValue { get; set; } = 100;
    [Expressionable]
    [Display(Name = "示例：自定义字符串", Description = "用来演示如何增加节点表达式参数")]
    public string StringValue { get; set; } = "Hello World";
    [Expressionable]
    [Display(Name = "示例：自定义双精度", Description = "用来演示如何增加节点表达式参数")]
    public double DoubleValue { get; set; } = 3.14;
}
