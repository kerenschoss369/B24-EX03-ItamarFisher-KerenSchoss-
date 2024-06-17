using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Ex03.GarageLogic.GarageOpenIssue;
using static Ex03.GarageLogic.Motorcycle;

namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        private eCarColor m_CarColor;
        private eCarDoorsAmount m_CarDoorsAmount;
        private const int k_NumberOfWheels = 5;
        private const float k_WheelsMaxAirPressure = 31;

        private const string k_CarColor = "Car color";
        private const string k_DoorsAmount = "Amount of Car Doors";
        public Car(EnergySourceManager i_EnergySourceManager, float i_MaxEnergySourceAmount, GasolineEnergySourceManager.eFuelType i_FuelType,
            ref List<Tuple<string, object>> o_AdditionalVehicleInformation)
            : base(k_NumberOfWheels, k_WheelsMaxAirPressure, i_EnergySourceManager, i_MaxEnergySourceAmount,
                  i_FuelType, ref o_AdditionalVehicleInformation)
        {
            addAdditionalInformationIntoList(ref o_AdditionalVehicleInformation);
        }
        public Car(EnergySourceManager i_EnergySourceManager, float i_MaxEnergySourceAmount, ref List<Tuple<string, object>> o_AdditionalVehicleInformation)
            : base(k_NumberOfWheels, k_WheelsMaxAirPressure, i_EnergySourceManager, i_MaxEnergySourceAmount, ref o_AdditionalVehicleInformation)
        {
            addAdditionalInformationIntoList(ref o_AdditionalVehicleInformation);
        }
        private void addAdditionalInformationIntoList(ref List<Tuple<string, object>> o_AdditionalVehicleInformation)
        {
            Tuple<string, object> carColor = new Tuple<string, object>(k_CarColor, m_CarColor);
            Tuple<string, object> carDoorAmount = new Tuple<string, object>(k_DoorsAmount, m_CarDoorsAmount);

            o_AdditionalVehicleInformation.Add(carColor);
            o_AdditionalVehicleInformation.Add(carDoorAmount);
        }
        public override void setAdditionalInformationFromList(List<Tuple<string, object>> i_AdditionalVehicleInformation)
        {
            foreach(Tuple<string,object> tuple in i_AdditionalVehicleInformation)
            {
                if(tuple.Item1 == k_CarColor)
                {
                    Enum.TryParse((string)tuple.Item2, out m_CarColor);
                }
                if(tuple.Item1 == k_DoorsAmount)
                {
                    Enum.TryParse((string)tuple.Item2, out m_CarDoorsAmount);
                }    
            }
        }

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

        public eCarDoorsAmount carDoorsAmount
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
        public override string ToString()
        {
            return string.Format(@"
Car color is: {0}
The number of doors is: {1}
", m_CarColor.ToString(), m_CarDoorsAmount,);
        }
    }
}
