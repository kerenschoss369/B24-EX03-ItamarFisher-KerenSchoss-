﻿using System;
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

        public void CreateNewVehicleAndAddToVehicleList(VehicleFactory.eVehicleType i_VehicleType, string i_VehiclePlateNumber)
        {
            Vehicle vehicle = garageSystemFactory.CreateVehicle(i_VehicleType);
            vehicle.plateNumber = i_VehiclePlateNumber;
            m_VehicleList.Add(vehicle);

        }
        public void AddNewOpenIssue(string i_OwnerName, string i_OwnerPhoneNumber, GarageOpenIssue.eVehicleState i_VehicleState, string i_VehiclePlateNumber)
        {
            GarageOpenIssue issue = new GarageOpenIssue(i_OwnerName, i_OwnerPhoneNumber, i_VehicleState, i_VehiclePlateNumber);
            m_GarageOpenIssues.Add(issue);

        }

        public void SetCarInputParameters(Car io_Car, Car.eCarColor i_CarColor, Car.eCarDoorsAmount i_CarDoorsAmount)
        {
            io_Car.carColor = i_CarColor;
            io_Car.carDoorsAmount = i_CarDoorsAmount;
        }

        public void SetMotorcycleInputParameters(Motorcycle io_Motorcycle, Motorcycle.eLicenseType i_LicenseType, int i_EngineDisplacementCc)
        {
            io_Motorcycle.licenseType = i_LicenseType;
            io_Motorcycle.engineDisplacementInCc = i_EngineDisplacementCc;
        }

        public void SetTruckcycleInputParameters(Truck io_Truck, float i_CargoVolume, bool i_IsCarryingHazardousMaterials)
        {
            io_Truck.cargoVolume = i_CargoVolume;
            io_Truck.isCarryingHazardousMaterials = i_IsCarryingHazardousMaterials;
        }
        public List<String> FilterVehiclesPlateNumbersByRequestedState(GarageOpenIssue.eVehicleState i_VehicleStateFilter, bool i_FetchAllVehicles)
        {
            List<String> filteredPlateNumberList = new List<String>();

            foreach (GarageOpenIssue openIssue in m_GarageOpenIssues)
            {

                if (i_FetchAllVehicles == true)
                {
                    filteredPlateNumberList.Add(openIssue.vehiclePlateNumber);
                }
                else
                {
                    if (openIssue.vehicleState == i_VehicleStateFilter)
                    {
                        filteredPlateNumberList.Add(openIssue.vehiclePlateNumber);
                    }
                }
            }

            return filteredPlateNumberList;
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
        public bool getOpenIssueUsingPlateNumberIfExist(string i_PlateNumber, out GarageOpenIssue o_GarageOpenIssue)
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
        public void changeVehicleState(GarageOpenIssue i_OpenIssue, GarageOpenIssue.eVehicleState i_NewVehicleState)
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

