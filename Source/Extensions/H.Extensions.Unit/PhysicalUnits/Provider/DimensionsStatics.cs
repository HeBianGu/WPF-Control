namespace H.Extensions.Unit
{
    // Generated from Dimensions.xml
    public partial class Dimensions
    {
        public static readonly Dimensions Dimensionless = new Dimensions(0, 0, 0);
        public static readonly Dimensions Mass = new Dimensions(1, 0, 0);
        public static readonly Dimensions Length = new Dimensions(0, 1, 0);
        public static readonly Dimensions Time = new Dimensions(0, 0, 1);
        public static readonly Dimensions Current = new Dimensions(0, 0, 0, 1);
        public static readonly Dimensions TemperatureChange = new Dimensions(0, 0, 0, 0, 1);
        public static readonly Dimensions AmountOfSubstance = new Dimensions(0, 0, 0, 0, 0, 1);
        public static readonly Dimensions LuminousIntensity = new Dimensions(0, 0, 0, 0, 0, 0, 1);
        public static readonly Dimensions Angle = new Dimensions(0, 0, 0, 0, 0, 0, 0, 1);
        public static readonly Dimensions Area = Length * Length;
        public static readonly Dimensions Volume = Area * Length;
        public static readonly Dimensions Density = Mass / Volume;
        public static readonly Dimensions MolarMass = Mass / AmountOfSubstance;
        public static readonly Dimensions MolarConcentration = AmountOfSubstance / Volume;
        public static readonly Dimensions Velocity = Length / Time;
        public static readonly Dimensions TangentialVelocity = Velocity;
        public static readonly Dimensions AngularVelocity = Angle / Time;
        public static readonly Dimensions SolidAngle = Angle * Angle;
        public static readonly Dimensions TimeSquared = Time * Time;
        public static readonly Dimensions VelocitySquared = Velocity * Velocity;
        public static readonly Dimensions AngularVelocitySquared = AngularVelocity * AngularVelocity;
        public static readonly Dimensions ByLength = Dimensionless / Length;
        public static readonly Dimensions ByArea = Dimensionless / Area;
        public static readonly Dimensions MassByLength = Mass / Length;
        public static readonly Dimensions MassByArea = Mass / Area;
        public static readonly Dimensions FourDimensionalVolume = Volume * Length;
        public static readonly Dimensions MolarConcentrationGradient = MolarConcentration / Length;
        public static readonly Dimensions AmountOfSubstanceByArea = AmountOfSubstance / Area;
        public static readonly Dimensions AmountOfSubstanceByTime = AmountOfSubstance / Time;
        public static readonly Dimensions DiffusionFlux = AmountOfSubstanceByArea / Time;
        public static readonly Dimensions KinematicViscosity = Area / Time;
        public static readonly Dimensions Acceleration = Velocity / Time;
        public static readonly Dimensions Momentum = Mass * Velocity;
        public static readonly Dimensions Force = Mass * Acceleration;
        public static readonly Dimensions MassFlowRate = Mass / Time;
        public static readonly Dimensions MomentOfInertia = Mass * Area;
        public static readonly Dimensions AngularMomentum = MomentOfInertia * AngularVelocity;
        public static readonly Dimensions Energy = Force * Length;
        public static readonly Dimensions SurfaceTension = Force / Length;
        public static readonly Dimensions ElectricCharge = Current * Time;
        public static readonly Dimensions ElectricPotential = Energy / ElectricCharge;
        public static readonly Dimensions ElectricPotentialSquared = ElectricPotential * ElectricPotential;
        public static readonly Dimensions Power = Energy / Time;
        public static readonly Dimensions Resistance = ElectricPotential / Current;
        public static readonly Dimensions Pressure = Force / Area;
        public static readonly Dimensions Frequency = Dimensionless / Time;
        public static readonly Dimensions VelocityGradient = Velocity / Length;
        public static readonly Dimensions CoefficientOfViscosity = Force / KinematicViscosity;
        public static readonly Dimensions VolumeFlowRate = Volume / Time;
        public static readonly Dimensions ResistanceToFlow = MassFlowRate / FourDimensionalVolume;
        public static readonly Dimensions MassByAreaByTimeSquared = MassByArea / TimeSquared;
        public static readonly Dimensions VelocityByDensity = Velocity / Density;
        public static readonly Dimensions ThermalCapacity = Energy / TemperatureChange;
        public static readonly Dimensions SpecificHeat = ThermalCapacity / Mass;
        public static readonly Dimensions MolarSpecificHeat = ThermalCapacity / AmountOfSubstance;
        public static readonly Dimensions MolarConcentrationTimesAbsoluteTemperature = MolarConcentration * AbsoluteTemperature;
        public static readonly Dimensions CoefficientOfThermalExpansion = Dimensionless / TemperatureChange;
        public static readonly Dimensions ThermalCapacityByVolume = ThermalCapacity / Volume;
        public static readonly Dimensions EnergyFlux = Power / Area;
        public static readonly Dimensions PowerGradient = Power / Length;
        public static readonly Dimensions TemperatureGradient = TemperatureChange / Length;
        public static readonly Dimensions ThermalConductivity = EnergyFlux / TemperatureGradient;
        public static readonly Dimensions ResistanceTimesArea = Resistance * Area;
        public static readonly Dimensions Resistivity = Resistance * Length;
        public static readonly Dimensions LuminousFlux = LuminousIntensity * SolidAngle;
        public static readonly Dimensions Illuminance = LuminousFlux / Area;

        public static readonly Dimensions[] allDimensions = new Dimensions[]
        {
            Dimensionless,
            Mass,
            Length,
            Time,
            Current,
            TemperatureChange,
            AmountOfSubstance,
            LuminousIntensity,
            Angle,
            Area,
            Volume,
            Density,
            MolarMass,
            MolarConcentration,
            Velocity,
            TangentialVelocity,
            AngularVelocity,
            SolidAngle,
            TimeSquared,
            VelocitySquared,
            AngularVelocitySquared,
            ByLength,
            ByArea,
            MassByLength,
            MassByArea,
            FourDimensionalVolume,
            MolarConcentrationGradient,
            AmountOfSubstanceByArea,
            AmountOfSubstanceByTime,
            DiffusionFlux,
            KinematicViscosity,
            Acceleration,
            Momentum,
            Force,
            MassFlowRate,
            MomentOfInertia,
            AngularMomentum,
            Energy,
            SurfaceTension,
            ElectricCharge,
            ElectricPotential,
            ElectricPotentialSquared,
            Power,
            Resistance,
            Pressure,
            Frequency,
            VelocityGradient,
            CoefficientOfViscosity,
            VolumeFlowRate,
            ResistanceToFlow,
            MassByAreaByTimeSquared,
            VelocityByDensity,
            ThermalCapacity,
            SpecificHeat,
            MolarSpecificHeat,
            MolarConcentrationTimesAbsoluteTemperature,
            CoefficientOfThermalExpansion,
            ThermalCapacityByVolume,
            AbsoluteTemperature,
            EnergyFlux,
            PowerGradient,
            TemperatureGradient,
            ThermalConductivity,
            ResistanceTimesArea,
            Resistivity,
            LuminousFlux,
            Illuminance,
        };

        public static readonly string[] DimensionNames = new string[]
        {
            "Dimensionless",
            "Mass",
            "Length",
            "Time",
            "Current",
            "TemperatureChange",
            "AmountOfSubstance",
            "LuminousIntensity",
            "Angle",
            "Area",
            "Volume",
            "Density",
            "MolarMass",
            "MolarConcentration",
            "Velocity",
            "TangentialVelocity",
            "AngularVelocity",
            "SolidAngle",
            "TimeSquared",
            "VelocitySquared",
            "AngularVelocitySquared",
            "ByLength",
            "ByArea",
            "MassByLength",
            "MassByArea",
            "FourDimensionalVolume",
            "MolarConcentrationGradient",
            "AmountOfSubstanceByArea",
            "AmountOfSubstanceByTime",
            "DiffusionFlux",
            "KinematicViscosity",
            "Acceleration",
            "Momentum",
            "Force",
            "MassFlowRate",
            "MomentOfInertia",
            "AngularMomentum",
            "Energy",
            "SurfaceTension",
            "ElectricCharge",
            "ElectricPotential",
            "ElectricPotentialSquared",
            "Power",
            "Resistance",
            "Pressure",
            "Frequency",
            "VelocityGradient",
            "CoefficientOfViscosity",
            "VolumeFlowRate",
            "ResistanceToFlow",
            "MassByAreaByTimeSquared",
            "VelocityByDensity",
            "ThermalCapacity",
            "SpecificHeat",
            "MolarSpecificHeat",
            "MolarConcentrationTimesAbsoluteTemperature",
            "CoefficientOfThermalExpansion",
            "ThermalCapacityByVolume",
            "AbsoluteTemperature",
            "EnergyFlux",
            "PowerGradient",
            "TemperatureGradient",
            "ThermalConductivity",
            "ResistanceTimesArea",
            "Resistivity",
            "LuminousFlux",
            "Illuminance",
        };

    }
}
