// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace H.Controls.PropertyGrid
{
    public abstract class PropertyDefinitionBase : DefinitionBase
    {
        #region Constructors

        internal PropertyDefinitionBase()
        {
            _targetProperties = new List<object>();
            this.PropertyDefinitions = new PropertyDefinitionCollection();
        }

        #endregion

        #region Properties

        #region TargetProperties

        [TypeConverter(typeof(ListConverter))]
        public IList TargetProperties
        {
            get { return _targetProperties; }
            set
            {
                this.ThrowIfLocked(() => this.TargetProperties);
                _targetProperties = value;
            }
        }

        private IList _targetProperties;

        #endregion

        #region PropertyDefinitions

        public PropertyDefinitionCollection PropertyDefinitions
        {
            get
            {
                return _propertyDefinitions;
            }
            set
            {
                this.ThrowIfLocked(() => this.PropertyDefinitions);
                _propertyDefinitions = value;
            }
        }

        private PropertyDefinitionCollection _propertyDefinitions;

        #endregion //PropertyDefinitions

        #endregion

        #region Overrides

        internal override void Lock()
        {
            if (this.IsLocked)
                return;

            base.Lock();

            // Just create a new copy of the properties target to ensure 
            // that the list doesn't ever get modified.

            List<object> newList = new List<object>();
            if (_targetProperties != null)
            {
                foreach (object p in _targetProperties)
                {
                    object prop = p;
                    // Convert all TargetPropertyType to Types
                    TargetPropertyType targetType = prop as TargetPropertyType;
                    if (targetType != null)
                    {
                        prop = targetType.Type;
                    }
                    newList.Add(prop);
                }
            }

            //In Designer Mode, the Designer is broken if using a ReadOnlyCollection
            _targetProperties = DesignerProperties.GetIsInDesignMode(this)
                                ? new Collection<object>(newList)
                                : new ReadOnlyCollection<object>(newList);
        }

        #endregion
    }
}
