// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control



using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace H.Modules.Guide
{
    public class GuideTreeNode
    {
        public GuideTreeNode(UIElement element)
        {
            this.Element = element;
        }
        public UIElement Element { get; set; }

        public List<GuideTreeNode> Chidren { get; } = new List<GuideTreeNode>();

        public GuideTreeNode Parent { get; set; }

        public GuideTreeNode GetRoot()
        {
            if (this.Parent == null) return this;

            return this.Parent.GetRoot();
        }

        public GuideTreeNode GetRight()
        {
            if (this.Parent == null) return null;

            int c = this.Parent.Chidren.IndexOf(this);

            if (c >= this.Parent.Chidren.Count - 1)
            {
                return this.Parent.GetRight();
            }

            return this.Parent.Chidren[c + 1];
        }

        public GuideTreeNode GetNext()
        {
            if (this.Chidren.Count > 0)
            {
                return this.Chidren.First();
            }

            return this.GetRight();
        }
    }
}
