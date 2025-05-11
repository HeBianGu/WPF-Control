// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Services.Message.IODialog;

public class IOFileDialogOptionActions
{
    public static Action<IIOFileDialogOption> Image = x => x.Filter = IIOFileDialogOption.defaultImageFilter;
    public static Action<IIOFileDialogOption> Zip = x => x.Filter = IIOFileDialogOption.defaultZipFilter;
    public static Action<IIOFileDialogOption> Video = x => x.Filter = IIOFileDialogOption.defaultVideoFilter;
    public static Action<IIOFileDialogOption> Excel = x => x.Filter = IIOFileDialogOption.defaultExcelFilter;
    public static Action<IIOFileDialogOption> Json = x => x.Filter = IIOFileDialogOption.defaultJsonFilter;
    public static Action<IIOFileDialogOption> Word = x => x.Filter = IIOFileDialogOption.defaultWordFilter;
    public static Action<IIOFileDialogOption> Audio = x => x.Filter = IIOFileDialogOption.defaultAudioFilter;
    public static Action<IIOFileDialogOption> Text = x => x.Filter = IIOFileDialogOption.defaultTextFilter;
    public static Action<IIOFileDialogOption> Pdf = x => x.Filter = IIOFileDialogOption.defaultPdfFilter;
    public static Action<IIOFileDialogOption> Xml = x => x.Filter = IIOFileDialogOption.defaultXmlFilter;
}

