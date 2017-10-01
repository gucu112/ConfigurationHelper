using ConfigurationHelper;
using ConfigurationHelper.Extensions;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace ConfigurationRunner
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
            var test = Config.AppSettings["TestString"];
            Console.WriteLine(test);
        }

        /// <summary>
        /// Gets the byte code key.
        /// </summary>
        public static void GetByteCodeKey()
        {
            var data64 = Config.AppData.Get("Data64Value");
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
            var amount = Config.AppData.Get<float>("DataFloat");
            var currency = Config.AppSettings.Get<char>("TestChar");
            Console.WriteLine($"{amount:0.#}{currency}");
        }

        #endregion
    }
}
