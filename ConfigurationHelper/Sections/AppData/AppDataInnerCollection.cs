using System.Configuration;

namespace Gucu112.ConfigurationHelper.Sections.AppData
{
    [ConfigurationCollection(typeof(AppDataElement))]
    public sealed class AppDataInnerCollection : AppDataCollection
    {
        /// <summary>
        /// Gets the element key for a specified configuration element.
        /// </summary>
        /// <param name="element">The <see cref="T:System.Configuration.ConfigurationElement" /> to return the key for.</param>
        /// <returns>
        /// An <see cref="T:System.Object" /> that acts as the key for the specified <see cref="T:System.Configuration.ConfigurationElement" />.
        /// </returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return (element as AppDataElement)?.Value;
        }
    }
}