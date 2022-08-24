﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Приложение_по_физре
{
    public class Calculation_ForMen
    {
        public Calculation_ForMen()
        {
            AgeToCount = CalcAgeToCount();
        }

        App app = (App)System.Windows.Application.Current;

        public static List<Results.GridClass> GridList = new List<Results.GridClass>();

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

        public int CalcAgeToCount()
        {
            int AgeToCount;
            AgeToCount = Convert.ToInt32(app.person.Age);

            if (app.person.Age < 19)
            {
                AgeToCount = 19;
            }
            if (app.person.Age > 29)
            {
                AgeToCount = 29;
            }
            AgeToCount -= 19;
            return AgeToCount;
        }

        public int AgeToCount;

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
            Weight();
            //--------------------------------------
            SystemPressure();
            //--------------------------------------
            PulseAtRest();
            //--------------------------------------
            if (app.person.Sport == true)
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
            double weightNorm = WeightNorm((int)app.person.Height, (int)app.person.Age);
            if (app.person.Weight - weightNorm < 1)
            {
                app.point.Weight = 30;
            }
            else
            {
                if ((app.person.Weight - weightNorm) > 30 || weightNorm == 0)
                {
                    app.point.Weight = 0;
                }
                else
                {
                    app.point.Weight = (int)(30 - (app.person.Weight - weightNorm));
                }
            }
        }

        public void SystemPressure()
        {
            app.point.SystemPressure = 30;
            if (app.person.SystolicPressure - NormaSistDavleniya((int)app.person.Age, (int)app.person.Weight) > 0)
            {
                app.point.SystemPressure = (int)(app.point.SystemPressure - Math.Truncate(((double)app.person.SystolicPressure - NormaSistDavleniya((int)app.person.Age, (int)app.person.Weight)) / 5));
            }
            if (app.person.DiastolicPressure - NormaDiastDavleniya((int)app.person.Age, (int)app.person.Weight) > 0)
            {
                app.point.SystemPressure = (int)(app.point.SystemPressure - Math.Truncate(((double)app.person.DiastolicPressure - NormaDiastDavleniya((int)app.person.Age, (int)app.person.Weight)) / 5));
            }
        }

        public void PulseAtRest()
        {
            app.point.PulseAtRest = (int)(90 - app.person.PulseAtRest);
            if (app.point.PulseAtRest < 1) { app.point.PulseAtRest = 0; }
        }

        public void OverallEndurance_NumberOfTrainingSessions()
        {
            app.person.OverallEndurance = (int?)Math.Truncate((double)app.person.OverallEndurance);
            if (app.person.OverallEndurance >= 7) { app.point.OverallEndurance = 30; }
            if (app.person.OverallEndurance == 4) { app.point.OverallEndurance = 25; }
            if (app.person.OverallEndurance == 3) { app.point.OverallEndurance = 20; }
            if (app.person.OverallEndurance == 2) { app.point.OverallEndurance = 10; }
            if (app.person.OverallEndurance == 1) { app.point.OverallEndurance = 5; }
            if (app.person.OverallEndurance < 1) { app.point.OverallEndurance = 0; }
        }

        public void OverallEndurance_Сross()
        {
            app.point.OverallEndurance = 30;
            app.point.OverallEndurance = (int)(app.point.OverallEndurance - Math.Truncate((TableOfNorms_ForMen[CalcAgeToCount(), 5] - (double)app.person.OverallEndurance) / 50) * 5);
        }

        public void HeartRateRecovery()
        {
            if (app.person.PulseAfterExercise >= app.person.PulseAtRest + 20)
            {
                app.point.HeartRateRecovery = -10;
            }
            if (app.person.PulseAfterExercise < app.person.PulseAtRest + 20)
            {
                app.point.HeartRateRecovery = 10;
            }
            if (app.person.PulseAfterExercise < app.person.PulseAtRest + 15)
            {
                app.point.HeartRateRecovery = 20;
            }
            if (app.person.PulseAfterExercise <= app.person.PulseAtRest + 10)     //пульс после == пульс до + 10
            {
                app.point.HeartRateRecovery = 30;
            }
        }

        public void Flexibility()
        {
            app.point.Flexibility = (int)(app.person.Flexibility - TableOfNorms_ForMen[AgeToCount, 0]);
            if (app.point.Flexibility < 0) { app.point.Flexibility = 0; }
        }

        public void Speed()
        {
            app.point.Speed = (int)(TableOfNorms_ForMen[AgeToCount, 1] - Convert.ToDouble(app.person.Speed)) * 2;
            if (app.point.Speed < 0) { app.point.Speed = 0; }
        }

        public void DynamicForce()
        {
            if ((app.person.DynamicForce - TableOfNorms_ForMen[AgeToCount, 2]) == 0)
            {
                app.point.DynamicForce = 2;
            }
            if ((app.person.DynamicForce - TableOfNorms_ForMen[AgeToCount, 2]) > 0)
            {
                app.point.DynamicForce = (int)(2 + (app.person.DynamicForce - TableOfNorms_ForMen[AgeToCount, 2]) * 2);
            }
            if (app.person.DynamicForce - TableOfNorms_ForMen[AgeToCount, 2] < 0) { app.point.DynamicForce = 0; }
        }

        public void SpeedEndurance()
        {
            if (app.person.SpeedEndurance - TableOfNorms_ForMen[AgeToCount, 3] >= 0)
            {
                app.point.SpeedEndurance = (int)((app.person.SpeedEndurance - (TableOfNorms_ForMen[AgeToCount, 3] - 1)) * 3);
            }
            if (app.person.SpeedEndurance - TableOfNorms_ForMen[AgeToCount, 3] < 0) { app.point.SpeedEndurance = 0; }
        }

        public void SpeedAndStrengthEndurance()
        {
            if (app.person.SpeedAndStrengthEndurance - TableOfNorms_ForMen[AgeToCount, 4] >= 0)
            {
                app.point.SpeedAndStrengthEndurance = (int)((app.person.SpeedAndStrengthEndurance - (TableOfNorms_ForMen[AgeToCount, 4] - 1)) * 4);
            }
            if (app.person.SpeedAndStrengthEndurance - TableOfNorms_ForMen[AgeToCount, 4] < 0) { app.point.SpeedAndStrengthEndurance = 0; }
        }

        public void CalculationFinalScore()
        {
            if (app.point.Sum() > 250) { app.TotalScore = "Высокий"; }
            if (app.point.Sum() <= 250) { app.TotalScore = "Выше среднего"; }
            if (app.point.Sum() <= 160) { app.TotalScore = "Средний"; }
            if (app.point.Sum() <= 90) { app.TotalScore = "Ниже среднего"; }
            if (app.point.Sum() < 50) { app.TotalScore = "Низкий"; }
        }
    }
}
