//-----------------------------------------------------------------------------------
// <copyright file="AppDataCollection.cs" company="Gucu112">
//     Copyright (c) Gucu112 2017-2019. All rights reserved.
// </copyright>
// <author>Bartlomiej Roszczypala</author>
//-----------------------------------------------------------------------------------

namespace Gucu112.ConfigurationHelper.Sections.AppData
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;

    /// <summary>
    /// Represents a configuration element containing a collection of <see cref="AppDataInnerCollection" /> collections.
    /// </summary>
    /// <seealso cref="ConfigurationElementCollection" />
    /// <seealso cref="IEnumerable{AppDataElement}" />
    [ConfigurationCollection(typeof(AppDataElement))]
    public class AppDataCollection : ConfigurationElementCollection, IEnumerable<AppDataElement>
    {
        #region Public indexers

        /// <summary>
        /// Gets the application data element at the specified index location.
        /// </summary>
        /// <value>
        /// The <see cref="AppDataElement" />.
        /// </value>
        /// <param name="index">The index.</param>
        /// <returns>The application data element.</returns>
        public AppDataElement this[int index]
        {
            get => BaseGet(index) as AppDataElement;
        }

        /// <summary>
        /// Gets the application data property with the specified key.
        /// </summary>
        /// <value>
        /// The <see cref="AppDataCollection" /> if there is more than one value; otherwise, <see cref="string" />.
        /// </value>
        /// <param name="key">The key.</param>
        /// <returns>The application data collection or element.</returns>
        public new dynamic this[string key]
        {
            get
            {
                AppDataElement element = BaseGet(key) as AppDataElement;
                if (element?.Collection?.Any() ?? false)
                {
                    return element.Collection;
                }

                return element?.Value;
            }
        }

        #endregion

        #region Public operators

        /// <summary>
        /// Performs an explicit conversion from <see cref="AppDataCollection" /> to <see cref="KeyValueConfigurationCollection" />.
        /// </summary>
        /// <param name="collection">The application data collection.</param>
        /// <returns>
        /// A collection of key/value pairs that contains the application data from the configuration file.
        /// </returns>
        public static explicit operator KeyValueConfigurationCollection(AppDataCollection collection)
        {
            return collection.Aggregate(
                new KeyValueConfigurationCollection(),
                (newCollection, element) =>
                {
                    newCollection.Add(new KeyValueConfigurationElement(element.Key, element.Value));
                    return newCollection;
                });
        }

        #endregion

        #region Inherited methods

        /// <summary>
        /// Returns an enumerator that iterates through the collection of an application data.
        /// </summary>
        /// <returns>
        /// An enumerator that can be used to iterate through the collection of an application data.
        /// </returns>
        IEnumerator<AppDataElement> IEnumerable<AppDataElement>.GetEnumerator()
        {
            return Enumerable.Range(0, Count)
                .Select(i => this[i]).GetEnumerator();
        }

        /// <summary>
        /// When overridden in a derived class, creates a new <see cref="ConfigurationElement" />.
        /// </summary>
        /// <returns>
        /// A newly created <see cref="ConfigurationElement" />.
        /// </returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new AppDataElement();
        }

        /// <summary>
        /// Gets the element key for a specified configuration element when overridden in a derived class.
        /// </summary>
        /// <param name="element">The <see cref="ConfigurationElement" /> to return the key for.</param>
        /// <returns>
        /// An <see cref="object" /> that acts as the key for the specified <see cref="ConfigurationElement" />.
        /// </returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return (element as AppDataElement)?.Key;
        }

        #endregion
    }
}
