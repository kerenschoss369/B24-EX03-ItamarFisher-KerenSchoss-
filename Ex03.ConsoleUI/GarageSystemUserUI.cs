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

                    break;
                case eGarageMenuOptions.DisplayAllPlateNumbersAndFilter:

                    break;
                case eGarageMenuOptions.ChangeVehicleStatus:

                    break;
                case eGarageMenuOptions.InflateVehicleTires:

                    break;
                case eGarageMenuOptions.RefuelGasVehicle:
                    refuelGasVehicle();
                    break;
                case eGarageMenuOptions.ChargeElectricVehicle:
                    break;
                case eGarageMenuOptions.DisplayVehicleDetails:
                    break;
            }
        }
        private string getPlateNumberFromUser()
        {
            string plateNumber;
            Console.Write("Please enter plate number: ");
            plateNumber = Console.ReadLine();
            //check that good input

            return plateNumber;
        }

        private void refuelGasVehicle()
        {
            string licenseNumber = getPlateNumberFromUser();
            string fuelType;
            float litersToAdd;
            bool isGasolineVehicle = true; //change

            // get the car with the licence number 

            if (!isGasolineVehicle)
            {
                //return that cant be fueled
            }
            else
            {
                Console.Write("Please enter fuel type: ");
                fuelType = Console.ReadLine();
                //check that the input and the out from the function is the same else  throw new ArgumentException("Invalid fuel type selected.", nameof(fuelType));
                Console.Write("Please enter amount of litters to add: ");
                litersToAdd = float.Parse(Console.ReadLine()); //maybe add exeption for parse
                //check that good input
                //call the function that fuel
            }
        }

    }
}
