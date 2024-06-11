using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    abstract class Vehicle
    {
        protected string m_ModelName;
        protected string m_LicenseNumber;
        protected float m_PercentageOfEnergyLeft;
        protected List<Wheel> m_WheelsList;
        protected EnergySource m_EnergySource; //maybe create as an object instead
    }
}
