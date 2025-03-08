using H.Extensions.Geometry;
using System.Windows.Markup;
using System.Windows.Media.Converters;

namespace H.Controls.Diagram.Presenter.NodeDatas;

public interface IGeometryNodeData : INodeData
{
    Geometry Data { get; set; }
}

public abstract class GeometryNodeDataBase : FlowableNodeData, IGeometryNodeData
{
    public GeometryNodeDataBase()
    {
        this.Data = this.GetGeometry();
    }

    private Geometry _data;
    [TypeConverter(typeof(GeometryConverter))]
    [ValueSerializer(typeof(GeometryValueSerializer))]
    public Geometry Data
    {
        get { return _data; }
        set
        {
            _data = value;
            RaisePropertyChanged();
        }
    }

    private Stretch _stretch = Stretch.Uniform;
    [XmlIgnore]
    [Browsable(false)]
    public Stretch Stretch
    {
        get { return _stretch; }
        set
        {
            _stretch = value;
            RaisePropertyChanged();
        }
    }

    protected abstract Geometry GetGeometry();

}

public class GeometryNodeData : GeometryNodeDataBase
{
    protected override Geometry GetGeometry()
    {
        GeometryConverter converter = new GeometryConverter();
        return converter.ConvertFromString("M0,0 100,0 100,60 0,60 Z") as Geometry;
    }
}

public class InfoNodeData : GeometryNodeDataBase, ICloneable
{
    public InfoNodeData()
    {

    }
    protected override Geometry GetGeometry()
    {
        return GeometryFactory.Create("F1M13,4L12,4 12,3C12,2.449 12.449,2 13,2 13.551,2 14,2.449 14,3 14,3.551 13.551,4 13,4 M11,3L11,9 11,13C11,13.552 10.551,14 10,14 9.733,14 9.482,13.896 9.293,13.707 9.104,13.518 9,13.267 9,13 9,12.733 9.104,12.482 9.293,12.293 9.482,12.104 9.733,12 10,12 10.276,12 10.5,11.776 10.5,11.5 10.5,11.224 10.276,11 10,11L4,11 4,3C4,2.449,4.449,2,5,2L11.278,2C11.106,2.295,11,2.634,11,3 M3,14C2.733,14 2.482,13.896 2.293,13.707 2.104,13.518 2,13.267 2,13 2,12.733 2.104,12.482 2.293,12.293 2.482,12.104 2.733,12 3,12L8.267,12C8.093,12.301 8,12.644 8,13 8,13.358 8.101,13.698 8.277,14z M13,1L5,1C3.897,1,3,1.897,3,3L3,11C2.466,11 1.964,11.208 1.586,11.586 1.208,11.964 1,12.466 1,13 1,13.534 1.208,14.036 1.586,14.414 1.964,14.792 2.466,15 3,15L10,15C11.103,15,12,14.103,12,13L12,9 12,5 13,5C14.103,5 15,4.103 15,3 15,1.897 14.103,1 13,1");
    }
}
public class BottleNodeData : GeometryNodeDataBase, ICloneable
{
    protected override Geometry GetGeometry()
    {
        return GeometryFactory.Create("F1M7,5.1543L7,2.0003 9,2.0003 9,5.1543 11.631,9.0003 4.369,9.0003z M14.271,11.0883L10,4.8453 10,2.0003 11,2.0003 11,1.0003 5,1.0003 5,2.0003 6,2.0003 6,4.8453 1.729,11.0883C1.203,11.8553 1.146,12.8433 1.581,13.6673 2.015,14.4893 2.862,15.0003 3.792,15.0003L12.208,15.0003C13.138,15.0003 13.985,14.4893 14.419,13.6673 14.854,12.8433 14.797,11.8553 14.271,11.0883");
    }
}

