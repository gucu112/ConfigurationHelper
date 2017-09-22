using ConfigurationHelper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            ProgramExamples.GetConfigSetting();
            ProgramExamples.GetByteCodeKey();
            ProgramExamples.DatabaseConnection();
            Console.ReadKey();
        }
    }

    public class ProgramExamples
    {
        public static void GetConfigSetting()
        {
            var test = Config.AppSettings["TestString"];
            Console.WriteLine(test);
        }

        public static void GetByteCodeKey()
        {
            var data64 = Config.AppData["Data64Value"];
            Console.WriteLine(data64.Split(' ')
                .Aggregate((str, b) => $"{str}{(char)int.Parse(b)}"));
        }

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
    }
}
