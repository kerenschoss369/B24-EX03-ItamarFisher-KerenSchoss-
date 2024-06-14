using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public Truck()
            : base(k_NumberOfWheels, k_WheelsMaxAirPressure, new GasolineEnergySourceManager(k_TankFuelCapacity, k_FuelType), k_TankFuelCapacity, k_FuelType)//Maybe 2 ctors could go along better
        {
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

