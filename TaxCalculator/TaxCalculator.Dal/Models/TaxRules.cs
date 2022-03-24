using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;

namespace TaxCalculator.Dal.Models
{
    public class TaxRules
    {
        [Key]
        public int TaxRuleId { get; set; }
        public int TaxRule { get; set; }
        public string Description { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), DataMember]
        public DateTime CreatedOn { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), DataMember]
        public DateTime UpdatedOn { get; set; }

        public virtual ICollection<Municipalities> Municipalities { get; set; }
    }
}
