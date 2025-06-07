// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.Diagram.Presenter;

public class DiagramKeys
{
    public static ComponentResourceKey EditTextBox => new ComponentResourceKey(typeof(DiagramKeys), "S.TextBox.Eidt");

    public static ComponentResourceKey StateBorderAnimation => new ComponentResourceKey(typeof(DiagramKeys), "S.Border.State.Animation");

    public static ComponentResourceKey StateBorder => new ComponentResourceKey(typeof(DiagramKeys), "S.Border.State");


    public static ComponentResourceKey StateNodePath => new ComponentResourceKey(typeof(DiagramKeys), "S.Path.Node.State");

    public static ComponentResourceKey StatePortPath => new ComponentResourceKey(typeof(DiagramKeys), "S.Path.Port.State");

}
