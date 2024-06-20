using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace H.Extensions.Unit
{
    /// <summary>
    /// Class to represent a quantity of a particular unit, e.g 5.25 ft
    /// </summary>
    public class QuantityWithUnit : INotifyPropertyChanged
    {
        public QuantityWithUnit()
        {
            _system = MetricUnits.System;
            _dimensions = Dimensions.Dimensionless;
            _quantity = 1.0;
            _unit = MetricUnits.Units;
        }

        public QuantityWithUnit(UnitsSystem system, IPhysicalQuantity qty)
        {
            _system = system;
            _dimensions = qty.Dimensions;
            _quantity = qty.Value;
            _unit = _system.FindDefaultUnitFor(_dimensions);
        }

        public QuantityWithUnit(double qty, Unit unit)
        {
            _system = unit.System;
            _dimensions = unit.Dimensions;
            _quantity = qty;
            _unit = unit;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private UnitsSystem _system;
        public UnitsSystem System
        {
            get { return _system; }
            set
            {
                if (value != _system)
                {
                    _system = value;
                    OnPropertyChanged();
                    ResetUnits();
                }
            }
        }

        private void ResetUnits()
        {
            _possibleUnits = null;
            OnPropertyChanged("PossibleUnits");
            _unit = _system.FindDefaultUnitFor(_dimensions);
            OnPropertyChanged("Unit");
        }

        private Dimensions _dimensions;
        public Dimensions Dimensions
        {
            get { return _dimensions; }
            set
            {
                if (value != _dimensions)
                {
                    _dimensions = value;
                    OnPropertyChanged();
                    ResetUnits();
                }
            }
        }

        private double _quantity;
        public double Quantity
        {
            get { return _quantity; }
            set
            {
                if (value != _quantity)
                {
                    _quantity = value;
                    OnPropertyChanged();
                }
            }
        }

        private Unit _unit;
        public Unit Unit
        {
            get { return _unit; }
            set
            {
                if (value != _unit)
                {
                    this.System = this.Unit.System;
                    _unit = value;
                    OnPropertyChanged();
                    OnPropertyChanged("CurrentUnitIndex");
                }
            }
        }

        public IPhysicalQuantity PhysicalQuantity
        {
            get { return _unit.GetPhysicalQuantity(_quantity); }
            set
            {
                if (value.Dimensions != _dimensions || value.Value != _quantity)
                {
                    this.Dimensions = value.Dimensions;
                    this.Quantity = value.Value;
                    OnPropertyChanged();
                }
            }
        }

        private List<Unit> _possibleUnits;
        private List<Unit> GetPossibleUnits()
        {
            if (_possibleUnits == null)
                _possibleUnits = _system?.UnitsFor(_dimensions);
            return _possibleUnits;
        }
        public List<Unit> PossibleUnits { get { return GetPossibleUnits(); } }

        public int CurrentUnitIndex
        {
            get { return GetPossibleUnits().IndexOf(_unit); }
            set { _unit = GetPossibleUnits()[value]; OnPropertyChanged("Units"); }
        }
        // Create the OnPropertyChanged method to raise the event
        // The calling member's name will be used as the parameter.
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
