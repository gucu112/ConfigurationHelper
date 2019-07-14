//-----------------------------------------------------------------------------------
// <copyright file="ConfigurationSettingsCollection.cs" company="Gucu112">
//     Copyright (c) Gucu112 2017-2018. All rights reserved.
// </copyright>
// <author>Bartlomiej Roszczypala</author>
//-----------------------------------------------------------------------------------

namespace Gucu112.ConfigurationHelper
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;

    /// <summary>
    /// Represents a configuration element containing a collection of configuration settings.
    /// </summary>
    /// <seealso cref="KeyValueConfigurationCollection" />
    public class ConfigurationSettingsCollection : KeyValueConfigurationCollection
    {
        #region Public constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationSettingsCollection" /> class.
        /// </summary>
        public ConfigurationSettingsCollection() : base()
        {
        }

        #endregion

        #region Internal constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationSettingsCollection" /> class.
        /// </summary>
        /// <param name="collection">The collection.</param>
        internal ConfigurationSettingsCollection(KeyValueConfigurationCollection collection)
        {
            if (collection == null)
            {
                return;
            }

            foreach (KeyValueConfigurationElement element in collection)
            {
                Add(element);
            }
        }

        #endregion

        #region Public indexers

        /// <summary>
        /// Gets the configuration setting based on the specified key.
        /// </summary>
        /// <value>
        /// The <see cref="string" />.
        /// </value>
        /// <param name="key">The key.</param>
        /// <returns>The configuration setting.</returns>
        public new string this[string key]
        {
            get
            {
                return base[key]?.Value;
            }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Gets the value for the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>The value.</returns>
        /// <exception cref="ArgumentException">key - Configuration value for particular key does not exists.</exception>
        public string Get(string key)
        {
            if (this[key] == null)
            {
                throw new ArgumentException($"Configuration value for '{key}' key does not exists.", "key");
            }

            return Environment.ExpandEnvironmentVariables(this[key]);
        }

        /// <summary>
        /// Gets the value for the specified key casted to the provided type.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="key">The key.</param>
        /// <returns>The value.</returns>
        /// <exception cref="ArgumentException">key - Configuration value for particular key does not exists.</exception>
        /// <exception cref="Exception">Cannot convert to specified type.</exception>
        public T Get<T>(string key)
        {
            string value = Get(key);
            if (!CanChangeType(value, typeof(T)))
            {
                throw new Exception($"Cannot convert to '{typeof(T).ToString()}' type.");
            }

            return (T)Convert.ChangeType(value, typeof(T));
        }

        /// <summary>
        /// Converts <see cref="ConfigurationSettingsCollection" /> to the list.
        /// </summary>
        /// <returns>The list.</returns>
        public IList<string> ToList()
        {
            return AllKeys.Select(key => Get(key)).ToList();
        }

        /// <summary>
        /// Converts <see cref="ConfigurationSettingsCollection" /> to the dictionary.
        /// </summary>
        /// <returns>The dictionary.</returns>
        public IDictionary<string, string> ToDictionary()
        {
            return AllKeys.ToDictionary(key => key, key => Get(key));
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Determines whether the specified value can be converted to the provided type.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="type">The type.</param>
        /// <returns>
        ///   <c>true</c> if the value can be converted; otherwise, <c>false</c>.
        /// </returns>
        private bool CanChangeType(object value, Type type)
        {
            // Cannot change type if arguments are empty
            if (value == null || type == null)
            {
                return false;
            }

            // Cannot change type if not convertible
            if (value as IConvertible == null)
            {
                return false;
            }

            return true;
        }

        #endregion
    }
}
