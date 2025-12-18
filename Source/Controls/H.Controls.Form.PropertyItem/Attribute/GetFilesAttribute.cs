// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Extensions.Common;
using System.IO;

namespace H.Controls.Form.PropertyItem.Attribute
{
    public interface IGetSource
    {
        IEnumerable GetSource(PropertyInfo propertyInfo, object obj);
    }
    public abstract class GetSourceAttribute : System.Attribute, IGetSource
    {
        public abstract IEnumerable GetSource(PropertyInfo propertyInfo, object obj);
    }

    public class GetFilesSourceAttribute : GetSourceAttribute
    {
        public GetFilesSourceAttribute(string folderPath)
        {
            this.FolderPath = folderPath;
        }

        public string FolderPath { get; set; }
        public SearchOption SearchOption { get; set; } = SearchOption.TopDirectoryOnly;
        public string SearchPattern { get; set; } = SearchPatterns.AllFiles;
        public override IEnumerable GetSource(PropertyInfo propertyInfo, object obj)
        {
            if (this.FolderPath == null)
                return null;
            return this.FolderPath.GetFiles(this.SearchPattern, this.SearchOption);
        }
    }

    public class GetBrushesSourceAttribute : GetSourceAttribute
    {
        public override IEnumerable GetSource(PropertyInfo propertyInfo, object obj)
        {
            return typeof(Brushes).GetProperties().Select(x => x.GetValue(null)).OfType<SolidColorBrush>();
        }
    }

    public class GetStandardBrushesSourceAttribute : GetSourceAttribute
    {
        public override IEnumerable GetSource(PropertyInfo propertyInfo, object obj)
        {
            yield return Brushes.Transparent;
            yield return Brushes.White;
            yield return Brushes.Gray;
            yield return Brushes.Black;
            yield return Brushes.Red;
            yield return Brushes.Green;
            yield return Brushes.Blue;
            yield return Brushes.Yellow;
            yield return Brushes.Orange;
            yield return Brushes.Purple;
        }
    }

    public class GetColorsSourceAttribute : GetSourceAttribute
    {
        public override IEnumerable GetSource(PropertyInfo propertyInfo, object obj)
        {
            return typeof(Colors).GetProperties().Select(x => x.GetValue(null)).OfType<Color>();
        }
    }

    public class GetStandardColorsSourceAttribute : GetSourceAttribute
    {
        public override IEnumerable GetSource(PropertyInfo propertyInfo, object obj)
        {
            yield return Colors.Transparent;
            yield return Colors.White;
            yield return Colors.Gray;
            yield return Colors.Black;
            yield return Colors.Red;
            yield return Colors.Green;
            yield return Colors.Blue;
            yield return Colors.Yellow;
            yield return Colors.Orange;
            yield return Colors.Purple;
        }
    }

    public class GetMethodNameSourceAttribute : GetSourceAttribute
    {
        private MethodInfo _sourceMethodInfo;
        public GetMethodNameSourceAttribute(string methodName)
        {
            this.MethodName = methodName;
        }

        public string MethodName { get; set; }

        public override IEnumerable GetSource(PropertyInfo propertyInfo, object obj)
        {
            if (_sourceMethodInfo == null)
                _sourceMethodInfo = obj.GetType().GetMethod(this.MethodName);
            if (_sourceMethodInfo.Invoke(obj, null) is IEnumerable objects)
                return objects;
            return null;
        }
    }

    public class GetPropertyNameSourceAttribute : GetSourceAttribute
    {
        private PropertyInfo _sourcePropertyInfo;
        public GetPropertyNameSourceAttribute(string propertyName)
        {
            this.PropertyName = propertyName;
        }

        public string PropertyName { get; set; }

        public override IEnumerable GetSource(PropertyInfo propertyInfo, object obj)
        {
            if (_sourcePropertyInfo == null)
                _sourcePropertyInfo = obj.GetType().GetProperty(this.PropertyName);
            return _sourcePropertyInfo.GetValue(obj) as IEnumerable;
        }
    }
}
