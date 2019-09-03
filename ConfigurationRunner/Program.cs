//-----------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Gucu112">
//     Copyright (c) Gucu112 2017-2018. All rights reserved.
// </copyright>
// <author>Bartlomiej Roszczypala</author>
//-----------------------------------------------------------------------------------

namespace Gucu112.ConfigurationHelper.Runner
{
    using System.Globalization;
    using System.Threading;

    /// <summary>
    /// Represents console program.
    /// </summary>
    public class Program
    {
        #region Main method

        /// <summary>
        /// Main method of the program.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public static void Main(string[] args)
        {
            // Change current culture
            Thread.CurrentThread.CurrentCulture = new CultureInfo(ConfigurationManager.AppSettings.Get("DefaultCulture"));

            // Run program examples
            ProgramExamples.GetConfigSetting();
            ProgramExamples.GetConfigNullSetting();
            ProgramExamples.DatabaseConnection();
            ProgramExamples.GetSecureShellAddress();
            ProgramExamples.GetByteCodeKey();
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
}
