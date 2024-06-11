using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal enum eFuelType
    {
        Soler,
        Octan95,
        Octan96,
        Octan98
    }
    internal class GasolineEnergySource : EnergySource
    {
        private eFuelType m_FuelType;

    }
}