public class HappyNodeData : GeometryNodeDataBase, ICloneable
{
    protected override Geometry GetGeometry()
    {
        return GeometryFactory.Create("F1M9.5,6.5C9.5,5.948 9.947,5.5 10.5,5.5 11.053,5.5 11.5,5.948 11.5,6.5 11.5,7.052 11.053,7.5 10.5,7.5 9.947,7.5 9.5,7.052 9.5,6.5 M4.5,6.5C4.5,5.948 4.948,5.5 5.5,5.5 6.052,5.5 6.5,5.948 6.5,6.5 6.5,7.052 6.052,7.5 5.5,7.5 4.948,7.5 4.5,7.052 4.5,6.5 M11.032,9.75L11.897,10.25C11.095,11.638 9.602,12.5 8,12.5 6.398,12.5 4.905,11.638 4.103,10.25L4.968,9.75C5.592,10.829 6.754,11.5 8,11.5 9.246,11.5 10.407,10.829 11.032,9.75 M8,14C4.691,14 2,11.309 2,8 2,4.691 4.691,2 8,2 11.309,2 14,4.691 14,8 14,11.309 11.309,14 8,14 M8,1C4.14,1 1,4.14 1,8 1,11.859 4.14,15 8,15 11.859,15 15,11.859 15,8 15,4.14 11.859,1 8,1");
    }
}

public class UnhappyNodeData : GeometryNodeDataBase, ICloneable
{
    protected override Geometry GetGeometry()
    {
        return GeometryFactory.Create("F1M11,5C10.447,5 10,5.448 10,6 10,6.552 10.447,7 11,7 11.553,7 12,6.552 12,6 12,5.448 11.553,5 11,5 M14,8C14,4.691 11.309,2 8,2 4.691,2 2,4.691 2,8 2,11.309 4.691,14 8,14 11.309,14 14,11.309 14,8 M15,8C15,11.859 11.859,15 8,15 4.14,15 1,11.859 1,8 1,4.14 4.14,1 8,1 11.859,1 15,4.14 15,8 M8,9C6.58,9,5.221,9.607,4.273,10.667L5.019,11.333C5.777,10.486 6.864,10 8,10 9.137,10 10.224,10.486 10.981,11.333L11.728,10.667C10.779,9.607,9.421,9,8,9 M4,6C4,5.448 4.448,5 5,5 5.552,5 6,5.448 6,6 6,6.552 5.552,7 5,7 4.448,7 4,6.552 4,6");
    }
}
public class SettingNodeData : GeometryNodeDataBase, ICloneable
{
    protected override Geometry GetGeometry()
    {
        return GeometryFactory.Create("F1M13,4L12,4 12,3C12,2.449 12.449,2 13,2 13.551,2 14,2.449 14,3 14,3.551 13.551,4 13,4 M11,3L11,9 11,13C11,13.552 10.551,14 10,14 9.733,14 9.482,13.896 9.293,13.707 9.104,13.518 9,13.267 9,13 9,12.733 9.104,12.482 9.293,12.293 9.482,12.104 9.733,12 10,12 10.276,12 10.5,11.776 10.5,11.5 10.5,11.224 10.276,11 10,11L4,11 4,3C4,2.449,4.449,2,5,2L11.278,2C11.106,2.295,11,2.634,11,3 M3,14C2.733,14 2.482,13.896 2.293,13.707 2.104,13.518 2,13.267 2,13 2,12.733 2.104,12.482 2.293,12.293 2.482,12.104 2.733,12 3,12L8.267,12C8.093,12.301 8,12.644 8,13 8,13.358 8.101,13.698 8.277,14z M13,1L5,1C3.897,1,3,1.897,3,3L3,11C2.466,11 1.964,11.208 1.586,11.586 1.208,11.964 1,12.466 1,13 1,13.534 1.208,14.036 1.586,14.414 1.964,14.792 2.466,15 3,15L10,15C11.103,15,12,14.103,12,13L12,9 12,5 13,5C14.103,5 15,4.103 15,3 15,1.897 14.103,1 13,1");
    }
}

public class PenNodeData : GeometryNodeDataBase, ICloneable
{
    protected override Geometry GetGeometry()
    {
        return GeometryFactory.Create("F1M14.5254,3.7666L13.9994,4.2926 11.7064,1.9996 12.2324,1.4736C12.8654,0.841600000000001 13.8924,0.8426 14.5254,1.4746 15.1574,2.1056 15.1574,3.1346 14.5254,3.7666 M2.0174,12.0546C2.9814,12.2576,3.7414,13.0186,3.9454,13.9826L1.3744,14.6246z M2.5344,11.1726L10.9994,2.7066 13.2924,4.9996 4.8264,13.4656C4.4854,12.3716,3.6284,11.5146,2.5344,11.1726 M15.2324,0.7666C14.2114,-0.2554,12.5484,-0.2554,11.5254,0.7666L1.2354,11.0566 0.000399999999999068,15.9996 4.9424,14.7636 15.2324,4.4736C16.2544,3.4516,16.2544,1.7886,15.2324,0.7666");
    }
}

