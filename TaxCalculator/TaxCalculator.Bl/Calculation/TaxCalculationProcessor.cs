using System;
using System.Collections.Generic;
using System.Text;
using TaxCalculator.Entities;
using System.Linq;

namespace TaxCalculator.Bl.Calculation
{
    public static class TaxCalculationProcessor
    {
        public static double CalculateTax(List<Municipalities> lstMunicipalities, out int taxRule)
        {
            double tax = 0; taxRule = 0;
            try
            {
                if (lstMunicipalities != null && lstMunicipalities.Count > 0)
                {
                    Municipalities municipalities = lstMunicipalities.OrderBy(x => x.Order).ThenBy(x => x.Date).FirstOrDefault();
                    taxRule = municipalities.TaxRule;

                    if (taxRule == 1)
                    {
                        foreach (var item in lstMunicipalities)
                            tax += item.Tax;
                    }
                    else if (taxRule == 2)
                        tax = municipalities.Tax;
                }
            }
            catch { throw; }
            return tax;
        }
    }
}
