using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal abstract class EnergySourceManager
    {
        protected float m_CurrentEnergySourceAmount;
        protected readonly float r_MaxEnergySourceAmount;

        protected EnergySourceManager(float i_MaxEnergySourceAmount)
        {
            r_MaxEnergySourceAmount = i_MaxEnergySourceAmount;
        }
    }
}
