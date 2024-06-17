using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Ex03.GarageLogic.Car;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        protected string m_ModelName;
        protected string m_PlateNumber;
        protected float m_PercentageOfEnergyLeft;
        internal List<Wheel> m_WheelsList;
        internal EnergySourceManager m_EnergySourceManager;

        internal Vehicle(int i_NumberOfWheels, float i_MaxAirPressure, EnergySourceManager i_EnergySourceManager,
            float i_MaxEnergySourceAmount, ref List<Tuple<string, object>> o_AdditionalVehicleInformation)
        {
            m_WheelsList = new List<Wheel>(i_NumberOfWheels);
            for (int i = 0; i < i_NumberOfWheels; i++)
            {
                m_WheelsList.Add(new Wheel(i_MaxAirPressure));
            }
            addAdditionalInformationIntoList(ref o_AdditionalVehicleInformation);
            m_EnergySourceManager = new ElectricEnergySourceManager(i_MaxEnergySourceAmount, ref o_AdditionalVehicleInformation);
        }
        internal Vehicle(int i_NumberOfWheels, float i_MaxAirPressure, EnergySourceManager i_EnergySourceManager, float i_MaxEnergySourceAmount,
            GasolineEnergySourceManager.eFuelType i_FuelType, ref List<Tuple<string, object>> o_AdditionalVehicleInformation)
        {
            m_WheelsList = new List<Wheel>(i_NumberOfWheels);
            for (int i = 0; i < i_NumberOfWheels; i++)
            {
                m_WheelsList.Add(new Wheel(i_MaxAirPressure));
            }
            addAdditionalInformationIntoList(ref o_AdditionalVehicleInformation);
            m_EnergySourceManager = new GasolineEnergySourceManager(i_MaxEnergySourceAmount, i_FuelType, ref o_AdditionalVehicleInformation);
        }

        private void addAdditionalInformationIntoList(ref List<Tuple<string, object>> o_AdditionalVehicleInformation)
        {
            Tuple<string, object> modelName = new Tuple<string, object>("Model name", m_ModelName);

            o_AdditionalVehicleInformation.Add(modelName);
        }
        public abstract void setAdditionalInformationFromList(List<Tuple<string, object>> i_AdditionalVehicleInformation);
        public void setBaseAdditionalInformationFromList(List<Tuple<string, object>> i_AdditionalVehicleInformation)
        {
            
            foreach (Tuple<string, object> tuple in i_AdditionalVehicleInformation)
            {
                if (tuple.Item1 == "Model name")
                {
                    m_ModelName = (string)tuple.Item2;
                }
            }
            m_EnergySourceManager.SetAdditionalInformationFromList(i_AdditionalVehicleInformation);
            percentageOfEnergyLeft = (float)(m_EnergySourceManager.currentEnergySourceAmount / m_EnergySourceManager.maxEnergySourceAmount);
            percentageOfEnergyLeft = percentageOfEnergyLeft * 100;

        }
        public string modelName
        {
            get
            {
                return m_ModelName;
            }
        }

        public string plateNumber
        {
            get
            {
                return m_PlateNumber;
            }
            set
            {
                m_PlateNumber = value;
            }
        }

        public float percentageOfEnergyLeft
        {
            get
            {
                return m_PercentageOfEnergyLeft;
            }
            set
            {
                m_PercentageOfEnergyLeft= value;
            }

        }

        public List<Wheel> wheelsList
        {
            get 
            {
                return m_WheelsList;
            }
        }
        public EnergySourceManager energySourceManager
        {
            get
            {
                return m_EnergySourceManager;
            }
        }
        public override string ToString()
        {
            string energyType;
            GasolineEnergySourceManager gasolineEnergySourceManager;
            if(energySourceManager is ElectricEnergySourceManager)
            {
                energyType = "Electricity";
            }
            else
            {
                gasolineEnergySourceManager = (GasolineEnergySourceManager)energySourceManager;
                energyType = gasolineEnergySourceManager.fuelType.ToString();
            }

        string vehicleString = string.Format(
           @"
Car model: {0}
Plate number is: {1}
Energy source is: {2}
Percentage of energy left is: %{3}
Amount of energy left is: {4}
", m_ModelName, m_PlateNumber, energyType, m_PercentageOfEnergyLeft, m_EnergySourceManager.currentEnergySourceAmount);

            return vehicleString;
        }
    }
}
