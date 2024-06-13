using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class VehicleFactory
    {
        public enum eVehicleType
        {
            GasolineMotorcycle,
            ElectricMotorcycle,
            GasolineCar,
            ElectricCar,
            Truck
        }
        public Vehicle CreateVehicle(eVehicleType i_VehicleType)
        {
            Vehicle vehicle = null;
            GasolineEnergySourceManager gasolineEnergySourceManager = null;
            ElectricEnergySourceManager electricEnergySourceManager = null;

            switch (i_VehicleType)
            {
                case eVehicleType.GasolineMotorcycle:
                    vehicle = new Motorcycle(gasolineEnergySourceManager, (float)5.5, GasolineEnergySourceManager.eFuelType.Octan98);
                    break;
                case eVehicleType.ElectricMotorcycle:
                    vehicle = new Motorcycle(electricEnergySourceManager, (float)2.5);
                    break;
                case eVehicleType.GasolineCar:
                    vehicle = new Car(gasolineEnergySourceManager, (float)45, GasolineEnergySourceManager.eFuelType.Octan95);
                    break;
                case eVehicleType.ElectricCar:
                    vehicle = new Car(electricEnergySourceManager, (float)3.5);
                    break;
                case eVehicleType.Truck:
                    vehicle = new Truck();
                    break;
                default://execption shel keren ;d:D:D:D:D:D:D:D:D
                    throw new ArgumentException("Unknown vehicle type");
    }

            return vehicle;
        }
    }
}