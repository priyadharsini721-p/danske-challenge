using System;
using System.Configuration;
using System.Threading.Tasks;
using TaxCalculator.ConsoleApp.Utilities;
using TaxCalculator.Entities;

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
            Console.Clear();
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1) Get Tax");
            Console.WriteLine("2) Add Municipality");
            Console.WriteLine("3) Modify Municipality");
            Console.Write("\r\nSelect an option: ");

            switch (Console.ReadLine())
            {
                case "1":
                    Console.Write("Enter the municipality name: ");
                    string name = Console.ReadLine();
                    while (!await TaxProcessor.CheckIfExists(baseUrl, name))
                    {
                        Console.WriteLine("Not a valid municipality, try again.");
                        name = Console.ReadLine();
                    }
                    Console.Write("Enter the date to calculate tax: ");
                    string day = Console.ReadLine();
                    DateTime date; DateTime.TryParse(day, out date);
                    while (!DateTime.TryParse(day, out date))
                    {
                        Console.WriteLine("Not a valid number, try again.");
                        day = Console.ReadLine();
                    }

                    Municipalities result = await TaxProcessor.GetTaxForMunicipality(baseUrl, name, date);
                    Console.Clear();
                    Console.WriteLine("---Output---");
                    Console.WriteLine("{0} | {1} | {2} | {3}", "Municipality", "Date", "Tax rule", "Result");
                    Console.WriteLine("{0} | {1} | {2} | {3}", name, day, result.TaxRule, result.Tax);
                    Console.Write("\r\nPress Enter to return to Main Menu");
                    Console.ReadLine();
                    return true;
                case "2":
                    return true;
                case "3":
                    return true;
                default:
                    return true;
            }
        }
    }
}
