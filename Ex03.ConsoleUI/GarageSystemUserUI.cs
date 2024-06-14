using Ex03.GarageLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using static Ex03.ConsoleUI.GarageSystemUserUI;
using static Ex03.GarageLogic.GarageOpenIssue;

namespace Ex03.ConsoleUI
{
    internal class GarageSystemUserUI
    {
        public enum eGarageMenuOptions
        {
            AddNewVehicle,
            DisplayAllPlateNumbersAndFilter,
            ChangeVehicleStatus,
            InflateVehicleTires,
            RefuelGasVehicle,
            ChargeElectricVehicle,
            DisplayVehicleDetails
        }

        private Ex03.GarageLogic.GarageSystemLogic m_systemLogic;
        public void printMenu()
        {
            int userChoiceOfAction;

            Console.Write("Please select an action:\n" +
                "1. Add a new vehicle to the garage\n" +
                "2. Display list of license numbers of vehicles in the garage and filter them by condition\n" +
                "3. Change the status of a vehicle in the garage\n" +
                "4. fill tires of vehicle in the garage\n" +
                "5. Refuel a gasoline-powered vehicle in the garage\n" +
                "6. Charge an electric vehicle in the garage\n" +
                "7. Display vehicle details by license number\n\n" +
                "Type your selection here: ");

            while (!int.TryParse(Console.ReadLine(), out userChoiceOfAction) || (userChoiceOfAction < 1) || (userChoiceOfAction > 7)) //FIX INVALID VALUES MAYBE WITH ERRORS
            {
                Console.Write("Invalid input. Please make sure that you enter a number between 1 and 7.");
            }

            switch ((eGarageMenuOptions)userChoiceOfAction)
            {
                case eGarageMenuOptions.AddNewVehicle:
                    //addNewVehicle();
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
            }
        }
        private string getPlateNumberFromUserAndTheMatchingVehicle(out Vehicle o_WantedVehicle)
        {
            bool isValidPlateNumber = false;
            bool isPlateNumberBelongsToExistingCar = false;
            string plateNumber = "";
            o_WantedVehicle = null;

            while (!isPlateNumberBelongsToExistingCar)
            {
                do
                {
                    Console.Write("Please enter plate number in the requested format (xxx-xx-xxx), should contain numbers only. \nYour input:");
                    plateNumber = Console.ReadLine();
                    if (m_systemLogic.validatePlateNumber(plateNumber))
                    {
                        isValidPlateNumber = true;
                    }
                    else
                    {
                        Console.WriteLine("Invalidplate number. Please try again.");
                    }
                }
                while (!isValidPlateNumber);

                if (m_systemLogic.getVehicleUsingPlateNumberIfExist(plateNumber, out o_WantedVehicle))
                {
                    isPlateNumberBelongsToExistingCar = true;
                }
            }

            return plateNumber;
        }

