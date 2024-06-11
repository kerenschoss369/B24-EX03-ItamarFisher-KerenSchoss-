using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal class ElectricEnergySourceManager : EnergySourceManager
    {
        private bool tryBatteryCharge(float i_HoursToAdd)
        {
            bool isBatteryCharge = true;

            if (m_CurrentEnergySourceAmount + i_HoursToAdd > r_MaxEnergySourceAmount) //make sure that this is the right check
            {
                isBatteryCharge = !isBatteryCharge;
            }

            m_CurrentEnergySourceAmount += i_HoursToAdd;
            return isBatteryCharge;
        }
    }
}
