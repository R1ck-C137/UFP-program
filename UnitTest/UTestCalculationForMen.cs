using Xunit;
using UFP_program;

namespace UnitTest
{
    public class UTestCalculationForMen
    {
        public UTestCalculationForMen()
        {
            CreatingFilledClass_ForTest1();
            _calculationForMen = new Calculation_ForMen(_person, _point);
            _calculationForMen.Ñalculation();
        }

        private readonly Calculation_ForMen _calculationForMen;
        private readonly Person _person = new();
        private readonly Point _point = new();

        [Fact]
        public void TestCalcAgeToCount()
        {
            Assert.Equal(0, _calculationForMen.CalcAgeToCount(12));
            Assert.Equal(10, _calculationForMen.CalcAgeToCount(42));
            Assert.Equal(3, _calculationForMen.CalcAgeToCount(22));
        }

        [Fact]
        public void TestWeightNorm()
        {
            Assert.Equal(72.5, _calculationForMen.WeightNorm(180, 20));
            Assert.Equal(64.25, _calculationForMen.WeightNorm(165, 35));
        }

        [Fact]
        public void TestNormaSistDavleniya()
        {
            Assert.Equal(137, _calculationForMen.NormaSistDavleniya(20, 180));
            Assert.Equal(143, _calculationForMen.NormaSistDavleniya(35, 165));
        }

        [Fact]
        public void TestNormaDiastDavleniya()
        {
            Assert.Equal(103, _calculationForMen.NormaDiastDavleniya(20, 180));
            Assert.Equal(102.25, _calculationForMen.NormaDiastDavleniya(35, 165));
        }

        [Fact]
        public void TestCalculationFunc()
        {
            Assert.Equal(22, _point.Age);
            Assert.Equal(29, _point.Weight);
            Assert.Equal(28, _point.SystemPressure);
            Assert.Equal(28, _point.PulseAtRest);
            Assert.Equal(10, _point.OverallEndurance);
            Assert.Equal(-10, _point.HeartRateRecovery);
            Assert.Equal(2, _point.Flexibility);
            Assert.Equal(0, _point.Speed);
            Assert.Equal(10, _point.DynamicForce);
            Assert.Equal(9, _point.SpeedEndurance);
            Assert.Equal(8, _point.SpeedAndStrengthEndurance);
            Assert.Equal(136, _point.Sum());
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