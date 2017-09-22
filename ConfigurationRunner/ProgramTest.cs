using ConfigurationHelper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Xunit;

namespace ConfigurationHelperTest
{
    public class ProgramTest
    {
        [Fact]
        public void AppSettingsTest()
        {
            Assert.Equal(Config.AppSettings["TestString"], "pq9u35b");
        }

        [Fact]
        public void DataSettingsTest()
        {
            Assert.Equal(Config.DataSettings["Data64Value"], "64 63 54 67 54 84");
            Assert.Equal(Config.AppData["Data64Value"], "64 63 54 67 54 84");
        }

        [Fact]
        public void ConnectionStringsSettingsTest()
        {
            Assert.Equal(Config.ConnectionStrings["DatabaseConnectionString"].Name,
                "DatabaseConnectionString");
            Assert.Equal(Config.ConnectionStrings["DatabaseConnectionString"].ProviderName,
                "System.Data.SqlClient");
            var connectionString = Config.ConnectionStrings["DatabaseConnectionString"].ConnectionString
                .Split(';').Where(attr => !string.IsNullOrEmpty(attr)).Select(attr => new KeyValuePair<string, string>
                (attr.Split('=')[0], attr.Split('=')[1])).ToDictionary(e => e.Key, e => e.Value);
            Assert.Equal(connectionString["Data Source"], @".\SQLEXPRESS");
            Assert.Equal(connectionString["Initial Catalog"], "DatabaseName");
            Assert.Equal(Convert.ToInt32(connectionString["Connect Timeout"]), 3);
            Assert.Equal(Convert.ToBoolean(connectionString["Integrated Security"]), true);
            Assert.Equal(Convert.ToBoolean(connectionString["MultipleActiveResultSets"]), true);
        }
    }
}
