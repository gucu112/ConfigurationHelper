using System.Configuration;

namespace Gucu112.ConfigurationHelper
{
    public static class Config
    {
        #region Private fields

        /// <summary>
        /// The configuration.
        /// </summary>
        private static Configuration _config;

        /// <summary>
        /// The application settings section.
        /// </summary>
        private static AppSettingsSection _appSettingsSection;

        /// <summary>
        /// The data settings section.
        /// </summary>
        private static AppSettingsSection _dataSettingsSection;

        /// <summary>
        /// The connection strings section.
        /// </summary>
        private static ConnectionStringsSection _connectionStringsSection;

        #endregion

        #region Static constructor

        /// <summary>
        /// Initializes the <see cref="Config"/> class.
        /// </summary>
        static Config()
        {
            _config = ConfigurationManager.OpenExeConfiguration
                (ConfigurationUserLevel.None);
            _appSettingsSection = _config.AppSettings;
            _dataSettingsSection = (AppSettingsSection)
                _config.GetSection("dataSettings");
            _connectionStringsSection = _config.ConnectionStrings;
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
            get => _appSettingsSection;
        }

        /// <summary>
        /// Gets the application settings.
        /// </summary>
        /// <value>
        /// The application settings key-value collection.
        /// </value>
        public static KeyValueConfigurationCollection AppSettings
        {
            get => _appSettingsSection?.Settings;
        }

        /// <summary>
        /// Gets the data settings section.
        /// </summary>
        /// <value>
        /// The data settings section.
        /// </value>
        public static AppSettingsSection DataSettingsSection
        {
            get => _dataSettingsSection;
        }

        /// <summary>
        /// Gets the data settings.
        /// </summary>
        /// <value>
        /// The data settings as the key-value collection.
        /// </value>
        public static KeyValueConfigurationCollection DataSettings
        {
            get => _dataSettingsSection?.Settings;
        }

        /// <summary>
        /// Gets the application data section.
        /// </summary>
        /// <value>
        /// The application data section.
        /// </value>
        public static AppSettingsSection AppDataSection => DataSettingsSection;

        /// <summary>
        /// Gets the application data.
        /// </summary>
        /// <value>
        /// The application data as the key-value collection.
        /// </value>
        public static KeyValueConfigurationCollection AppData => DataSettings;

        /// <summary>
        /// Gets the connection strings section.
        /// </summary>
        /// <value>
        /// The connection strings section.
        /// </value>
        public static ConnectionStringsSection ConnectionStringsSection
        {
            get => _connectionStringsSection;
        }

        /// <summary>
        /// Gets the connection strings.
        /// </summary>
        /// <value>
        /// The connection strings collection.
        /// </value>
        public static ConnectionStringSettingsCollection ConnectionStrings
        {
            get => _connectionStringsSection?.ConnectionStrings;
        }

        #endregion
    }
}
