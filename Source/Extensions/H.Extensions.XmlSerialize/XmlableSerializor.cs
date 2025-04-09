// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Markup;
using System.Xml;
using System.Xml.Serialization;
namespace H.Extensions.XmlSerialize
{
    public class XmlableSerializor
    {
        private XmlableSerializor()
        {

        }
        public static XmlableSerializor Instance = new XmlableSerializor();

        private string _root = "Root";
        /// <summary> 说明  </summary>
        public string Root
        {
            get { return _root; }
            set
            {
                _root = value;
            }
        }

        //private string _exceptStartNamespance = "System.";
        ///// <summary> 说明  </summary>
        //public string ExceptStartNamespance
        //{
        //    get { return _exceptStartNamespance; }
        //    set
        //    {
        //        _exceptStartNamespance = value;
        //    }
        //}

        public object Load(string path, object t, Func<PropertyInfo, object, bool> match = null)
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(path);
            return Load(xmldoc, t, match);
        }

        public object Load(XmlDocument xmldoc, object t, Func<PropertyInfo, object, bool> match = null)
        {
            XmlNode root = xmldoc.SelectSingleNode(this.Root);
            XmlElement baseElement = root[t.GetType().Name];

            if (t is IXmlable xmlable)
            {
                XmlElement e = root[xmlable.GetType().Name];
                xmlable.FromXml(e, xmldoc, match);
            }
            else
            {
                this.FromXml(baseElement, t, xmldoc, match);
            }
            return t;
        }

