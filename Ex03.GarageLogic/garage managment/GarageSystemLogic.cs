using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
        private bool checkIfVehicleIsGasPowered(Vehicle i_Vehicle)
        {

            return (i_Vehicle.m_EnergySourceManager is GasolineEnergySourceManager);
        }


        /* private string getFuelTypeForGasVehicleAsString(Vehicle i_Vehicle) // prob not needed
         {
             GasolineEnergySourceManager gasolineEnergySourceManager = (GasolineEnergySourceManager)i_Vehicle.m_EnergySourceManager;
             eFuelType fuelType = gasolineEnergySourceManager.fuelType;
             return fuelType.ToString();
         }*/

        private float chargeBatteryToVehicle(Vehicle i_Vehicle, float i_AmountOfEnergyToAdd)
        {
            ElectricEnergySourceManager electricEnergySourceManagar = (ElectricEnergySourceManager)i_Vehicle.m_EnergySourceManager;
            float hoursCharged = electricEnergySourceManagar.ChargeBatteryUntillFullOrHoursToAdd(i_AmountOfEnergyToAdd);

            return hoursCharged;
        }
        private float addFuelToVehicle(Vehicle i_Vehicle, float i_AmountOfLittersToFill, eFuelType fuelType)
        {
            GasolineEnergySourceManager gasolineEnergySourceManagar = (GasolineEnergySourceManager)i_Vehicle.m_EnergySourceManager;
            float littersFilled;

            gasolineEnergySourceManagar.RefuelVehicleUntillFullOrLitersToAdd(i_AmountOfLittersToFill, fuelType, out littersFilled);

            return littersFilled;
        }
        public bool validatePlateNumber(string i_plateNumber)
        {
            bool isValidatePlateNumber = true;
            if (i_plateNumber.Length != 11)
            {
                isValidatePlateNumber = false;
            }

            for (int i = 0; i < i_plateNumber.Length; i++)
            {
                if (i == 3 || i == 6)
                {
                    if (i_plateNumber[i] != '-')
                    {
                        isValidatePlateNumber = false;
                    }
                }
                else
                {
                    if (!char.IsDigit(i_plateNumber[i]))
                    {
                        isValidatePlateNumber = false;
                    }
                }
            }

            return isValidatePlateNumber;
        }

    }
}

