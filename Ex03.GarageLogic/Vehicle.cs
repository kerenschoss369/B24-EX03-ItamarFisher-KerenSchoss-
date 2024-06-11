using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal class Vehicle
    {
        string modelName;
        string licenseNumber;
        float percentageOfEnergyLeft;
        List<Wheel> WheelsList;
        EnergySource energySource;
    }
}
