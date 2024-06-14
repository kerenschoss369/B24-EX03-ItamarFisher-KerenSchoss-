using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static Ex03.GarageLogic.VehicleFactory;

namespace Ex03.GarageLogic
{
    public class GarageSystemLogic
    {
        GarageSystemLogic m_GarageSystemLogic;
        List<GarageOpenIssue> m_GarageOpenIssues = new List<GarageOpenIssue>();
        List<Vehicle> m_VehicleList = new List<Vehicle>();
        VehicleFactory garageSystemFactory = new VehicleFactory();

        public void CreateNewVehicle(VehicleFactory.eVehicleType i_VehicleType)
        {
            garageSystemFactory.CreateVehicle(i_VehicleType);
        }
        public void Set
        public List<Vehicle> FilterAndPrintVehiclesPlateNumbers(GarageOpenIssue.eVehicleState i_VehicleStateFilter, bool i_FetchAllVehicles)
        {
            List<Vehicle> filteredVehicleList = new List<Vehicle>();
            Vehicle vehicleToAddToList;

            if (i_FetchAllVehicles == true)
            {
                filteredVehicleList = m_VehicleList;
            }
            else
            {
                foreach (GarageOpenIssue openIssue in m_GarageOpenIssues)
                {
                    if (openIssue.vehicleState == i_VehicleStateFilter)
                    {
                        getVehicleUsingPlateNumberIfExist(openIssue.vehiclePlateNumber, out vehicleToAddToList);
                        filteredVehicleList.Add(vehicleToAddToList);
                    }
                }
            }

            return filteredVehicleList;
        }
    
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
        private bool getOpenIssueUsingPlateNumberIfExist(string i_PlateNumber, out GarageOpenIssue o_GarageOpenIssue)
        {
            o_GarageOpenIssue = null;
            bool vehicleExists = false;
            foreach (GarageOpenIssue openIssue in m_GarageOpenIssues)
            {
                if (i_PlateNumber.Equals(openIssue.vehiclePlateNumber))
                {
                    o_GarageOpenIssue = openIssue;
                    vehicleExists = true;
                }
            }

            return vehicleExists;
        }
        public bool CheckIfVehicleIsGasPowered(Vehicle i_Vehicle)
        {

            return (i_Vehicle.m_EnergySourceManager is GasolineEnergySourceManager);
        }

        /* private string getFuelTypeForGasVehicleAsString(Vehicle i_Vehicle) // prob not needed
         {
             GasolineEnergySourceManager gasolineEnergySourceManager = (GasolineEnergySourceManager)i_Vehicle.m_EnergySourceManager;
             eFuelType fuelType = gasolineEnergySourceManager.fuelType;
             return fuelType.ToString();
         }*/
        private void changeVehicleState(GarageOpenIssue i_OpenIssue, GarageOpenIssue.eVehicleState i_NewVehicleState)
        {
            i_OpenIssue.vehicleState = i_NewVehicleState;
        }
        public void FillAllWheelsAirPressureToMax(Vehicle i_Vehicle)
        {
            float amountOfAirToFill;
            foreach (Wheel wheel in i_Vehicle.m_WheelsList)
            {
                amountOfAirToFill = wheel.maxAirPressureDefinedByManufacturer - wheel.currentAirPressure;
                wheel.tryInsreaseWheelAirPressure(amountOfAirToFill);
            }
        }
        public float chargeBatteryToVehicle(Vehicle i_Vehicle, float i_AmountOfEnergyToAdd)
        {
            ElectricEnergySourceManager electricEnergySourceManagar = (ElectricEnergySourceManager)i_Vehicle.m_EnergySourceManager;
            float hoursCharged = electricEnergySourceManagar.ChargeBatteryUntillFullOrHoursToAdd(i_AmountOfEnergyToAdd);

            return hoursCharged;
        }
        public float addFuelToVehicle(Vehicle i_Vehicle, float i_AmountOfLittersToFill, GasolineEnergySourceManager.eFuelType fuelType)
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

