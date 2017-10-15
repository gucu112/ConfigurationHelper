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
        #region Public methods

        /// <summary>
        /// Tests the applications settings object.
        /// </summary>
        [Fact]
        public void AppSettingsTest()
        {
            foreach (var element in Config.AppSettings.ToDictionary())
            {
                Assert.Equal(ConfigurationManager.AppSettings[element.Key],
                    element.Value);
            }
        }

        /// <summary>
        /// Tests the data settings object.
        /// </summary>
        [Fact]
        public void DataSettingsTest()
        {
            var configurationManagerDataSettings = ((AppSettingsSection)
                ConfigurationManager.GetSection("dataSettings"))?.Settings;
            if (configurationManagerDataSettings == null)
            {
                Assert.Empty(Config.DataSettings.ToDictionary());
                Assert.Empty(Config.AppData.ToDictionary());
            }
            else
            {
                foreach (var element in Config.DataSettings.ToDictionary())
                {
                    Assert.Equal(configurationManagerDataSettings[element.Key].Value,
                        element.Value);
                }
                foreach (var element in Config.AppData.ToDictionary())
                {
                    Assert.Equal(configurationManagerDataSettings[element.Key].Value,
                        element.Value);
                }
            }
        }

        /// <summary>
        /// Tests the connection strings object.
        /// </summary>
        [Fact]
        public void ConnectionStringsTest()
        {
            Assert.Equal(ConfigurationManager.ConnectionStrings,
                Config.ConnectionStrings);
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
            if (value == null)
            {
                Assert.Null(value);
            }
            else if (type == typeof(object))
            {
                var convertedValue = Convert.ChangeType(value, type);
                Assert.True(value.Equals(convertedValue));
            }
            else
            {
                MethodInfo privateMethodCanChangeType = typeof(KeyValueConfigurationCollectionExtensions)
                    .GetMethod("CanChangeType", BindingFlags.NonPublic | BindingFlags.Static);
                var canChangeValue = (bool)privateMethodCanChangeType.Invoke(null, new object[] { value, type });
                Assert.True(canChangeValue);
            }
        }

        /// <summary>
        /// Tests whether the provided values can be acquired from string as the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="value">The value.</param>
        [Theory]
        [MemberData(nameof(ConfigTestData.BuiltInTypes), MemberType = typeof(ConfigTestData))]
        public void ConvertToStringTest(Type type, object value)
        {
            if (value == null)
            {
                Assert.Null(value);
            }
            else
            {
                switch (type.Name)
                {
                    case nameof(Object):
                        Assert.Equal(type.FullName, value.ToString());
                        break;
                    case nameof(DBNull):
                        Assert.Equal(string.Empty, value.ToString());
                        break;
                    case nameof(Single):
                        var singleValue = Convert.ChangeType
                            (((float)value).ToString("R"), type);
                        Assert.Equal(singleValue, value);
                        break;
                    case nameof(Double):
                        var doubleValue = Convert.ChangeType
                            (((double)value).ToString("R"), type);
                        Assert.Equal(doubleValue, value);
                        break;
                    case nameof(DateTime):
                        var dateTimeValue = Convert.ChangeType
                            (((DateTime)value).ToString("o"), type);
                        Assert.Equal(dateTimeValue, value);
                        break;
                    default:
                        var convertedValue = Convert.ChangeType(value.ToString(), type);
                        Assert.Equal(convertedValue, value);
                        break;
                }
            }
        }

        #endregion
    }
}
