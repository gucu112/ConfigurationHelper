//-----------------------------------------------------------------------------------
// <copyright file="AppDataInnerCollection.cs" company="Gucu112">
//     Copyright (c) Gucu112 2017-2018. All rights reserved.
// </copyright>
// <author>Bartlomiej Roszczypala</author>
//-----------------------------------------------------------------------------------

namespace Gucu112.ConfigurationHelper.Sections.AppData
{
    using System.Configuration;

    /// <summary>
    /// Represents a configuration element containing a collection of application data elements.
    /// </summary>
    /// <seealso cref="AppDataCollection" />
    [ConfigurationCollection(typeof(AppDataElement))]
    public sealed class AppDataInnerCollection : AppDataCollection
    {
        #region Inherited methods

        /// <summary>
        /// Gets the element key for a specified configuration element.
        /// </summary>
        /// <param name="element">The <see cref="ConfigurationElement" /> to return the key for.</param>
        /// <returns>
        /// An <see cref="object" /> that acts as the key for the specified <see cref="ConfigurationElement" />.
        /// </returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return (element as AppDataElement)?.Value;
        }

        #endregion
    }
}
