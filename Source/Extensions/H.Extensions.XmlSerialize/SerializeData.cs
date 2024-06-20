﻿// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;

namespace H.Extensions.XmlSerialize
{
    [XmlType("Class")]
    public class XmlClassData
    {
        [XmlAttribute]
        public string AssemblyQualifiedName { get; set; }

        public List<XmlProperty> Properties { get; set; } = new List<XmlProperty>();

        public List<XmlClassProperty> ClassProperties { get; set; } = new List<XmlClassProperty>();

        public XmlClassData()
        {

        }
        public XmlClassData(object obj)
        {
            Type type = obj.GetType();
            this.AssemblyQualifiedName = type.AssemblyQualifiedName;
            IEnumerable<PropertyInfo> ps = type.GetProperties().Where(x => x.CanRead && x.CanWrite).Where(x => x.GetCustomAttribute<XmlIgnoreAttribute>() == null);
            ps = ps.Where(x => !x.DeclaringType.FullName.StartsWith("System.")).ToList();
            foreach (PropertyInfo p in ps)
            {
                object value = p.GetValue(obj);
                DefaultValueAttribute dv = p.GetCustomAttribute<DefaultValueAttribute>();
                if (dv != null && dv.Value == value)
                    continue;
                XmlProperty xmlProperty = new XmlProperty();
                xmlProperty.Name = p.Name;

                {
                    TypeConverterAttribute pTypeC = p.GetCustomAttribute<TypeConverterAttribute>();
                    if (pTypeC != null)
                    {
                        Type ctype = Type.GetType(pTypeC.ConverterTypeName);
                        TypeConverter ddd = Activator.CreateInstance(ctype) as TypeConverter;
                        xmlProperty.Value = ddd.ConvertToString(null, System.Globalization.CultureInfo.CurrentUICulture, value);
                        this.Properties.Add(xmlProperty);
                        continue;
                    }
                }

                {
                    TypeConverterAttribute pTypeC = p.PropertyType.GetCustomAttribute<TypeConverterAttribute>();
                    if (pTypeC != null)
                    {
                        Type ctype = Type.GetType(pTypeC.ConverterTypeName);
                        TypeConverter ddd = Activator.CreateInstance(ctype) as TypeConverter;
                        xmlProperty.Value = ddd.ConvertToString(null, System.Globalization.CultureInfo.CurrentUICulture, value);
                        this.Properties.Add(xmlProperty);
                        continue;
                    }
                }

                if (typeof(IConvertible).IsAssignableFrom(p.PropertyType))
                {
                    xmlProperty.Value = value?.ToString();
                    this.Properties.Add(xmlProperty);
                    continue;
                }


                if (typeof(IXmlClassSerilize).IsAssignableFrom(p.PropertyType))
                {
                    IXmlClassSerilize xmlClassSerilize = value as IXmlClassSerilize;

                    if (xmlClassSerilize != null)
                    {
                        XmlClassProperty xmlClassProperty = new XmlClassProperty(p.Name, xmlClassSerilize.Save());
                        this.ClassProperties.Add(xmlClassProperty);
                        continue;
                    }
                }

                {
                    XmlClassProperty xmlClassProperty = new XmlClassProperty(p.Name, value);
                    this.ClassProperties.Add(xmlClassProperty);
                }
            }
        }

        public object ToObject()
        {
            Type type = Type.GetType(this.AssemblyQualifiedName);
            object obj = Activator.CreateInstance(type);
            return this.ToObject(obj);
        }

        public object ToObject(object obj)
        {
            Type type = obj.GetType();
            IEnumerable<PropertyInfo> ps = type.GetProperties().Where(x => x.CanRead && x.CanWrite).Where(x => x.GetCustomAttribute<XmlIgnoreAttribute>() == null);

            foreach (XmlProperty property in this.Properties)
            {
                PropertyInfo p = ps.FirstOrDefault(x => x.Name == property.Name);

                if (p.PropertyType.IsEnum)
                {
                    object r = Enum.Parse(p.PropertyType, property.Value);
                    p.SetValue(obj, r);
                    continue;
                }

                {
                    if (typeof(IConvertible).IsAssignableFrom(p.PropertyType))
                    {
                        object r = Convert.ChangeType(property.Value, p.PropertyType);
                        p.SetValue(obj, r);
                        continue;
                    }
                }

                {
                    TypeConverterAttribute pTypeC = p.GetCustomAttribute<TypeConverterAttribute>();
                    if (pTypeC != null)
                    {
                        Type ctype = Type.GetType(pTypeC.ConverterTypeName);
                        TypeConverter ddd = Activator.CreateInstance(ctype) as TypeConverter;
                        object r = ddd.ConvertFrom(null, System.Globalization.CultureInfo.CurrentUICulture, property.Value);
                        p.SetValue(obj, r);
                        continue;
                    }
                }

                {
                    TypeConverterAttribute pTypeC = p.PropertyType.GetCustomAttribute<TypeConverterAttribute>();
                    if (pTypeC != null)
                    {
                        Type ctype = Type.GetType(pTypeC.ConverterTypeName);
                        TypeConverter ddd = Activator.CreateInstance(ctype) as TypeConverter;
                        object r = ddd.ConvertFrom(null, System.Globalization.CultureInfo.CurrentUICulture, property.Value);
                        p.SetValue(obj, r);
                        continue;
                    }
                }

                p.SetValue(obj, property.Value);
            }
            foreach (XmlClassProperty property in this.ClassProperties)
            {
                PropertyInfo p = ps.FirstOrDefault(x => x.Name == property.Name);
                if (p == null) continue;
                object pObj = property.ToObject();

                if (typeof(IXmlClassSerilize).IsAssignableFrom(p.PropertyType))
                {
                    IXmlClassSerilize xmlClassSerilize = pObj as IXmlClassSerilize;
                    xmlClassSerilize.Load(property.Class);
                }

                p.SetValue(obj, pObj);
            }
            return obj;
        }
    }

    [XmlType("Property")]
    public class XmlProperty
    {
        [XmlAttribute]
        public string Name { get; set; }

        [XmlAttribute]
        public string Value { get; set; }
    }

    [XmlType("ClassProperty")]
    public class XmlClassProperty
    {
        public XmlClassProperty()
        {

        }
        public XmlClassProperty(string propertyName, object obj)
        {
            this.Name = propertyName;
            XmlClassData data = new XmlClassData(obj);
            this.Class = data;
        }

        public XmlClassProperty(string propertyName, XmlClassData data)
        {
            this.Name = propertyName;
            this.Class = data;
        }

        [XmlAttribute]
        public string Name { get; set; }

        public XmlClassData Class { get; set; }

        public object ToObject()
        {
            return this.Class?.ToObject();
        }
    }

    public class XmlCollection
    {

    }

    public interface IXmlClassSerilize
    {
        XmlClassData Save();

        void Load(XmlClassData t);
    }
}