        public bool IsValid(string path, Type type)
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(path);
            XmlNode root = xmldoc.SelectSingleNode(this.Root);
            if (root == null)
                return false;
            XmlElement baseElement = root[type.Name];
            return baseElement != null;
        }

        public object Load(string path, Type type, Func<PropertyInfo, object, bool> match)
        {
            XmlDocument xmldoc = new XmlDocument();
            object t = Activator.CreateInstance(type);
            xmldoc.Load(path);
            return this.Load(xmldoc, t, match);
            //var root = xmldoc.SelectSingleNode(this.Root);
            //var baseElement = root[t.GetType().Name];
            //if (t is IXmlable xmlable)
            //{
            //    xmlable.FromXml(baseElement, xmldoc, match);
            //}
            //else
            //{
            //    this.FromXml(baseElement, t, xmldoc, match);
            //}
            //return t;
        }

        public T Load<T>(string path, Func<PropertyInfo, object, bool> match = null) where T : class, new()
        {
            XmlDocument xmldoc = new XmlDocument();
            T t = new T();
            xmldoc.Load(path);
            return this.Load(xmldoc, t, match) as T;
            //var root = xmldoc.SelectSingleNode(this.Root);
            //var baseElement = root[t.GetType().Name];
            //if (t is IXmlable xmlable)
            //{
            //    xmlable.FromXml(baseElement, xmldoc, match);
            //}
            //else
            //{
            //    this.FromXml(baseElement, t, xmldoc, match);
            //}
            //return t;
        }

        public void Save(string path, object obj, Func<PropertyInfo, object, bool> match = null)
        {
            string folder = Path.GetDirectoryName(path);
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            XmlDocument xmldoc = this.SaveAs(obj, match);
            xmldoc.Save(path);
        }

        public XmlDocument SaveAs(object obj, Func<PropertyInfo, object, bool> match = null)
        {
            XmlDocument xmldoc = new XmlDocument();
            XmlDeclaration dec = xmldoc.CreateXmlDeclaration("1.0", "utf-8", null);
            xmldoc.AppendChild(dec);
            XmlElement root = xmldoc.CreateElement(this.Root);

            if (obj is IXmlable xmlable)
            {
                XmlElement e = xmldoc.CreateElement(obj.GetType().Name);
                e.SetAttribute("AssemblyQualifiedName", xmlable.GetType().AssemblyQualifiedName);
                e.SetAttribute("FullName", xmlable.GetType().FullName);
                root.AppendChild(e);
                xmlable.ToXml(e, xmldoc, match);
            }
            else
            {
                this.ToXml(root, obj, obj.GetType().Name, xmldoc, match);
            }

            xmldoc.AppendChild(root);

            return xmldoc;
        }

        public void FromXml(XmlElement e, object o, XmlDocument doc, Func<PropertyInfo, object, bool> match = null)
        {
            IEnumerable<PropertyInfo> ps = o.GetType().GetProperties().Where(x => x.CanRead && x.CanWrite).Where(x => x.GetCustomAttribute<XmlIgnoreAttribute>() == null);
            //ps = ps.Where(x => !x.DeclaringType.FullName.StartsWith(this.ExceptStartNamespance)).ToList();
            ps = ps.Where(x => match?.Invoke(x, o) != false);
            foreach (PropertyInfo p in ps)
            {
                string str = e.GetAttribute(p.Name);
                if (p.PropertyType.IsEnum)
                {
                    if (string.IsNullOrEmpty(str)) continue;
                    object r = System.Enum.Parse(p.PropertyType, str);
                    p.SetValue(o, r);
                    continue;
                }

                //  Do ：ValueSerializerAttribute
                {
                    ValueSerializerAttribute pTypeC = p.GetCustomAttribute<ValueSerializerAttribute>();
                    if (pTypeC != null)
                    {
                        Type ctype = Type.GetType(pTypeC.ValueSerializerTypeName);
                        ValueSerializer ddd = Activator.CreateInstance(ctype) as ValueSerializer;
                        if (ddd.CanConvertFromString(str, null))
                        {
                            object value = ddd.ConvertFromString(str, null);
                            p.SetValue(o, value);
                        }
                        continue;
                    }
                }

                {
                    ValueSerializerAttribute pTypeC = p.PropertyType.GetCustomAttribute<ValueSerializerAttribute>();
                    if (pTypeC != null)
                    {
                        Type ctype = Type.GetType(pTypeC.ValueSerializerTypeName);
                        ValueSerializer ddd = Activator.CreateInstance(ctype) as ValueSerializer;
                        if (string.IsNullOrEmpty(str)) continue;
                        if (ddd.CanConvertFromString(str, null))
                        {
                            if (typeof(Freezable).IsAssignableFrom(p.PropertyType))
                            {
                                Application.Current.Dispatcher.Invoke(() =>
                                {
                                    object value = ddd.ConvertFromString(str, null);
                                    p.SetValue(o, value);
                                });
                            }
                            else
                            {
                                object value = ddd.ConvertFromString(str, null);
                                p.SetValue(o, value);
                            }
                        }
                        continue;
                    }
                }


                //  Do ：TypeConverter
                {
                    TypeConverterAttribute pTypeC = p.GetCustomAttribute<TypeConverterAttribute>();
                    if (pTypeC != null)
                    {
                        Type ctype = Type.GetType(pTypeC.ConverterTypeName);
                        TypeConverter ddd = Activator.CreateInstance(ctype) as TypeConverter;
                        object value = ddd.ConvertFromString(null, System.Globalization.CultureInfo.CurrentUICulture, str);
                        p.SetValue(o, value);
                        continue;
                    }
                }

                {
                    TypeConverterAttribute pTypeC = p.PropertyType.GetCustomAttribute<TypeConverterAttribute>();
                    if (pTypeC != null)
                    {
                        Type ctype = Type.GetType(pTypeC.ConverterTypeName);
                        TypeConverter ddd = Activator.CreateInstance(ctype) as TypeConverter;
                        if (string.IsNullOrEmpty(str))
                            continue;

                        if (typeof(Freezable).IsAssignableFrom(p.PropertyType))
                        {
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                object value = ddd.ConvertFromString(null, System.Globalization.CultureInfo.CurrentUICulture, str);
                                p.SetValue(o, value);
                            });
                        }
                        else
                        {
                            object value = ddd.ConvertFromString(null, System.Globalization.CultureInfo.CurrentUICulture, str);
                            p.SetValue(o, value);
                        }
                        continue;
                    }
                }

                if (typeof(IConvertible).IsAssignableFrom(p.PropertyType))
                {

                    if (string.IsNullOrEmpty(str))
                    {
                        object value = p.PropertyType.IsValueType ? Activator.CreateInstance(p.PropertyType) : null;
                        p.SetValue(o, value);
                    }
                    else
                    {
                        try
                        {
                            object value = Convert.ChangeType(str, p.PropertyType);
                            p.SetValue(o, value);
                        }
                        catch (Exception ex)
                        {
                            IocLog.Instance.Error(ex);
                            System.Diagnostics.Debug.WriteLine(ex);
                        }
                    }

                    continue;
                }

                if (typeof(IXmlable).IsAssignableFrom(p.PropertyType))
                {
                    XmlElement xmlableElement = e[p.Name];
                    string assemblyQualifiedName = xmlableElement.GetAttribute("AssemblyQualifiedName");
                    Type itemType = Type.GetType(assemblyQualifiedName);
                    object value = Activator.CreateInstance(itemType);
                    IXmlable xmlable = value as IXmlable;
                    if (xmlable != null)
                    {
                        xmlable.FromXml(xmlableElement, doc, match);
                        p.SetValue(o, value);
                        continue;
                    }
                }

                if (typeof(IEnumerable).IsAssignableFrom(p.PropertyType))
                {
                    IList list = Activator.CreateInstance(p.PropertyType) as IList;

                    if (e[p.Name] != null)
                    {
                        foreach (XmlElement item in e[p.Name])
                        {
                            if (item == null) continue;
                            string assemblyQualifiedName = item.GetAttribute("AssemblyQualifiedName");

                            Type itemType = Type.GetType(assemblyQualifiedName);
                            //var instance = Activator.CreateInstance(type);
                            {
                                TypeConverterAttribute pTypeC = itemType.GetCustomAttribute<TypeConverterAttribute>();
                                if (pTypeC != null)
                                {
                                    Type ctype = Type.GetType(pTypeC.ConverterTypeName);
                                    TypeConverter ddd = Activator.CreateInstance(ctype) as TypeConverter;
                                    object obj = ddd.ConvertFromString(null, System.Globalization.CultureInfo.CurrentUICulture, item.InnerText);
                                    list.Add(obj);
                                    continue;
                                }
                            }

                            if (typeof(IConvertible).IsAssignableFrom(itemType))
                            {
                                object obj = Convert.ChangeType(item.InnerText, itemType);
                                list.Add(obj);
                                continue;
                            }

                            if (typeof(IXmlable).IsAssignableFrom(itemType))
                            {
                                IXmlable xmlable = Activator.CreateInstance(itemType) as IXmlable;
                                if (xmlable != null)
                                {
                                    xmlable.FromXml(item, doc, match);
                                    list.Add(xmlable);
                                    continue;
                                }
                            }

                            //action.Invoke(item, item);

                            object instance = Activator.CreateInstance(itemType);

                            this.FromXml(item, instance, doc, match);

                            list.Add(instance);
                        }

                    }
                    p.SetValue(o, list);
                    continue;
                }
                {
                    XmlElement element = e[p.Name];
                    if (element == null) continue;
                    string assemblyQualifiedName = element.GetAttribute("AssemblyQualifiedName");
                    Type itemType = Type.GetType(assemblyQualifiedName);
                    object value = Activator.CreateInstance(itemType);
                    //var value = p.GetValue(o);
                    //action.Invoke(value, element);
                    this.FromXml(element, value, doc, match);
                    p.SetValue(o, value);
                }
            }
        }

        public void ToXml(XmlElement e, object o, string n, XmlDocument doc, Func<PropertyInfo, object, bool> match = null, bool useNew = true)
        {
            XmlElement element = e;

            if (useNew)
            {
                element = doc.CreateElement(n);
                element.SetAttribute("AssemblyQualifiedName", o.GetType().AssemblyQualifiedName);
                element.SetAttribute("FullName", o.GetType().FullName);
            }

            Type type = o.GetType();
            IEnumerable<PropertyInfo> ps = type.GetProperties().Where(x => x.CanRead && x.CanWrite);

            ps = o.GetType().GetProperties().Where(x => x.CanRead && x.CanWrite).Where(x => x.GetCustomAttribute<XmlIgnoreAttribute>() == null);
            //if (!string.IsNullOrEmpty(this.ExceptStartNamespance))
            //{
            //    ps = ps.Where(x => !x.DeclaringType.FullName.StartsWith(this.ExceptStartNamespance)).ToList();
            //}

            ps = ps.Where(x => match?.Invoke(x, o) != false).ToList();

            if (!type.IsPrimitive && !type.IsEnum && type.IsValueType)
            {
                //  Do ：struct
                FieldInfo[] fs = o.GetType().GetFields();

            }

            foreach (PropertyInfo p in ps)
            {
                if (p.Name == "Item")
                    continue;
                object value = p.GetValue(o);
                if (value == null)
                    continue;

                DefaultValueAttribute dv = p.GetCustomAttribute<DefaultValueAttribute>();
                if (dv != null && dv.Value == value)
                    continue;

                if (p.PropertyType.IsEnum)
                {
                    element.SetAttribute(p.Name, value?.ToString());
                    continue;
                }

                //  Do ：ValueSerializerAttribute
                {
                    ValueSerializerAttribute pTypeC = p.GetCustomAttribute<ValueSerializerAttribute>();
                    if (pTypeC != null)
                    {
                        Type ctype = Type.GetType(pTypeC.ValueSerializerTypeName);
                        ValueSerializer ddd = Activator.CreateInstance(ctype) as ValueSerializer;

                        if (typeof(Freezable).IsAssignableFrom(p.PropertyType))
                        {

                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                if (ddd.CanConvertToString(value, null))
                                {
                                    string str = ddd.ConvertToString(value, null);
                                    element.SetAttribute(p.Name, str);
                                }
                            });
                        }
                        else
                        {
                            if (ddd.CanConvertToString(value, null))
                            {
                                string str = ddd.ConvertToString(value, null);
                                element.SetAttribute(p.Name, str);
                            }
                        }
                        continue;
                    }
                }

                {
                    ValueSerializerAttribute pTypeC = p.PropertyType.GetCustomAttribute<ValueSerializerAttribute>();
                    if (pTypeC != null)
                    {
                        Type ctype = Type.GetType(pTypeC.ValueSerializerTypeName);
                        ValueSerializer ddd = Activator.CreateInstance(ctype) as ValueSerializer;

                        if (typeof(Freezable).IsAssignableFrom(p.PropertyType))
                        {
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                if (ddd.CanConvertToString(value, null))
                                {
                                    string str = ddd.ConvertToString(value, null);
                                    element.SetAttribute(p.Name, str);
                                }
                            });
                        }
                        else
                        {
                            if (ddd.CanConvertToString(value, null))
                            {
                                string str = ddd.ConvertToString(value, null);
                                element.SetAttribute(p.Name, str);
                            }

                        }
                        continue;
                    }
                }

                //  Do ：TypeConverterAttribute
                {
                    TypeConverterAttribute pTypeC = p.GetCustomAttribute<TypeConverterAttribute>();
                    if (pTypeC != null)
                    {
                        Type ctype = Type.GetType(pTypeC.ConverterTypeName);
                        TypeConverter ddd = Activator.CreateInstance(ctype) as TypeConverter;

                        if (typeof(Freezable).IsAssignableFrom(p.PropertyType))
                        {
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                string str = ddd.ConvertToString(null, System.Globalization.CultureInfo.CurrentUICulture, value);
                                element.SetAttribute(p.Name, str);
                            });
                        }
                        else
                        {
                            string str = ddd.ConvertToString(null, System.Globalization.CultureInfo.CurrentUICulture, value);
                            element.SetAttribute(p.Name, str);
                        }
                        continue;
                    }
                }

                {
                    TypeConverterAttribute pTypeC = p.PropertyType.GetCustomAttribute<TypeConverterAttribute>();
                    if (pTypeC != null)
                    {
                        Type ctype = Type.GetType(pTypeC.ConverterTypeName);
                        TypeConverter ddd = Activator.CreateInstance(ctype) as TypeConverter;

                        if (typeof(Freezable).IsAssignableFrom(p.PropertyType))
                        {
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                string str = ddd.ConvertToString(null, System.Globalization.CultureInfo.CurrentUICulture, value);
                                element.SetAttribute(p.Name, str);
                            });
                        }
                        else
                        {
                            string str = ddd.ConvertToString(null, System.Globalization.CultureInfo.CurrentUICulture, value);
                            element.SetAttribute(p.Name, str);
                        }
                        continue;
                    }
                }

                if (typeof(IConvertible).IsAssignableFrom(p.PropertyType))
                {
                    string str = value?.ToString();
                    element.SetAttribute(p.Name, str);
                    continue;
                }

                if (typeof(IXmlable).IsAssignableFrom(p.PropertyType))
                {
                    IXmlable xmlable = value as IXmlable;

                    if (xmlable != null)
                    {
                        XmlElement toXml = doc.CreateElement(p.Name);
                        toXml.SetAttribute("AssemblyQualifiedName", xmlable.GetType().AssemblyQualifiedName);
                        toXml.SetAttribute("FullName", xmlable.GetType().FullName);
                        xmlable.ToXml(toXml, doc, match);
                        element.AppendChild(toXml);
                        continue;
                    }
                }

                if (typeof(IEnumerable).IsAssignableFrom(p.PropertyType))
                {
                    IEnumerable collection = value as IEnumerable;

                    if (collection != null)
                    {
                        XmlElement collectionElement = doc.CreateElement(p.Name);
                        foreach (object item in collection)
                        {
                            Type itemType = item.GetType();
                            string name = itemType.Name;
                            if (itemType.IsGenericType)
                            {
                                name = itemType.Name.Split('`')[0];
                            }
                            XmlElement itemElment = doc.CreateElement(name);
                            itemElment.SetAttribute("AssemblyQualifiedName", itemType.AssemblyQualifiedName);
                            itemElment.SetAttribute("FullName", itemType.FullName);

                            {
                                TypeConverterAttribute pTypeC = item.GetType().GetCustomAttribute<TypeConverterAttribute>();
                                if (pTypeC != null)
                                {
                                    Type ctype = Type.GetType(pTypeC.ConverterTypeName);
                                    TypeConverter ddd = Activator.CreateInstance(ctype) as TypeConverter;
                                    string str = ddd.ConvertToString(null, System.Globalization.CultureInfo.CurrentUICulture, item);
                                    itemElment.InnerText = str;
                                    collectionElement.AppendChild(itemElment);
                                    continue;
                                }
                            }

                            if (typeof(IConvertible).IsAssignableFrom(itemType))
                            {
                                string str = item?.ToString();
                                itemElment.InnerText = str;
                                collectionElement.AppendChild(itemElment);
                                continue;
                            }

                            if (typeof(IXmlable).IsAssignableFrom(itemType))
                            {
                                IXmlable xmlable = item as IXmlable;

                                if (xmlable != null)
                                {
                                    xmlable.ToXml(itemElment, doc, match);
                                    collectionElement.AppendChild(itemElment);
                                    continue;
                                }
                            }

                            //action.Invoke(item, collectionElement, item.GetType().Name);
                            string name1 = item.GetType().Name;
                            if (item.GetType().IsGenericType)
                            {
                                name1 = item.GetType().Name.Split('`')[0];
                            }
                            this.ToXml(collectionElement, item, name1, doc, match);

                        }
                        element.AppendChild(collectionElement);
                        continue;
                    }
                }

                this.ToXml(element, value, p.Name, doc, match);
            }
            if (useNew)
                e.AppendChild(element);
        }

        public object XmlClone(object obj)
        {
            XmlDocument document = this.SaveAs(obj);
            object n = Activator.CreateInstance(obj.GetType());
            return this.Load(document, n);
        }
    }

    public class TestXmlData
    {
        public TestXmlData()
        {
            for (int i = 0; i < 5; i++)
            {
                this.TestXmlables1111.Add(new TestXmlable());
            }

            this.TestXmlables1111.Add(new OtherTestXmlable());
        }

        public string Name { get; set; } = "etet";

        //public Color Brush { get; set; } = Colors.AntiqueWhite;

        public int Intttt { get; set; } = 11;

        public double Double111 { get; set; } = 0.2324;

        public DateTime Time { get; set; } = DateTime.MaxValue;

        public UriHostNameType Enum { get; set; }

        public TestXmlDataChild TestXmlData11 { get; set; } = new TestXmlDataChild();
        public TestXmlDataChild TestXmlData22 { get; set; } = new TestXmlDataChild();

        public TestXmlable TestXmlable { get; set; } = new TestXmlable();

        public OtherTestXmlable OtherTestXmlable { get; set; } = new OtherTestXmlable();

        public List<TestXmlable> TestXmlables1111 { get; set; } = new List<TestXmlable>();
    }

    public class TestXmlDataChild
    {
        public string Name { get; set; } = "etet";

        //public Color Brush { get; set; } = Colors.AntiqueWhite;

        public int Intttt { get; set; } = 11;

        public double Double111 { get; set; } = 0.2324;

        public DateTime Time { get; set; } = DateTime.MaxValue;

        public UriHostNameType Enum { get; set; }
    }

    public class TestXmlable : IXmlable
    {
        public virtual void FromXml(XmlElement xmlEle, XmlDocument doc, Func<PropertyInfo, object, bool> match)
        {

        }

        public virtual void ToXml(XmlElement xmlEle, XmlDocument doc, Func<PropertyInfo, object, bool> match)
        {
            //xmlEle.SetAttribute("Test", "3535");
            //XmlableSerilizor xmlableSerilizor=new XmlableSerilizor(); 
            TestXmlableData testXmlable = new TestXmlableData();
            XmlableSerializor.Instance.ToXml(xmlEle, testXmlable, this.GetType().Name, doc, match);
        }
    }

    public class OtherTestXmlable : TestXmlable
    {
        public override void ToXml(XmlElement xmlEle, XmlDocument cnt, Func<PropertyInfo, object, bool> match)
        {
            xmlEle.SetAttribute("OtherTest", "3535");
            xmlEle.SetAttribute("OtherTest", "3535");
            base.ToXml(xmlEle, cnt, match);
        }
    }

    public class TestXmlableData
    {
        public TestXmlableData()
        {
            for (int i = 0; i < 11; i++)
            {
                this.MyProperty.Add(i);
            }
        }
        public string Name { get; set; } = "Name";

        public List<int> MyProperty { get; set; } = new List<int>();
    }
    public static class XmlDocumentExtension
    {
        //public static string ToXmlString(this XmlDocument xmlDoc)
        //{
        //    using (StringWriter sw = new StringWriter())
        //    {
        //        using (XmlTextWriter tx = new XmlTextWriter(sw))
        //        {
        //            xmlDoc.WriteTo(tx);
        //            string strXmlText = sw.ToString();
        //            return strXmlText;
        //        }
        //    }
        //}

        //public static string LoadXmlString(this XmlDocument xmlDoc, string xml)
        //{
        //    return xmlDoc.LoadXml(tx);
        //    //using (StringReader sw = new StringReader(str))
        //    //{
        //    //    using (XmlTextReader tx = new XmlTextReader(sw))
        //    //    {

        //    //        string strXmlText = sw.ToString();
        //    //        return strXmlText;
        //    //    }
        //    //}
        //}
    }
}
