using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace ConfigurationHelper.Extensions
{
    public static class KeyValueConfigurationCollectionExtensions
    {
        #region Public methods

        /// <summary>
        /// Gets the value for the specified key.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <param name="key">The key.</param>
        /// <returns>The value.</returns>
        /// <exception cref="ArgumentNullException">collection - Collection cannot be null.</exception>
        /// <exception cref="ArgumentException">key - Configuration value for particular key does not exists.</exception>
        public static string Get(this KeyValueConfigurationCollection collection, string key)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("collection", "Collection cannot be null.");
            }
            if (collection[key] == null)
            {
                throw new ArgumentException($"Configuration value for '{key}' key does not exists.", "key");
            }
            return Environment.ExpandEnvironmentVariables(collection[key].Value);
        }

        /// <summary>
        /// Gets the value for the specified key casted to the provided type.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="key">The key.</param>
        /// <returns>The value.</returns>
        /// <exception cref="ArgumentNullException">collection - Collection cannot be null.</exception>
        /// <exception cref="ArgumentException">key - Configuration value for particular key does not exists.</exception>
        /// <exception cref="Exception">Cannot convert to specified type.</exception>
        public static T Get<T>(this KeyValueConfigurationCollection collection, string key)
        {
            var value = collection.Get(key);
            if (!CanChangeType(value, typeof(T)))
            {
                throw new Exception($"Cannot convert to '{typeof(T).ToString()}' type.");
            }
            return (T)Convert.ChangeType(value, typeof(T));
        }

        /// <summary>
        /// Converts <see cref="KeyValueConfigurationCollection"/> to the dictionary.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <returns>The dictionary.</returns>
        public static IDictionary<string, string> ToDictionary(this KeyValueConfigurationCollection collection)
        {
            if (collection == null)
            {
                return new Dictionary<string, string>();
            }
            return collection.AllKeys.ToDictionary
                (key => key, key => collection.Get(key));
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
        private static bool CanChangeType(object value, Type type)
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
