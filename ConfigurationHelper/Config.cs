using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationHelper
{
    public static class Config
    {
        /// <summary>
        /// Gets the application settings.
        /// </summary>
        /// <value>
        /// The application settings object.
        /// </value>
        public static NameValueCollection AppSettings
        {
            get
            {
                return ConfigurationManager.AppSettings;
            }
        }

        /// <summary>
        /// Gets the application data (alias for data settings).
        /// </summary>
        /// <value>
        /// The data settings object.
        /// </value>
        public static NameValueCollection AppData => DataSettings;

        /// <summary>
        /// Gets the data settings.
        /// </summary>
        /// <value>
        /// The data settings object.
        /// </value>
        public static NameValueCollection DataSettings
        {
            get
            {
                return (NameValueCollection)
                    ConfigurationManager.GetSection("dataSettings");
            }
        }

        /// <summary>
        /// Gets the connection strings.
        /// </summary>
        /// <value>
        /// The connection strings.
        /// </value>
        public static ConnectionStringSettingsCollection ConnectionStrings
        {
            get
            {
                return ConfigurationManager.ConnectionStrings;
            }
        }
    }
}
