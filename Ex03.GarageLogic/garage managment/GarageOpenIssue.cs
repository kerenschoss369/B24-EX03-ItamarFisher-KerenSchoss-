﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public enum eVehicleState
    {
        InMaintenance,
        Fixed,
        PaidFor
    }
    internal class GarageOpenIssue
    {
        private readonly string r_VehicleOwnerName;
        private readonly string r_VehiclePhoneNumber;
        private eVehicleState m_VehicleState;
        private readonly int m_VehiclePlateNumber;

        public eVehicleState vehicleState
        {
            get
            {
                return m_VehicleState;
            }
        }

    }

}