using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal class ElectricEnergySourceManager : EnergySourceManager
    {
        public ElectricEnergySourceManager(float i_MaxEnergySourceAmount)
            : base(i_MaxEnergySourceAmount)
        {
        }
        public float ChargeBatteryUntillFullOrHoursToAdd(float i_HoursToAdd)
        {
            float amountOfHoursCharged = r_MaxEnergySourceAmount - i_HoursToAdd;
            m_CurrentEnergySourceAmount += amountOfHoursCharged;
            return amountOfHoursCharged;
        }
    }
}
