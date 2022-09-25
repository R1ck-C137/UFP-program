using System;
using UFP_program.CastomClass;

namespace UFP_program
{
    public class Calculation_ForMen : Calculation
    {
        public Calculation_ForMen(Person person, Point point)
        {
            this.person = person;
            this.point = point;
            AgeToCount = CalcAgeToCount(person.Age);
        }

        public double[,] TableOfNorms_ForMen =
        {                                    //Возраст
            { 9, 13, 57, 18, 23, 3000, 7  },    //19
            { 9, 13, 56, 18, 22, 2900, 7.1},    //20
            { 9, 14, 55, 17, 22, 2800, 7.2},    //21
            { 9, 14, 53, 17, 21, 2750, 7.3},    //22
            { 8, 14, 52, 17, 21, 2700, 7.4},    //23
            { 8, 15, 51, 16, 20, 2650, 7.5},    //24
            { 8, 15, 50, 16, 20, 2600, 8  },    //25
            { 8, 15, 49, 16, 20, 2550, 8.1},    //26
            { 8, 16, 48, 15, 19, 2500, 8.2},    //27
            { 8, 16, 47, 15, 19, 2450, 8.27},   //28
            { 7, 16, 46, 15, 19, 2400, 8.37}    //29
        };
        
        public double WeightNorm(int Height, int Age)
        {
            if ((50 + (Height - 150) * 0.75 + ((Age - 21) / 4)) >= 0)
                return (double)(50 + (Height - 150) * 0.75 + ((Age - 21) / 4));
            else
                return 0;
        }

        public double NormaSistDavleniya(int Age, int Weight)
        {
            return (double)(109 + 0.5 * Age + 0.1 * Weight);
        }

        public double NormaDiastDavleniya(int Age, int Weight)
        {
            return (double)(74 + 0.1 * Age + 0.15 * Weight);
        }

        public void Сalculation()
        {
            Age();
            //--------------------------------------
            Weight();
            //--------------------------------------
            SystemPressure();
            //--------------------------------------
            PulseAtRest();
            //--------------------------------------
            if (person.Sport == true)
                OverallEndurance_Сross();
            else
                OverallEndurance_NumberOfTrainingSessions();
            //--------------------------------------
            HeartRateRecovery();
            //--------------------------------------
            Flexibility();
            //--------------------------------------
            Speed();
            //--------------------------------------
            DynamicForce();
            //--------------------------------------
            SpeedEndurance();
            //--------------------------------------
            SpeedAndStrengthEndurance();
            //--------------------------------------
            CalculationFinalScore();
        }
        
        private void Weight()
        {
            double weightNorm = WeightNorm((int)person.Height, (int)person.Age);
            if (person.Weight - weightNorm < 1)
            {
                point.Weight = 30;
            }
            else
            {
                if ((person.Weight - weightNorm) > 30 || weightNorm == 0)
                {
                    point.Weight = 0;
                }
                else
                {
                    point.Weight = (int)(30 - (person.Weight - weightNorm));
                }
            }
        }

        private void SystemPressure()
        {
            point.SystemPressure = 30;
            if (person.SystolicPressure - NormaSistDavleniya((int)person.Age, (int)person.Weight) > 0)
            {
                point.SystemPressure = (int)(point.SystemPressure - Math.Truncate(((double)person.SystolicPressure - NormaSistDavleniya((int)person.Age, (int)person.Weight)) / 5));
            }
            if (person.DiastolicPressure - NormaDiastDavleniya((int)person.Age, (int)person.Weight) > 0)
            {
                point.SystemPressure = (int)(point.SystemPressure - Math.Truncate(((double)person.DiastolicPressure - NormaDiastDavleniya((int)person.Age, (int)person.Weight)) / 5));
            }
        }
        
        private void OverallEndurance_Сross()
        {
            point.OverallEndurance = 30;
            point.OverallEndurance = (int)(point.OverallEndurance - Math.Truncate((TableOfNorms_ForMen[AgeToCount, 5] - (double)person.OverallEndurance) / 50) * 5);
        }
        
        private void Flexibility()
        {
            point.Flexibility = (int)(person.Flexibility - TableOfNorms_ForMen[AgeToCount, 0]);
            if (point.Flexibility < 0) { point.Flexibility = 0; }
        }

        private void Speed()
        {
            point.Speed = (int)(TableOfNorms_ForMen[AgeToCount, 1] - Convert.ToDouble(person.Speed)) * 2;
            if (point.Speed < 0) { point.Speed = 0; }
        }

        private void DynamicForce()
        {
            if ((person.DynamicForce - TableOfNorms_ForMen[AgeToCount, 2]) == 0)
            {
                point.DynamicForce = 2;
            }
            if ((person.DynamicForce - TableOfNorms_ForMen[AgeToCount, 2]) > 0)
            {
                point.DynamicForce = (int)(2 + (person.DynamicForce - TableOfNorms_ForMen[AgeToCount, 2]) * 2);
            }
            if (person.DynamicForce - TableOfNorms_ForMen[AgeToCount, 2] < 0) { point.DynamicForce = 0; }
        }

        private void SpeedEndurance()
        {
            if (person.SpeedEndurance - TableOfNorms_ForMen[AgeToCount, 3] >= 0)
            {
                point.SpeedEndurance = (int)((person.SpeedEndurance - (TableOfNorms_ForMen[AgeToCount, 3] - 1)) * 3);
            }
            if (person.SpeedEndurance - TableOfNorms_ForMen[AgeToCount, 3] < 0) { point.SpeedEndurance = 0; }
        }

        private void SpeedAndStrengthEndurance()
        {
            if (person.SpeedAndStrengthEndurance - TableOfNorms_ForMen[AgeToCount, 4] >= 0)
            {
                point.SpeedAndStrengthEndurance = (int)((person.SpeedAndStrengthEndurance - (TableOfNorms_ForMen[AgeToCount, 4] - 1)) * 4);
            }
            if (person.SpeedAndStrengthEndurance - TableOfNorms_ForMen[AgeToCount, 4] < 0) { point.SpeedAndStrengthEndurance = 0; }
        }
    }
}
