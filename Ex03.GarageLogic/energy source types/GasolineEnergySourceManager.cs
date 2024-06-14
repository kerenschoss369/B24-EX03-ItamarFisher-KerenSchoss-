using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    

    public class GasolineEnergySourceManager : EnergySourceManager
    {
        private readonly eFuelType r_FuelType;

        public enum eFuelType
        {
            Soler,
            Octan95,
            Octan96,
            Octan98
        }

        public GasolineEnergySourceManager(float i_MaxEnergySourceAmount, eFuelType i_FuelType)
            : base(i_MaxEnergySourceAmount)
        {
            r_FuelType = i_FuelType;
        }
        public bool RefuelVehicleUntillFullOrLitersToAdd(float i_LitersToAdd, eFuelType i_FuelType, out float o_AmountOfFuelFilled)
        {
            bool isRefuel = i_FuelType == r_FuelType;
            o_AmountOfFuelFilled = r_MaxEnergySourceAmount - i_LitersToAdd;
            m_CurrentEnergySourceAmount += o_AmountOfFuelFilled;

            return isRefuel;
        }


        public eFuelType fuelType
        {
            get
            {
                return r_FuelType;
            }
        }

    }
}
