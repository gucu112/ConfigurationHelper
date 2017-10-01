using System.Configuration;

namespace ConfigurationHelper
{
    public static class Config
    {
        #region Private fields

        /// <summary>
        /// The configuration
        /// </summary>
        private static Configuration _config;

        /// <summary>
        /// The application settings
        /// </summary>
        private static AppSettingsSection _appSettings;

        /// <summary>
        /// The data settings
        /// </summary>
        private static AppSettingsSection _dataSettings;

        /// <summary>
        /// The connection strings
        /// </summary>
        private static ConnectionStringsSection _connectionStringSettings;

        #endregion

        #region Static constructor

        /// <summary>
        /// Initializes the <see cref="Config"/> class.
        /// </summary>
        static Config()
        {
            _config = ConfigurationManager.OpenExeConfiguration
                (ConfigurationUserLevel.None);
            _appSettings = _config.AppSettings;
            _dataSettings = (AppSettingsSection)
                _config.GetSection("dataSettings");
            _connectionStringSettings = _config.ConnectionStrings;
        }

        #endregion

        #region Public fields

        /// <summary>
        /// Gets the application settings.
        /// </summary>
        /// <value>
        /// The application settings key-value collection.
        /// </value>
        public static KeyValueConfigurationCollection AppSettings
        {
            get => _appSettings?.Settings;
        }

        /// <summary>
        /// Gets the application data (alias for data settings).
        /// </summary>
        /// <value>
        /// The data settings key-value collection.
        /// </value>
        public static KeyValueConfigurationCollection AppData => DataSettings;

        /// <summary>
        /// Gets the data settings.
        /// </summary>
        /// <value>
        /// The data settings key-value collection.
        /// </value>
        public static KeyValueConfigurationCollection DataSettings
        {
            get => _dataSettings?.Settings;
        }

        /// <summary>
        /// Gets the connection strings.
        /// </summary>
        /// <value>
        /// The connection strings.
        /// </value>
        public static ConnectionStringSettingsCollection ConnectionStrings
        {
            get => _connectionStringSettings?.ConnectionStrings;
        }

        #endregion
    }
}
