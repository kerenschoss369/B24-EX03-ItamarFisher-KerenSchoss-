using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class EnergySourceManager
    {
        protected float m_CurrentEnergySourceAmount = 0;
        protected readonly float r_MaxEnergySourceAmount;

        protected EnergySourceManager(float i_MaxEnergySourceAmount)
        {
            r_MaxEnergySourceAmount = i_MaxEnergySourceAmount;
        }

        public float currentEnergySourceAmount
        {
            get
            {
                return m_CurrentEnergySourceAmount;
            }
            set
            {
                m_CurrentEnergySourceAmount = value;
            }
        }
        public float maxEnergySourceAmount
        {
            get
            {
                return r_MaxEnergySourceAmount;
            }
        }
    }
}
