using System;
using System.Configuration;
using System.Threading.Tasks;
using TaxCalculator.ConsoleApp.Utilities;

namespace TaxCalculator.ConsoleApp
{
    class Program
    {
        static string path;
        static string baseUrl;

        static void Main(string[] args)
        {
            baseUrl = ConfigurationManager.AppSettings["TaxCalcApiUrl"];
            path = ConfigurationManager.AppSettings["LogPath"];

            Task t = MainAsync(args);
            t.Wait();            
        }

        static async Task MainAsync(string[] args)
        {
            try
            {
                bool showMenu = true;
                while (showMenu)
                {
                    showMenu = await MainMenu();
                }
            }
            catch(Exception ex)
            {
                LogInfo.WriteLog(path, ex);
                Console.WriteLine("Error occured while processing your request.");
            }
        }

        private static async Task<bool> MainMenu()
        {
            TaxComponent _tax = new TaxComponent();
            switch (Console.ReadLine())
            {
                case "1":
                    return await _tax.GetTaxComponent(baseUrl);
                case "2":
                    return await _tax.AddMunicipalityComponent(baseUrl);
                case "3":
                    return await _tax.UpdateMunicipalityComponent(baseUrl);
                default:
                    return false;
            }
        }
    }
}
