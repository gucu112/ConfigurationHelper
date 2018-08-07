using Gucu112.ConfigurationHelper.Extensions;
using Gucu112.ConfigurationHelper.Sections.AppData;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using Xunit;

namespace ConfigurationHelper.Test
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
            foreach (KeyValuePair<string, string> element in Gucu112.ConfigurationHelper.ConfigurationManager.AppSettings.ToDictionary())
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
            KeyValueConfigurationCollection configurationManagerDataSettings = ((AppSettingsSection)
                ConfigurationManager.GetSection("dataSettings"))?.Settings;
            if (configurationManagerDataSettings == null)
            {
                Assert.Empty(Gucu112.ConfigurationHelper.ConfigurationManager.DataSettings.ToList());
                Assert.Empty(Gucu112.ConfigurationHelper.ConfigurationManager.DataSettings.ToDictionary());
            }
            else
            {
                Assert.Equal(configurationManagerDataSettings.AllKeys.Select(key => configurationManagerDataSettings[key].Value).ToList(),
                    Gucu112.ConfigurationHelper.ConfigurationManager.DataSettings.ToList());
                foreach (KeyValuePair<string, string> element in Gucu112.ConfigurationHelper.ConfigurationManager.DataSettings.ToDictionary())
                {
                    Assert.Equal(configurationManagerDataSettings[element.Key].Value,
                        element.Value);
                }
            }
        }

        /// <summary>
        /// Tests the application data object.
        /// </summary>
        [Fact]
        public void AppDataSettingsTest()
        {
            KeyValueConfigurationCollection configurationManagerAppData = ((AppDataSection)
                ConfigurationManager.GetSection("appData"))?.Settings;
            if (configurationManagerAppData == null)
            {
                Assert.Empty(Gucu112.ConfigurationHelper.ConfigurationManager.AppData.ToList());
                Assert.Empty(Gucu112.ConfigurationHelper.ConfigurationManager.AppData.ToDictionary());
            }
            else
            {
                Assert.Equal(configurationManagerAppData.AllKeys.Select(key => configurationManagerAppData[key].Value).ToList(),
                    Gucu112.ConfigurationHelper.ConfigurationManager.AppData.ToList());
                foreach (KeyValuePair<string, string> element in Gucu112.ConfigurationHelper.ConfigurationManager.AppData.ToDictionary())
                {
                    Assert.Equal(configurationManagerAppData[element.Key].Value,
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
                Gucu112.ConfigurationHelper.ConfigurationManager.ConnectionStrings);
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
                object convertedValue = Convert.ChangeType(value, type);
                Assert.True(value.Equals(convertedValue));
            }
            else
            {
                MethodInfo privateMethodCanChangeType = typeof(KeyValueConfigurationCollectionExtensions)
                    .GetMethod("CanChangeType", BindingFlags.NonPublic | BindingFlags.Static);
                bool canChangeValue = (bool)privateMethodCanChangeType.Invoke(null, new object[] { value, type });
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
                        object singleValue = Convert.ChangeType
                            (((float)value).ToString("R"), type);
                        Assert.Equal(singleValue, value);
                        break;
                    case nameof(Double):
                        object doubleValue = Convert.ChangeType
                            (((double)value).ToString("R"), type);
                        Assert.Equal(doubleValue, value);
                        break;
                    case nameof(DateTime):
                        object dateTimeValue = Convert.ChangeType
                            (((DateTime)value).ToString("o"), type);
                        Assert.Equal(dateTimeValue, value);
                        break;
                    default:
                        object convertedValue = Convert.ChangeType(value.ToString(), type);
                        Assert.Equal(convertedValue, value);
                        break;
                }
            }
        }

        #endregion
    }
}
