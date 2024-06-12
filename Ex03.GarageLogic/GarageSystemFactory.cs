using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static Ex03.GarageLogic.GarageSystemFactory;

namespace Ex03.GarageLogic
{
    public class GarageSystemFactory
    {
        private List<Vehicle> m_VehiclesList;
       
       
        public enum eVehicleType
        {
            Motorcycle ,
            ElectricMotorcycle,
            GasolineCar,
            ElectricCar,
            Truck
        }
        public static Vehicle CreateVehicle(eVehicleType i_VehicleType)
        {
            Vehicle vehicle = null;

            switch (i_VehicleType)
            {
                case eVehicleType.Motorcycle:
                    vehicle = new Motorcycle();
                    break;
                case eVehicleType.ElectricMotorcycle:
                    vehicle = new ElectricMotorcycle();
                    break;
                case eVehicleType.GasolineCar:
                    vehicle = new GasolineCar();
                    break;
                case eVehicleType.ElectricCar:
                    vehicle = new ElectricCar();
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

}
