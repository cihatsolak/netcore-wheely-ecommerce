using Moq;
using System;
using Wheely.Core.Entities.Concrete.Wheels;
using Wheely.Core.Services.Results.Concrete;
using Wheely.Data.Abstract.Repositories.EntityFrameworkCore;
using Wheely.Service.Wheels;
using Xunit;

namespace Wheely.Service.Test
{
    public sealed class WheelManagerTest
    {
        #region Fields
        public Mock<IWheelRepository> WheelRepositoryMock { get; set; }
        public WheelManager WheelManager { get; set; }
        #endregion

        #region Constructor
        public WheelManagerTest()
        {
            WheelRepositoryMock = new Mock<IWheelRepository>();
            WheelManager = new WheelManager(WheelRepositoryMock.Object);
        }
        #endregion

        #region Methods
        [Theory]
        [InlineData(0)]
        public void GetWheelById_ZeroPrimaryKey_ReturnArgumentException(int id)
        {
            Assert.Throws<ArgumentException>(() => WheelManager.GetWheelById(id));
        }

        [Theory]
        [InlineData(-1)]
        public void GetWheelById_NegativePrimaryKey_ReturnArgumentException(int id)
        {
            Assert.Throws<ArgumentException>(() => WheelManager.GetWheelById(id));
        }

        [Theory]
        [InlineData(999)]
        public void GetWheelById_PrimaryKeyNotInTable_ReturnErrorDataResult(int id)
        {
            Wheel wheel = null;
            WheelRepositoryMock.Setup(p => p.GetWheelWithRelatedTablesById(id)).Returns(wheel);

            var dataResult = WheelManager.GetWheelById(id);

            WheelRepositoryMock.Verify(p => p.GetWheelWithRelatedTablesById(id), Times.Once);

            Assert.False(dataResult.Succeeded);
            Assert.Null(dataResult.Data);
        }

        [Theory]
        [InlineData(5)]
        public void GetWheelById_ValidPrimaryKey_ReturnSuccessDataResult(int id)
        {
            Wheel wheel = new()
            {
                Id = 1,
                Name = "Test"
            };

            WheelRepositoryMock.Setup(p => p.GetWheelWithRelatedTablesById(id)).Returns(wheel);

            var dataResult = WheelManager.GetWheelById(id);

            WheelRepositoryMock.Verify(p => p.GetWheelWithRelatedTablesById(id), Times.Once);

            Assert.True(dataResult.Succeeded);
            Assert.NotNull(dataResult.Data);
        }
        #endregion
    }
}
