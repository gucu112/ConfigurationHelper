using ConfigurationHelper;
using ConfigurationHelper.Extensions;
using System;
using System.Configuration;
using System.Linq;
using System.Reflection;
using Xunit;

namespace ConfigurationHelperTest
{
    public class ConfigTest
    {
        /// <summary>
        /// Tests the applications settings object.
        /// </summary>
        [Fact]
        public void AppSettingsTest()
        {
            Assert.Equal(Config.AppSettings,
                ConfigurationManager.AppSettings);
        }

        /// <summary>
        /// Tests the data settings object.
        /// </summary>
        [Fact]
        public void DataSettingsTest()
        {
            Assert.Equal(Config.DataSettings,
                ConfigurationManager.GetSection("dataSettings"));
            Assert.Equal(Config.AppData,
                ConfigurationManager.GetSection("dataSettings"));
        }

        /// <summary>
        /// Tests the connection strings object.
        /// </summary>
        [Fact]
        public void ConnectionStringsTest()
        {
            Assert.Equal(Config.ConnectionStrings,
                ConfigurationManager.ConnectionStrings);
        }

        /// <summary>
        /// Tests whether the provided values can be casted to the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="value">The value.</param>
        [Theory]
        [MemberData(nameof(ConfigTestData.BuiltInTypes), MemberType = typeof(ConfigTestData))]
        public void CanChangeTypeTest(Type type, object value)
        {
            if (type == typeof(object))
            {
                Assert.True(value.Equals(Convert.ChangeType(value, type)));
            }
            else if (value != null)
            {
                MethodInfo privateMethod = typeof(NameValueCollectionExtensions)
                    .GetMethod("CanChangeType", BindingFlags.NonPublic | BindingFlags.Static);
                object invokedValue = privateMethod.Invoke(null, new object[] { value, type });
                Assert.Equal(invokedValue, true);
            }
            else
            {
                Assert.Null(value);
            }
        }
    }
}
