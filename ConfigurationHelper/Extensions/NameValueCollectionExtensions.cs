using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationHelper.Extensions
{
    public static class NameValueCollectionExtensions
    {
        /// <summary>
        /// Gets the value for specified key casted to indicated type.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="key">The key.</param>
        /// <returns>The value.</returns>
        /// <exception cref="ArgumentException">Configuration value for particular key does not exists.</exception>
        /// <exception cref="Exception">Cannot convert to specified type.</exception>
        public static T Get<T>(this NameValueCollection collection, string key)
        {
            if (collection[key] == null)
            {
                throw new ArgumentException($"Configuration value for '{key}' key does not exists.", "key");
            }
            if (!CanChangeType(collection[key], typeof(T)))
            {
                throw new Exception($"Cannot convert to '{typeof(T).ToString()}' type.");
            }
            return (T)Convert.ChangeType(collection[key], typeof(T));
        }

        /// <summary>
        /// Determines whether the provided value can be converted to the specified value.
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
            if (value as IConvertible != null)
            {
                return true;
            }
            return false;
        }
    }
}
