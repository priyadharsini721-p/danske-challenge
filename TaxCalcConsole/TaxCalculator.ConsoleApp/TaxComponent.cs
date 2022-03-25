using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.Entities;
using System.Linq;

namespace TaxCalculator.ConsoleApp
{
    public class TaxComponent
    {
        public TaxComponent()
        {
            Console.Clear();
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1) Get Tax");
            Console.WriteLine("2) Add Municipality");
            Console.WriteLine("3) Modify Municipality");
            Console.Write("\r\nSelect an option: ");
        }

        public async Task<bool> GetTaxComponent(string baseUrl)
        {
            Console.Write("Enter the municipality name: ");
            string name = Console.ReadLine();
            while (!await TaxProcessor.CheckIfExists(baseUrl, name))
            {
                Console.WriteLine("Not a valid municipality, try again.");
                name = Console.ReadLine();
            }
                        
            DateTime date = GetValidDate(out string day); 
            Municipalities model = new Municipalities
            {
                MunicipalityName = name,
                Date = date
            };
            Municipalities result = await TaxProcessor.GetTaxForMunicipality(baseUrl, model);
            Console.Clear();
            if (result != null)
            {
                Console.WriteLine("---Output---");
                Console.WriteLine("{0} | {1} | {2} | {3}", "Municipality", "Date", "Tax rule", "Result");
                Console.WriteLine("{0} | {1} | {2} | {3}", name, day, result.TaxRule, result.Tax);
            }else
                Console.WriteLine("No data.");

            Console.Write("\r\nPress Enter to return to Main Menu");
            Console.ReadLine();
            return true;
        }

        public async Task<bool> AddMunicipalityComponent(string baseUrl)
        {
            Console.Write("Enter the municipality name: ");
            string name = Console.ReadLine();
            while (await TaxProcessor.CheckIfExists(baseUrl, name))
            {
                Console.WriteLine("Entered Municipality already exists, try again.");
                name = Console.ReadLine();
            }

            Lookup lookup  = await TaxProcessor.GetLookup(baseUrl);
            int taxRuleId = GetTaxRuleId(lookup.TaxRules);
            int taxTypeId = GetTaxTypeId(lookup.TaxTypes);
            Console.Write("---From---");
            DateTime dateFrom = GetValidDate(out string dayFrom);
            DateTime? dateTo = null;

            var _taxType = lookup.TaxTypes.Where(x => x.TaxTypeId == taxTypeId).FirstOrDefault();
            string taxType = _taxType == null ? "" : _taxType.TaxTypeName;
            if (!taxType.ToUpper().Equals("DAILY"))
            {
                Console.Write("---To---");
                dateTo = GetValidDate(out string dayTo);
            }

            double tax = GetValidDouble();

            Municipalities result = new Municipalities { 
                MunicipalityName = name,
                TaxRuleId = taxRuleId,
                TaxTypeId = taxTypeId,
                From = dateFrom,
                To = dateTo,
                Tax = tax
            };
            bool output = await TaxProcessor.AddDetails(baseUrl, result);
            if (output) Console.Write("\r\nAdded Successfully!"); else Console.Write("\r\nSomething went wrong, try again!");

            Console.Write("\r\nPress Enter to return to Main Menu");
            Console.ReadLine();
            return true;
        }

        public async Task<bool> UpdateMunicipalityComponent(string baseUrl)
        {
            Console.Write("Enter the municipality name: ");
            string name = Console.ReadLine();
            while (!await TaxProcessor.CheckIfExists(baseUrl, name))
            {
                Console.WriteLine("Entered Municipality not available to update, try again.");
                name = Console.ReadLine();
            }
            
            Lookup lookup = await TaxProcessor.GetLookup(baseUrl);
            int taxRuleId = GetTaxRuleId(lookup.TaxRules);
            Municipalities result = await TaxProcessor.GetDetails(baseUrl, name, taxRuleId.ToString());
            Console.Clear();
            if (result != null)
            {
                Console.WriteLine("---Details:---");
                Console.WriteLine("{0} | {1}", "Municipality Name", "Tax rule");
                Console.WriteLine("{0} | {1}", result.MunicipalityName, result.TaxRule);
                                
                result.TaxRuleId = GetTaxRuleId(lookup.TaxRules);
                bool output = await TaxProcessor.UpdateDetails(baseUrl, result);
                if (output) Console.Write("\r\nUpdated Successfully!"); else Console.Write("\r\nSomething went wrong, try again!");
            }
            else
                Console.WriteLine("No data.");

            Console.Write("\r\nPress Enter to return to Main Menu");
            Console.ReadLine();
            return true;
        }

        private int GetTaxRuleId(List<Lookup.TaxRule> lstTaxRule)
        {
            Console.WriteLine("Choose an option for tax rule:");
            for (int i = 0; i < lstTaxRule.Count; i++)
                Console.WriteLine(String.Join(" ", "Rule", lstTaxRule[i].TaxRuleName));
            Console.Write("Enter the tax rule: ");
            int taxRuleId = 0; string _taxRuleId = Console.ReadLine();
            int.TryParse(_taxRuleId, out taxRuleId);
            while (taxRuleId == 0 || (lstTaxRule.ElementAtOrDefault(taxRuleId - 1) == null))
            {
                Console.WriteLine("Choose a valid tax rule.");
                _taxRuleId = Console.ReadLine();
                int.TryParse(_taxRuleId, out taxRuleId);
            }
            return taxRuleId;
        }

        private int GetTaxTypeId(List<Lookup.TaxType> lstTaxType)
        {
            Console.WriteLine("Choose an option for tax type:");
            for (int i = 0; i < lstTaxType.Count; i++)
                Console.WriteLine(String.Join(" ", i + 1 + ")", lstTaxType[i].TaxTypeName));
            Console.Write("Enter the tax type: ");
            int taxRuleId = 0; string _taxRuleId = Console.ReadLine();
            int.TryParse(_taxRuleId, out taxRuleId);
            while (taxRuleId == 0 || (lstTaxType.ElementAtOrDefault(taxRuleId - 1) == null))
            {
                Console.WriteLine("Choose a valid tax rule.");
                _taxRuleId = Console.ReadLine();
                int.TryParse(_taxRuleId, out taxRuleId);
            }
            return taxRuleId;
        }

        private DateTime GetValidDate(out string day)
        {
            Console.Write("Enter the date: ");
            day = Console.ReadLine();
            DateTime.TryParse(day, out DateTime date);
            while (!DateTime.TryParse(day, out date))
            {
                Console.WriteLine("Not a valid date, try again.");
                day = Console.ReadLine();
            }
            return date;
        }

        private double GetValidDouble()
        {
            Console.Write("Enter the tax: ");
            string value = Console.ReadLine();
            Double.TryParse(value, out double result);
            while (!Double.TryParse(value, out result))
            {
                Console.WriteLine("Not a valid tax, try again.");
                value = Console.ReadLine();
            }
            return result;
        }
    }
}
