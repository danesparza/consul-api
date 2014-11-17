using System;
using Consul.Models;
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
    }
}
