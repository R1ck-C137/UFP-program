using Xunit;
using UFP_program;

namespace UnitTest
{
    public class UTestCalculation_ForMen
    {
        public UTestCalculation_ForMen()
        {
            CreatingFilledClass_ForTest1();
            calculation_ForMen1 = new Calculation_ForMen(person1, point1);
        }
        Calculation_ForMen calculation_ForMen1;

        private Person person1 = new();
        private Person person2 = new();

        private Point point1 = new();
        private Point point2 = new();

        [Fact]
        public void TestCalcAgeToCount()
        {
            //Calculation_ForMen calculation_ForMen = new Calculation_ForMen(person1, point1);
            Assert.Equal(0, calculation_ForMen1.CalcAgeToCount(12));
            Assert.Equal(10, calculation_ForMen1.CalcAgeToCount(42));
            Assert.Equal(3, calculation_ForMen1.CalcAgeToCount(22));
        }
        [Fact]
        public void TestWeightNorm()
        {
            //Calculation_ForMen calculation_ForMen = new Calculation_ForMen(person1, point1);
            Assert.Equal(72.5, calculation_ForMen1.WeightNorm(180, 20));
            Assert.Equal(64.25, calculation_ForMen1.WeightNorm(165, 35));
        }
        [Fact]
        public void TestNormaSistDavleniya()
        {
            //Calculation_ForMen calculation_ForMen = new Calculation_ForMen(person1, point1);
            Assert.Equal(137, calculation_ForMen1.NormaSistDavleniya(20, 180));
            Assert.Equal(143, calculation_ForMen1.NormaSistDavleniya(35, 165));
        }
        [Fact]
        public void TestNormaDiastDavleniya()
        {
            Assert.Equal(103, calculation_ForMen1.NormaDiastDavleniya(20, 180));
            Assert.Equal(102.25, calculation_ForMen1.NormaDiastDavleniya(35, 165));
        }


        private void CreatingFilledClass_ForTest1()
        {
            person1.FIO = "TestFIO";
            person1.Group = "TestGroup";
            person1.Gender = true;
            person1.Age = 22;
            person1.Weight = 75;
            person1.Height = 182;
            person1.PulseAtRest = 62;
            person1.PulseAfterExercise = 82;
            person1.SystolicPressure = 140;
            person1.DiastolicPressure = 85;
            person1.Flexibility = 11;
            person1.Speed = 16;
            person1.DynamicForce = 57;
            person1.OverallEndurance = 2;
            person1.Sport = false;
            person1.SpeedEndurance = 19;
            person1.SpeedAndStrengthEndurance = 22;
        }
    }
}