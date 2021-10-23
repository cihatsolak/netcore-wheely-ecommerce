using Moq;
using Wheely.Service.Wheels;
using Xunit;

namespace Wheely.Service.Test
{
    public sealed class WheelManagerTest
    {
        #region Fields
        public Mock<IWheelService> WheelServiceMock { get; set; }
        #endregion

        #region Constructor
        public WheelManagerTest()
        {
            WheelServiceMock = new Mock<IWheelService>();
        }
        #endregion

        #region Methods
        #endregion
    }
}
