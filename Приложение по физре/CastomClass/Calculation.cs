using System;

namespace UFP_program.CastomClass
{
    public abstract class Calculation
    {
        protected Person person;
        public Point point;
        public int AgeToCount { get; set; }

        public int CalcAgeToCount(int? age)
        {
            int AgeToCount;
            AgeToCount = Convert.ToInt32(age);

            if (age < 19)
            {
                AgeToCount = 19;
            }
            if (age > 29)
            {
                AgeToCount = 29;
            }
            AgeToCount -= 19;
            return AgeToCount;
        }

        protected void Age()
        {
            point.Age = (int)person.Age;
        }

        protected void PulseAtRest()
        {
            point.PulseAtRest = (int)(90 - person.PulseAtRest);
            if (point.PulseAtRest < 1) { point.PulseAtRest = 0; }
        }

        public void OverallEndurance_NumberOfTrainingSessions()
        {
            person.OverallEndurance = (int?)Math.Truncate((double)person.OverallEndurance);
            if (person.OverallEndurance >= 7) { point.OverallEndurance = 30; }
            if (person.OverallEndurance == 4) { point.OverallEndurance = 25; }
            if (person.OverallEndurance == 3) { point.OverallEndurance = 20; }
            if (person.OverallEndurance == 2) { point.OverallEndurance = 10; }
            if (person.OverallEndurance == 1) { point.OverallEndurance = 5; }
            if (person.OverallEndurance < 1) { point.OverallEndurance = 0; }
        }

        protected void HeartRateRecovery()
        {
            if (person.PulseAfterExercise >= person.PulseAtRest + 20)
            {
                point.HeartRateRecovery = -10;
            }
            if (person.PulseAfterExercise < person.PulseAtRest + 20)
            {
                point.HeartRateRecovery = 10;
            }
            if (person.PulseAfterExercise < person.PulseAtRest + 15)
            {
                point.HeartRateRecovery = 20;
            }
            if (person.PulseAfterExercise <= person.PulseAtRest + 10)     //пульс после == пульс до + 10
            {
                point.HeartRateRecovery = 30;
            }
        }

        protected void CalculationFinalScore()
        {
            if (point.Sum() > 250) { point.TotalScore = "Высокий"; }
            if (point.Sum() <= 250) { point.TotalScore = "Выше среднего"; }
            if (point.Sum() <= 160) { point.TotalScore = "Средний"; }
            if (point.Sum() <= 90) { point.TotalScore = "Ниже среднего"; }
            if (point.Sum() < 50) { point.TotalScore = "Низкий"; }
        }
    }
}
