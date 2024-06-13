using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public enum eCarColor
    {
        Yellow,
        White,
        Red,
        Black
    }

    public enum eCarDoorsAmount
    {
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5
    }

    internal class Car : Vehicle
    {
        private readonly eCarColor r_CarColor;
        private readonly eCarDoorsAmount r_CarDoorsAmount;
        private const int k_NumberOfWheels = 5;
        private const float k_WheelsMaxAirPressure = 31;
        public Car(EnergySourceManager i_EnergySourceManager, float i_MaxEnergySourceAmount, GasolineEnergySourceManager.eFuelType i_FuelType)
            : base(k_NumberOfWheels, k_WheelsMaxAirPressure, i_EnergySourceManager, i_MaxEnergySourceAmount, i_FuelType)
        {
        }
        public Car(EnergySourceManager i_EnergySourceManager, float i_MaxEnergySourceAmount)
            : base(k_NumberOfWheels, k_WheelsMaxAirPressure, i_EnergySourceManager, i_MaxEnergySourceAmount)
        {
        }
    }
}
