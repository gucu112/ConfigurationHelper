using Gucu112.ConfigurationHelper.Sections.AppData;
using System.Configuration;

namespace Gucu112.ConfigurationHelper
{
    public static class Config
    {
        #region Private fields

        /// <summary>
        /// The configuration.
        /// </summary>
        private static Configuration config;

        /// <summary>
        /// The application settings section.
        /// </summary>
        private static AppSettingsSection appSettingsSection;

        /// <summary>
        /// The data settings section.
        /// </summary>
        private static AppSettingsSection dataSettingsSection;

        /// <summary>
        /// The application data section.
        /// </summary>
        private static AppDataSection appDataSection;

        /// <summary>
        /// The connection strings section.
        /// </summary>
        private static ConnectionStringsSection connectionStringsSection;

        #endregion

        #region Static constructor

        /// <summary>
        /// Initializes the <see cref="Config"/> class.
        /// </summary>
        static Config()
        {
            config = ConfigurationManager.OpenExeConfiguration
                (ConfigurationUserLevel.None);
            appSettingsSection = config.AppSettings;
            dataSettingsSection = (AppSettingsSection)
                config.GetSection("dataSettings");
            appDataSection = (AppDataSection)
                config.GetSection("appData");
            connectionStringsSection = config.ConnectionStrings;
        }

        #endregion

        #region Public fields

        /// <summary>
        /// Gets the application settings section.
        /// </summary>
        /// <value>
        /// The application settings section.
        /// </value>
        public static AppSettingsSection AppSettingsSection
        {
            get => appSettingsSection;
        }

        /// <summary>
        /// Gets the application settings.
        /// </summary>
        /// <value>
        /// The application settings key/value collection.
        /// </value>
        public static KeyValueConfigurationCollection AppSettings
        {
            get => appSettingsSection?.Settings;
        }

        /// <summary>
        /// Gets the data settings section.
        /// </summary>
        /// <value>
        /// The data settings section.
        /// </value>
        public static AppSettingsSection DataSettingsSection
        {
            get => dataSettingsSection;
        }

        /// <summary>
        /// Gets the data settings.
        /// </summary>
        /// <value>
        /// The data settings as the key/value collection.
        /// </value>
        public static KeyValueConfigurationCollection DataSettings
        {
            get => dataSettingsSection?.Settings;
        }

        /// <summary>
        /// Gets the application data section.
        /// </summary>
        /// <value>
        /// The application data section.
        /// </value>
        public static AppDataSection AppDataSection
        {
            get => appDataSection;
        }

        /// <summary>
        /// Gets the application data.
        /// </summary>
        /// <value>
        /// The application data as the key/value collection.
        /// </value>
        public static KeyValueConfigurationCollection AppData
        {
            get => appDataSection?.Settings;
        }

        /// <summary>
        /// Gets the connection strings section.
        /// </summary>
        /// <value>
        /// The connection strings section.
        /// </value>
        public static ConnectionStringsSection ConnectionStringsSection
        {
            get => connectionStringsSection;
        }

        /// <summary>
        /// Gets the connection strings.
        /// </summary>
        /// <value>
        /// The connection strings collection.
        /// </value>
        public static ConnectionStringSettingsCollection ConnectionStrings
        {
            get => connectionStringsSection?.ConnectionStrings;
        }

        #endregion
    }
}
