using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Gucu112.ConfigurationHelper.Sections.AppData
{
    public sealed class AppDataSection : ConfigurationSection
    {
        #region Public fields

        /// <summary>
        /// Gets the default collection of application data section.
        /// </summary>
        /// <value>
        /// The default application data collection.
        /// </value>
        [ConfigurationProperty("", IsDefaultCollection = true)]
        public AppDataCollection Collection
        {
            get => base[""] as AppDataCollection;
        }

        /// <summary>
        /// Gets a collection of key/value pairs that contains application data.
        /// </summary>
        /// <value>
        /// A collection of key/value pairs that contains the application data from the configuration file.
        /// </value>
        public KeyValueConfigurationCollection Settings
        {
            get => (KeyValueConfigurationCollection)Collection;
        }

        /// <summary>
        /// Gets the application data properties within the specified key as a list of strings.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>
        /// A list of values that contains the internal properties from particular application data property.
        /// </returns>
        public IList<string> GetList(string key)
        {
            if (!(Collection[key] is AppDataCollection collection))
            {
                return new List<string>();
            }
            return collection.Select(i => i.Value).ToList();
        }

        /// <summary>
        /// Gets the application data properties within the specified key as a list of values casted to the provided type.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="key">The key.</param>
        /// <returns>
        /// A list of values that contains the internal properties from particular application data property.
        /// </returns>
        public IList<T> GetList<T>(string key)
        {
            if (!(Collection[key] is AppDataCollection collection))
            {
                return new List<T>();
            }
            return collection.Select(i => (T)Convert.ChangeType(i.Value, typeof(T))).ToList();
        }

        /// <summary>
        /// Gets the application data properties within the specified key as a dictionary of strings.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>
        /// A dictionary of values that contains the internal properties from particular application data property.
        /// </returns>
        public IDictionary<string, string> GetDictionary(string key)
        {
            if (!(Collection[key] is AppDataCollection collection))
            {
                return new Dictionary<string, string>();
            }
            return collection.ToDictionary(e => e.Key, e => e.Value);
        }

        /// <summary>
        /// Gets the application data properties within the specified key as a dictionary of values casted to the provided type.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="key">The key.</param>
        /// <returns>
        /// A dictionary of values that contains the internal properties from particular application data property.
        /// </returns>
        public IDictionary<string, T> GetDictionary<T>(string key)
        {
            if (!(Collection[key] is AppDataCollection collection))
            {
                return new Dictionary<string, T>();
            }
            return collection.ToDictionary(e => e.Key, e => (T)Convert.ChangeType(e.Value, typeof(T)));
        }

        #endregion
    }
}
