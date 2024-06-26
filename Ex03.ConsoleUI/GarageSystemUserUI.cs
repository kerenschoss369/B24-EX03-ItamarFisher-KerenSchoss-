﻿using Ex03.GarageLogic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using static Ex03.ConsoleUI.GarageSystemUserUI;
using static Ex03.GarageLogic.Car;
using static Ex03.GarageLogic.GarageOpenIssue;
using static Ex03.GarageLogic.GasolineEnergySourceManager;
using static Ex03.GarageLogic.Motorcycle;
using static Ex03.GarageLogic.VehicleFactory;


namespace Ex03.ConsoleUI
{
    internal class GarageSystemUserUI
    {
        public enum eGarageMenuOptions
        {
            AddNewVehicle = 1,
            DisplayAllPlateNumbersAndFilter,
            ChangeVehicleStatus,
            InflateVehicleTires,
            RefuelGasVehicle,
            ChargeElectricVehicle,
            DisplayVehicleDetails,
            Exit
        }

        private Ex03.GarageLogic.GarageSystemLogic m_SystemLogic = new GarageSystemLogic();

        public void printMenuMessage()
        {
            Console.Write("\n--------------------------------------------------------------------------\n\n" +
                    "Please select an action:\n\n" +
                    "1. Add a new vehicle to the garage\n" +
                    "2. Display list of license numbers of vehicles in the garage and filter them by condition\n" +
                    "3. Change the status of a vehicle in the garage\n" +
                    "4. Fill tires of vehicle in the garage\n" +
                    "5. Refuel a gasoline-powered vehicle in the garage\n" +
                    "6. Charge an electric vehicle in the garage\n" +
                    "7. Display vehicle details by license number\n" +
                    "8. Exit\n\n" +
                    "--------------------------------------------------------------------------\n\n" +
                    "Type your selection here: ");
        }
        public void printMenuAndGetSelectionFromUser()
        {
            int userChoiceOfAction;
            while (true)
            {
                printMenuMessage();
                while (!int.TryParse(Console.ReadLine(), out userChoiceOfAction) || (userChoiceOfAction < 1) || (userChoiceOfAction > 8)) //FIX INVALID VALUES MAYBE WITH ERRORS
                {
                    Console.WriteLine("Invalid input. Please make sure that you enter a number between 1 and 8.");
                    printMenuMessage();
                }
                switch ((eGarageMenuOptions)userChoiceOfAction)
                {
                    case eGarageMenuOptions.AddNewVehicle:
                        addNewVehicleToTheGarage();
                        break;
                    case eGarageMenuOptions.DisplayAllPlateNumbersAndFilter:
                        displayAllPlateNumbersAndFilter();
                        break;
                    case eGarageMenuOptions.ChangeVehicleStatus:
                        changeVehicleStatus();
                        break;
                    case eGarageMenuOptions.InflateVehicleTires:
                        inflateVehicleTires();
                        break;
                    case eGarageMenuOptions.RefuelGasVehicle:
                        refuelGasVehicle();
                        break;
                    case eGarageMenuOptions.ChargeElectricVehicle:
                        chargeElectricVehicle();
                        break;
                    case eGarageMenuOptions.DisplayVehicleDetails:
                        displayVehicleDetails();
                        break;
                    case eGarageMenuOptions.Exit:
                        return;
                        break;
                }
            }
        }
        private void addNewVehicleToTheGarage()
        {
            Vehicle vehicleToAddOrUpdate;
            GarageOpenIssue issueToAddOrUpdate = null;
            string plateNumber;
            bool isValidVehicledEntered = true;

            plateNumber = getValidatePlateNumberFromUser();
            if (m_SystemLogic.getVehicleUsingPlateNumberIfExist(plateNumber, out vehicleToAddOrUpdate))
            {
                Console.WriteLine("Car has been already in the garage. the state will be changed to in maintenance.");
                m_SystemLogic.getOpenIssueUsingPlateNumberIfExist(plateNumber, out issueToAddOrUpdate);
                m_SystemLogic.changeVehicleState(issueToAddOrUpdate, GarageOpenIssue.eVehicleState.InMaintenance);
            }
            else
            {
                getCarDetailsFromUserAndAddToGarage(plateNumber, vehicleToAddOrUpdate, out isValidVehicledEntered);
                if (isValidVehicledEntered)
                {
                    getOpenIssueDetailsFromUserAndAddToGarage(plateNumber);
                }
            }
        }
        private void fillAddtionalInfoFromUserInput(ref List<Tuple<string, object>> o_AdditionalInfoList)//maybe in logic??????
        {
            List<Tuple<string, object>> newTuplesList = new List<Tuple<string, object>>();
            foreach (Tuple<string, object> tuple in o_AdditionalInfoList)
            {
                Console.WriteLine("Please enter " + tuple.Item1 + ":");
                Tuple<string, object> newTuple = new Tuple<string, object>(tuple.Item1, Console.ReadLine());
                newTuplesList.Add(newTuple);
            }
            o_AdditionalInfoList.Clear();
            o_AdditionalInfoList = newTuplesList;
        }
        private void getCarDetailsFromUserAndAddToGarage(string i_PlateNumber, Vehicle vehicleToAdd, out bool o_IsValidCarEnterd)
        {
            o_IsValidCarEnterd = false;
            eVehicleType vehicleType;
            string wheelsSetUpOptionInput;
            List<Tuple<string, object>> additionalInfoList;
            bool isValidCarDetails = false, isValidWheelsDetails = false;

            Console.WriteLine("\nLet's begin with adding the new car by getting the requested details:");
            vehicleType = getVehicleTypeFromUser();

            m_SystemLogic.CreateNewVehicleAndAddToVehicleList(vehicleType, i_PlateNumber);
            m_SystemLogic.getVehicleUsingPlateNumberIfExist(i_PlateNumber, out vehicleToAdd);
            m_SystemLogic.GetAdditionalInfo(vehicleType, out additionalInfoList);//what props

            while (!isValidCarDetails)
            {
                try
                {
                    fillAddtionalInfoFromUserInput(ref additionalInfoList);
                    isValidCarDetails = m_SystemLogic.setAdditionalInfoParams(ref vehicleToAdd, additionalInfoList);
                }
                catch (FormatException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            while (!isValidWheelsDetails)
            {
                try
                {
                    wheelsSetUpOptionInput = getWheelsSetUpOptioneFromUser();
                    setWheelsCondition(wheelsSetUpOptionInput, vehicleToAdd);
                    Console.WriteLine("\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n" +
                        "Congratulations! Your car has been added successfully to the garage.\n" +
                        "~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                    isValidWheelsDetails = true;
                    o_IsValidCarEnterd = true;
                }
                catch (ValueOutOfRangeException e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            //here
        }
        private void setWheelsCondition(string i_WheelsSetUpOptionInput, Vehicle i_VehicleToAdd)
        {
            int wheelIndex = 1;
            float validateAirPressure;
            string manufacturerName;

            if (i_WheelsSetUpOptionInput == "1")
            {
                foreach (Wheel wheel in i_VehicleToAdd.wheelsList)
                {
                    Console.Write("\nwheel no.{0} - ", wheelIndex);
                    setWheelCurrentAirPressure(wheel, out validateAirPressure);
                    Console.WriteLine("Please enter wheel's manufacturer");
                    wheel.manufacturerName = Console.ReadLine();
                    wheel.currentAirPressure = validateAirPressure;
                    wheelIndex++;
                }
            }
            else if (i_WheelsSetUpOptionInput == "2")
            {
                setWheelCurrentAirPressure(i_VehicleToAdd.wheelsList[0], out validateAirPressure);
                Console.WriteLine("Please enter wheel's manufacturer");
                manufacturerName = Console.ReadLine();
                foreach (Wheel wheel in i_VehicleToAdd.wheelsList)
                {
                    wheel.currentAirPressure = validateAirPressure;
                    wheel.manufacturerName = manufacturerName;
                }
            }
            else
            {
                ;
            }
        }

        private void setWheelCurrentAirPressure(Wheel i_wheel, out float o_ValidateAirPressure)
        {
            float airPressureInput = 0f;
            bool isValidAirPressure = false;

            while (!isValidAirPressure)
            {
                Console.Write("Air pressure: ");
                if (!float.TryParse(Console.ReadLine(), out airPressureInput))
                {
                    Console.WriteLine("Invalid input, input isn't a float");
                }
                else
                {
                    if (!(m_SystemLogic.IsAirPressureLowerOrEqualToMaxAirPressure(airPressureInput, i_wheel)))
                    {
                        Console.WriteLine("Invalid input. The pressure should not be higher than the maximum that has been defined by the manufacturer");
                    }
                }
                isValidAirPressure = m_SystemLogic.IsAirPressureLowerOrEqualToMaxAirPressure(airPressureInput, i_wheel);
            }
            o_ValidateAirPressure = airPressureInput;
        }

        private string getWheelsSetUpOptioneFromUser()
        {
            string WheelsSetUpOptionInput;
            bool isValidWheelsSetUpOptionInput = false;

            do
            {
                Console.Write("\nWheels set up: \n" +
                "1. Add wheels one by one\n" +
                "2. Add all wheels at once\n" +
                "Your input here:");
                WheelsSetUpOptionInput = Console.ReadLine();
                if (WheelsSetUpOptionInput == "1" || WheelsSetUpOptionInput == "2")
                {
                    isValidWheelsSetUpOptionInput = true;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a '1' or '2'.");
                }
            }
            while (!isValidWheelsSetUpOptionInput);
            return WheelsSetUpOptionInput;
        }
        private void setExsitingEnergyAmountFromUserInput(Vehicle i_VehicleToUpdate)
        {
            float existingEnergyAmount;

            do
            {
                Console.Write("\nExisting energy amount (fuel amount or hours left in the battery): ");
                if (!float.TryParse(Console.ReadLine(), out existingEnergyAmount))
                {
                    Console.WriteLine("Ivalid input, input isn't a number");
                }
                else
                {
                    if (!(m_SystemLogic.IsEnergyAmountLowerOrEqualToMaxEnergyAmount(existingEnergyAmount, i_VehicleToUpdate)))
                    {
                        Console.WriteLine("Invalid input.The existing energy amount should not be higher than the maximum that has been defined by the manufacturer");
                    }
                }
            }
            while (!(m_SystemLogic.IsEnergyAmountLowerOrEqualToMaxEnergyAmount(existingEnergyAmount, i_VehicleToUpdate)));

            i_VehicleToUpdate.energySourceManager.currentEnergySourceAmount = existingEnergyAmount;
        }

        private eVehicleType getVehicleTypeFromUser()
        {
            eVehicleType vehicleType = eVehicleType.ElectricMotorcycle;
            string vehicleTypeFromUser;
            bool isValidVehicleType = false;

            while (!isValidVehicleType)
            {
                try
                {
                    Console.WriteLine("\nChoose the vehicle type:\n");
                    foreach (eVehicleType type in Enum.GetValues(typeof(eVehicleType)))
                    {
                        Console.WriteLine($"{(int)type}. {type.ToString()}");
                    }
                    Console.Write("Your choice: ");
                    vehicleTypeFromUser = Console.ReadLine();
                    isValidVehicleType = (m_SystemLogic.isValidVehicleTypeAndConvertToEVehicleType(vehicleTypeFromUser, out vehicleType));
                }
                catch (FormatException e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            return vehicleType;
        }
        private void getOpenIssueDetailsFromUserAndAddToGarage(string i_PlateNumber)
        {
            string ownerName, ownerPhoneNumber;
            Console.WriteLine("\nPlease enter the owner details that we will be able to notify you whenever your car will be fixed!");
            Console.Write("Owner's full name: ");
            ownerName = Console.ReadLine();
            ownerPhoneNumber = getValidatePhoneNumberFromUser();
            m_SystemLogic.AddNewOpenIssue(ownerName, ownerPhoneNumber, GarageOpenIssue.eVehicleState.InMaintenance, i_PlateNumber);
            Console.WriteLine("\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n" +
                "Congratulations! Your personal details has been added successfully.\n" +
                "~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
        }
        private string getValidatePhoneNumberFromUser()
        {
            string phoneNumber;
            do
            {
                Console.Write("Owner's phone number (format should be XXX-XXXXXXX): ");
                phoneNumber = Console.ReadLine();
            }
            while (!(m_SystemLogic.ValidatePhoneNumber(phoneNumber)));

            return phoneNumber;
        }
        private string getValidatePlateNumberFromUser()
        {
            bool isValidPlateNumber = false;
            string plateNumber = "";

            while (!isValidPlateNumber)
            {

                Console.Write("\nPlease enter plate number in the requested format (xxx-xx-xxx), should contain numbers only. \nYour input:");
                plateNumber = Console.ReadLine();
                if (m_SystemLogic.validatePlateNumber(plateNumber))
                {
                    isValidPlateNumber = true;
                }

                else
                {
                    Console.WriteLine("Invalid plate number. Please try again.");
                }
            }

            return plateNumber;
        }

        private string getPlateNumberFromUserAndTheMatchingVehicle(out Vehicle o_WantedVehicle)
        {
            string plateNumber = "";
            bool isPlateNumberBelongsToExistingCar = false;
            o_WantedVehicle = null;

            while (!isPlateNumberBelongsToExistingCar)
            {
                plateNumber = getValidatePlateNumberFromUser();
                if (m_SystemLogic.getVehicleUsingPlateNumberIfExist(plateNumber, out o_WantedVehicle))
                {
                    isPlateNumberBelongsToExistingCar = true;
                }
            }
            return plateNumber;
        }
        private GasolineEnergySourceManager.eFuelType getFuelTypeFromUser()
        {
            string fuelTypeInput;
            GasolineEnergySourceManager.eFuelType fuelType = GasolineEnergySourceManager.eFuelType.Soler;
            bool isValidFuelType = false;

            do
            {
                try
                {
                    Console.Write("Please enter the fuel type (Soler, Octan95, Octan96, Octan98): ");
                    fuelTypeInput = Console.ReadLine();
                    if (m_SystemLogic.isValidFuelTypeAndConvertToEVehicleType(fuelTypeInput, out fuelType))
                    {
                        isValidFuelType = true;
                    }
                }
                catch (FormatException e)
                {
                    Console.WriteLine(e.Message);
                }

            }
            while (!isValidFuelType);

            return fuelType;
        }
        private float getEnergyAmountToAddFromUser()
        {
            float energyToAdd;
            bool isValidInput = false;

            do
            {
                Console.Write("Please enter amount of energy to add: ");
                string input = Console.ReadLine();

                if (float.TryParse(input, out energyToAdd))
                {
                    isValidInput = true;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid float number.");
                }

            } while (!isValidInput);

            return energyToAdd;
        }
        private void refuelGasVehicle()
        {
            Vehicle vehicleToRefuel = null;
            string plateNumber;
            GasolineEnergySourceManager.eFuelType fuelType = GasolineEnergySourceManager.eFuelType.Octan98;
            float litersToAdd = 0f;
            bool isValidFuelType = false, isValidLittersToAdd = false, isValidVehicleType = false;

            try
            {
                plateNumber = getPlateNumberFromUserAndTheMatchingVehicle(out vehicleToRefuel);
                if (m_SystemLogic.CheckIfVehicleIsGasPowered(vehicleToRefuel))
                {
                    isValidVehicleType = true;
                }
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
            if (isValidVehicleType)
            {
                while (!isValidFuelType)
                {
                    try
                    {
                        fuelType = getFuelTypeFromUser();
                        isValidFuelType = m_SystemLogic.isFuelTypeCorrectForCar(fuelType, vehicleToRefuel);
                    }
                    catch (ArgumentException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                while (!isValidLittersToAdd)
                {
                    try
                    {
                        litersToAdd = getEnergyAmountToAddFromUser();
                        isValidLittersToAdd = m_SystemLogic.isLittersToAddCorrectForCar(litersToAdd, vehicleToRefuel);
                    }
                    catch (ValueOutOfRangeException ex_outOfRange)
                    {
                        Console.WriteLine(ex_outOfRange.Message);
                    }

                }
                m_SystemLogic.addFuelToVehicle(vehicleToRefuel, litersToAdd, fuelType);
            }

        }
        private void chargeElectricVehicle()
        {
            Vehicle vehicleToCharge = null;
            string plateNumber;
            float hoursToAdd = 0;
            bool isValidHoursToAdd = false, isValidVehicleType = false;

            try
            {
                plateNumber = getPlateNumberFromUserAndTheMatchingVehicle(out vehicleToCharge);
                if (m_SystemLogic.CheckIfVehicleIsElectricPowered(vehicleToCharge))
                {
                    isValidVehicleType = true;
                }
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
            if (isValidVehicleType)
            {
                while (!isValidHoursToAdd)
                {
                    try
                    {
                        hoursToAdd = getEnergyAmountToAddFromUser();
                        isValidHoursToAdd = m_SystemLogic.isLittersToAddCorrectForCar(hoursToAdd, vehicleToCharge);
                    }
                    catch (ValueOutOfRangeException ex_outOfRange)
                    {
                        Console.WriteLine(ex_outOfRange.Message);
                    }

                }
                m_SystemLogic.chargeBatteryToVehicle(vehicleToCharge, hoursToAdd);
            }
        }

        private GarageOpenIssue.eVehicleState getStateToChangeToFromUser()
        {
            GarageOpenIssue.eVehicleState stateToChangeTo = eVehicleState.Fixed;
            bool isValidState = false;

            while (!isValidState)
            {
                try
                {
                    Console.WriteLine("Please select the state to change to:");
                    foreach (eVehicleState state in Enum.GetValues(typeof(eVehicleState)))
                    {
                        Console.WriteLine($"Press {(int)state} for {state.ToString()}");
                    }
                    Console.Write("your choise: ");
                    string stateGivenByUser = Console.ReadLine();
                    isValidState = m_SystemLogic.isValidVehicleStateAndConvertToEVehicleState(stateGivenByUser, out stateToChangeTo);
                }
                catch (FormatException e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            Console.WriteLine($"State changed to: {stateToChangeTo}");

            return stateToChangeTo;
        }
        private void changeVehicleStatus()
        {
            Vehicle vehicleToChangeStateTo;
            GarageOpenIssue issueToChangeStateTo = null;
            string plateNumber;
            GarageOpenIssue.eVehicleState stateToChangeTo;
            bool isPlateNumberHasOpenIssue = false;

            //argument ex
            plateNumber = getPlateNumberFromUserAndTheMatchingVehicle(out vehicleToChangeStateTo);

            while (!isPlateNumberHasOpenIssue)
            {
                try
                {
                    isPlateNumberHasOpenIssue = (m_SystemLogic.getOpenIssueUsingPlateNumberIfExist(plateNumber, out issueToChangeStateTo));
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            Console.WriteLine("Current vehicle state is: " + issueToChangeStateTo.vehicleState);
            stateToChangeTo = getStateToChangeToFromUser();
            m_SystemLogic.changeVehicleState(issueToChangeStateTo, stateToChangeTo);

        }

        private void inflateVehicleTires()
        {
            Vehicle vehicleToFlateTiresTo;
            string plateNumber;

            plateNumber = getPlateNumberFromUserAndTheMatchingVehicle(out vehicleToFlateTiresTo);
            m_SystemLogic.FillAllWheelsAirPressureToMax(vehicleToFlateTiresTo);
            Console.WriteLine("The air pressure in your wheels got filled to the max!");

        }

        private void displayAllPlateNumbersAndFilter()
        {
            int stateChosenByUserIntegered = -1;
            string stateChosenByUser = "";
            eVehicleState stateToFilterBy;
            List<string> vehicleToPrint;
            bool notFetchAllVehicles = true;
            bool isValidFilter = false;

            //get a state from the user

            while (!isValidFilter)
            {
                try
                {
                    foreach (eVehicleState state in Enum.GetValues(typeof(eVehicleState)))
                    {
                        Console.Write($"\nPress {(int)state} for {state.ToString()}");
                    }
                    Console.WriteLine($"Press {Enum.GetValues(typeof(eVehicleState)).Length} for all");
                    Console.Write("Your choise: ");
                    stateChosenByUser = Console.ReadLine();
                    isValidFilter = (!m_SystemLogic.CheckIfValidFilter(stateChosenByUser, out stateChosenByUserIntegered, 0, Enum.GetValues(typeof(eVehicleState)).Length));
                }
                catch (ValueOutOfRangeException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (FormatException e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            if (stateChosenByUserIntegered == Enum.GetValues(typeof(eVehicleState)).Length)
            {
                notFetchAllVehicles = false;
                stateToFilterBy = eVehicleState.InMaintenance;
            }
            else
            {
                eVehicleState.TryParse(stateChosenByUser, out stateToFilterBy);
            }
            vehicleToPrint = m_SystemLogic.FilterVehiclesPlateNumbersByRequestedState(stateToFilterBy, !notFetchAllVehicles);

            printPlateNumbersList(vehicleToPrint);
        }

        private void printPlateNumbersList(List<string> i_VehicleToPrint)
        {
            if (i_VehicleToPrint == null || i_VehicleToPrint.Count == 0)
            {
                Console.WriteLine("\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n" +
                    "Sorry! no vehicles to display.\n" +
                    "~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            }
            else
            {
                Console.WriteLine("\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n" +
                    "List of plate numbers:\n" +
                    "----------------------\n");
                foreach (string plateNumber in i_VehicleToPrint)
                {
                    Console.WriteLine("[ {0} ]\n", plateNumber);
                }
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n");
            }
        }

        private void displayVehicleDetails()
        {
            Vehicle vehicleToDisplayDetailsOf;
            GarageOpenIssue openIssueToDisplayDetailsOf;
            string plateNumber;
            GarageOpenIssue.eVehicleState stateToChangeTo;
            bool isPlateNumberHasOpenIssue = false;

            plateNumber = getPlateNumberFromUserAndTheMatchingVehicle(out vehicleToDisplayDetailsOf);

            do
            {
                if (m_SystemLogic.getOpenIssueUsingPlateNumberIfExist(plateNumber, out openIssueToDisplayDetailsOf))
                {
                    isPlateNumberHasOpenIssue = true;
                }
                else
                {
                    Console.WriteLine("Sorry, this plate Number does not have an open issue in our garage. \nPlease enter a valid state.");
                    plateNumber = getPlateNumberFromUserAndTheMatchingVehicle(out vehicleToDisplayDetailsOf);
                }
            }
            while (!isPlateNumberHasOpenIssue);

            printVehicleDetails(vehicleToDisplayDetailsOf, openIssueToDisplayDetailsOf);
        }

        private void printVehicleDetails(Vehicle i_VehicleToDisplayDetailsOf, GarageOpenIssue i_OpenIssueToDisplayDetailsOf)
        {
            Console.WriteLine(i_OpenIssueToDisplayDetailsOf.ToString());
            Console.WriteLine(i_VehicleToDisplayDetailsOf.ToString());
            Console.WriteLine("Wheels Info:");
            foreach (var wheel in i_VehicleToDisplayDetailsOf.wheelsList)
            {
                Console.WriteLine(string.Format("Manufacturer: {0}, Current Air Pressure: {1}, Max Air Pressure: {2}", wheel.manufacturerName, wheel.currentAirPressure, wheel.maxAirPressureDefinedByManufacturer));
            }
        }
    }
}
