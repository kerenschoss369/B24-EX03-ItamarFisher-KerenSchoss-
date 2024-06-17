using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Ex03.GarageLogic.Car;

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
        private GasolineEnergySourceManager m_TruckGasolineEnergyManagaer;

        private const string k_IsCarryingHazardousMaterials = "Is carrying hazardous materials";
        private const string k_CargoVolume = "Cargo volume";
        public Truck(ref List<Tuple<string, object>> o_AdditionalVehicleInformation)
            : base(k_NumberOfWheels, k_WheelsMaxAirPressure, new GasolineEnergySourceManager(k_TankFuelCapacity, k_FuelType, ref o_AdditionalVehicleInformation),
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
                    Enum.TryParse((string)tuple.Item2, out m_IsCarryingHazardousMaterials);
                }
                if (tuple.Item1 == k_CargoVolume)
                {
                    Enum.TryParse((string)tuple.Item2, out m_CargoVolume);
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
    }
}

