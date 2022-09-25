using Xunit;
using UFP_program;

namespace UnitTest
{
    public class UTestCalculationForWomen
    {
        public UTestCalculationForWomen()
        {
            CreatingFilledClass_ForTest1();
            _calculationForWomen = new Calculation_ForWomen(_person, _point);
            _calculationForWomen.Сalculation();
        }

        private readonly Calculation_ForWomen _calculationForWomen;
        private readonly Person _person = new();
        private readonly Point _point = new();

        [Fact]
        public void TestCalcAgeToCount()
        {
            Assert.Equal(0, _calculationForWomen.CalcAgeToCount(12));
            Assert.Equal(10, _calculationForWomen.CalcAgeToCount(42));
            Assert.Equal(3, _calculationForWomen.CalcAgeToCount(22));
        }

        [Fact]
        public void TestWeightNorm()
        {
            Assert.Equal(75.6, _calculationForWomen.WeightNorm(180, 20));
            Assert.Equal(85.8, _calculationForWomen.WeightNorm(165, 35));
        }

        [Fact]
        public void TestNormaSistDavleniya()
        {
            Assert.Equal(143, _calculationForWomen.NormaSistDavleniya(20, 180));
            Assert.Equal(151.25, _calculationForWomen.NormaSistDavleniya(35, 165));
        }

        [Fact]
        public void TestNormaDiastDavleniya()
        {
            Assert.Equal(99.4, _calculationForWomen.NormaDiastDavleniya(20, 180));
            Assert.Equal(100.45, _calculationForWomen.NormaDiastDavleniya(35, 165));
        }

        [Fact]
        public void TestCalculationFunc()
        {
            Assert.Equal(22, _point.Age);
            Assert.Equal(30, _point.Weight);
            Assert.Equal(28, _point.SystemPressure);
            Assert.Equal(28, _point.PulseAtRest);
            Assert.Equal(10, _point.OverallEndurance);
            Assert.Equal(-10, _point.HeartRateRecovery);
            Assert.Equal(1, _point.Flexibility);
            Assert.Equal(0, _point.Speed);
            Assert.Equal(40, _point.DynamicForce);
            Assert.Equal(18, _point.SpeedEndurance);
            Assert.Equal(16, _point.SpeedAndStrengthEndurance);
            Assert.Equal(183, _point.Sum());
        }

        private void CreatingFilledClass_ForTest1()
        {
            _person.FIO = "TestFIO";
            _person.Group = "TestGroup";
            _person.Gender = true;
            _person.Age = 22;
            _person.Weight = 75;
            _person.Height = 182;
            _person.PulseAtRest = 62;
            _person.PulseAfterExercise = 82;
            _person.SystolicPressure = 140;
            _person.DiastolicPressure = 85;
            _person.Flexibility = 11;
            _person.Speed = 16;
            _person.DynamicForce = 57;
            _person.OverallEndurance = 2;
            _person.Sport = false;
            _person.SpeedEndurance = 19;
            _person.SpeedAndStrengthEndurance = 22;
        }
    }
}
