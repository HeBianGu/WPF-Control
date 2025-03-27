// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Mvvm.ViewModels.Tree;

public interface ITreeNode
{
    bool IsExpanded { get; set; }
    bool? IsChecked { get; set; }
    Visibility Visibility { get; set; }
}
