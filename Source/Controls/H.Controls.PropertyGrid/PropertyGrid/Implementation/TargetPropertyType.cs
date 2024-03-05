// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;


namespace H.Controls.PropertyGrid
{
    /// <summary>
    /// This class is intended to provide the "Type" target
    /// for property definitions or editor definitions when
    /// using Property Element Syntax.
    /// </summary>
    public sealed class TargetPropertyType
    {
        private Type _type;
        private bool _sealed;

        public Type Type
        {
            get { return _type; }
            set
            {
                if (_sealed)
                    throw new InvalidOperationException(
                      string.Format(
                      "{0}.Type property cannot be modified once the instance is used",
                      typeof(TargetPropertyType)));

                _type = value;
            }
        }

        internal void Seal()
        {
            if (_type == null)
                throw new InvalidOperationException(
                  string.Format("{0}.Type property must be initialized", typeof(TargetPropertyType)));

            _sealed = true;
        }
    }
}
