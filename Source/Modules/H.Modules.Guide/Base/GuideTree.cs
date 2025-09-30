// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Modules.Guide.Base;

public class GuideTree
{
    public GuideTree(List<GuideTreeNode> roots)
    {
        this.Roots = roots;
        this.Current = roots?.FirstOrDefault();
    }
    public List<GuideTreeNode> Roots { get; } = new List<GuideTreeNode>();

    public GuideTreeNode Current { get; private set; }

    public int CurrentIndex { get; private set; } = 1;

    public GuideTreeNode Next()
    {
        this.CurrentIndex++;
        if (this.Current == null) return null;
        GuideTreeNode next = this.Current.GetNext();
        if (next == null)
        {
            int c = this.Roots.IndexOf(this.Current.GetRoot());
            if (c >= this.Roots.Count - 1)
                return this.Current = null;

            this.Current = this.Roots[c + 1];
            return this.Current;
        }
        return this.Current = next;
    }

    public void Foreach(Action<GuideTreeNode> action)
    {

    }
    public IEnumerable<GuideTreeNode> GetGuideTreeNodes()
    {
        foreach (var item in this.Roots)
        {
            foreach (var node in item.GetGuideTreeNodes())
            {
                yield return node;
            }
        }
    }
}
