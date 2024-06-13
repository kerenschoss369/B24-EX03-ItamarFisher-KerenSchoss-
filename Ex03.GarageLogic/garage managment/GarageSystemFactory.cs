using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public static class VehicleFactory
    {
        public enum eVehicleType
        {
            GasolineMotorcycle,
            ElectricMotorcycle,
            GasolineCar,
            ElectricCar,
            Truck
        }

        public static Vehicle CreateVehicle(eVehicleType i_VehicleType)
        {
            Vehicle vehicle = null;
            GasolineEnergySourceManager gasolineEnergySourceManager = null;
            ElectricEnergySourceManager electricEnergySourceManager = null;

            switch (i_VehicleType)
            {
                case eVehicleType.GasolineMotorcycle:
                    vehicle = new Motorcycle(gasolineEnergySourceManager, (float)5.5, eFuelType.Octan98);
                    break;
                case eVehicleType.ElectricMotorcycle:
                    vehicle = new Motorcycle(electricEnergySourceManager, (float)2.5, eFuelType.Octan96);
                    break;
                case eVehicleType.GasolineCar:
                    vehicle = new Car(gasolineEnergySourceManager, (float)45, eFuelType.Octan95);
                    break;
                case eVehicleType.ElectricCar:
                    vehicle = new Car(electricEnergySourceManager, (float)3.5, eFuelType.Octan95);
                    break;
                case eVehicleType.Truck:
                    vehicle = new Truck();
                    break;
                default:
                    throw new ArgumentException("Unknown vehicle type");
            }

            return vehicle;
        }
    }
}