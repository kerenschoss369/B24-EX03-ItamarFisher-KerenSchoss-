using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class GarageSystemLogic
    {
        GarageSystemLogic m_GarageSystemLogic;
        List<GarageOpenIssue> m_GarageOpenIssues = new List<GarageOpenIssue>();
        List<Vehicle> m_VehicleList = new List<Vehicle>();
        /*public List<Vehicle> FilterAndPrintVehiclesPlateNumbers(GarageSystemFactory.eVehicleState i_FilterByVehicleState, bool i_FetchAllVehicles)
        {
            List<Vehicle> filteredVehicleList = new List<Vehicle>();
            if (i_FetchAllVehicles == true)
            {
                filteredVehicleList = m_GarageFactory.vehiclesList;
            }
            else
            {
                foreach (Vehicle vehicle in m_GarageFactory.vehiclesList)
                {
                    if(vehicle.)
                    filteredVehicleList.Add(vehicle);
                }
            }

            return filteredVehicleList;
        }
    }
    //public bool CheckIfPlateNumberInSystem()*/

        public bool getVehicleUsingPlateNumberIfExist(string i_PlateNumber, out Vehicle o_WantedVehicle)
        {
            o_WantedVehicle = null;
            bool vehicleExists = false;
            foreach (Vehicle vehicle in m_VehicleList)
            {
                if (i_PlateNumber.Equals(vehicle.plateNumber))
                {
                    o_WantedVehicle = vehicle;
                    vehicleExists = true;
                }
            }

            return vehicleExists;
        }
        private bool CheckIfVehicleIsGasAndSetGasType(Vehicle vehicle, out eFuelType fuelType)
        {
            fuelType = eFuelType.None;
            bool isVehicleGas = true;
            if (vehicle.m_EnergySourceManager is GasolineEnergySourceManager)
            {
                GasolineEnergySourceManager currentGasolineEnergyManager = (GasolineEnergySourceManager)vehicle.m_EnergySourceManager;
                fuelType = currentGasolineEnergyManager.fuelType;
            }
            else
            {
                isVehicleGas = false;
            }

            return isVehicleGas;
        }
        public bool validatePlateNumber(string i_plateNumber)
        {
            bool isValidatePlateNumber= false;
            for (int i = 0; i < i_plateNumber.Length; i++)
            {
                if (i == 3 || i == 6)
                {
                    if (i_plateNumber[i] == '-')
                    {
                        return false;
                    }
                }
                else
                {
                    if (!char.IsDigit(i_plateNumber[i]))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

    }

}
