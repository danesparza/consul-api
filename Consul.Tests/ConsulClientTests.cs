using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Consul.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Consul.Tests
{
    [TestClass]
    public class ConsulClientTests
    {
        [TestMethod]
        public async Task GetConfigItem_ValidKey_Successful()
        {
            //  Arrange
            string consulServer = "http://localhost:8500/";
            ConfigItem configItem = null;
            string configKey = "testing/testconfigitem";

            //  Act
            ConsulClient client = new ConsulClient(consulServer);
            configItem = await client.GetConfigItem(configKey);

            //  Assert
            Assert.IsNotNull(configItem);
        }
    }
}
