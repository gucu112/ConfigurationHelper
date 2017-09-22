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
        public static NameValueCollection AppSettings
        {
            get
            {
                return ConfigurationManager.AppSettings;
            }
        }

        public static NameValueCollection AppData => DataSettings;

        public static NameValueCollection DataSettings
        {
            get
            {
                return (NameValueCollection)
                    ConfigurationManager.GetSection("dataSettings");
            }
        }

        public static ConnectionStringSettingsCollection ConnectionStrings
        {
            get
            {
                return ConfigurationManager.ConnectionStrings;
            }
        }
    }
}
