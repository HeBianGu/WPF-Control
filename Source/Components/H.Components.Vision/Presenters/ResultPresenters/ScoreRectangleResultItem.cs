// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")
namespace H.Components.Vision.Presenters.ResultPresenters;

public class ScoreRectangleResultItem : RectangleResultPresenterItem
{
    public ScoreRectangleResultItem(Rect rect, double score) : base(rect)
    {
        this.Score = score;
    }

    private double _score;
    [DataGridColumn("Auto", StringFormat = "{0:F2}%")]
    [Display(Name = "置信度", GroupName = "基础信息")]
    public double Score
    {
        get { return _score; }
        set
        {
            _score = value;
            RaisePropertyChanged();
        }
    }
}

