
using H.Controls.Diagram;
using H.Controls.Diagram.Presenter.PortDatas;
using H.Extensions.Geometry;
using System.Threading.Tasks;
using System.Windows.Media;

namespace H.Test.Diagram
{
    public class TestPortData : FlowablePortData
    {
        public TestPortData()
        {

        }
        public TestPortData(string nodeID, PortType portType) : base(nodeID, portType)
        {

        }

        /// <summary>
        /// 显示的形状
        /// </summary>
        /// <returns></returns>
        protected override Geometry GetGeometry()
        {
            return GeometryFactory.LineRect;
        }

        /// <summary>
        /// 设置是否可以放下连线
        /// </summary>
        /// <param name="part"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public override bool CanDrop(Part part, out string message)
        {
            return base.CanDrop(part, out message);
        }

        /// <summary>
        /// 初始化动态连线
        /// </summary>
        /// <param name="link"></param>
        public override void InitLink(Link link)
        {
            base.InitLink(link);
        }

        /// <summary>
        /// 重写执行方法
        /// </summary>
        /// <param name="previors"></param>
        /// <param name="current"></param>
        /// <returns></returns>
        public override Task<IFlowableResult> InvokeAsync(Part previors, Port current)
        {
            return base.InvokeAsync(previors, current);
        }
    }
}
