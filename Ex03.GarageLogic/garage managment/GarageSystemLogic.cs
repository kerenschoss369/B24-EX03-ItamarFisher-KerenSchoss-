using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using static Ex03.GarageLogic.Car;
using static Ex03.GarageLogic.GarageOpenIssue;
using static Ex03.GarageLogic.GasolineEnergySourceManager;
using static Ex03.GarageLogic.Motorcycle;
using static Ex03.GarageLogic.VehicleFactory;

namespace Ex03.GarageLogic
{
    public class GarageSystemLogic
    {
        GarageSystemLogic m_GarageSystemLogic;
        List<GarageOpenIssue> m_GarageOpenIssues = new List<GarageOpenIssue>();
        List<Vehicle> m_VehicleList = new List<Vehicle>();
        VehicleFactory garageSystemFactory = new VehicleFactory();


        public bool setAdditionalInfoParams(ref Vehicle i_vehicle, List<Tuple<string, object>> o_additionalInfoParams )
        {
            bool isValidCarDetails=true;
            i_vehicle.setBaseAdditionalInformationFromList(o_additionalInfoParams,out isValidCarDetails);
            if (isValidCarDetails)
            {
                isValidCarDetails = i_vehicle.setAdditionalInformationFromList(o_additionalInfoParams);
            }
            return isValidCarDetails;
        }
        public void CreateNewVehicleAndAddToVehicleList(VehicleFactory.eVehicleType i_VehicleType, string i_VehiclePlateNumber)
        {
            Vehicle vehicle = garageSystemFactory.CreateVehicle(i_VehicleType);
            vehicle.plateNumber = i_VehiclePlateNumber;
            m_VehicleList.Add(vehicle);

        }
        public void GetAdditionalInfo(eVehicleType i_VehicleType, out List<Tuple<string, object>> o_AdditionalInfoTuplesList)
        {
            o_AdditionalInfoTuplesList = garageSystemFactory.GetAdditionalInfo(i_VehicleType);
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
            if (!vehicleExists)
            {
                throw new ArgumentException("This is no open issue with plate number: " + i_PlateNumber);
            }

            return vehicleExists;
        }
        public bool CheckIfVehicleIsGasPowered(Vehicle i_Vehicle)
        {
            bool isGasPowered = true;
            if (!(i_Vehicle.m_EnergySourceManager is GasolineEnergySourceManager))
            {
                isGasPowered = false;
                throw new ArgumentException(i_Vehicle.plateNumber + " is electric powered.");
            }
            return isGasPowered;
        }
        public bool CheckIfVehicleIsElectricPowered(Vehicle i_Vehicle)
        {
            bool isElectricPowered = true;
            if (!(i_Vehicle.m_EnergySourceManager is ElectricEnergySourceManager))
            {
                isElectricPowered = false;
                throw new ArgumentException(i_Vehicle.plateNumber + " is gas powered.");
            }
            return isElectricPowered;
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
                wheel.tryIncreaseWheelAirPressure(amountOfAirToFill);
            }
        }
        public float chargeBatteryToVehicle(Vehicle i_Vehicle, float i_AmountOfEnergyToAdd)
        {
            ElectricEnergySourceManager electricEnergySourceManagar = (ElectricEnergySourceManager)i_Vehicle.m_EnergySourceManager;
            float hoursCharged = electricEnergySourceManagar.ChargeBatteryUntillFullOrHoursToAdd(i_AmountOfEnergyToAdd);
            i_Vehicle.percentageOfEnergyLeft = (float)(i_Vehicle.m_EnergySourceManager.currentEnergySourceAmount / i_Vehicle.m_EnergySourceManager.maxEnergySourceAmount);
            i_Vehicle.percentageOfEnergyLeft = i_Vehicle.percentageOfEnergyLeft * 100;

            return hoursCharged;
        }
        public float addFuelToVehicle(Vehicle i_Vehicle, float i_AmountOfLittersToFill, GasolineEnergySourceManager.eFuelType fuelType)
        {
            GasolineEnergySourceManager gasolineEnergySourceManagar = (GasolineEnergySourceManager)i_Vehicle.m_EnergySourceManager;
            float littersFilled;

            gasolineEnergySourceManagar.RefuelVehicleUntillFullOrLitersToAdd(i_AmountOfLittersToFill, fuelType, out littersFilled);
            i_Vehicle.percentageOfEnergyLeft = (float)(i_Vehicle.m_EnergySourceManager.currentEnergySourceAmount / i_Vehicle.m_EnergySourceManager.maxEnergySourceAmount);
            i_Vehicle.percentageOfEnergyLeft = i_Vehicle.percentageOfEnergyLeft * 100;
            return littersFilled;
        }
        public bool SetWheelManufacturerAndWheelAirPressure(ref Wheel io_Wheel, string i_ManufacturerName, float i_WheelCurrentAirPressure)
        {
            bool isValidCurrentAirPressure = isValidCurrentAirPressure = io_Wheel.tryIncreaseWheelAirPressure(i_WheelCurrentAirPressure);
            if (isValidCurrentAirPressure == true)
            {
                io_Wheel.manufacturerName = i_ManufacturerName;
            }

            return isValidCurrentAirPressure;
        }
        public bool ValidatePhoneNumber(string i_PhoneNumber)
        {
            bool isValidatePhoneNumber = true;
            if (i_PhoneNumber.Length != 11)
            {
                isValidatePhoneNumber = false;
            }

            for (int i = 0; i < i_PhoneNumber.Length; i++)
            {
                if (i == 0)
                {
                    if (i_PhoneNumber[i] != '0')
                        isValidatePhoneNumber = false;
                }
                if (i == 1)
                {
                    if (i_PhoneNumber[i] != '5')
                        isValidatePhoneNumber = false;
                }
                if (i == 3)
                {
                    if (i_PhoneNumber[i] != '-')
                    {
                        isValidatePhoneNumber = false;
                    }
                }
                else
                {
                    if (!char.IsDigit(i_PhoneNumber[i]))
                    {
                        isValidatePhoneNumber = false;
                    }
                }
            }

            return isValidatePhoneNumber;
        }
        public bool validatePlateNumber(string i_PlateNumber)
        {
            bool isValidatePlateNumber = true;
            if (i_PlateNumber.Length != 10)
            {
                isValidatePlateNumber = false;
            }

            for (int i = 0; i < i_PlateNumber.Length; i++)
            {
                if (i == 3 || i == 6)
                {
                    if (i_PlateNumber[i] != '-')
                    {
                        isValidatePlateNumber = false;
                    }
                }
                else
                {
                    if (!char.IsDigit(i_PlateNumber[i]))
                    {
                        isValidatePlateNumber = false;
                    }
                }
            }

            return isValidatePlateNumber;
        }

