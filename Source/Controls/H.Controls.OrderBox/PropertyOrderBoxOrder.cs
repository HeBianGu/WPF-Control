// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.OrderBox
{
    public class PropertyOrderBoxOrder : IOrderWhereable
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
