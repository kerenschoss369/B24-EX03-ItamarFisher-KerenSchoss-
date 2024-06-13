using Ex03.GarageLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using static Ex03.ConsoleUI.GarageSystemUserUI;

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
                    //displayAllPlateNumbersAndFilter();
                    break;
                case eGarageMenuOptions.ChangeVehicleStatus:
                    //changeVehicleStatus();
                    break;
                case eGarageMenuOptions.InflateVehicleTires:
                    //inflateVehicleTires();
                    break;
                case eGarageMenuOptions.RefuelGasVehicle:
                    refuelGasVehicle();
                    break;
                case eGarageMenuOptions.ChargeElectricVehicle:
                    chargeElectricVehicle();
                    break;
                case eGarageMenuOptions.DisplayVehicleDetails:
                    //displayVehicleDetails();
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
                    isPlateNumberBelongsToExistingCar= true;
                }
            }

            return plateNumber;
        }

        private eFuelType getFuelTypeFromUser()
        {
            string fuelTypeInput;
            eFuelType fuelType;
            bool isValidFuelType = false;

            do
            {
                Console.Write("Please enter the fuel type (Soler, Octan95, Octan96, Octan98): ");
                fuelTypeInput = Console.ReadLine();

                if (Enum.TryParse(fuelTypeInput, true, out fuelType) && Enum.IsDefined(typeof(eFuelType), fuelType))
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
            string licenseNumber;
            eFuelType fuelType;
            float litersToAdd;

            licenseNumber = getPlateNumberFromUserAndTheMatchingVehicle(out vehicleToRefuel);
            fuelType = getFuelTypeFromUser();
            litersToAdd = getEnergyAmountToAddFromUser();
            if (m_systemLogic.checkIfVehicleIsGasPowered(vehicleToRefuel))
            {
                m_systemLogic.addFuelToVehicle(vehicleToRefuel,litersToAdd,fuelType);
            }
            else
            {
                Console.WriteLine("Cannot refuel an electric powerd car.");
            }
        }
        private void chargeElectricVehicle()
        {
            Vehicle vehicleToCharge;
            string licenseNumber;
            float hoursToAdd;

            licenseNumber = getPlateNumberFromUserAndTheMatchingVehicle(out vehicleToCharge);
            hoursToAdd = getEnergyAmountToAddFromUser();
            if (m_systemLogic.checkIfVehicleIsGasPowered(vehicleToCharge))
            {
                Console.WriteLine("Cannot refuel a gasoline powerd car.");
            }
            else
            {
                m_systemLogic.chargeBatteryToVehicle(vehicleToCharge, hoursToAdd);
            }
        }

    }
}