        public bool IsAirPressureLowerOrEqualToMaxAirPressure(float i_AirPressure, Wheel i_CheckedWheel)
        {
            bool isAirPressureLowerOrEqualToMax = true;

            if (!(i_AirPressure <= i_CheckedWheel.maxAirPressureDefinedByManufacturer))
            {
                isAirPressureLowerOrEqualToMax=false;
                throw new ValueOutOfRangeException(i_AirPressure, 0f, i_CheckedWheel.maxAirPressureDefinedByManufacturer);
            }

            return isAirPressureLowerOrEqualToMax;
        }

        public bool IsEnergyAmountLowerOrEqualToMaxEnergyAmount(float i_EnergyAmount, Vehicle i_VehicleToUpdate)
        {
            return (i_EnergyAmount <= i_VehicleToUpdate.energySourceManager.maxEnergySourceAmount);
        }
        public bool isValidCarDoorsAmountAndConvertToECarDoorsAmount(string i_CarDoorsAmountFromUser, out eCarDoorsAmount o_CarDoorsAmount)
        {
            return Enum.TryParse(i_CarDoorsAmountFromUser, true, out o_CarDoorsAmount) && Enum.IsDefined(typeof(eCarDoorsAmount), o_CarDoorsAmount);
        }
        public bool isValidCarColorAndConvertToECarColor(string i_CarColorFromUser, out eCarColor o_CarColor)
        {
            return Enum.TryParse(i_CarColorFromUser, true, out o_CarColor) && Enum.IsDefined(typeof(eCarColor), o_CarColor);
        }
        public bool isValidVehicleTypeAndConvertToEVehicleType(string i_VehicleTypeFromUser, out eVehicleType o_VehicleType)
        {
            bool isValidVehicleType = false;
            if ((Enum.TryParse(i_VehicleTypeFromUser, true, out o_VehicleType)) && (Enum.IsDefined(typeof(eVehicleType), o_VehicleType)))
            {
                isValidVehicleType = true;
            }
            else
            {
                throw new FormatException("Couldnt Parse the input: [" + i_VehicleTypeFromUser + "] to eVehicleType");
            }
            return isValidVehicleType;
        }
        public bool isValidLicenseTypeAndConvertToELicenseType(string i_LicenseTypeFromUser, out eLicenseType o_LicenseType)
        {
            return Enum.TryParse(i_LicenseTypeFromUser, true, out o_LicenseType) && Enum.IsDefined(typeof(eLicenseType), o_LicenseType);
        }
        public bool isValidVehicleStateAndConvertToEVehicleState(string i_VehicleStateFromUser, out GarageOpenIssue.eVehicleState o_VehicleState)
        {
            bool isValidVehicleState = false;
            if (Enum.TryParse(i_VehicleStateFromUser, true, out o_VehicleState) && Enum.IsDefined(typeof(GarageOpenIssue.eVehicleState), o_VehicleState))
            {
                isValidVehicleState = true;
            }
            else
            {
                throw new FormatException("Couldnt Parse the input: [" + i_VehicleStateFromUser + "] to eVehicleState");
            }
            return isValidVehicleState;
        }

