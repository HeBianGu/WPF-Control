// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.Form.PropertyItem.Attribute.SourcePropertyItem
{
    public class MethodNameSourcePropertyItemAttribute : SourcePropertyItemBaseAttribute
    {
        private MethodInfo _sourceMethodInfo;
        public MethodNameSourcePropertyItemAttribute(Type type, string methodName) : base(type)
        {
            this.MethodName = methodName;
        }

        public string MethodName { get; set; }

        public override IEnumerable GetSource(PropertyInfo propertyInfo, object obj)
        {
            if (_sourceMethodInfo == null)
                _sourceMethodInfo = obj.GetType().GetMethod(this.MethodName);
            return _sourceMethodInfo.Invoke(obj, null) as IEnumerable;
        }
    }
}
