using System;
using Consul.Config;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConsulClient.Tests
{
    [TestClass]
    public class ConsulManagerTests
    {
        [TestMethod]
        public void GetConfigItem_ValidKey_IsSuccessful()
        {
            //  Arrange
            string consulServer = "http://localhost:8500/";
            ConfigItem configItem = null;
            string configKey = "testing/testconfigitem";

            //  Act
            ConsulManager client = new ConsulManager(consulServer);
            configItem = client.GetConfigItem(configKey);

            //  Assert
            Assert.IsNotNull(configItem);
        }

        [TestMethod]
        public void GetConfigItem_InvalidKey_NullReturnValue()
        {
            //  Arrange
            string consulServer = "http://localhost:8500/";
            ConfigItem configItem = null;
            string configKey = "testing/bogus";

            //  Act
            ConsulManager client = new ConsulManager(consulServer);
            configItem = client.GetConfigItem(configKey);

            //  Assert
            Assert.IsNull(configItem);
        }

        [TestMethod]
        public void SetConfigItem_ValidKey_ReturnsTrue()
        {
            //  Arrange
            string consulServer = "http://localhost:8500/";
            string configValue = "This is a test";
            string configKey = "testing/testforput";

            //  Act
            ConsulManager client = new ConsulManager(consulServer);
            var wasSuccessful = client.SetConfigItem(configKey, configValue);

            //  Assert
            Assert.IsTrue(wasSuccessful);
        }
    }
}