public class ErrorNodeData : GeometryNodeDataBase, ICloneable
{
    protected override Geometry GetGeometry()
    {
        return GeometryFactory.Create("F1M13.3535,12.6465L12.6465,13.3535 11.4995,12.2075 10.3535,13.3535 9.6465,12.6465 10.7925,11.5005 9.6465,10.3535 10.3535,9.6465 11.4995,10.7925 12.6465,9.6465 13.3535,10.3535 12.2075,11.5005z M11.4995,8.0005C9.5675,8.0005 7.9995,9.5675 7.9995,11.5005 7.9995,13.4325 9.5675,15.0005 11.4995,15.0005 13.4325,15.0005 14.9995,13.4325 14.9995,11.5005 14.9995,9.5675 13.4325,8.0005 11.4995,8.0005");
    }
}

/// <summary>
/// 错
/// </summary>
public class WrongNodeData : GeometryNodeDataBase, ICloneable
{
    protected override Geometry GetGeometry()
    {
        return GeometryFactory.Create("F1M15,9L12.5,12 15,15 14.656,15 11.988,12.615 10.154,14.815C9.916,15.079 9.434,15.044 9.199,14.815 8.958,14.581 8.91,14.163 9.199,13.878L11.3,12 9.199,10.122C8.91,9.837 8.958,9.419 9.199,9.185 9.434,8.956 9.916,8.921 10.154,9.185L11.988,11.385 14.656,9z");
    }
}

/// <summary>
/// 五边形
/// </summary>
public class PentagonNodeData : GeometryNodeDataBase, ICloneable
{
    protected override Geometry GetGeometry()
    {
        return GeometryFactory.Create("F1M8,1L1,7 4,15 12,15 15,7z");
    }
}

/// <summary>
/// 三角形
/// </summary>
public class TriangleNodeData : GeometryNodeDataBase, ICloneable
{
    protected override Geometry GetGeometry()
    {
        return GeometryFactory.Create("F1M1,15L8,1 15,15z");
    }
}

/// <summary>
/// 盾牌
/// </summary>
public class ShieldNodeData : GeometryNodeDataBase, ICloneable
{
    protected override Geometry GetGeometry()
    {
        return GeometryFactory.Create("F1M13.9512,4L8.0002,1 2.0492,4C2.0492,4 1.0572,13 8.0002,15 14.9422,13 13.9512,4 13.9512,4");
    }
}

/// <summary>
/// 文件夹
/// </summary>
public class FolderNodeData : GeometryNodeDataBase, ICloneable
{
    protected override Geometry GetGeometry()
    {
        return GeometryFactory.Create("F1M2,3L8.374,3 8.874,4 2,4z M13.496,4L10,4 9.992,4 8.992,2 1.5,2C1.225,2,1,2.224,1,2.5L1,12.5C1,12.776,1.225,13,1.5,13L13.496,13C13.773,13,13.996,12.776,13.996,12.5L13.996,4.5C13.996,4.224,13.773,4,13.496,4");
    }
}

public class HeartNodeData : GeometryNodeDataBase, ICloneable
{
    protected override Geometry GetGeometry()
    {
        return GeometryFactory.Create("F1M16,6C16,7.353,15.465,8.621,14.493,9.572L8,15.834 1.514,9.58C0.536,8.624 0,7.354 0,6 0,3.243 2.243,1 5,1 6.085,1 7.137,1.367 8,2.03 8.863,1.367 9.915,1 11,1 13.757,1 16,3.243 16,6");
    }
}
public class WarnNodeData : GeometryNodeDataBase, ICloneable
{
    protected override Geometry GetGeometry()
    {
        return GeometryFactory.Create("F1M12,12L11,12 11,10 12,10z M12,14L11,14 11,13 12,13z M11.891,8.016L11.109,8.016 8.094,14.016 8.609,15 14.375,15 14.859,14z");
    }
}

