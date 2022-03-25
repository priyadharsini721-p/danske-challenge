using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;

namespace TaxCalculator.Entities
{
    public class Municipalities
    {
        public int MunicipalityId { get; set; }
        public int TaxPeriodId { get; set; }
        public int TaxTypeId { get; set; }
        public int TaxRuleId { get; set; }

        public string MunicipalityName { get; set; }
        public string TaxType { get; set; }
        public int TaxRule { get; set; }
        public double Tax { get; set; }
        public int Order { get; set; }
        public DateTime Date { get; set; }
        
        //Tax Period
        public DateTime From { get; set; }
        public DateTime? To { get; set; }
    }
}
