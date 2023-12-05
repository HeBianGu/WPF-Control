using H.Providers.Ioc;
using System.Collections;

namespace H.Controls.OrderBox
{
    public class PropertyOrderBoxOrder : IOrder
    {
        private PropertyOrderBox _propertyOrder;
        public PropertyOrderBoxOrder(PropertyOrderBox propertyOrder)
        {
            _propertyOrder = propertyOrder;
        }

        public string DisplayName => _propertyOrder.DisplayName;

        public IEnumerable Where(IEnumerable from)
        {
            if (_propertyOrder.PropertyOrders == null)
                return from;
            return _propertyOrder.PropertyOrders.Where(from);
        }
    }
}
