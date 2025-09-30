// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Common.Interfaces;

public interface IZoombox
{
    void FillToBounds();
    void FitToBounds();
    void Zoom(double percentage);
    void Zoom(double percentage, Point relativeTo);
    void ZoomTo(double scale);
    void ZoomTo(double scale, Point relativeTo);
    void ZoomTo(Point position);
    void ZoomTo(Rect region);
}