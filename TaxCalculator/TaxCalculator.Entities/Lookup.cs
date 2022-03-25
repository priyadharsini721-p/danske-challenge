using System;
using System.Collections.Generic;
using System.Text;

namespace TaxCalculator.Entities
{
    public class Lookup
    {
        public List<TaxRule> TaxRules { get; set; }
        public List<TaxType> TaxTypes { get; set; }

        public class TaxRule
        {
            public int TaxRuleId { get; set; }
            public int TaxRuleName { get; set; }
        }

        public class TaxType
        {
            public int TaxTypeId { get; set; }
            public string TaxTypeName { get; set; }
        }
    }
}
