using ConfigurationHelper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            var test = Config.AppSettings["test"];
            Console.WriteLine(test);

            var data64 = Config.DataSettings["data64"];
            Console.WriteLine(data64.Split(' ')
                .Aggregate((str, b) => $"{str}{(char)int.Parse(b)}"));

            Console.ReadKey();
        }
    }
}
