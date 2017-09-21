using ConfigurationHelper;
using System;
using System.Configuration;
using System.Linq;
using Xunit;

namespace ConfigurationHelperTest
{
    public class ConfigTest
    {
        [Fact]
        public void AppSettingsTest()
        {
            Assert.Equal(Config.AppSettings,
                ConfigurationManager.AppSettings);
        }

        [Fact]
        public void ConnectionStringsTest()
        {
            Assert.Equal(Config.ConnectionStrings,
                ConfigurationManager.ConnectionStrings);
        }
    }
}
