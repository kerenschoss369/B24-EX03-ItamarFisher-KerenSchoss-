using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        protected string r_ModelName;
        protected string r_PlateNumber;
        protected float m_PercentageOfEnergyLeft;
        internal List<Wheel> m_WheelsList;
        internal EnergySourceManager m_EnergySourceManager;

        internal Vehicle(int i_NumberOfWheels, float i_MaxAirPressure, EnergySourceManager i_EnergySourceManager, float i_MaxEnergySourceAmount)
        {
            m_WheelsList = new List<Wheel>(i_NumberOfWheels);
            for (int i = 0; i < i_NumberOfWheels; i++)
            {
                m_WheelsList.Add(new Wheel(i_MaxAirPressure));
            }

            m_EnergySourceManager = new ElectricEnergySourceManager(i_MaxEnergySourceAmount);
        }
        internal Vehicle(int i_NumberOfWheels, float i_MaxAirPressure, EnergySourceManager i_EnergySourceManager, float i_MaxEnergySourceAmount, GasolineEnergySourceManager.eFuelType i_FuelType)
        {
            m_WheelsList = new List<Wheel>(i_NumberOfWheels);
            for (int i = 0; i < i_NumberOfWheels; i++)
            {
                m_WheelsList.Add(new Wheel(i_MaxAirPressure));
            }

            m_EnergySourceManager = new GasolineEnergySourceManager(i_MaxEnergySourceAmount, i_FuelType);
        }
        public string modelName
        {
            get
            {
                return r_ModelName;
            }
        }

        public string plateNumber
        {
            get
            {
                return r_PlateNumber;
            }
        }

        public float percentageOfEnergyLeft
        {
            get
            {
                return m_PercentageOfEnergyLeft;
            }
        }

        public List<Wheel> wheelsList
        {
            get 
            {
                return m_WheelsList;
            }
        }
    }
}
