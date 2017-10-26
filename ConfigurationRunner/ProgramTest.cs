using ConfigurationHelper;
using ConfigurationHelper.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading;
using Xunit;

namespace ConfigurationHelperTest
{
    public class ProgramTest
    {
        #region Public constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgramTest"/> class.
        /// </summary>
        public ProgramTest()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            Environment.SetEnvironmentVariable("ENV", "development");
            Environment.SetEnvironmentVariable("DATA_DIR", "test");
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Tests the applications settings object.
        /// </summary>
        [Fact]
        public void AppSettingsTest()
        {
            Assert.Equal("pq9u35b", Config.AppSettings.Get("TestString"));
            Assert.Equal("pq9u35b", Config.AppSettings["TestString"].Value);
        }

        /// <summary>
        /// Tests the applications settings object using generic method.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        [Theory]
        [InlineData(typeof(bool), "TestBool", true)]
        [InlineData(typeof(byte), "TestByte", (byte)69)]
        [InlineData(typeof(char), "TestChar", '$')]
        [InlineData(typeof(double), "TestDouble", 99.99)]
        [InlineData(typeof(float), "TestFloat", 6.6f)]
        [InlineData(typeof(int), "TestInt", 0)]
        [InlineData(typeof(long), "TestLong", 4294967296)]
        public void AppSettingsGenericTest(Type type, string key, object value)
        {
            object convertedValue = typeof(KeyValueConfigurationCollectionExtensions)
                .GetMethods().Single(method => method.Name == "Get" && method.IsGenericMethod)
                .MakeGenericMethod(type).Invoke(null, new object[] { Config.AppSettings, key });
            Assert.Equal(Convert.ChangeType(value, type), convertedValue);
        }

        /// <summary>
        /// Tests the data settings object.
        /// </summary>
        [Fact]
        public void DataSettingsTest()
        {
            Assert.Equal("64 63 54 67 54 84", Config.DataSettings.Get("Data64Value"));
            Assert.Equal("64 63 54 67 54 84", Config.DataSettings["Data64Value"].Value);
            Assert.Equal("64 63 54 67 54 84", Config.AppData.Get("Data64Value"));
            Assert.Equal("64 63 54 67 54 84", Config.AppData["Data64Value"].Value);
        }

        /// <summary>
        /// Tests the data settings object using generic method.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        [Theory]
        [InlineData(typeof(bool), "DataBool", false)]
        [InlineData(typeof(byte), "DataByte", (byte)42)]
        [InlineData(typeof(char), "DataChar", '^')]
        [InlineData(typeof(double), "DataDouble", 4.20)]
        [InlineData(typeof(float), "DataFloat", 4.2f)]
        [InlineData(typeof(int), "DataInt", -1)]
        [InlineData(typeof(long), "DataLong", -4294967296)]
        public void DataSettingsGenericTest(Type type, string key, object value)
        {
            MethodInfo genericMethodGet = typeof(KeyValueConfigurationCollectionExtensions).GetMethods()
                .Single(method => method.Name == "Get" && method.IsGenericMethod).MakeGenericMethod(type);
            object invokedByDataSettingsValue = genericMethodGet.Invoke(null, new object[] { Config.DataSettings, key });
            object invokedByDataAliasValue = genericMethodGet.Invoke(null, new object[] { Config.AppData, key });
            Assert.Equal(Convert.ChangeType(value, type), invokedByDataSettingsValue);
            Assert.Equal(Convert.ChangeType(value, type), invokedByDataAliasValue);
        }

        /// <summary>
        /// Tests the connection strings object.
        /// </summary>
        [Fact]
        public void ConnectionStringsSettingsTest()
        {
            Assert.Equal("DatabaseConnectionString",
                Config.ConnectionStrings["DatabaseConnectionString"].Name);
            Assert.Equal("System.Data.SqlClient",
                Config.ConnectionStrings["DatabaseConnectionString"].ProviderName);
            var connectionString = Config.ConnectionStrings["DatabaseConnectionString"].ConnectionString
                .Split(';').Where(attr => !string.IsNullOrEmpty(attr)).Select(attr => new KeyValuePair<string, string>
                (attr.Split('=')[0], attr.Split('=')[1])).ToDictionary(e => e.Key, e => e.Value);
            Assert.Equal(@".\SQLEXPRESS", connectionString["Data Source"]);
            Assert.Equal("DatabaseName", connectionString["Initial Catalog"]);
            Assert.Equal(3, Convert.ToInt32(connectionString["Connect Timeout"]));
            Assert.True(Convert.ToBoolean(connectionString["Integrated Security"]));
            Assert.True(Convert.ToBoolean(connectionString["MultipleActiveResultSets"]));
        }

        /// <summary>
        /// Tests expanding of the application settings by environment variables.
        /// </summary>
        [Fact]
        public void ExpandAppSettingsByEnvironmentVariablesTest()
        {
            Assert.Equal("development", Config.AppSettings.Get("ApplicationEnvironment"));
            Assert.Equal("development", Environment.ExpandEnvironmentVariables
                (Config.AppSettings["ApplicationEnvironment"].Value));
            Environment.SetEnvironmentVariable("ENV", "test");
            Assert.Equal("test", Config.AppSettings.Get("ApplicationEnvironment"));
            Assert.Equal("test", Environment.ExpandEnvironmentVariables
                (Config.AppSettings["ApplicationEnvironment"].Value));
        }

        /// <summary>
        /// Expands the data settings by environment variables test.
        /// </summary>
        [Fact]
        public void ExpandDataSettingsByEnvironmentVariablesTest()
        {
            Assert.Equal(@"C:\Data\test", Config.DataSettings.Get("DataFolder"));
            Assert.Equal(@"C:\Data\test", Environment.ExpandEnvironmentVariables
                (Config.AppData["DataFolder"].Value));
            Environment.SetEnvironmentVariable("DATA_DIR", "data");
            Assert.Equal(@"C:\Data\data", Config.DataSettings.Get("DataFolder"));
            Assert.Equal(@"C:\Data\data", Environment.ExpandEnvironmentVariables
                (Config.AppData["DataFolder"].Value));
        }

        #endregion
    }
}
