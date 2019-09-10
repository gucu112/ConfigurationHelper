//-----------------------------------------------------------------------------------
// <copyright file="ConfigurationManager.cs" company="Gucu112">
//     Copyright (c) Gucu112 2017-2019. All rights reserved.
// </copyright>
// <author>Bartlomiej Roszczypala</author>
//-----------------------------------------------------------------------------------

namespace Gucu112.ConfigurationHelper
{
    using System.Configuration;
    using Gucu112.ConfigurationHelper.Sections.AppData;

    /// <summary>
    /// Provides access to configuration file for application.
    /// This class cannot be inherited.
    /// </summary>
    public static class ConfigurationManager
    {
        #region Private fields

        /// <summary>
        /// The configuration.
        /// </summary>
        private static Configuration configuration;

        #endregion

        #region Static constructor

        /// <summary>
        /// Initializes static members of the <see cref="ConfigurationManager" /> class.
        /// </summary>
        static ConfigurationManager()
        {
            configuration = System.Configuration.ConfigurationManager
                .OpenExeConfiguration(ConfigurationUserLevel.None);
            AppSettingsSection = configuration.AppSettings;
            ConnectionStringsSection = configuration.ConnectionStrings;
            ServerSettingsSection = (AppSettingsSection)configuration
                .GetSection("serverSettings");
            DataSettingsSection = (AppSettingsSection)configuration
                .GetSection("dataSettings");
            AppDataSection = (AppDataSection)configuration
                .GetSection("appData");
        }

        #endregion

        #region Public fields

        /// <summary>
        /// Gets the application settings section.
        /// </summary>
        /// <value>
        /// The application settings section.
        /// </value>
        public static AppSettingsSection AppSettingsSection { get; private set; }

        /// <summary>
        /// Gets the application settings.
        /// </summary>
        /// <value>
        /// The application settings key/value collection.
        /// </value>
        public static ConfigurationSettingsCollection AppSettings
        {
            get => new ConfigurationSettingsCollection(AppSettingsSection?.Settings);
        }

        /// <summary>
        /// Gets the connection strings section.
        /// </summary>
        /// <value>
        /// The connection strings section.
        /// </value>
        public static ConnectionStringsSection ConnectionStringsSection { get; private set; }

        /// <summary>
        /// Gets the connection strings.
        /// </summary>
        /// <value>
        /// The connection strings collection.
        /// </value>
        public static ConnectionStringSettingsCollection ConnectionStrings
        {
            get => ConnectionStringsSection?.ConnectionStrings;
        }

        /// <summary>
        /// Gets the server settings section.
        /// </summary>
        /// <value>
        /// The server settings section.
        /// </value>
        public static AppSettingsSection ServerSettingsSection { get; private set; }

        /// <summary>
        /// Gets the server settings.
        /// </summary>
        /// <value>
        /// The server settings as the key/value collection.
        /// </value>
        public static ConfigurationSettingsCollection ServerSettings
        {
            get => new ConfigurationSettingsCollection(ServerSettingsSection?.Settings);
        }

        /// <summary>
        /// Gets the data settings section.
        /// </summary>
        /// <value>
        /// The data settings section.
        /// </value>
        public static AppSettingsSection DataSettingsSection { get; private set; }

        /// <summary>
        /// Gets the data settings.
        /// </summary>
        /// <value>
        /// The data settings as the key/value collection.
        /// </value>
        public static ConfigurationSettingsCollection DataSettings
        {
            get => new ConfigurationSettingsCollection(DataSettingsSection?.Settings);
        }

        /// <summary>
        /// Gets the application data section.
        /// </summary>
        /// <value>
        /// The application data section.
        /// </value>
        public static AppDataSection AppDataSection { get; private set; }

        /// <summary>
        /// Gets the application data.
        /// </summary>
        /// <value>
        /// The application data as the key/value collection.
        /// </value>
        public static ConfigurationSettingsCollection AppData
        {
            get => new ConfigurationSettingsCollection(AppDataSection?.Settings);
        }

        #endregion
    }
}
