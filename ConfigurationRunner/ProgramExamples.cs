//-----------------------------------------------------------------------------------
// <copyright file="ProgramExamples.cs" company="Gucu112">
//     Copyright (c) Gucu112 2017-2018. All rights reserved.
// </copyright>
// <author>Bartlomiej Roszczypala</author>
//-----------------------------------------------------------------------------------

namespace Gucu112.ConfigurationHelper.Runner
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;

    /// <summary>
    /// Contains console program example functions.
    /// </summary>
    public class ProgramExamples
    {
        #region Public methods

        /// <summary>
        /// Gets the configuration setting.
        /// </summary>
        public static void GetConfigSetting()
        {
            string test = ConfigurationManager.AppSettings["TestString"];
            Console.WriteLine(test);
        }

        /// <summary>
        /// Establishes connection to the database.
        /// </summary>
        public static void DatabaseConnection()
        {
            using (SqlConnection connection = new SqlConnection(
                ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString))
            {
                try
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        Console.WriteLine("Gaining access to the database...");
                        connection.Open();
                    }
                }
                catch (SqlException exception)
                {
                    Console.Error.WriteLine("Error while trying to connect!");
                    Console.Error.WriteLine(exception.ToString());
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                        Console.WriteLine("Connection closed.");
                    }
                }
            }
        }

        /// <summary>
        /// Gets the secure shell address.
        /// </summary>
        public static void GetSecureShellAddress()
        {
            string ip = ConfigurationManager.ServerSettings.Get("SecureShellAddress");
            Console.WriteLine(ip);
        }

        /// <summary>
        /// Gets the byte code key.
        /// </summary>
        public static void GetByteCodeKey()
        {
            string data64 = ConfigurationManager.DataSettings.Get("Data64Value");
            Console.WriteLine(data64.Split(' ')
                .Aggregate((str, b) => $"{str}{(char)int.Parse(b)}"));
        }

        /// <summary>
        /// Gets the casted value.
        /// </summary>
        public static void GetCastedValue()
        {
            float amount = ConfigurationManager.DataSettings.Get<float>("DataFloat");
            char currency = ConfigurationManager.AppSettings.Get<char>("TestChar");
            Console.WriteLine($"{amount:0.#}{currency}");
        }

        /// <summary>
        /// Gets the configuration from application data.
        /// </summary>
        public static void GetConfigFromAppData()
        {
            // Simple data
            ConfigurationSettingsCollection collection = ConfigurationManager.AppData;
            Console.WriteLine($"Non-Existing: {collection["NonExisting"] ?? "null"}");
            Console.WriteLine($"EmptyValue: '{collection["EmptyValue"]}'");
            Console.WriteLine($"Value: {collection["Value"]}");

            // Empty list
            IList<string> emptyList = ConfigurationManager.AppDataSection.GetList("EmptyCollection");
            ShowEnumeratorData("EmptyList", emptyList);

            // Single element list
            IList<string> singleElementList = ConfigurationManager.AppDataSection.GetList("SingleElementList");
            ShowEnumeratorData("SingleElementList", singleElementList);

            // Multiple elements list
            IList<double> multipleElementsList = ConfigurationManager.AppDataSection.GetList<double>("MultipleElementsList");
            ShowEnumeratorData("MultipleElementsList", multipleElementsList);
            Console.WriteLine($"MultipleElementsList[1]: {multipleElementsList[1]}");

            // Empty dictionary
            IDictionary<string, string> emptyDictionary = ConfigurationManager.AppDataSection.GetDictionary("EmptyCollection");
            ShowEnumeratorData("EmptyDictionary", emptyDictionary);

            // Single element dictionary
            IDictionary<string, string> singleElementDictionary = ConfigurationManager.AppDataSection.GetDictionary("SingleElementDictionary");
            ShowEnumeratorData("SingleElementDictionary", singleElementDictionary, e => $"{e.Key}: {e.Value}");

            // Multiple elements dictionary
            IDictionary<string, int> multipleElementsDictionary = ConfigurationManager.AppDataSection.GetDictionary<int>("MultipleElementsDictionary");
            ShowEnumeratorData("MultipleElementsDictionary", multipleElementsDictionary, e => $"{e.Key}: {e.Value}");
            Console.WriteLine($"MultipleElementsDictionary[\"Second\"]: {multipleElementsDictionary["Second"]}");
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Shows the enumerator details and elements.
        /// </summary>
        /// <typeparam name="T">The type of enumerator elements.</typeparam>
        /// <param name="name">The name of enumerator.</param>
        /// <param name="enumerator">The enumerator.</param>
        /// <param name="toString">Method that converts enumerator element to string.</param>
        private static void ShowEnumeratorData<T>(
            string name,
            IEnumerable<T> enumerator,
            Func<T, string> toString = null)
        {
            Console.WriteLine($"{name}:");
            Console.WriteLine($"# Type: {enumerator.GetType()}");
            Console.WriteLine($"# Count: {enumerator.Count()}");
            if (enumerator.Any())
            {
                Console.WriteLine("# Items:");
                if (toString == null)
                {
                    toString = e => e.ToString();
                }

                foreach (T item in enumerator)
                {
                    Console.WriteLine($"  > {toString(item)}");
                }
            }
        }

        #endregion
    }
}
