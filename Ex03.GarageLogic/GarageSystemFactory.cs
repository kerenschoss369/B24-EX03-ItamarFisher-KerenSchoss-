using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static Ex03.GarageLogic.GarageSystemFactory;

namespace Ex03.GarageLogic
{
    public class GarageSystemFactory
    {
        private List<Vehicle> m_VehiclesList;
       
        public class VehicleInGarageDetails
        {
            private readonly int m_VehiclePlateNumber;
            private readonly string r_VehicleOwnerName;
            private readonly string r_VehiclePhoneNumber;
            private eVehicleState m_VehicleState;

            public eVehicleState vehicleState
            {
                get
                {
                    return m_VehicleState;
                }
            }

        }
        public List<Vehicle> vehiclesList
        {
            get
            { 

                return m_VehiclesList; 
            } 
        }

       
        public enum eVehicleState
        {
            InMaintenance = 1,
            Fixed,
            PaidFor
        }
        //public void AddVechileToList()

    }

}
