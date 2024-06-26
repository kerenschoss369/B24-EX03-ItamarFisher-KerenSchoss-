﻿using System;
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

        public GasolineEnergySourceManager(float i_MaxEnergySourceAmount, eFuelType i_FuelType, ref List<Tuple<string, object>> o_AdditionalVehicleInformation)
            : base(i_MaxEnergySourceAmount, ref o_AdditionalVehicleInformation)
        {
            r_FuelType = i_FuelType;
        }

        public bool RefuelVehicleUntillFullOrLitersToAdd(float i_LitersToAdd, eFuelType i_FuelType, out float o_AmountOfFuelFilled)
        {
            bool isRefuel = (i_FuelType == r_FuelType);
            o_AmountOfFuelFilled =  i_LitersToAdd;
            if(o_AmountOfFuelFilled>  r_MaxEnergySourceAmount)
            {
                o_AmountOfFuelFilled = o_AmountOfFuelFilled - r_MaxEnergySourceAmount;
            }
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
