using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Приложение_по_физре
{
    class Point
    {
        public int Age { get; set; }
        public int Weight { get; set; }
        public int SystemPressure { get; set; }
        public int PulseAtRest { get; set; }
        public int OverallEndurance { get; set; }
        public int HeartRateRecovery { get; set; }
        public int Flexibility { set; get; }
        public int Speed { get; set; }
        public int DynamicForce { get; set; }
        public int SpeedEndurance { get; set; }
        public int SpeedAndStrengthEndurance { get; set; }
        public Point() { }
        public int Sum()
        {
            return Age + Weight + SystemPressure + PulseAtRest + OverallEndurance + HeartRateRecovery + Flexibility + Speed + DynamicForce + SpeedEndurance + SpeedAndStrengthEndurance;
        }
    }
}
