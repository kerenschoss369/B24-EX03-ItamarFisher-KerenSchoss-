﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.ConsoleUI
{
    internal class Program
    {
        public static void Main(string[] args)// The Main method must be static
        {
            GarageSystemUserUI garageSystemUserUI = new GarageSystemUserUI();
            garageSystemUserUI.printMenu();

        }
    }
}
