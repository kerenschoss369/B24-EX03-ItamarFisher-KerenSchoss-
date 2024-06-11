using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public enum eCarColor
    {
        Yellow,
        White,
        Red,
        Black
    }

    public enum eCarDoorsAmount
    {
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5
    }

    internal class Car
    {
        private readonly eCarColor r_CarColor;
        private readonly eCarDoorsAmount r_CarDoorsAmount;
    }
}
