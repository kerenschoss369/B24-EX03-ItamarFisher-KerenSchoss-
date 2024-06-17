using Ex03.GarageLogic;
using System;
using System.Collections.Generic;
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
            DisplayVehicleDetails
        }

        private Ex03.GarageLogic.GarageSystemLogic m_systemLogic = new GarageSystemLogic();
        public void printMenu()
        {
            int userChoiceOfAction;
            while (true)
            {
                Console.Write("\n--------------------------------------------------------------------------\n\n" +
                    "Please select an action:\n\n" +
                    "1. Add a new vehicle to the garage\n" +
                    "2. Display list of license numbers of vehicles in the garage and filter them by condition\n" +
                    "3. Change the status of a vehicle in the garage\n" +
                    "4. fill tires of vehicle in the garage\n" +
                    "5. Refuel a gasoline-powered vehicle in the garage\n" +
                    "6. Charge an electric vehicle in the garage\n" +
                    "7. Display vehicle details by license number\n\n" +
                    "--------------------------------------------------------------------------\n\n" +
                    "Type your selection here: ");

                while (!int.TryParse(Console.ReadLine(), out userChoiceOfAction) || (userChoiceOfAction < 1) || (userChoiceOfAction > 7)) //FIX INVALID VALUES MAYBE WITH ERRORS
                {
                    Console.Write("Invalid input. Please make sure that you enter a number between 1 and 7.");
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
                }
            }
        }
        private void addNewVehicleToTheGarage()
        {
            Vehicle vehicleToAddOrUpdate;
            GarageOpenIssue issueToAddOrUpdate = null;
            string plateNumber;

            plateNumber = getValidatePlateNumberFromUser();
            if (m_systemLogic.getVehicleUsingPlateNumberIfExist(plateNumber, out vehicleToAddOrUpdate))
            {
                Console.WriteLine("Car has been already in the garage. the state will be changed to in maintenance.");
                m_systemLogic.getOpenIssueUsingPlateNumberIfExist(plateNumber, out issueToAddOrUpdate);
                m_systemLogic.changeVehicleState(issueToAddOrUpdate, GarageOpenIssue.eVehicleState.InMaintenance);
            }
            else
            {
                getCarDetailsFromUserAndAddToGarage(plateNumber, vehicleToAddOrUpdate);
                getOpenIssueDetailsFromUserAndAddToGarage(plateNumber);
            }
        }
        private void fillEzPz(ref List<Tuple<string,object>> ezpz)
        {
            List<Tuple<string, object>> newTuplesList = new List <Tuple<string, object>>();
            foreach (Tuple<string,object> tuple in ezpz)
            {
                Console.WriteLine("Please enter: " + tuple.Item1);
                Tuple<string, object> newTuple = new Tuple<string, object>(tuple.Item1, Console.ReadLine());
                newTuplesList.Add(newTuple);
            }
            ezpz.Clear();
            ezpz = newTuplesList;
        }
        private void getCarDetailsFromUserAndAddToGarage(string i_PlateNumber, Vehicle vehicleToAdd)
        {
            eCarColor carColor;
            eCarDoorsAmount doorsAmount;
            eVehicleType vehicleType;
            eLicenseType licenceType;
            string wheelsSetUpOptionInput;
            float energyAmount, existingAirPressure, cargoVolume;
            int engineCapacityInCc;
            bool isCarryingHazardousMaterials;

            Console.WriteLine("\nLet's begin with adding the new car by getting the requested details:");
            vehicleType = getVehicleTypeFromUser();
            m_systemLogic.CreateNewVehicleAndAddToVehicleList(vehicleType, i_PlateNumber);

            VehicleFactory blah = m_systemLogic.GarageSystemFactory; // ssussssyyyyyy bakakakakakakakak
            List<Tuple<string,object>> ezpz = blah.GetAdditionalInfo(eVehicleType.GasolineCar);
            Vehicle vehicle;
            m_systemLogic.getVehicleUsingPlateNumberIfExist("123-12-123", out vehicle);
            Console.WriteLine("here is interesting");
            fillEzPz(ref ezpz);
            //Fill ezpz
            m_systemLogic.setAdditionalInfoParams(ref vehicle, ezpz);
            Console.WriteLine("here is interesting");


            m_systemLogic.getVehicleUsingPlateNumberIfExist(i_PlateNumber, out vehicleToAdd);
            wheelsSetUpOptionInput = getWheelsSetUpOptioneFromUser();
            setWheelsCondition(wheelsSetUpOptionInput, vehicleToAdd);
            setExsitingEnergyAmountFromUserInput(vehicleToAdd);

            switch (vehicleType)
            {
                case eVehicleType.GasolineCar:
                case eVehicleType.ElectricCar:
                    carColor = getCarColorFromUser();
                    doorsAmount = getCarDoorsAmountFromUser();
                    m_systemLogic.SetCarInputParameters((Car)vehicleToAdd, carColor, doorsAmount);
                    break;

                case eVehicleType.GasolineMotorcycle:
                case eVehicleType.ElectricMotorcycle:
                    licenceType = getLicenseTypeFromUser();
                    engineCapacityInCc = getEngineCapacityInCcFromUser();
                    m_systemLogic.SetMotorcycleInputParameters((Motorcycle)vehicleToAdd, licenceType, engineCapacityInCc);
                    break;

                case eVehicleType.Truck:
                    isCarryingHazardousMaterials = getIsCarryingHazardousMaterialsFromUser();
                    cargoVolume = getCargoVolumeFromUser();
                    m_systemLogic.SetTruckcycleInputParameters((Truck)vehicleToAdd, cargoVolume, isCarryingHazardousMaterials);
                    break;
                default:
                    break;
            }

            Console.WriteLine("\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n" +
        "Congratulations! Your car has been added successfully to the garage.\n" +
        "~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
        }
        private void setWheelsCondition(string i_WheelsSetUpOptionInput, Vehicle i_VehicleToAdd)
        {
            int wheelIndex = 1;
            float validateAirPressure;

            if (i_WheelsSetUpOptionInput == "1")
            {
                foreach (Wheel wheel in i_VehicleToAdd.wheelsList)
                {
                    Console.Write("\nwheel no.{0} - ", wheelIndex);
                    setWheelCurrentAirPressure(wheel, out validateAirPressure);
                    wheel.currentAirPressure = validateAirPressure;
                    wheelIndex++;
                }
            }
            else if (i_WheelsSetUpOptionInput == "2")
            {
                setWheelCurrentAirPressure(i_VehicleToAdd.wheelsList[0], out validateAirPressure);
                foreach (Wheel wheel in i_VehicleToAdd.wheelsList)
                {
                    wheel.currentAirPressure = validateAirPressure;
                }
            }
            else
            {
                ;
            }
        }

        private void setWheelCurrentAirPressure(Wheel i_wheel, out float o_ValidateAirPressure)
        {
            float airPressureInput;

            do
            {
                Console.Write("Air pressure: ");
                if(!float.TryParse(Console.ReadLine(), out airPressureInput))
                {
                    Console.WriteLine("Invalid input, input isn't a float");
                }
                else
                {
                    if (airPressureInput > i_wheel.maxAirPressureDefinedByManufacturer)
                    {
                        Console.WriteLine("Invalid input. The pressure should not be higher than the maximum that has been defined by the manufacturer");
                    }
                }
                
            }
            while (airPressureInput > i_wheel.maxAirPressureDefinedByManufacturer);
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
            string existingEnergyAmountFromUser;
            float existingEnergyAmount;

            do
            {
                Console.Write("\nExisting energy amount (fuel amount or hours left in the battery): ");
                if(!float.TryParse(Console.ReadLine(), out existingEnergyAmount))
                {
                    Console.WriteLine("Ivalid input, input isn't a number");
                }
                else
                {
                    if (existingEnergyAmount > i_VehicleToUpdate.energySourceManager.maxEnergySourceAmount)
                    {
                        Console.WriteLine("Invalid input.The existing energy amount should not be higher than the maximum that has been defined by the manufacturer");
                    }
                }
            }
            while (existingEnergyAmount > i_VehicleToUpdate.energySourceManager.maxEnergySourceAmount);

            i_VehicleToUpdate.energySourceManager.currentEnergySourceAmount = existingEnergyAmount;
        }

        private float getCargoVolumeFromUser()
        {
            float cargoVolume;
            string userInput;
            bool isValidInput = false;

            do
            {
                Console.Write("Enter the cargo volume: ");
                userInput = Console.ReadLine();
                if (float.TryParse(userInput, out cargoVolume))
                {
                    isValidInput = true;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid number for the cargo volume.");
                }
            }
            while (!isValidInput);

            return cargoVolume;
        }
        private bool getIsCarryingHazardousMaterialsFromUser()
        {
            string userInput;
            bool isValidInput = false;
            bool isCarryingHazardousMaterials = false;

            do
            {
                Console.Write("Is the vehicle carrying hazardous materials? (y/n): ");
                userInput = Console.ReadLine().Trim().ToLower();
                if (userInput == "yes" || userInput == "y" || userInput == "no" || userInput == "n")
                {
                    isValidInput = true;
                    if (userInput == "yes" || userInput == "y")
                    {
                        isCarryingHazardousMaterials = true;
                    }
                    else
                    {
                        isCarryingHazardousMaterials = false;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter 'y' or 'n' (or 'yes' or 'no').");
                }
            }
            while (!isValidInput);

            return isCarryingHazardousMaterials;
        }

        private int getEngineCapacityInCcFromUser()
        {
            int engineCapacityInCc;
            string engineCapacityFromUser;
            bool isValidInput = false;

            do
            {
                Console.Write("Engine capacity (in cc): ");
                engineCapacityFromUser = Console.ReadLine();
                if (int.TryParse(engineCapacityFromUser, out engineCapacityInCc))
                {
                    isValidInput = true;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid integer for the engine capacity.");
                }
            }
            while (!isValidInput);

            return engineCapacityInCc;
        }
        private eLicenseType getLicenseTypeFromUser()
        {
            eLicenseType licenseType;
            string licenseTypeFromUser;
            do
            {
                Console.Write("\nChoose the license type:\n" +
                    "1. A\n" +
                    "2. A1\n" +
                    "3. AA\n" +
                    "4. B1\n" +
                    "Your choice: ");
                licenseTypeFromUser = Console.ReadLine();
            }
            while (!isValidLicenseTypeAndConvertToELicenseType(licenseTypeFromUser, out licenseType));

            return licenseType;
        }

        private bool isValidLicenseTypeAndConvertToELicenseType(string i_LicenseTypeFromUser, out eLicenseType o_LicenseType)
        {
            return Enum.TryParse(i_LicenseTypeFromUser, true, out o_LicenseType) && Enum.IsDefined(typeof(eLicenseType), o_LicenseType);
        }

        private eCarDoorsAmount getCarDoorsAmountFromUser()
        {
            eCarDoorsAmount carDoorsAmount;
            string carDoorsAmountFromUser;
            do
            {
                Console.Write("\nChoose the car doors amount:\n" +
                    "2. Two\n" +
                    "3. Three\n" +
                    "4. Four\n" +
                    "5. Five\n" +
                    "Your choice: ");
                carDoorsAmountFromUser = Console.ReadLine();
            }
            while (!isValidCarDoorsAmountAndConvertToECarDoorsAmount(carDoorsAmountFromUser, out carDoorsAmount));

            return carDoorsAmount;
        }

        private bool isValidCarDoorsAmountAndConvertToECarDoorsAmount(string i_CarDoorsAmountFromUser, out eCarDoorsAmount o_CarDoorsAmount)
        {
            return Enum.TryParse(i_CarDoorsAmountFromUser, true, out o_CarDoorsAmount) && Enum.IsDefined(typeof(eCarDoorsAmount), o_CarDoorsAmount);
        }
        private eCarColor getCarColorFromUser()
        {
            eCarColor carColor;
            string carColorFromUser;
            do
            {
                Console.Write("\nChoose the car color:\n" +
                    "1. Yellow\n" +
                    "2. White\n" +
                    "3. Red\n" +
                    "4. Black\n" +
                    "Your choise: ");
                carColorFromUser = Console.ReadLine();
            }
            while (!isValidCarColorAndConvertToECarColor(carColorFromUser, out carColor));

            return carColor;
        }

        private bool isValidCarColorAndConvertToECarColor(string i_CarColorFromUser, out eCarColor o_CarColor)
        {
            return Enum.TryParse(i_CarColorFromUser, true, out o_CarColor) && Enum.IsDefined(typeof(eCarColor), o_CarColor);
        }
        private eVehicleType getVehicleTypeFromUser()// not good not generic
        {
            eVehicleType vehicleType;
            string vehicleTypeFromUser;
            do
            {
                Console.Write("\nChoose the vehicle type:\n" +
                    "1. Gasoline motorcycle\n" +
                    "2. Electric motorcycle\n" +
                    "3. Gasoline car\n" +
                    "4.Electric car\n" +
                    "5. Truck\n" +
                    "Your choise: ");
                vehicleTypeFromUser = Console.ReadLine();
            }
            while (!isValidVehicleTypeAndConvertToEVehicleType(vehicleTypeFromUser, out vehicleType));

            return vehicleType;
        }

        private bool isValidVehicleTypeAndConvertToEVehicleType(string i_VehicleTypeFromUser, out eVehicleType o_VehicleType)
        {

            return Enum.TryParse(i_VehicleTypeFromUser, true, out o_VehicleType) && Enum.IsDefined(typeof(eVehicleType), o_VehicleType);
        }

        private void getOpenIssueDetailsFromUserAndAddToGarage(string i_PlateNumber)
        {
            string ownerName, ownerPhoneNumber;
            Console.WriteLine("\nPlease enter the owner details that we will be able to notify you whenever your car will be fixed!");
            Console.Write("Owner's full name: ");
            ownerName = Console.ReadLine();
            ownerPhoneNumber = getValidatePhoneNumberFromUser();
            m_systemLogic.AddNewOpenIssue(ownerName, ownerPhoneNumber, GarageOpenIssue.eVehicleState.InMaintenance, i_PlateNumber);
            Console.WriteLine("\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n" +
                "Congratulations! Your personal details has been added successfully.\n" +
                "~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
        }
        private string getValidatePhoneNumberFromUser()// not good
        {
            string phoneNumber;
            do
            {
                Console.Write("Owner's phone number (format should be XXX-XXXXXXX): ");
                phoneNumber = Console.ReadLine();
            }
            while (!(m_systemLogic.ValidatePhoneNumber(phoneNumber)));

            return phoneNumber;
        }
        private string getValidatePlateNumberFromUser()
        {
            bool isValidPlateNumber = false;
            string plateNumber = "";

            do
            {
                Console.Write("\nPlease enter plate number in the requested format (xxx-xx-xxx), should contain numbers only. \nYour input:");
                plateNumber = Console.ReadLine();
                if (m_systemLogic.validatePlateNumber(plateNumber))
                {
                    isValidPlateNumber = true;
                }
                else
                {
                    Console.WriteLine("Invalid plate number. Please try again.");
                }
            }
            while (!isValidPlateNumber);

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
                Console.WriteLine("Cannot refuel an electric powered car.");
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
                Console.WriteLine("Cannot refuel a gasoline powered car.");
            }
            else
            {
                m_systemLogic.chargeBatteryToVehicle(vehicleToCharge, hoursToAdd);
            }
        }

        private GarageOpenIssue.eVehicleState getStateToChangeToFromUser()
        {
            GarageOpenIssue.eVehicleState stateToChangeTo = eVehicleState.Fixed;
            bool isValidState = false;

            do
            {
                Console.WriteLine("Please select the state to change to:");
                foreach (eVehicleState state in Enum.GetValues(typeof(eVehicleState)))
                {
                    Console.WriteLine($"Press {(int)state} for {state.ToString()}");
                }

                string input = Console.ReadLine();
                if (eVehicleState.TryParse(input, out stateToChangeTo))
                {

                    isValidState = true;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid number corresponding to the state.");
                }
            }
            while (!isValidState);

            Console.WriteLine($"State changed to: {stateToChangeTo}");

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
            Console.WriteLine("Current vehicle state is: " + issueToChangeStateTo.vehicleState);
            stateToChangeTo = getStateToChangeToFromUser();
            m_systemLogic.changeVehicleState(issueToChangeStateTo, stateToChangeTo);
        }

        private void inflateVehicleTires()
        {
            Vehicle vehicleToFlateTiresTo;
            string plateNumber;

            plateNumber = getPlateNumberFromUserAndTheMatchingVehicle(out vehicleToFlateTiresTo);
            m_systemLogic.FillAllWheelsAirPressureToMax(vehicleToFlateTiresTo);
            Console.WriteLine("The air pressure in your wheels got filled to the max");

        }

        private void displayAllPlateNumbersAndFilter()
        {
            int stateChosenByUserIntegered = -1;
            string stateChosenByUser;
            eVehicleState stateToFilterBy;
            List<string> vehicleToPrint;
            bool fetchAllVehicles = true;

            do
            {
                foreach (eVehicleState state in Enum.GetValues(typeof(eVehicleState)))
                {
                    Console.WriteLine($"Press {(int)state} for {state.ToString()}");
                }

                Console.WriteLine($"Press {Enum.GetValues(typeof(eVehicleState)).Length} for all");
                stateChosenByUser = Console.ReadLine();
            }
            while ((!int.TryParse(stateChosenByUser, out stateChosenByUserIntegered)) && (stateChosenByUserIntegered >= 0 &&
            stateChosenByUserIntegered <= Enum.GetValues(typeof(eVehicleState)).Length));

            if (stateChosenByUserIntegered == Enum.GetValues(typeof(eVehicleState)).Length)
            {
                fetchAllVehicles = false;
                stateToFilterBy = eVehicleState.InMaintenance;
            }
            else
            {
                eVehicleState.TryParse(stateChosenByUser, out stateToFilterBy);
            }
            vehicleToPrint = m_systemLogic.FilterVehiclesPlateNumbersByRequestedState(stateToFilterBy, !fetchAllVehicles);

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
                    "List of plate numbers:\n"+
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
