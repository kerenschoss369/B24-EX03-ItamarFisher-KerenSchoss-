﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public enum eLicenseType
    {
        A,
        A1,
        AA,
        B1
    }


    internal class Motorcycle : Vehicle
    {
        private readonly eLicenseType r_LicenseType;
        private readonly int r_EngineDisplacementInCc;
        private const int k_NumberOfWheels = 2;
        private const float k_WheelsMaxAirPressure = 33;

        public Motorcycle(float i_MaxEnergySourceAmount, EnergySourceManager i_EnergySourceManager, eFuelType i_FuelType)
            :base(k_NumberOfWheels, k_WheelsMaxAirPressure, i_EnergySourceManager, i_FuelType)
        {
        }
    }
}
