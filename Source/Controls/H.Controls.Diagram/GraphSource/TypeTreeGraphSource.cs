// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace H.Controls.Diagram.GraphSource;

/// <summary>
/// 类型树结构数据源
/// </summary>
public class TypeTreeGraphSource : TreeGraphSource<Type>
{
    /// <summary>
    /// 要构建Type树结构的程序集
    /// </summary>
    /// <param name="assembly"></param>
    public TypeTreeGraphSource(Assembly assembly) : base(null, (child, parent) => child.BaseType == parent, l => l.BaseType == null)
    {
        List<Type> types = assembly.GetTypes()?.ToList();

        types = types.Where(l => l.IsClass && l.IsPublic && !l.IsGenericType && !typeof(Attribute).IsAssignableFrom(l))?.ToList();

        List<Type> result = this.Build(types);

        this.Nodes = this.GetSource(result);

    }

    /// <summary>
    /// 要构建Type树结构的类型集合
    /// </summary>
    /// <param name="types"></param>
    public TypeTreeGraphSource(List<Type> types) : base(null, (child, parent) => child.BaseType == parent, l => l.BaseType == null)
    {
        List<Type> result = this.Build(types);

        this.Nodes = this.GetSource(result);
    }

    private List<Type> Build(List<Type> types)
    {
        HashSet<Type> result = new HashSet<Type>();

        Action<List<Type>> action = null;

        action = t =>
        {
            result.UnionWith(t);

            List<Type> child = t.Where(l => l.BaseType != null).Select(l => l.BaseType)?.ToList();

            if (child.Count > 0)
                action(child);
        };

        action?.Invoke(types);

        return result.ToList();
    }

}
