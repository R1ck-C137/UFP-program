using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Приложение_по_физре
{
    public class Person
    {
        public string FIO { get; set; }
        public string Group { get; set; }
        public bool? Gender { get; set; }
        public int? Age { get; set; }
        public int? Weight { get; set; }
        public int? Height { get; set; }
        public int? PulseAtRest { get; set; }
        public int? PulseAfterExercise { get; set; }
        public int? SystolicPressure { get; set; }
        public int? DiastolicPressure { get; set; }
        public int? Flexibility { set; get; }
        public int? Speed { get; set; }
        public int? DynamicForce { get; set; }
        public int? OverallEndurance { get; set; }
        public bool? Sport { get; set; }
        public int? SpeedEndurance { get; set; }
        public int? SpeedAndStrengthEndurance { get; set; }

        public Person() { }

        public void Clear()
        {
            FIO = null;
            Group = null;
            Gender = null;
            Age = null;
            Weight = null;
            Height = null;
            PulseAtRest = null;
            PulseAfterExercise = null;
            SystolicPressure = null;
            DiastolicPressure = null;
            Flexibility = null;
            Speed = null;
            DynamicForce = null;
            OverallEndurance = null;
            Sport = null;
            SpeedEndurance = null;
            SpeedAndStrengthEndurance = null;
        }

        public bool CheckingTheFullness()
        {
            if (FIO == null || Group == null || Gender == null || Age == null || Weight == null || Height == null || PulseAtRest == null
                || PulseAfterExercise == null || SystolicPressure == null || DiastolicPressure == null || Flexibility == null || Speed == null
                || DynamicForce == null || OverallEndurance == null || Sport == null || SpeedEndurance == null || SpeedAndStrengthEndurance == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
