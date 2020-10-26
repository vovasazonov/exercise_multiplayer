using Models.Characters;
using NSubstitute;
using NUnit.Framework;

namespace ModelsTests
{
    [TestFixture]
    public class HealthPointModelTester
    {
        [Test]
        [TestCase(100u, 300u)]
        public void TakePoints_TakeMoreThanHave_PointsAreZero(uint currentPoints, uint amountTake)
        {
            var healthPointData = Substitute.For<IHealthPointData>();
            healthPointData.Points.Returns(currentPoints);
            var healthPointModel = new HealthPointModel(healthPointData);

            healthPointModel.TakePoints(amountTake);

            Assert.AreEqual(healthPointData.Points, (uint) 0);
        }

        [Test]
        [TestCase(50u, 100u,55u)]
        public void AddPoints_AddMoreThanMaxPoints_PointsSameMaxPoints(uint currentPoints, uint maxPoints, uint amountAdd)
        {
            var healthPointData = Substitute.For<IHealthPointData>();
            healthPointData.MaxPoints.Returns(maxPoints);
            healthPointData.Points.Returns(currentPoints);
            var healthPointModel = new HealthPointModel(healthPointData);

            healthPointModel.AddPoints(amountAdd);

            Assert.AreEqual(healthPointData.Points, maxPoints);
        }
        
        [Test]
        [TestCase(uint.MaxValue - 5, uint.MaxValue,55u)]
        public void AddPoints_AddMoreThanMaxValue_PointsSameMaxValue(uint currentPoints, uint maxPoints, uint amountAdd)
        {
            var healthPointData = Substitute.For<IHealthPointData>();
            healthPointData.MaxPoints.Returns(maxPoints);
            healthPointData.Points.Returns(currentPoints);
            var healthPointModel = new HealthPointModel(healthPointData);

            healthPointModel.AddPoints(amountAdd);

            Assert.AreEqual(healthPointData.Points, maxPoints);
        }
    }
}