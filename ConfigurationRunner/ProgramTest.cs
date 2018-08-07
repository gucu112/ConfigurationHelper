﻿using Gucu112.ConfigurationHelper;
using Gucu112.ConfigurationHelper.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using Xunit;

namespace ConfigurationHelper.Test
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
            Environment.SetEnvironmentVariable("TEMP", @"D:\AppData\Local\Temp");
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Tests the applications settings object.
        /// </summary>
        [Fact]
        public void AppSettingsTest()
        {
            Assert.Equal("pq9u35b", ConfigurationManager.AppSettings["TestString"]);
            Assert.Equal("pq9u35b", ConfigurationManager.AppSettings.Get("TestString"));
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
                .MakeGenericMethod(type).Invoke(null, new object[] { ConfigurationManager.AppSettings, key });
            Assert.Equal(Convert.ChangeType(value, type), convertedValue);
        }

        /// <summary>
        /// Tests the data settings object.
        /// </summary>
        [Fact]
        public void DataSettingsTest()
        {
            Assert.Equal("64 63 54 67 54 84", ConfigurationManager.DataSettings["Data64Value"]);
            Assert.Equal("64 63 54 67 54 84", ConfigurationManager.DataSettings.Get("Data64Value"));
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
            object convertedValue = typeof(KeyValueConfigurationCollectionExtensions)
                .GetMethods().Single(method => method.Name == "Get" && method.IsGenericMethod)
                .MakeGenericMethod(type).Invoke(null, new object[] { ConfigurationManager.DataSettings, key });
            Assert.Equal(Convert.ChangeType(value, type), convertedValue);
        }

        /// <summary>
        /// Tests the connection strings object.
        /// </summary>
        [Fact]
        public void ConnectionStringsSettingsTest()
        {
            Assert.Equal("DatabaseConnectionString",
                ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].Name);
            Assert.Equal("System.Data.SqlClient",
                ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ProviderName);
            IDictionary<string, string> connectionString = ConfigurationManager
                .ConnectionStrings["DatabaseConnectionString"].ConnectionString
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
            Assert.Equal("development", Environment.ExpandEnvironmentVariables
                (ConfigurationManager.AppSettings["ApplicationEnvironment"]));
            Assert.Equal("development", ConfigurationManager.AppSettings.Get("ApplicationEnvironment"));
            Environment.SetEnvironmentVariable("ENV", "test");
            Assert.Equal("test", Environment.ExpandEnvironmentVariables
                (ConfigurationManager.AppSettings["ApplicationEnvironment"]));
            Assert.Equal("test", ConfigurationManager.AppSettings.Get("ApplicationEnvironment"));
        }

        /// <summary>
        /// Tests expanding of the data settings by environment variables.
        /// </summary>
        [Fact]
        public void ExpandDataSettingsByEnvironmentVariablesTest()
        {
            Assert.Equal(@"C:\Data\test", Environment.ExpandEnvironmentVariables
                (ConfigurationManager.DataSettings["DataFolder"]));
            Assert.Equal(@"C:\Data\test", ConfigurationManager.DataSettings.Get("DataFolder"));
            Environment.SetEnvironmentVariable("DATA_DIR", "data");
            Assert.Equal(@"C:\Data\data", Environment.ExpandEnvironmentVariables
                (ConfigurationManager.DataSettings["DataFolder"]));
            Assert.Equal(@"C:\Data\data", ConfigurationManager.DataSettings.Get("DataFolder"));
        }

        /// <summary>
        /// Tests expanding of the application data by environment variables.
        /// </summary>
        [Fact]
        public void ExpandAppDataByEnvironmentVariablesTest()
        {
            Assert.Equal(@"D:\AppData\Local\Temp", Environment.ExpandEnvironmentVariables
                (ConfigurationManager.AppData["TemporaryFolder"]));
            Assert.Equal(@"D:\AppData\Local\Temp", ConfigurationManager.AppData.Get("TemporaryFolder"));
            Assert.Equal(@"D:\AppData\Local\Temp", Environment.ExpandEnvironmentVariables
                (ConfigurationManager.AppDataSection.Collection["TemporaryFolder"]));
        }

        /// <summary>
        /// Tests acquiring of non-existing value from application data.
        /// </summary>
        [Fact]
        public void AppDataNonExistingValueTest()
        {
            Assert.Throws<NullReferenceException>(() => ConfigurationManager.AppData["NonExisting"]);
            Assert.Throws<ArgumentException>(() => ConfigurationManager.AppData.Get("NonExisting"));
            Assert.Null(ConfigurationManager.AppDataSection.Collection["NonExisting"]);
        }

        /// <summary>
        /// Tests acquiring of empty value from application data.
        /// </summary>
        [Fact]
        public void AppDataEmptyValueTest()
        {
            Assert.Empty(ConfigurationManager.AppData["EmptyValue"]);
            Assert.Empty(ConfigurationManager.AppData.Get("EmptyValue"));
            Assert.Empty(ConfigurationManager.AppDataSection.Collection["EmptyValue"]);
        }

        /// <summary>
        /// Tests acquiring of value from application data.
        /// </summary>
        [Fact]
        public void AppDataValueTest()
        {
            Assert.Equal("123", ConfigurationManager.AppData["Value"]);
            Assert.Equal("123", ConfigurationManager.AppData.Get("Value"));
            Assert.Equal("123", ConfigurationManager.AppDataSection.Collection["Value"]);
        }

        /// <summary>
        /// Tests acquiring of empty list from application data.
        /// </summary>
        [Fact]
        public void AppDataEmptyListTest()
        {
            Assert.Empty(ConfigurationManager.AppDataSection.GetList("EmptyCollection"));
        }

        /// <summary>
        /// Tests acquiring of single element list from application data.
        /// </summary>
        [Fact]
        public void AppDataSingleElementListTest()
        {
            Assert.Collection(ConfigurationManager.AppDataSection.GetList("SingleElementList"),
                i => Assert.Equal("Test", i));
        }

        /// <summary>
        /// Tests acquiring of multiple elements list from application data by generic method.
        /// </summary>
        [Fact]
        public void AppDataMultipleElementsGenericListTest()
        {
            Assert.Collection(ConfigurationManager.AppDataSection.GetList<double>("MultipleElementsList"),
                i => Assert.Equal(1.3D, i), i => Assert.Equal(2.6D, i), i => Assert.Equal(3.9D, i));
        }

        /// <summary>
        /// Tests acquiring of empty dictionary from application data.
        /// </summary>
        [Fact]
        public void AppDataEmptyDictionaryTest()
        {
            Assert.Empty(ConfigurationManager.AppDataSection.GetDictionary("EmptyCollection"));
        }

        /// <summary>
        /// Tests acquiring of single element dictionary from application data.
        /// </summary>
        [Fact]
        public void AppDataSingleElementDictionaryTest()
        {
            Assert.Collection(ConfigurationManager.AppDataSection.GetDictionary("SingleElementDictionary"),
                (KeyValuePair<string, string> e) => { Assert.Equal("Key", e.Key); Assert.Equal("Value", e.Value); });
        }

        /// <summary>
        /// Tests acquiring of multiple elements dictionary from application data by generic method.
        /// </summary>
        [Fact]
        public void AppDataMultipleElementsGenericDictionaryTest()
        {
            Assert.Collection(ConfigurationManager.AppDataSection.GetDictionary<int>("MultipleElementsDictionary"),
                (KeyValuePair<string, int> e) => { Assert.Equal("First", e.Key); Assert.Equal(1, e.Value); },
                (KeyValuePair<string, int> e) => { Assert.Equal("Second", e.Key); Assert.Equal(2, e.Value); },
                (KeyValuePair<string, int> e) => { Assert.Equal("Third", e.Key); Assert.Equal(3, e.Value); });
        }

        #endregion
    }
}
