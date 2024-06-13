using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal class Truck : Vehicle
    {
        private float m_CargoVolume;
        private bool m_IsCarryingHazardousMaterials;
        private const int k_NumberOfWheels = 12;
        private const float k_WheelsMaxAirPressure = 28;
        private const eFuelType k_FuelType = eFuelType.Soler;
        private const float k_TankFuelCapacity = 120;
        private GasolineEnergySourceManager m_TruckGasolineEnergyManagaer;
        public Truck()
            : base(k_NumberOfWheels, k_WheelsMaxAirPressure, new GasolineEnergySourceManager(k_TankFuelCapacity, k_FuelType), k_TankFuelCapacity, k_FuelType)//Maybe 2 ctors could go along better
        {
        }
    }
}
