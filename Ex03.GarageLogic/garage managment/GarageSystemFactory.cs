﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Ex03.GarageLogic
{
    public class VehicleFactory
    {
        private Dictionary<eVehicleType, List<Tuple<string, object>>> m_VehicleTypeAdditionalNonDefaultParameters =
            new Dictionary<eVehicleType, List<Tuple<string, object>>>();

        public enum eVehicleType
        {
            GasolineMotorcycle=1,
            ElectricMotorcycle,
            GasolineCar,
            ElectricCar,
            Truck
        }
        public List<Tuple<string,object>> GetAdditionalInfo(eVehicleType i_VehicleType)
        {
            List<Tuple<string, object>> additionalInformationList;
             m_VehicleTypeAdditionalNonDefaultParameters.TryGetValue(i_VehicleType, out additionalInformationList);
            return additionalInformationList;
        }
        public Vehicle CreateVehicle(eVehicleType i_VehicleType)
        {
            Vehicle vehicle = null;
            GasolineEnergySourceManager gasolineEnergySourceManager = null;
            ElectricEnergySourceManager electricEnergySourceManager = null;
            List <Tuple<string, object>> additionalVehicleInformation;
            if (!m_VehicleTypeAdditionalNonDefaultParameters.TryGetValue(i_VehicleType, out additionalVehicleInformation))
            {
                additionalVehicleInformation = new List <Tuple<string, object>>();
                m_VehicleTypeAdditionalNonDefaultParameters.Add(i_VehicleType, additionalVehicleInformation);
            }
            switch (i_VehicleType)
            {
                case eVehicleType.GasolineMotorcycle:
                    vehicle = new Motorcycle(gasolineEnergySourceManager, (float)5.5, GasolineEnergySourceManager.eFuelType.Octan98,
                        ref additionalVehicleInformation);
                    break;
                case eVehicleType.ElectricMotorcycle:
                    vehicle = new Motorcycle(electricEnergySourceManager, (float)2.5, ref additionalVehicleInformation);
                    break;
                case eVehicleType.GasolineCar:
                    vehicle = new Car(gasolineEnergySourceManager, (float)45, GasolineEnergySourceManager.eFuelType.Octan95, ref additionalVehicleInformation);
                    break;
                case eVehicleType.ElectricCar:
                    vehicle = new Car(electricEnergySourceManager, (float)3.5, ref additionalVehicleInformation);
                    break;
                case eVehicleType.Truck:
                    vehicle = new Truck(gasolineEnergySourceManager, ref additionalVehicleInformation);
                    break;
                default:
                    throw new ArgumentException("Unknown vehicle type");
    }

            return vehicle;
        }
    }
}