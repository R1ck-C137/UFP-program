using System;
using UFP_program.CastomClass;

namespace UFP_program
{
    public class Calculation_ForWomen : Calculation
    {
        public Calculation_ForWomen(Person person, Point point)
        {
            this.person = person;
            this.point = point;
            AgeToCount = CalcAgeToCount(person.Age);
        }

        public double[,] TableOfNorms_ForWomen =
        {                                    //Возраст
            { 10, 15, 41, 15, 21, 2065, 8.43},  //19
            { 10, 15, 40, 15, 20, 2010, 8.55},  //20
            { 10, 16, 39, 14, 20, 1960, 9.1 },  //21
            { 10, 16, 38, 14, 19, 1920, 9.23},  //22
            { 9, 16, 37, 14, 19, 1875, 9.36 },  //23
            { 9, 17, 37, 13, 18, 1840, 9.48 },  //24
            { 9, 17, 36, 13, 18, 1800, 10   },  //25
            { 9, 18, 35, 13, 18, 1765, 10.12},  //26
            { 9, 18, 35, 12, 17, 1730, 10.35},  //27
            { 8, 18, 34, 12, 17, 1700, 10.35},  //28
            { 8, 18, 33, 12, 17, 1670, 10.47}   //29
        };
        
        public double WeightNorm(int Height, int Age)
        {
            if ((50 + (Height - 150) * 0.32 + (Age - 21 / 5)) >= 0)
                return (double)(50 + (Height - 150) * 0.32 + (Age - 21 / 5));
            return 0;
        }

        public double NormaSistDavleniya(int Age, int Weight)
        {
            return (double)(102 + 0.7 * Age + 0.15 * Weight);
        }

        public double NormaDiastDavleniya(int Age, int Weight)
        {
            return (double)(78 + 0.17 * Age + 0.1 * Weight);
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
        
        public void Weight()
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

        public void SystemPressure()
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
        
        public void OverallEndurance_Сross()
        {
            point.OverallEndurance = 30;
            point.OverallEndurance = (int)(point.OverallEndurance - Math.Truncate((TableOfNorms_ForWomen[AgeToCount, 5] - (double)person.OverallEndurance) / 50) * 5);
        }

        public void Flexibility()
        {
            point.Flexibility = (int)(person.Flexibility - TableOfNorms_ForWomen[AgeToCount, 0]);
            if (point.Flexibility < 0) { point.Flexibility = 0; }
        }

        public void Speed()
        {
            point.Speed = (int)(TableOfNorms_ForWomen[AgeToCount, 1] - Convert.ToDouble(person.Speed)) * 2;
            if (point.Speed < 0) { point.Speed = 0; }
        }

        public void DynamicForce()
        {
            if ((person.DynamicForce - TableOfNorms_ForWomen[AgeToCount, 2]) == 0)
            {
                point.DynamicForce = 2;
            }
            if ((person.DynamicForce - TableOfNorms_ForWomen[AgeToCount, 2]) > 0)
            {
                point.DynamicForce = (int)(2 + (person.DynamicForce - TableOfNorms_ForWomen[AgeToCount, 2]) * 2);
            }
            if (person.DynamicForce - TableOfNorms_ForWomen[AgeToCount, 2] < 0) { point.DynamicForce = 0; }
        }

        public void SpeedEndurance()
        {
            if (person.SpeedEndurance - TableOfNorms_ForWomen[AgeToCount, 3] >= 0)
            {
                point.SpeedEndurance = (int)((person.SpeedEndurance - (TableOfNorms_ForWomen[AgeToCount, 3] - 1)) * 3);
            }
            if (person.SpeedEndurance - TableOfNorms_ForWomen[AgeToCount, 3] < 0) { point.SpeedEndurance = 0; }
        }

        public void SpeedAndStrengthEndurance()
        {
            if (person.SpeedAndStrengthEndurance - TableOfNorms_ForWomen[AgeToCount, 4] >= 0)
            {
                point.SpeedAndStrengthEndurance = (int)((person.SpeedAndStrengthEndurance - (TableOfNorms_ForWomen[AgeToCount, 4] - 1)) * 4);
            }
            if (person.SpeedAndStrengthEndurance - TableOfNorms_ForWomen[AgeToCount, 4] < 0) { point.SpeedAndStrengthEndurance = 0; }
        }
    }
}
