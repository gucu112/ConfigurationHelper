using Gucu112.ConfigurationHelper.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace Gucu112.ConfigurationHelper.Runner
{
    class Program
    {
        #region Main method

        static void Main(string[] args)
        {
            // Change current culture
            Thread.CurrentThread.CurrentCulture = new CultureInfo(Config.AppSettings.Get("DefaultCulture"));
            // Run program examples
            ProgramExamples.GetConfigSetting();
            ProgramExamples.GetByteCodeKey();
            ProgramExamples.DatabaseConnection();
            ProgramExamples.GetCastedValue();
            ProgramExamples.GetConfigFromAppData();
#if DEBUG
            // Wait 3 seconds while debugging
            Thread.Sleep(3000);
#else
            // Wait for user input
            Console.Write("Press any key to continue...");
            Console.ReadKey();
#endif
        }

        #endregion
    }

    public class ProgramExamples
    {
        #region Public methods

        /// <summary>
        /// Gets the configuration setting.
        /// </summary>
        public static void GetConfigSetting()
        {
            var test = Config.AppSettings["TestString"].Value;
            Console.WriteLine(test);
        }

        /// <summary>
        /// Gets the byte code key.
        /// </summary>
        public static void GetByteCodeKey()
        {
            var data64 = Config.DataSettings.Get("Data64Value");
            Console.WriteLine(data64.Split(' ')
                .Aggregate((str, b) => $"{str}{(char)int.Parse(b)}"));
        }

        /// <summary>
        /// Establishes connection to the database.
        /// </summary>
        public static void DatabaseConnection()
        {
            using (var connection = new SqlConnection
                (Config.ConnectionStrings["DatabaseConnectionString"].ConnectionString))
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
        /// Gets the casted value.
        /// </summary>
        public static void GetCastedValue()
        {
            var amount = Config.DataSettings.Get<float>("DataFloat");
            var currency = Config.AppSettings.Get<char>("TestChar");
            Console.WriteLine($"{amount:0.#}{currency}");
        }

        /// <summary>
        /// Gets the configuration from application data.
        /// </summary>
        public static void GetConfigFromAppData()
        {
            // Simple data
            var collection = Config.AppDataSection.Collection;
            Console.WriteLine($"Non-Existing: {collection["NonExisting"] ?? "null"}");
            Console.WriteLine($"EmptyValue: '{collection["EmptyValue"]}'");
            Console.WriteLine($"Value: {collection["Value"]}");

            // Empty list
            var emptyList = Config.AppDataSection.GetList("EmptyCollection");
            ShowEnumeratorData("EmptyList", emptyList);
            // Single element list
            var singleElementList = Config.AppDataSection.GetList("SingleElementList");
            ShowEnumeratorData("SingleElementList", singleElementList);
            // Multiple elements list
            var multipleElementsList = Config.AppDataSection.GetList<double>("MultipleElementsList");
            ShowEnumeratorData("MultipleElementsList", multipleElementsList);
            Console.WriteLine($"MultipleElementsList[1]: {multipleElementsList[1]}");

            // Empty dictionary
            var emptyDictionary = Config.AppDataSection.GetDictionary("EmptyCollection");
            ShowEnumeratorData("EmptyDictionary", emptyDictionary);
            // Single element dictionary
            var singleElementDictionary = Config.AppDataSection.GetDictionary("SingleElementDictionary");
            ShowEnumeratorData("SingleElementDictionary", singleElementDictionary,
                e => $"{e.Key}: {e.Value}");
            // Multiple elements dictionary
            var multipleElementsDictionary = Config.AppDataSection.GetDictionary<int>("MultipleElementsDictionary");
            ShowEnumeratorData("MultipleElementsDictionary", multipleElementsDictionary,
                e => $"{e.Key}: {e.Value}");
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
        private static void ShowEnumeratorData<T>(string name, IEnumerable<T> enumerator,
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
                foreach (var item in enumerator)
                {
                    Console.WriteLine($"  > {toString(item)}");
                }
            }
        }

        #endregion
    }
}
