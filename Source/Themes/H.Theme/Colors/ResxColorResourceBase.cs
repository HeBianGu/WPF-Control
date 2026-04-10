// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Themes.Colors;

public abstract class ResxColorResourceBase : ColorResourceBase
{
    const string resxFormat = "  <data name=\"{0}\" xml:space=\"preserve\">\r\n    <value>{1}</value>\r\n  </data>";

    public ResxColorResourceBase()
    {
        this.UpdateResx();
    }

    private void UpdateResx()
    {
        {
            string key = this.GetType().Name;
            string resx = Properties.Resources.ResourceManager.GetString(key);
#if DEBUG
            if (resx == null && this.Name != null)
                this.WhiteLine(key, this.Name);
#endif
            if (resx != null)
                this.Name = resx;
        }
        {
            string key = this.GetType().Name + "_GroupName";
            string resx = Properties.Resources.ResourceManager.GetString(key);
#if DEBUG
            if (resx == null && this.GroupName != null)
                this.WhiteLine(key, this.GroupName);
#endif
            if (resx != null)
                this.GroupName = resx;
        }
        {
            string key = this.GetType().Name + "_Prompt";
            string resx = Properties.Resources.ResourceManager.GetString(key);
#if DEBUG
            if (resx == null && this.Prompt != null)
                this.WhiteLine(key, this.Prompt);
#endif
            if (resx != null)
                this.Prompt = resx;
        }
        {
            string key = this.GetType().Name + "_Description";
            string resx = Properties.Resources.ResourceManager.GetString(key);
#if DEBUG
            if (resx == null && this.Description != null)
                this.WhiteLine(key, this.Description);
#endif
            if (resx != null)
                this.Description = resx;
        }
    }

    protected void WhiteLine(string key, string value)
    {
        string v = string.Format(resxFormat, key, value);
        System.Diagnostics.Debug.WriteLine(v);
    }
}
