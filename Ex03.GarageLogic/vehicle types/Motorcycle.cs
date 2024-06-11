using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public enum eLicenseType
    {
        A,
        A1,
        AA,
        B1
    }


    internal class Motorcycle
    {
        private readonly eLicenseType r_LicenseType;
        private readonly int r_EngineDisplacementInCc;
    }
}
