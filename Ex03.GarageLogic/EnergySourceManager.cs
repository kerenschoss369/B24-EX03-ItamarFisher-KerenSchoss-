using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Ex03.GarageLogic.Car;
using static Ex03.GarageLogic.Motorcycle;

namespace Ex03.GarageLogic
{
    public abstract class EnergySourceManager
    {
        protected float m_CurrentEnergySourceAmount = 0;
        protected readonly float r_MaxEnergySourceAmount;

        protected EnergySourceManager(float i_MaxEnergySourceAmount, ref List<Tuple<string, object>> o_AdditionalVehicleInformation)
        {
            r_MaxEnergySourceAmount = i_MaxEnergySourceAmount;
            Tuple<string, object> currentEnergySourceAmount = new Tuple<string, object>("Current Energy Source Amount", m_CurrentEnergySourceAmount);
            o_AdditionalVehicleInformation.Add(currentEnergySourceAmount);
        }
        public void SetAdditionalInformationFromList(List<Tuple<string, object>> i_AdditionalVehicleInformation)
        {
            foreach (Tuple<string, object> tuple in i_AdditionalVehicleInformation)
            {
                if (tuple.Item1 == "Current Energy Source Amount")
                {
                    float.TryParse((string)tuple.Item2, out m_CurrentEnergySourceAmount);
                }
            }
        }

        public float currentEnergySourceAmount
        {
            get
            {
                return m_CurrentEnergySourceAmount;
            }
            set
            {
                m_CurrentEnergySourceAmount = value;
            }
        }
        public float maxEnergySourceAmount
        {
            get
            {
                return r_MaxEnergySourceAmount;
            }
        }

    }
}
