using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Приложение_по_физре
{
    public class AddInTable
    {
        App app = (App)System.Windows.Application.Current;

        public static void AddInTableValue(ref List<Results.GridClass> GridList, string lineHeader, double? result = null, double? norm = null, double? point = null)
        {
            if (result == null && norm == null && point != null)
                GridList.Add(new Results.GridClass()
                {
                    lineHeader = lineHeader,
                    point = Convert.ToString(point)
                });
            if (result != null && norm == null && point == null)
                GridList.Add(new Results.GridClass()
                {
                    lineHeader = lineHeader,
                    result = Convert.ToString(result)
                });
            if (result == null && norm != null && point == null)
                GridList.Add(new Results.GridClass()
                {
                    lineHeader = lineHeader,
                    norm = Convert.ToString(norm)
                });


            if (result != null && norm != null && point == null)
                GridList.Add(new Results.GridClass()
                {
                    lineHeader = lineHeader,
                    result = Convert.ToString(result),
                    norm = Convert.ToString(norm)
                });
            if (result != null && norm == null && point != null)
                GridList.Add(new Results.GridClass()
                {
                    lineHeader = lineHeader,
                    result = Convert.ToString(result),
                    point = Convert.ToString(point)
                });
            if (result == null && norm != null && point != null)
                GridList.Add(new Results.GridClass()
                {
                    lineHeader = lineHeader,
                    norm = Convert.ToString(norm),
                    point = Convert.ToString(point)
                });


            if (result != null && norm != null && point != null)
                GridList.Add(new Results.GridClass()
                {
                    lineHeader = lineHeader,
                    result = Convert.ToString(result),
                    norm = Convert.ToString(norm),
                    point = Convert.ToString(point)
                });
        }

        public static void AddInTableFinalScore(ref List<Results.GridClass> GridList, string lineHeader, string norm, double point)
        {
            GridList.Add(new Results.GridClass()
            {
                lineHeader = lineHeader,
                norm = Convert.ToString(norm),
                point = Convert.ToString(point)
            });
        }

        public void Men(ref List<Results.GridClass> GridList)
        {
            Calculation_ForMen Calculation_ForMen = new Calculation_ForMen();

            AddInTable.AddInTableValue(ref GridList, "Масса тела", app.person.Weight, Calculation_ForMen.WeightNorm((int)app.person.Height, (int)app.person.Age), app.point.Weight);
            AddInTable.AddInTableValue(ref GridList, "Системное артериальное давление", point: app.point.SystemPressure);
            AddInTable.AddInTableValue(ref GridList, "     Систолическое давление", app.person.SystolicPressure, Calculation_ForMen.NormaSistDavleniya((int)app.person.Age, (int)app.person.Weight));
            AddInTable.AddInTableValue(ref GridList, "     Диастолическое давление", app.person.DiastolicPressure, Calculation_ForMen.NormaDiastDavleniya((int)app.person.Age, (int)app.person.Weight));
            AddInTable.AddInTableValue(ref GridList, "Пульс в покое", app.person.PulseAtRest, 60, app.point.PulseAtRest);
            if (app.person.Sport == true)         //  кросс
            {
                AddInTable.AddInTableValue(ref GridList, "Общая выносливость", app.person.OverallEndurance, Calculation_ForMen.TableOfNorms_ForMen[Calculation_ForMen.AgeToCount, 5], app.point.OverallEndurance);
            }
            else                            //  кол-во тренеровок в неделю
            {
                AddInTable.AddInTableValue(ref GridList, "Общая выносливость", app.person.OverallEndurance, 3, app.point.OverallEndurance);
            }
            AddInTable.AddInTableValue(ref GridList, "Востанавливваемость пульса", app.person.PulseAfterExercise, app.person.PulseAtRest + 10, app.point.HeartRateRecovery);
            AddInTable.AddInTableValue(ref GridList, "Гибкость", app.person.Flexibility, Calculation_ForMen.TableOfNorms_ForMen[Calculation_ForMen.AgeToCount, 0], app.point.Flexibility);
            AddInTable.AddInTableValue(ref GridList, "Быстрота", app.person.Speed, Calculation_ForMen.TableOfNorms_ForMen[Calculation_ForMen.AgeToCount, 1], app.point.Speed);
            AddInTable.AddInTableValue(ref GridList, "Динамическая сила", app.person.DynamicForce, Calculation_ForMen.TableOfNorms_ForMen[Calculation_ForMen.AgeToCount, 2], app.point.DynamicForce);
            AddInTable.AddInTableValue(ref GridList, "Скоростная выносливость", app.person.SpeedEndurance, Calculation_ForMen.TableOfNorms_ForMen[Calculation_ForMen.AgeToCount, 3], app.point.SpeedEndurance);
            AddInTable.AddInTableValue(ref GridList, "Скоростно-силовая выностивость", app.person.SpeedAndStrengthEndurance, Calculation_ForMen.TableOfNorms_ForMen[Calculation_ForMen.AgeToCount, 4], app.point.SpeedAndStrengthEndurance);
            AddInTable.AddInTableFinalScore(ref GridList, "Ваш уровень физического состояния ", app.TotalScore, app.point.Sum());
        }

        public void Women(ref List<Results.GridClass> GridList)
        {
            Calculation_ForWomen Calculation_ForWomen = new Calculation_ForWomen();

            AddInTable.AddInTableValue(ref GridList, "Масса тела", app.person.Weight, Calculation_ForWomen.WeightNorm((int)app.person.Height, (int)app.person.Age), app.point.Weight);
            AddInTable.AddInTableValue(ref GridList, "Системное артериальное давление", point: app.point.SystemPressure);
            AddInTable.AddInTableValue(ref GridList, "     Систолическое давление", app.person.SystolicPressure, Calculation_ForWomen.NormaSistDavleniya((int)app.person.Age, (int)app.person.Weight));
            AddInTable.AddInTableValue(ref GridList, "     Диастолическое давление", app.person.DiastolicPressure, Calculation_ForWomen.NormaDiastDavleniya((int)app.person.Age, (int)app.person.Weight));
            AddInTable.AddInTableValue(ref GridList, "Пульс в покое", app.person.PulseAtRest, 60, app.point.PulseAtRest);
            if (app.person.Sport == true)    //  кросс
            {
                AddInTable.AddInTableValue(ref GridList, "Общая выносливость", app.person.OverallEndurance,
                    Calculation_ForWomen.TableOfNorms_ForWomen[Calculation_ForWomen.AgeToCount, 5], app.point.OverallEndurance);
            }
            else                            //  кол-во тренеровок в неделю
            {
                AddInTable.AddInTableValue(ref GridList, "Общая выносливость", app.person.OverallEndurance, 3, app.point.OverallEndurance);
            }
            AddInTable.AddInTableValue(ref GridList, "Востанавливваемость пульса", app.person.PulseAfterExercise, app.person.PulseAtRest + 10, app.point.HeartRateRecovery);
            AddInTable.AddInTableValue(ref GridList, "Гибкость", app.person.Flexibility, Calculation_ForWomen.TableOfNorms_ForWomen[Calculation_ForWomen.AgeToCount, 0], app.point.Flexibility);
            AddInTable.AddInTableValue(ref GridList, "Быстрота", app.person.Speed, Calculation_ForWomen.TableOfNorms_ForWomen[Calculation_ForWomen.AgeToCount, 1], app.point.Speed);
            AddInTable.AddInTableValue(ref GridList, "Динамическая сила", app.person.DynamicForce, Calculation_ForWomen.TableOfNorms_ForWomen[Calculation_ForWomen.AgeToCount, 2], app.point.DynamicForce);
            AddInTable.AddInTableValue(ref GridList, "Скоростная выносливость", app.person.SpeedEndurance, Calculation_ForWomen.TableOfNorms_ForWomen[Calculation_ForWomen.AgeToCount, 3], app.point.SpeedEndurance);
            AddInTable.AddInTableValue(ref GridList, "Скоростно-силовая выностивость", app.person.SpeedAndStrengthEndurance, Calculation_ForWomen.TableOfNorms_ForWomen[Calculation_ForWomen.AgeToCount, 4], app.point.SpeedAndStrengthEndurance);
            AddInTable.AddInTableFinalScore(ref GridList, "Ваш уровень физического состояния ", app.TotalScore, app.point.Sum());
        }
    }
}
