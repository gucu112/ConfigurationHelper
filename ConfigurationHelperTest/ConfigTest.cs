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
        public void DataSettingsTest()
        {
            Assert.Equal(Config.DataSettings,
                ConfigurationManager.GetSection("dataSettings"));
            Assert.Equal(Config.AppData,
                ConfigurationManager.GetSection("dataSettings"));
        }

        [Fact]
        public void ConnectionStringsTest()
        {
            Assert.Equal(Config.ConnectionStrings,
                ConfigurationManager.ConnectionStrings);
        }
    }
}
