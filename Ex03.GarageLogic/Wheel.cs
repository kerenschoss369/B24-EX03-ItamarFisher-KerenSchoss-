using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        private string m_ManufacturerName;
        private float m_CurrentAirPressure;
        private readonly float r_MaxAirPressureDefinedByManufacturer;

        public Wheel(float i_MaxAirPressure)
        {
            r_MaxAirPressureDefinedByManufacturer = i_MaxAirPressure;
        }
        public bool tryIncreaseWheelAirPressure(float i_AirToAdd)
        {
            bool isIncreasedWheelAirPressure = true;

            if (m_CurrentAirPressure + i_AirToAdd > r_MaxAirPressureDefinedByManufacturer)
            {
                isIncreasedWheelAirPressure = !isIncreasedWheelAirPressure;
                throw new ValueOutOfRangeException(i_AirToAdd, 0f, r_MaxAirPressureDefinedByManufacturer - m_CurrentAirPressure);
            }
            else
            {
                m_CurrentAirPressure += i_AirToAdd;
            }

            return isIncreasedWheelAirPressure;
        }
        public float currentAirPressure
        {
            get
            {
                return m_CurrentAirPressure;
            }
            
            set
            {
               m_CurrentAirPressure = value;
            }
        }
        public float maxAirPressureDefinedByManufacturer
        {
            get
            {
                return r_MaxAirPressureDefinedByManufacturer;
            }
        }

        public string manufacturerName
        {
            get
            {
                return m_ManufacturerName;
            }
            set
            {
                m_ManufacturerName = value;
            }
        }
    }
}
