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
            foreach (Tuple<string, object> tuple in i_AdditionalVehicleInformation)
            {
                if (tuple.Item1 == k_CarColor)
                {
                    if (!Enum.TryParse<eCarColor>(tuple.Item2.ToString(), out m_CarColor))
                    {
                        throw new FormatException("Could not parse the car color.");
                    }
                }
                if (tuple.Item1 == k_DoorsAmount)
                {
                    if (!Enum.TryParse<eCarDoorsAmount>(tuple.Item2.ToString(), out m_CarDoorsAmount))
                    {
                        throw new FormatException("Could not parse the number of doors.");
                    }
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
            StringBuilder stringBuilder = new StringBuilder();
            string carString = string.Format(@"
Car color is: {0}
The number of doors is: {1}
", m_CarColor.ToString(), m_CarDoorsAmount);
            stringBuilder.Append(base.ToString());
            stringBuilder.Append(carString);
            return stringBuilder.ToString();

        }
    }
}
