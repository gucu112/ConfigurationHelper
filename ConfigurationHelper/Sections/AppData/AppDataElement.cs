//-----------------------------------------------------------------------------------
// <copyright file="AppDataElement.cs" company="Gucu112">
//     Copyright (c) Gucu112 2017-2018. All rights reserved.
// </copyright>
// <author>Bartlomiej Roszczypala</author>
//-----------------------------------------------------------------------------------

namespace Gucu112.ConfigurationHelper.Sections.AppData
{
    using System.Configuration;

    /// <summary>
    /// Represents a configuration element within an application data collection.
    /// </summary>
    /// <seealso cref="ConfigurationElement" />
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
            get => base[string.Empty] as AppDataInnerCollection;
        }

        #endregion
    }
}
