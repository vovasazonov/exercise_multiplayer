using Models.Characters;
using NUnit.Framework;
using NSubstitute;

namespace ModelsTest
{
    [TestFixture]
    public class HealthPointModelTester
    {
        [Test]
        public void TakePoints_IsResult0_True([Values(50)] int currentPoints, [Range(50, 60, 1)] int amountTake)
        {
            var healthPointData = Substitute.For<IHealthPointData>();
            healthPointData.Points.Returns((uint) currentPoints);
            var healthPointModel = new HealthPointModel(healthPointData);

            healthPointModel.TakePoints((uint) amountTake);

            Assert.AreEqual(healthPointData.Points, (uint) 0);
        }

        [Test]
        public void AddPoints_IsResultMaxPoints_True([Values(50)] int currentPoints, [Values(100)] int maxPoints, [Range(50, 60, 1)] int amountAdd)
        {
            var healthPointData = Substitute.For<IHealthPointData>();
            healthPointData.MaxPoints.Returns((uint) maxPoints);
            healthPointData.Points.Returns((uint) currentPoints);
            var healthPointModel = new HealthPointModel(healthPointData);

            healthPointModel.AddPoints((uint) amountAdd);

            Assert.AreEqual(healthPointData.Points, maxPoints);
        }
    }
}