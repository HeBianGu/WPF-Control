// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Common;
using System.Resources;
using System.Runtime.CompilerServices;

namespace H.Controls.Form;

public class TabFormPresenter : FormPresenter, ITabFormOption
{
    public TabFormPresenter()
    {

    }
    public TabFormPresenter(object value) : base(value)
    {
        this.UpdateTabNames();
    }

    public Dock TabStripPlacement { get; set; } = Dock.Top;

    public bool UseTabAttribute { get; set; } = false;

    public ObservableCollection<string> TabNames { get; set; } = new ObservableCollection<string>();

    public string UseTabNames { get; set; }

    public IComparer<string> TabOrderComparer { get; set; }

    public void UpdateTabNames()
    {
        var names = this.UseTabAttribute ? this.GetTabs() : this.GetGroups();
        var gs = this.UseTabNames?.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
        if (this.TabOrderComparer != null)
            gs = gs.Order(this.TabOrderComparer).ToArray();
        if (gs == null || gs.Count() == 0)
        {
            this.TabNames = names.Distinct().ToObservable();
        }
        else
        {
            this.TabNames = gs.ToObservable();
        }
    }

    private IEnumerable<string> GetGroups()
    {
        return this.GetNames(p => p.Attributes.OfType<DisplayAttribute>()?.FirstOrDefault()?.GroupName);

    }

    private IEnumerable<string> GetNames(Func<PropertyDescriptor, string> selector)
    {
        foreach (var p in TypeDescriptor.GetProperties(this.SelectObject).OfType<PropertyDescriptor>())
        {
            if (p.Attributes.OfType<BrowsableAttribute>().Any(x => x.Browsable == false))
                continue;
            var name = selector?.Invoke(p);
            if (name == null)
                continue;
            var resx = this.SelectObject.GetType().GetPropertyGroupNameResx(p.Name);
            var names = (resx ?? name).Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            foreach (var n in names)
            {
                yield return n;
            }
        }
    }

    private IEnumerable<string> GetTabs()
    {
        return this.GetNames(p => p.Attributes.OfType<TabAttribute>()?.FirstOrDefault()?.Tab);
    }
}
