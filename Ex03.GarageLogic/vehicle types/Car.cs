using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Ex03.GarageLogic.Car;

namespace Ex03.GarageLogic
{


    public class Car : Vehicle
    {
        private eCarColor m_CarColor;
        private eCarDoorsAmount m_CarDoorsAmount;
        private const int k_NumberOfWheels = 5;
        private const float k_WheelsMaxAirPressure = 31;
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
                return m_CarColor;
            }
            set
            {
                m_CarColor = value;
            }
        }

        public eCarDoorsAmount CarDoorsAmount
        {
            get
            {
                return m_CarDoorsAmount;
            }
            set
            {
                m_CarDoorsAmount = value;
            }
        }
    }
}
