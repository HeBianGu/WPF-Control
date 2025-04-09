// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

global using H.Extensions.Common;
using H.Extensions.Attach;

namespace H.Modules.Guide;

public static class GuideExtension
{
    public static GuideTree GetGuideTree(this UIElement element, Predicate<UIElement> predicate = null)
    {
        List<UIElement> items = element.GetChildren<UIElement>(l => Cattach.GetUseGuide(l) && Cattach.GetGuideParentTitle(l) == null)?.ToList();
        IEnumerable<UIElement> roots = items.Where(l => Cattach.GetGuideParentTitle(l) == null && l.Visibility == Visibility.Visible);
        List<GuideTreeNode> guideTreeNodes = new List<GuideTreeNode>();
        Action<GuideTreeNode> build = null;
        Predicate<UIElement> cpredicate = x =>
        {
            if (x.Visibility != Visibility.Visible)
                return false;
            if (x.IsVisible == false)
                return false;
            if (x.Opacity <= 0)
                return false;
            if (x.RenderSize.Width < 10 || x.RenderSize.Height < 10)
                return false;
            if (x is FrameworkElement framework && framework.IsLoaded == false)
                return false;
            return predicate?.Invoke(x) != false;
        };
        build = parent =>
        {
            IEnumerable<UIElement> children = items.Where(x => cpredicate.Invoke(x));
            children = children.Where(x => Cattach.GetGuideParentTitle(x) != null && Cattach.GetGuideParentTitle(x) == Cattach.GetGuideTitle(parent.Element)?.ToString());
            foreach (UIElement child in children)
            {
                GuideTreeNode childNode = new GuideTreeNode(child);
                childNode.Parent = parent;
                parent.Chidren.Add(childNode);
                build.Invoke(childNode);
            }
        };

        foreach (UIElement root in roots.Where(x => cpredicate.Invoke(x)))
        {
            GuideTreeNode rootNode = new GuideTreeNode(root);
            guideTreeNodes.Add(rootNode);
            build.Invoke(rootNode);
        }

        return new GuideTree(guideTreeNodes);
    }

    public static UIElement GetAdornerElement()
    {
        var child = Application.Current.MainWindow.GetChild<UIElement>(x => Cattach.GetIsGuideAdonerElement(x));
        if (child == null)
            child = Application.Current.MainWindow.Content as UIElement;
        return child;
    }
}
