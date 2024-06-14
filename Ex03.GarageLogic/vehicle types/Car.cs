using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Ex03.GarageLogic.GarageOpenIssue;

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

    public class Car : Vehicle
    {
        private eCarColor r_CarColor;
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

        public eCarColor carColor
        {
            get
            {
                return r_CarColor;
            }
        }

        public eCarDoorsAmount carDoorsAmount
        {
            get
            {
                return r_CarDoorsAmount;
            }
        }

        public int numberOfWheels
        {
            get
            {
                return k_NumberOfWheels;
            }
        }

        public float wheelsMaxAirPressure
        {
            get
            {
                return k_WheelsMaxAirPressure;
            }
        }

    }
}
