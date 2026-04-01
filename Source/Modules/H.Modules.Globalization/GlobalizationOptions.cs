// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Common.Interfaces;
using H.Controls.Form.Attributes;
using H.Controls.Form.PropertyItem.Attribute;
using H.Controls.Form.PropertyItem.ComboBoxPropertyItems;
using H.Controls.Form.PropertyItems.Base;
using H.Extensions.FontIcon;
using H.Extensions.Mvvm.Commands;
using H.Extensions.Setting;
using H.Mvvm.Commands;
using H.Services.AppPath;
using H.Services.Message;
using H.Services.Setting;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Threading;
using System.Xml.Serialization;

namespace H.Modules.Globalization;

[Display(Name = "语言设置", GroupName = SettingGroupNames.GroupStyle, Description = "语言设置设置的信息")]
public class GlobalizationOptions : IocOptionInstance<GlobalizationOptions>, IGlobalizationOptions, IPropertyItemValueChanged
{
    public GlobalizationOptions()
    {
        this.SupportedCultureInfos = this.GetSupportedCultureInfos().AsReadOnly();
    }
    private CultureInfo _CultureInfo;
    [JsonIgnore]
    [XmlIgnore]
    [GetPropertyNameSource(nameof(SupportedCultureInfos))]
    [DisplayMemberPath("NativeName")]
    [PropertyItem(typeof(ComboBoxPropertyItem))]
    [Display(Name = "语言")]
    public CultureInfo CultureInfo
    {
        get { return _CultureInfo; }
        set
        {
            if (_CultureInfo == value)
                return;
            _CultureInfo = value;
            RaisePropertyChanged();
            this.UpdateCultureInfo(value);
        }
    }

    [Browsable(false)]
    public string CultureInfoKey { get; set; }

    public void UpdateCultureInfo(CultureInfo culture)
    {
        CultureInfo.DefaultThreadCurrentCulture = culture;
        CultureInfo.DefaultThreadCurrentUICulture = culture;
        //// 3) 让 WPF 绑定采用该文化（影响 StringFormat、数字/日期等）
        //FrameworkElement.LanguageProperty.OverrideMetadata(
        //    typeof(FrameworkElement),
        //    new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(culture.IetfLanguageTag)));
        Thread.CurrentThread.CurrentUICulture = culture;
        Thread.CurrentThread.CurrentCulture = culture;
        Application.Current.Dispatcher.Invoke(() =>
        {
            Application.Current.Dispatcher.Thread.CurrentUICulture = culture;
            Application.Current.Dispatcher.Thread.CurrentCulture = culture;
        });
    }
    [JsonIgnore]
    public IReadOnlyCollection<CultureInfo> SupportedCultureInfos { get; set; }

    public IList<CultureInfo> GetSupportedCultureInfos()
    {
        var name = Assembly.GetEntryAssembly().GetName().Name;
        List<CultureInfo> infos = new List<CultureInfo>();
        try
        {
            foreach (var dir in Directory.GetDirectories(AppDomain.CurrentDomain.BaseDirectory))
            {
                try
                {
                    if (!File.Exists(Path.Combine(dir, $"{name}.resources.dll")))
                        continue;
                    DirectoryInfo langDir = new DirectoryInfo(dir);
                    infos.Add(CultureInfo.GetCultureInfo(langDir.Name));
                }
                catch { }
            }
        }
        catch { }
        infos.Sort((l, r) => string.Compare(l.Name, r.Name));
        return infos;
    }

    public override bool Save(out string message)
    {
        this.CultureInfoKey = this.CultureInfo.Name;
        return base.Save(out message);
    }

    public override bool Load(out string message)
    {
        var r = base.Load(out message);
        var find = this.SupportedCultureInfos.FirstOrDefault(x => x.Name == this.CultureInfoKey);
        this.CultureInfo = find ?? new CultureInfo("zh-Hans");
        this.UpdateResx();
        return r;
    }

    public async void OnPropertyVlaueChanged(string propertyName, object o, object n)
    {
        if (propertyName == nameof(CultureInfo))
        {
            if (o == null)
                return;
            var r = await IocMessage.ShowDialogMessage("语言修改需要重启软件才生效，是否立即重启?");
            if (r != true)
                return;
            this.Save(out string message);
            Application.Current.Shutdown();
        }
    }

    public override void LoadDefault()
    {
        base.LoadDefault();
        var systemRegionCulture = CultureInfo.CurrentCulture;
        var systemMatch = this.GetSupportedCultureInfos().FirstOrDefault(x => x.Name == systemRegionCulture.Name);
        this.CultureInfo = systemMatch ?? new CultureInfo("zh-Hans");

    }
}
