using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wheely.Core.Web.Settings.RedisServerSettings;
using Wheely.Service.Redis;
using Xunit;

namespace Wheely.Service.Test
{
    public sealed class RedisApiManagerTest : BaseTestManager
    {
        public Mock<IRedisServerSettings> IRedisServerSettingsMock { get; set; }
        public RedisApiManager RedisApiManager { get; set; }

        public RedisApiManagerTest()
        {
            IRedisServerSettingsMock = new Mock<IRedisServerSettings>();
            RedisApiManager = new RedisApiManager(IRedisServerSettingsMock.Object);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Increment_IsNullOrWhiteSpace_ReturnArgumentNullException(string cacheKey)
        {
        }

    }
}
