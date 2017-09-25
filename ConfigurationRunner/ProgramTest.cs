using ConfigurationHelper;
using ConfigurationHelper.Extensions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading;
using Xunit;

namespace ConfigurationHelperTest
{
    public class ProgramTest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProgramTest"/> class.
        /// </summary>
        public ProgramTest()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
        }

        /// <summary>
        /// Tests the applications settings object.
        /// </summary>
        [Fact]
        public void AppSettingsTest()
        {
            Assert.Equal(Config.AppSettings["TestString"], "pq9u35b");
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
            object invokedValue = typeof(NameValueCollectionExtensions).GetMethod("Get")
                .MakeGenericMethod(type).Invoke(null, new object[] { Config.AppSettings, key });
            Assert.Equal(invokedValue, Convert.ChangeType(value, type));
        }

        /// <summary>
        /// Tests the data settings object.
        /// </summary>
        [Fact]
        public void DataSettingsTest()
        {
            Assert.Equal(Config.DataSettings["Data64Value"], "64 63 54 67 54 84");
            Assert.Equal(Config.AppData["Data64Value"], "64 63 54 67 54 84");
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
            MethodInfo genericMethod = typeof(NameValueCollectionExtensions).GetMethod("Get").MakeGenericMethod(type);
            object invokedByDataSettingsValue = genericMethod.Invoke(null, new object[] { Config.DataSettings, key });
            object invokedByDataAliasValue = genericMethod.Invoke(null, new object[] { Config.AppData, key });
            Assert.Equal(invokedByDataSettingsValue, Convert.ChangeType(value, type));
            Assert.Equal(invokedByDataAliasValue, Convert.ChangeType(value, type));
        }

        /// <summary>
        /// Tests the connection strings object.
        /// </summary>
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
