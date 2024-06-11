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
    internal class GasolineEnergySourceManager : EnergySourceManager
    {
        private readonly eFuelType r_FuelType;

        private bool tryRefuel(float i_LitersToAdd, eFuelType i_FuelType)
        {
            bool isRefuel = true;

            if ((i_FuelType != r_FuelType) || (m_CurrentEnergySourceAmount + i_LitersToAdd > r_MaxEnergySourceAmount)) //make sure that this is the right check
            {
                isRefuel= !isRefuel; 
            }

            m_CurrentEnergySourceAmount += i_LitersToAdd;
            return isRefuel;
        }

    }
}
