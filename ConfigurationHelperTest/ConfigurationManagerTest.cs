﻿//-----------------------------------------------------------------------------------
// <copyright file="ConfigurationManagerTest.cs" company="Gucu112">
//     Copyright (c) Gucu112 2017-2019. All rights reserved.
// </copyright>
// <author>Bartlomiej Roszczypala</author>
//-----------------------------------------------------------------------------------

namespace Gucu112.ConfigurationHelper.Test
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Reflection;
    using Gucu112.ConfigurationHelper.Sections.AppData;
    using Xunit;

    /// <summary>
    /// Tests the <see cref="ConfigurationHelper.ConfigurationManager" /> class.
    /// </summary>
    public class ConfigurationManagerTest
    {
        #region Public methods

        /// <summary>
        /// Tests the application settings object.
        /// </summary>
        [Fact]
        public void AppSettingsTest()
        {
            foreach (KeyValuePair<string, string> element in ConfigurationHelper.ConfigurationManager.AppSettings.ToDictionary())
            {
                Assert.Equal(
                    ConfigurationManager.AppSettings[element.Key],
                    element.Value);
            }
        }

        /// <summary>
        /// Tests the connection strings object.
        /// </summary>
        [Fact]
        public void ConnectionStringsTest()
        {
            Assert.Equal(
                ConfigurationManager.ConnectionStrings,
                ConfigurationHelper.ConfigurationManager.ConnectionStrings);
        }

        /// <summary>
        /// Tests the server settings object.
        /// </summary>
        [Fact]
        public void ServerSettingsTest()
        {
            KeyValueConfigurationCollection configurationManagerServerSettings = ((AppSettingsSection)
                ConfigurationManager.GetSection("serverSettings"))?.Settings;
            if (configurationManagerServerSettings == null)
            {
                Assert.Empty(ConfigurationHelper.ConfigurationManager.ServerSettings.ToList());
                Assert.Empty(ConfigurationHelper.ConfigurationManager.ServerSettings.ToDictionary());
            }
            else
            {
                Assert.Equal(
                    configurationManagerServerSettings.AllKeys.Select(key => configurationManagerServerSettings[key].Value).ToList(),
                    ConfigurationHelper.ConfigurationManager.ServerSettings.ToList());
                foreach (KeyValuePair<string, string> element in ConfigurationHelper.ConfigurationManager.ServerSettings.ToDictionary())
                {
                    Assert.Equal(
                        configurationManagerServerSettings[element.Key].Value,
                        element.Value);
                }
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
                Assert.Empty(ConfigurationHelper.ConfigurationManager.DataSettings.ToList());
                Assert.Empty(ConfigurationHelper.ConfigurationManager.DataSettings.ToDictionary());
            }
            else
            {
                Assert.Equal(
                    configurationManagerDataSettings.AllKeys.Select(key => configurationManagerDataSettings[key].Value).ToList(),
                    ConfigurationHelper.ConfigurationManager.DataSettings.ToList());
                foreach (KeyValuePair<string, string> element in ConfigurationHelper.ConfigurationManager.DataSettings.ToDictionary())
                {
                    Assert.Equal(
                        configurationManagerDataSettings[element.Key].Value,
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
                Assert.Empty(ConfigurationHelper.ConfigurationManager.AppData.ToList());
                Assert.Empty(ConfigurationHelper.ConfigurationManager.AppData.ToDictionary());
            }
            else
            {
                Assert.Equal(
                    configurationManagerAppData.AllKeys.Select(key => configurationManagerAppData[key].Value).ToList(),
                    ConfigurationHelper.ConfigurationManager.AppData.ToList());
                foreach (KeyValuePair<string, string> element in ConfigurationHelper.ConfigurationManager.AppData.ToDictionary())
                {
                    Assert.Equal(
                        configurationManagerAppData[element.Key].Value,
                        element.Value);
                }
            }
        }

        /// <summary>
        /// Tests whether the provided values can be casted to the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="value">The value.</param>
        [Theory]
        [MemberData(nameof(ConfigurationManagerTestData.BuiltInTypes),
            MemberType = typeof(ConfigurationManagerTestData))]
        public void IsTypeConvertionPossibleTest(Type type, object value)
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
                MethodInfo isTypeConvertionPossiblePrivateMethod = typeof(ConfigurationSettingsCollection)
                    .GetMethod("IsTypeConvertionPossible", BindingFlags.NonPublic | BindingFlags.Instance);
                bool isTypeConvertionPossibleMethodResultValue = (bool)isTypeConvertionPossiblePrivateMethod.Invoke(
                    new ConfigurationSettingsCollection(),
                    new object[] { value, type });
                Assert.True(isTypeConvertionPossibleMethodResultValue);
            }
        }

        /// <summary>
        /// Tests whether the provided values can be acquired from string as the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="value">The value.</param>
        [Theory]
        [MemberData(nameof(ConfigurationManagerTestData.BuiltInTypes),
            MemberType = typeof(ConfigurationManagerTestData))]
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
                        object singleValue = Convert.ChangeType(
                            ((float)value).ToString("R"), type);
                        Assert.Equal(singleValue, value);
                        break;
                    case nameof(Double):
                        object doubleValue = Convert.ChangeType(
                            ((double)value).ToString("R"), type);
                        Assert.Equal(doubleValue, value);
                        break;
                    case nameof(DateTime):
                        object dateTimeValue = Convert.ChangeType(
                            ((DateTime)value).ToString("o"), type);
                        Assert.Equal(dateTimeValue, value);
                        break;
                    default:
                        object convertedValue = Convert.ChangeType(
                            value.ToString(), type);
                        Assert.Equal(convertedValue, value);
                        break;
                }
            }
        }

        #endregion
    }
}
