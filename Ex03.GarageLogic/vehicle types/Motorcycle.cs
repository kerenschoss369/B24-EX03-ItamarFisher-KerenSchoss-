using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    

    public class Motorcycle : Vehicle
    {
        private eLicenseType m_LicenseType;
        private int m_EngineDisplacementInCc;
        private const int k_NumberOfWheels = 2;
        private const float k_WheelsMaxAirPressure = 33;

        public enum eLicenseType
        {
            A,
            A1,
            AA,
            B1
        }

    public class Motorcycle : Vehicle
    {
        private readonly eLicenseType r_LicenseType;
        private readonly int r_EngineDisplacementInCc;
        private const int k_NumberOfWheels = 2;
        private const float k_WheelsMaxAirPressure = 33;
        public Motorcycle(EnergySourceManager i_EnergySourceManager, float i_MaxEnergySourceAmount, GasolineEnergySourceManager.eFuelType i_FuelType)
            :base(k_NumberOfWheels, k_WheelsMaxAirPressure, i_EnergySourceManager, i_MaxEnergySourceAmount, i_FuelType)
        {
        }
        public Motorcycle(EnergySourceManager i_EnergySourceManager, float i_MaxEnergySourceAmount)
            : base(k_NumberOfWheels, k_WheelsMaxAirPressure, i_EnergySourceManager, i_MaxEnergySourceAmount)
        {
        }
       

        public eLicenseType LicenseType
        {
            get
            {
                return m_LicenseType;
            }
            set
            {
                m_LicenseType = value;
            }
        }

        public int EngineDisplacementInCc
        {
            get
            {
                return m_EngineDisplacementInCc;
            }
            set
            {
                m_EngineDisplacementInCc = value;
            }
        }
    }
}
