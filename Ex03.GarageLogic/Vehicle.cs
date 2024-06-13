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

        internal Vehicle(int i_NumberOfWheels, float i_MaxAirPressure, EnergySourceManager i_EnergySourceManager, float i_MaxEnergySourceAmount, eFuelType i_FuelType)
        {
            m_WheelsList = new List<Wheel>(i_NumberOfWheels);
            for(int i = 0;  i < i_NumberOfWheels; i++)
            {
                m_WheelsList.Add(new Wheel(i_MaxAirPressure));
            }

            if(i_EnergySourceManager is GasolineEnergySourceManager)
            {
                m_EnergySourceManager = new GasolineEnergySourceManager(i_MaxEnergySourceAmount, i_FuelType);
            }
            else
            {
                m_EnergySourceManager = new ElectricEnergySourceManager(i_MaxEnergySourceAmount);
            }
        }
        public string plateNumber
        {
            get
            {
                return r_PlateNumber;
            }
        }
    }
}
