//-----------------------------------------------------------------------------------
// <copyright file="ConfigurationSettingsCollection.cs" company="Gucu112">
//     Copyright (c) Gucu112 2017-2018. All rights reserved.
// </copyright>
// <author>Bartlomiej Roszczypala</author>
//-----------------------------------------------------------------------------------

namespace Gucu112.ConfigurationHelper
{
    using System.Configuration;

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
                this.Add(element);
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
    }
}
