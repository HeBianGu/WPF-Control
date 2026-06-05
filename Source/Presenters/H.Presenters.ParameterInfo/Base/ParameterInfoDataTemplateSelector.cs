// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Presenters.ParameterInfo.Presenters;

namespace H.Presenters.ParameterInfo.Base;

public class ParameterInfoDataTemplateSelector : DataTemplateSelector
{
    public DataTemplate ComboBoxDataTemplate { get; set; }
    public DataTemplate TextDataTemplate { get; set; }
    public DataTemplate CheckBoxDataTemplate { get; set; }
    public DataTemplate CommandDataTemplate { get; set; }
    public override DataTemplate SelectTemplate(object item, DependencyObject container)
    {
        if (item is ParameterInfoPresenter parameterInfoPresenter && parameterInfoPresenter.ParameterInfo is IParameterInfo parameterInfo)
        {
            if (parameterInfo.Options != null && parameterInfo.Options.Count > 0)
                return this.ComboBoxDataTemplate;
            if (parameterInfo.Type == ParameterType.Boolean)
                return this.CheckBoxDataTemplate;
            if (parameterInfo.Type == ParameterType.Integer || parameterInfo.Type == ParameterType.Float)
                return this.TextDataTemplate;
            if (parameterInfo.Type == ParameterType.String)
                return this.TextDataTemplate;
            if (parameterInfo.Type == ParameterType.Command)
                return this.CommandDataTemplate;
        }
        return this.TextDataTemplate;
    }
}


