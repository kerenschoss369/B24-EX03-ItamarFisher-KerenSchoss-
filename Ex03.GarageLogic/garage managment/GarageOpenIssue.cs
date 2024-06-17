using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
   
    public class GarageOpenIssue
    {
        private readonly string r_VehicleOwnerName;
        private readonly string r_OwnerPhoneNumber;
        private eVehicleState m_VehicleState;
        private readonly string m_VehiclePlateNumber;

        public enum eVehicleState
        {
            InMaintenance,
            Fixed,
            PaidFor
        }

        public GarageOpenIssue(string r_VehicleOwnerName, string r_VehiclePhoneNumber, eVehicleState vehicleState, string vehiclePlateNumber)
        {
            this.r_VehicleOwnerName = r_VehicleOwnerName;
            this.r_OwnerPhoneNumber = r_VehiclePhoneNumber;
            this.vehicleState = vehicleState;
            m_VehiclePlateNumber = vehiclePlateNumber;
            this.vehicleState = vehicleState;
        }

        public eVehicleState vehicleState
        {
            get
            {
                return m_VehicleState;
            }
            set
            {
                m_VehicleState = value;
            }
        }

        public string vehicleOwnerName
        {
            get
            {
                return r_VehicleOwnerName;
            }
        }

        public string vehiclePhoneNumber
        {
            get
            {
                return r_OwnerPhoneNumber;
            }
        }

        public string vehiclePlateNumber
        {
            get
            {
                return m_VehiclePlateNumber;
            }
        }
        public override string ToString()
        {
        string vehicleString = string.Format(
               @"
Car Owner's name is: {0}
Owner's phone number is: {1}
Vehicle state is: {2}
", r_VehicleOwnerName, r_OwnerPhoneNumber, m_VehicleState.ToString());

            return vehicleString;
        }

    }

}
