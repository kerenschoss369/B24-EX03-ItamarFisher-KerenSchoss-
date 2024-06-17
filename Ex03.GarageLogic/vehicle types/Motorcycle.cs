using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Ex03.GarageLogic.Car;

namespace Ex03.GarageLogic
{
   public class Motorcycle : Vehicle
    {
        private eLicenseType m_LicenseType;
        private int m_EngineDisplacementInCc;
        private const int k_NumberOfWheels = 2;
        private const float k_WheelsMaxAirPressure = 33;

        private const string K_LicenseType = "License type";
        private const string k_EngineDisplacementInCc = "Engine displacement in cc";
        public Motorcycle(EnergySourceManager i_EnergySourceManager, float i_MaxEnergySourceAmount,
            GasolineEnergySourceManager.eFuelType i_FuelType, ref List<Tuple<string, object>>  o_AdditionalVehicleInformation)
            :base(k_NumberOfWheels, k_WheelsMaxAirPressure, i_EnergySourceManager, i_MaxEnergySourceAmount, i_FuelType, ref o_AdditionalVehicleInformation)
        {
            addAdditionalInformationIntoList(ref o_AdditionalVehicleInformation);
        }
        public Motorcycle(EnergySourceManager i_EnergySourceManager, float i_MaxEnergySourceAmount,
            ref List<Tuple<string, object>> o_AdditionalVehicleInformation)
            : base(k_NumberOfWheels, k_WheelsMaxAirPressure, i_EnergySourceManager, i_MaxEnergySourceAmount, ref o_AdditionalVehicleInformation)
        {
            addAdditionalInformationIntoList(ref o_AdditionalVehicleInformation);
        }
        private void addAdditionalInformationIntoList(ref List<Tuple<string, object>> o_AdditionalVehicleInformation)
        {
            Tuple<string, object> licenseType = new Tuple<string, object>(K_LicenseType, m_LicenseType);
            Tuple<string, object> engineDisplacementInCc = new Tuple<string, object>(k_EngineDisplacementInCc, m_EngineDisplacementInCc);

            o_AdditionalVehicleInformation.Add(licenseType);
            o_AdditionalVehicleInformation.Add(engineDisplacementInCc);
        }
        public override void setAdditionalInformationFromList(List<Tuple<string, object>> i_AdditionalVehicleInformation)
        {
            foreach (Tuple<string, object> tuple in i_AdditionalVehicleInformation)
             {
                if ((tuple.Item1 == K_LicenseType) && (tuple.Item1 == k_EngineDisplacementInCc))
                {
                    if (!(Enum.TryParse<eLicenseType>((string)tuple.Item2, out m_LicenseType) &&
                    (int.TryParse((string)tuple.Item2, out m_EngineDisplacementInCc))))
                    {
                        throw new FormatException("Could not Parse the input.");
                    }
                }
            }
        }
        public enum eLicenseType
        {
            A,
            A1,
            AA,
            B1
        }

        public eLicenseType licenseType
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

        public int engineDisplacementInCc
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
