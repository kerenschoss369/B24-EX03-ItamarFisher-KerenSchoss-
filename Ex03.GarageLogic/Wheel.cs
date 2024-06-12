using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal class Wheel
    {
        private readonly string r_ManufacturerName;
        private float m_CurrentAirPressure;
        private readonly float r_MaxAirPressureDefinedByManufacturer;

        public Wheel(float i_MaxAirPressure)
        {
            r_MaxAirPressureDefinedByManufacturer = i_MaxAirPressure;
        }
        private bool tryInsreaseWheelAirPressure(float i_AirToAdd)
        {
            bool isIncreasedWheelAirPressure = true;

            if (m_CurrentAirPressure + i_AirToAdd > r_MaxAirPressureDefinedByManufacturer)
            {
                isIncreasedWheelAirPressure = !isIncreasedWheelAirPressure;
            }

            m_CurrentAirPressure += i_AirToAdd;
            return isIncreasedWheelAirPressure;
        }
    }
}