        private GasolineEnergySourceManager.eFuelType getFuelTypeFromUser()
        {
            string fuelTypeInput;
            GasolineEnergySourceManager.eFuelType fuelType;
            bool isValidFuelType = false;

            do
            {
                Console.Write("Please enter the fuel type (Soler, Octan95, Octan96, Octan98): ");
                fuelTypeInput = Console.ReadLine();

                if (Enum.TryParse(fuelTypeInput, true, out fuelType) && Enum.IsDefined(typeof(GasolineEnergySourceManager.eFuelType), fuelType))
                {
                    isValidFuelType = true;
                }
                else
                {
                    Console.WriteLine("Invalid fuel type. Please try again.");
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
            Vehicle vehicleToRefuel;
            string plateNumber;
            GasolineEnergySourceManager.eFuelType fuelType;
            float litersToAdd;

            plateNumber = getPlateNumberFromUserAndTheMatchingVehicle(out vehicleToRefuel);
            fuelType = getFuelTypeFromUser();
            litersToAdd = getEnergyAmountToAddFromUser();
            if (m_systemLogic.CheckIfVehicleIsGasPowered(vehicleToRefuel))
            {
                m_systemLogic.addFuelToVehicle(vehicleToRefuel, litersToAdd, fuelType);
            }
            else
            {
                Console.WriteLine("Cannot refuel an electric powerd car.");
            }
        }
        private void chargeElectricVehicle()
        {
            Vehicle vehicleToCharge;
            string plateNumber;
            float hoursToAdd;

            plateNumber = getPlateNumberFromUserAndTheMatchingVehicle(out vehicleToCharge);
            hoursToAdd = getEnergyAmountToAddFromUser();
            if (m_systemLogic.CheckIfVehicleIsGasPowered(vehicleToCharge))
            {
                Console.WriteLine("Cannot refuel a gasoline powerd car.");
            }
            else
            {
                m_systemLogic.chargeBatteryToVehicle(vehicleToCharge, hoursToAdd);
            }
        }

        private GarageOpenIssue.eVehicleState getStateToChangeToFromUser()
        {
            GarageOpenIssue.eVehicleState stateToChangeTo;
            bool isValidState = false;

            do
            {
                Console.Write("Please enter the state to change to (InMaintenance, Fixed, PaidFor): ");
                string input = Console.ReadLine();

                if (Enum.TryParse(input, true, out stateToChangeTo) && Enum.IsDefined(typeof(GarageOpenIssue.eVehicleState), stateToChangeTo))
                {
                    isValidState = true;

                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid state.");
                }

            }
            while (!isValidState);

            return stateToChangeTo;
        }
        private void changeVehicleStatus()
        {
            Vehicle vehicleToChangeStateTo;
            GarageOpenIssue issueToChangeStateTo;
            string plateNumber;
            GarageOpenIssue.eVehicleState stateToChangeTo;
            bool isPlateNumberHasOpenIssue = false;

            plateNumber = getPlateNumberFromUserAndTheMatchingVehicle(out vehicleToChangeStateTo);
            stateToChangeTo = getStateToChangeToFromUser();
            do
            {
                if (m_systemLogic.getOpenIssueUsingPlateNumberIfExist(plateNumber, out issueToChangeStateTo))
                {
                    isPlateNumberHasOpenIssue = true;
                }
                else
                {
                    Console.WriteLine("Sorry, this plate Number does not have an open issue in our garage. \nPlease enter a valid state.");
                    plateNumber = getPlateNumberFromUserAndTheMatchingVehicle(out vehicleToChangeStateTo);
                }
            }
            while (!isPlateNumberHasOpenIssue);

            m_systemLogic.changeVehicleState(issueToChangeStateTo, stateToChangeTo);
        }

        private void inflateVehicleTires()
        {
            Vehicle vehicleToFlateTiresTo;
            string plateNumber;

            plateNumber = getPlateNumberFromUserAndTheMatchingVehicle(out vehicleToFlateTiresTo);
            m_systemLogic.FillAllWheelsAirPressureToMax(vehicleToFlateTiresTo);

        }

        private void displayAllPlateNumbersAndFilter()
        {
            string stateChosenByUser;
            eVehicleState stateToFilterBy;
            List<string> vehicleToPrint;
            bool fetchAllVehicles = true;

            do
            {
                Console.Write("Please select the requested state to filter the plate numbers by,\n"
                + "to view all the existing vehicles in the garage Please type ''all''\n" +
                "Your Input: ");
                stateChosenByUser = Console.ReadLine();
            }
            while ((stateChosenByUser == "all") || (Enum.TryParse(stateChosenByUser, true, out stateToFilterBy) && Enum.IsDefined(typeof(eVehicleState), stateToFilterBy)));

            if (stateChosenByUser != "all")
            {
                fetchAllVehicles = false;
            }
            vehicleToPrint = m_systemLogic.FilterVehiclesPlateNumbersByRequestedState(stateToFilterBy, !fetchAllVehicles);

            printPlateNumbersList(vehicleToPrint);
        }

        private void printPlateNumbersList(List<string> i_VehicleToPrint)
        {
            if (i_VehicleToPrint == null || i_VehicleToPrint.Count == 0)
            {
                Console.WriteLine("No vehicles to display.");
            }
            else
            {
                Console.WriteLine("List of plate numbers:");
                foreach (string plateNumber in i_VehicleToPrint)
                {
                    Console.WriteLine(plateNumber);
                }
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
            stateToChangeTo = getStateToChangeToFromUser();
            do
            {
                if (m_systemLogic.getOpenIssueUsingPlateNumberIfExist(plateNumber, out openIssueToDisplayDetailsOf))
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
            Console.WriteLine(string.Format("Plate Number: {0}", i_VehicleToDisplayDetailsOf.plateNumber));
            Console.WriteLine(string.Format("Model Name: {0}", i_VehicleToDisplayDetailsOf.modelName));
            Console.WriteLine(string.Format("Owner Name: {0}", i_OpenIssueToDisplayDetailsOf.vehicleOwnerName));
            Console.WriteLine(string.Format("Owner Phone Number: {0}", i_OpenIssueToDisplayDetailsOf.vehiclePhoneNumber));
            Console.WriteLine(string.Format("Energy Percentage Left: {0}%", i_VehicleToDisplayDetailsOf.percentageOfEnergyLeft));

            if (i_VehicleToDisplayDetailsOf is Car car)
            {
                Console.WriteLine(string.Format("Car Color: {0}", car.carColor));
                Console.WriteLine(string.Format("Number of Doors: {0}", car.carDoorsAmount));
            }
            else if (i_VehicleToDisplayDetailsOf is Motorcycle motorcycle)
            {
                Console.WriteLine(string.Format("License Type: {0}", motorcycle.licenseType));
                Console.WriteLine(string.Format("Engine Displacement (cc): {0}", motorcycle.engineDisplacementInCc));
            }
            else if (i_VehicleToDisplayDetailsOf is Truck truck)
            {
                Console.WriteLine(string.Format("Cargo Volume: {0}", truck.cargoVolume));
                Console.WriteLine(string.Format("Carrying Hazardous Materials: {0}", truck.isCarryingHazardousMaterials));
            }

            Console.WriteLine("Wheels Info:");
            foreach (var wheel in i_VehicleToDisplayDetailsOf.wheelsList)
            {
                Console.WriteLine(string.Format("Manufacturer: {0}, Current Air Pressure: {1}, Max Air Pressure: {2}", wheel.manufacturerName, wheel.currentAirPressure, wheel.maxAirPressureDefinedByManufacturer));
            }

        }
    }
}

