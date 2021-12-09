using Moq;
using Wheely.Core.Web.Settings;
using Wheely.Service.Redis;
using Xunit;

namespace Wheely.Service.Test
{
    public sealed class RedisApiManagerTest : BaseTestManager
    {
        public Mock<IRedisServerSetting> IRedisServerSettingMock { get; set; }
        public RedisApiManager RedisApiManager { get; set; }

        public RedisApiManagerTest()
        {
            IRedisServerSettingMock = new Mock<IRedisServerSetting>();
            RedisApiManager = new RedisApiManager(IRedisServerSettingMock.Object);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Increment_IsNullOrWhiteSpace_ReturnArgumentNullException(string cacheKey)
        {
        }

    }
}
