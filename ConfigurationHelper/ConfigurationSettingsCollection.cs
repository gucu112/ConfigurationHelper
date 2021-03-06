﻿//-----------------------------------------------------------------------------------
// <copyright file="ConfigurationSettingsCollection.cs" company="Gucu112">
//     Copyright (c) Gucu112 2017-2019. All rights reserved.
// </copyright>
// <author>Bartlomiej Roszczypala</author>
//-----------------------------------------------------------------------------------

namespace Gucu112.ConfigurationHelper
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Text.RegularExpressions;

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

            string expandedValue = Environment.ExpandEnvironmentVariables(this[key]);

            if (IsConfigurationKeyValuePairEmpty(
                new KeyValuePair<string, string>(key, expandedValue)))
            {
                return null;
            }

            return expandedValue;
        }

        /// <summary>
        /// Gets the value for the specified key casted to the provided type or enum.
        /// </summary>
        /// <typeparam name="T">The type or the enum.</typeparam>
        /// <param name="key">The key.</param>
        /// <returns>The value.</returns>
        /// <exception cref="ArgumentException">key - Configuration value for particular key does not exists.</exception>
        /// <exception cref="InvalidOperationException">Cannot convert to specified type or enum.</exception>
        public T Get<T>(string key)
        {
            string value = Get(key);
            if (!IsTypeConvertionPossible(value, typeof(T)))
            {
                throw new InvalidOperationException($"Cannot convert to '{typeof(T).ToString()}' type.");
            }

            if (typeof(T).IsEnum)
            {
                if (int.TryParse(value, out int numericValue)
                    && Enum.IsDefined(typeof(T), numericValue))
                {
                    return (T)Enum.ToObject(typeof(T), numericValue);
                }

                if (!typeof(T).IsEnumDefined(value))
                {
                    throw new InvalidOperationException($"Cannot convert to '{typeof(T).ToString()}' type. Enum '{value}' is not defined.");
                }

                return (T)Enum.Parse(typeof(T), value);
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
        private bool IsTypeConvertionPossible(object value, Type type)
        {
            // Cannot change type if arguments are empty
            if (value == null || type == null)
            {
                return false;
            }

            // Can change type if it is enum
            if (type.IsEnum)
            {
                return true;
            }

            // Cannot change type if not convertible
            if (value as IConvertible == null)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Checks whether configuration key/value pair is empty and should return <c>null</c>.
        /// </summary>
        /// <param name="item">The expected key and expanded value pair.</param>
        /// <returns>
        ///   <c>true</c> if value is empty; otherwise, <c>false</c>.
        /// </returns>
        private bool IsConfigurationKeyValuePairEmpty(KeyValuePair<string, string> item)
        {
            // Value is not empty if not fully an environment varialble
            if (!item.Value.StartsWith("%") || !item.Value.EndsWith("%"))
            {
                return false;
            }

            // Value is empty if equals to environment variable name (also as snake case string)
            return item.Key.ToLowerInvariant() == item.Value.Trim('%').ToLowerInvariant()
                || TransformKeyValueToSnakeCase(item.Key) == item.Value.Trim('%').ToUpperInvariant();
        }

        /// <summary>
        /// Transforms key value using snake case variable naming convention.
        /// </summary>
        /// <example>
        ///     Transforms key value like this:
        ///     SnakeCaseUppercaseExample
        ///     into this:
        ///     SNAKE_CASE_UPPERCASE_EXAMPLE
        /// </example>
        /// <param name="key">The key value for transformation.</param>
        /// <returns>The uppercase key value as a snake case string.</returns>
        private string TransformKeyValueToSnakeCase(string key)
        {
            return new Regex(@"((?:[a-zA-Z0-9][a-z0-9]+)|(?:[A-Z0-9]+))")
                .Replace(key, "_$1").TrimStart('_').ToUpperInvariant();
        }

        #endregion
    }
}
