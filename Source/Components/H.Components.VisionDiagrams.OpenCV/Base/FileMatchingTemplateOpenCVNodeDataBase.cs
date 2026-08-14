// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using H.Controls.Diagram.Presenter.Flowables;
global using H.Controls.Form.Attributes;
global using H.Services.Message;
namespace H.Components.VisionDiagrams.OpenCV.Base;

[Icon(FontIcons.BrowsePhotos)]
public abstract class OpenCVFileTemplateMatchingNodeDataBase : OpenCVDetectorNodeDataBase
{
    private string _templateFilePath;
    [Required]
    [Tab(VisionTabNames.RunParameters)]
    [Display(Name = "模板图片", GroupName = VisionTabNames.RunParameters, Description = "设置用于图像匹配的模板文件")]
    [PropertyItem(typeof(OpenFileDialogPropertyItem))]
    public string TemplateFilePath
    {
        get { return _templateFilePath; }
        set
        {
            _templateFilePath = value;
            RaisePropertyChanged();
        }
    }

    protected override async Task<IFlowableResult> BeforeInvokeAsync(IFlowableLinkData previors, IFlowableDiagramData diagram)
    {
        if (File.Exists(this.TemplateFilePath) == false)
        {
            bool? r = await IocMessage.Form?.ShowEdit(this, x => x.Title = $"{this.Name}:请先选择文件", null, x =>
            {
                x.UsePropertyNames = nameof(TemplateFilePath);
            });
            if (r != true)
                return this.Error("未设置模板图片地址");
        }
        return await base.BeforeInvokeAsync(previors, diagram);
    }
}

