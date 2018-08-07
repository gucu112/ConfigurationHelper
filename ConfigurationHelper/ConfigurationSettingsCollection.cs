using System.Configuration;

namespace Gucu112.ConfigurationHelper
{
    public class ConfigurationSettingsCollection : KeyValueConfigurationCollection
    {
        #region Public constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationSettingsCollection"/> class.
        /// </summary>
        public ConfigurationSettingsCollection() : base() { }

        #endregion

        #region Internal constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationSettingsCollection"/> class.
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
        /// The <see cref="string"/>.
        /// </value>
        /// <param name="key">The key.</param>
        /// <returns>The configuration setting.</returns>
        public new string this[string key]
        {
            get
            {
                return base[key].Value;
            }
        }

        #endregion
    }
}
