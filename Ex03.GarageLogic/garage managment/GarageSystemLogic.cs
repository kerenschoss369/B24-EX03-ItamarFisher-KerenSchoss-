using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using static Ex03.GarageLogic.Car;
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

        public VehicleFactory GarageSystemFactory// sussy baka
        {
            get { return garageSystemFactory; }
        }
        public void setAdditionalInfoParams(ref Vehicle i_vehicle, List<Tuple<string,object>> o_additionalInfoParams)
        {
            i_vehicle.setBaseAdditionalInformationFromList(o_additionalInfoParams);
            i_vehicle.setAdditionalInformationFromList(o_additionalInfoParams);
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
                wheel.tryIncreaseWheelAirPressure(amountOfAirToFill);
            }
        }
        public float chargeBatteryToVehicle(Vehicle i_Vehicle, float i_AmountOfEnergyToAdd)
        {
            ElectricEnergySourceManager electricEnergySourceManagar = (ElectricEnergySourceManager)i_Vehicle.m_EnergySourceManager;
            float hoursCharged = electricEnergySourceManagar.ChargeBatteryUntillFullOrHoursToAdd(i_AmountOfEnergyToAdd);
            i_Vehicle.percentageOfEnergyLeft = (float)(i_Vehicle.m_EnergySourceManager.maxEnergySourceAmount / i_Vehicle.m_EnergySourceManager.currentEnergySourceAmount);
            i_Vehicle.percentageOfEnergyLeft = i_Vehicle.percentageOfEnergyLeft * 100;

            return hoursCharged;
        }
        public float addFuelToVehicle(Vehicle i_Vehicle, float i_AmountOfLittersToFill, GasolineEnergySourceManager.eFuelType fuelType)
        {
            GasolineEnergySourceManager gasolineEnergySourceManagar = (GasolineEnergySourceManager)i_Vehicle.m_EnergySourceManager;
            float littersFilled;

            gasolineEnergySourceManagar.RefuelVehicleUntillFullOrLitersToAdd(i_AmountOfLittersToFill, fuelType, out littersFilled);

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
                if (i==0)
                {
                    if (i_PhoneNumber[i]!= '0')
                        isValidatePhoneNumber=false;
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
            return (i_AirPressure <= i_CheckedWheel.maxAirPressureDefinedByManufacturer);
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
            return Enum.TryParse(i_VehicleTypeFromUser, true, out o_VehicleType) && Enum.IsDefined(typeof(eVehicleType), o_VehicleType);
        }
        public bool isValidLicenseTypeAndConvertToELicenseType(string i_LicenseTypeFromUser, out eLicenseType o_LicenseType)
        {
            return Enum.TryParse(i_LicenseTypeFromUser, true, out o_LicenseType) && Enum.IsDefined(typeof(eLicenseType), o_LicenseType);
        }

    }
}