/// <summary>
/// 油漆桶
/// </summary>
public class PaintNodeData : GeometryNodeDataBase, ICloneable
{
    protected override Geometry GetGeometry()
    {
        return GeometryFactory.CreateGroup("F1M4.5,9C4.776,9,5,8.776,5,8.5L5,4C5,2.896 5.896,2 7,2 7.149,2 7.294,2.019 7.434,2.05 7.609,2.018 7.792,2 7.983,2 8.446,2 8.943,2.103 9.451,2.279 8.909,1.507 8.015,1 7,1 5.343,1 4,2.343 4,4L4,8.5C4,8.776,4.224,9,4.5,9", "F1M10.1729,6.418C9.7829,6.809 9.8389,7.964 10.7239,9 10.2289,8.712 9.7049,8.329 9.1869,7.81 7.4769,6.1 7.0889,4.304 7.5739,3.819 8.0579,3.336 9.8539,3.723 11.5629,5.433 12.0809,5.952 12.4649,6.474 12.7529,6.97 11.7179,6.085 10.5649,6.028 10.1729,6.418 M11.9189,5.078C10.0669,3.222,7.8079,2.469,6.8799,3.397L5.9999,4.277 5.9999,8.5C5.9999,9.327 5.3269,10 4.4999,10 3.6729,10 2.9999,9.327 2.9999,8.5L2.9999,7.276 2.3979,7.879C1.4689,8.808 2.2219,11.065 4.0779,12.92 5.9349,14.777 8.1909,15.531 9.1199,14.603L12.9999,10.722 12.9999,13C12.9999,13.276 13.2239,13.5 13.4999,13.5 13.7769,13.5 13.9999,13.276 13.9999,13L13.9999,9C13.9959,7.898,13.2339,6.392,11.9189,5.078");
    }
}

public class MessageCircleNodeData : GeometryNodeDataBase, ICloneable
{
    protected override Geometry GetGeometry()
    {
        return GeometryFactory.Create("F1M8,2C11.252,2 14,3.832 14,6 14,8.09 11.447,9.879 8.309,9.988L7.862,10.004 7.576,10.347 5,13.438 5,10.101 5,9.482 4.447,9.206C2.915,8.441 2,7.243 2,6 2,3.832 4.748,2 8,2 M8,1C4.134,1 1,3.238 1,6 1,7.698 2.188,9.196 4,10.101L4,15 5,15 8.344,10.987C12.049,10.858 15,8.679 15,6 15,3.238 11.866,1 8,1");
    }
}

/// <summary>
/// 对
/// </summary>
public class CheckNodeData : GeometryNodeDataBase, ICloneable
{
    protected override Geometry GetGeometry()
    {
        return GeometryFactory.Create("F1M11,2L6.5,11 5,8 2,8 5,14 8,14 14,2z");
    }
}

/// <summary>
/// 消息
/// </summary>
public class MessageRectNodeData : GeometryNodeDataBase, ICloneable
{
    protected override Geometry GetGeometry()
    {
        return GeometryFactory.Create("F1M0,-0.000199999999999534L0,12.0008 3,12.0008 3,15.9998 5.5,15.9998 8.5,12.0008 16,12.0008 16,-0.000199999999999534z");
    }
}

/// <summary>
/// 云
/// </summary>
public class CloudNodeData : GeometryNodeDataBase, ICloneable
{
    protected override Geometry GetGeometry()
    {
        return GeometryFactory.Create("F1M8.4302,3C7.0962,3 5.8602,3.695 5.1282,4.797 4.9412,4.766 4.7522,4.75 4.5612,4.75 2.5982,4.75 1.0002,6.376 1.0002,8.375 1.0002,10.374 2.5982,12 4.5612,12L12.2982,12C13.7882,12 15.0002,10.766 15.0002,9.25 15.0002,7.762 13.8342,6.547 12.3832,6.501 12.1142,4.526 10.4442,3 8.4302,3 M8.4302,4C10.0922,4,11.4392,5.371,11.4392,7.063L11.4392,7.5 12.2992,7.5C13.2492,7.5 14.0172,8.283 14.0172,9.25 14.0172,10.216 13.2482,11 12.2992,11L4.5612,11C3.1372,11 1.9822,9.825 1.9822,8.375 1.9822,6.925 3.1372,5.75 4.5612,5.75 4.9402,5.75 5.2972,5.838 5.6212,5.988 6.0512,4.829 7.1412,4 8.4302,4");
    }
}
