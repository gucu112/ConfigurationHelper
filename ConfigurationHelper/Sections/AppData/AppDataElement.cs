using System.Configuration;

namespace Gucu112.ConfigurationHelper.Sections.AppData
{
    public sealed class AppDataElement : ConfigurationElement
    {
        #region Public fields

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        [ConfigurationProperty("key")]
        public string Key
        {
            get => base["key"] as string;
            set => base["key"] = value;
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        [ConfigurationProperty("value")]
        public string Value
        {
            get => base["value"] as string;
            set => base["value"] = value;
        }

        /// <summary>
        /// Gets the collection.
        /// </summary>
        /// <value>
        /// The collection.
        /// </value>
        [ConfigurationProperty("", IsDefaultCollection = true)]
        public AppDataInnerCollection Collection
        {
            get => base[""] as AppDataInnerCollection;
        }

        #endregion
    }
}
