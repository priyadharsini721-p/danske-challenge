using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;

namespace TaxCalculator.Dal.Models
{
    public class Municipalities
    {
        [Key]
        public int MunicipalityId { get; set; }
        public string MunicipalityName { get; set; }
        [ForeignKey("TaxRules")]
        public int TaxRuleId { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), DataMember]
        public DateTime CreatedOn { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), DataMember]
        public DateTime UpdatedOn { get; set; }

        [NotMapped]
        public DateTime Date { get; set; }
        [NotMapped]
        public int TaxRule { get; set; }
        [NotMapped]
        public double Tax { get; set; }
        [NotMapped]
        public int Order { get; set; }

        public virtual TaxRules TaxRules { get; set; }
        public virtual ICollection<TaxPeriod> TaxPeriod { get; set; }
    }
}