        public bool CheckIfValidFilter(string i_StateChosenByUser, out int i_StateChosenByUserIntegered, int i_Min, int i_Max)
        {
            bool isValidInput = false;

            if (int.TryParse(i_StateChosenByUser, out i_StateChosenByUserIntegered))
            {
                if (((i_StateChosenByUserIntegered >= i_Min) && (i_StateChosenByUserIntegered <= i_Max)))
                {
                    isValidInput = true;
                }
                else
                {
                    throw new ValueOutOfRangeException((float)i_StateChosenByUserIntegered, (float)i_Min, (float)i_Max);
                }
            }
            else
            {
                throw new ArgumentException("Choose a value that is a state from the enum or 'all'.");
            }

            return isValidInput;

        }

        public bool isValidFuelTypeAndConvertToEVehicleType(string i_FuelTypeFromUser, out GasolineEnergySourceManager.eFuelType o_FuelType)
        {
            bool isValidFuelType = false;
            if (Enum.TryParse<eFuelType>(i_FuelTypeFromUser, true, out o_FuelType))
            {
                isValidFuelType = true;
            }
            else
            {
                throw new FormatException("This is not a type of fuel.");
            }
            return isValidFuelType;
        }

        public bool isFuelTypeCorrectForCar(GasolineEnergySourceManager.eFuelType i_FuelType, Vehicle i_VehicleToRefuel)
        {
            bool isValidFuelType = true;
            if (i_FuelType != ((GasolineEnergySourceManager)i_VehicleToRefuel.energySourceManager).fuelType)
            {
                isValidFuelType = false;
                throw new ArgumentException("This is not the correct fuel for the vehicle.");
            }
            return isValidFuelType;
        }

        public bool isLittersToAddCorrectForCar(float i_LittersToAdd, Vehicle i_VehicleToRefuel)
        {
            bool isValidLittersToAdd = true;
            if ((i_LittersToAdd + i_VehicleToRefuel.energySourceManager.currentEnergySourceAmount) > i_VehicleToRefuel.energySourceManager.maxEnergySourceAmount)
            {
                isValidLittersToAdd = false;
                throw new ValueOutOfRangeException(i_LittersToAdd, 0f, (i_VehicleToRefuel.energySourceManager.maxEnergySourceAmount - i_VehicleToRefuel.energySourceManager.currentEnergySourceAmount));
            }
            return isValidLittersToAdd;
        }


    }
}

