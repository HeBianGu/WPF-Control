// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Runtime.CompilerServices;

namespace H.Controls.Form;

public class TabFormPresenter : FormPresenter
{
    public TabFormPresenter()
    {

    }
    public TabFormPresenter(object value) : base(value)
    {
        this.UpdateTabNames();
    }

    public ObservableCollection<string> TabNames { get; set; } = new ObservableCollection<string>();

    public void UpdateTabNames()
    {
        var gs = this.UseGroupNames?.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
        if (this.GroupOrderComparer != null)
            gs = gs.Order(this.GroupOrderComparer).ToArray();
        if (gs == null || gs.Count() == 0)
        {
            var names = GetGroups().Distinct();
            this.TabNames = names.ToObservable();
        }
        else
        {
            this.TabNames = gs.ToObservable();
        }
    }

    private IEnumerable<string> GetGroups()
    {
        foreach (var p in TypeDescriptor.GetProperties(this.SelectObject).OfType<PropertyDescriptor>())
        {
            if (p.Attributes.OfType<BrowsableAttribute>().Any(x => x.Browsable == false))
                continue;
            foreach (var d in p.Attributes.OfType<DisplayAttribute>())
            {
                if (d.GroupName == null)
                    continue;
                var names = d.GroupName.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                foreach (var n in names)
                {
                    yield return n;
                }
            }
        }
    }
}
