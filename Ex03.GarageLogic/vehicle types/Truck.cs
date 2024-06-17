using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Ex03.GarageLogic.Car;
using static Ex03.GarageLogic.Motorcycle;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        private float m_CargoVolume;
        private bool m_IsCarryingHazardousMaterials;
        private const int k_NumberOfWheels = 12;
        private const float k_WheelsMaxAirPressure = 28;
        private const GasolineEnergySourceManager.eFuelType k_FuelType = GasolineEnergySourceManager.eFuelType.Soler;
        private const float k_TankFuelCapacity = 120;
        private const string k_IsCarryingHazardousMaterials = "Is carrying hazardous materials";
        private const string k_CargoVolume = "Cargo volume";
        public Truck(EnergySourceManager i_EnergySourceManager, ref List<Tuple<string, object>> o_AdditionalVehicleInformation)
            : base(k_NumberOfWheels, k_WheelsMaxAirPressure, i_EnergySourceManager,
                  k_TankFuelCapacity, k_FuelType, ref o_AdditionalVehicleInformation)
        {
            Tuple<string, object> cargoVolume = new Tuple<string, object>(k_CargoVolume, m_CargoVolume);
            Tuple<string, object> isCarryingHazardousMaterials =
               new Tuple<string, object>(k_IsCarryingHazardousMaterials, m_IsCarryingHazardousMaterials);

            o_AdditionalVehicleInformation.Add(cargoVolume);
            o_AdditionalVehicleInformation.Add(isCarryingHazardousMaterials);
        }

        public override void setAdditionalInformationFromList(List<Tuple<string, object>> i_AdditionalVehicleInformation)
        {
            foreach (Tuple<string, object> tuple in i_AdditionalVehicleInformation)
            {
                if (tuple.Item1 == k_IsCarryingHazardousMaterials)
                {
                    if (!bool.TryParse(tuple.Item2.ToString(), out m_IsCarryingHazardousMaterials))
                    {
                        throw new FormatException("Could not parse the hazardous materials flag.");
                    }
                }
                if (tuple.Item1 == k_CargoVolume)
                {
                    if (!float.TryParse(tuple.Item2.ToString(), out m_CargoVolume))
                    {
                        throw new FormatException("Could not parse the cargo volume.");
                    }
                }
            }
        }
        public float cargoVolume
        {
            get
            {
                return m_CargoVolume;
            }
            set
            {
                m_CargoVolume = value;
            }
        }

        public bool isCarryingHazardousMaterials
        {
            get
            {
                return m_IsCarryingHazardousMaterials;
            }
            set
            {
                m_IsCarryingHazardousMaterials = value;
            }
        }
        public override string ToString()
        {
            string carryingHazardouMaterials;
            if (m_IsCarryingHazardousMaterials == true)
            {
                carryingHazardouMaterials = "Truck is carrying hazardous materials";
            }
            else
            {
                carryingHazardouMaterials = "Truck isn't carrying hazardous materials";
            }
            StringBuilder stringBuilder = new StringBuilder();
            string carString = string.Format(@"
Motorcycle License type is: {0}
Enginge displacement in cc is: {1}
", m_CargoVolume.ToString(), carryingHazardouMaterials);
            stringBuilder.Append(base.ToString());
            stringBuilder.Append(carString);

            return stringBuilder.ToString();


        }
    }
}

