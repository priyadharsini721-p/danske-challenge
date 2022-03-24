using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;

namespace TaxCalculator.Dal.Models
{
    public class TaxTypes
    {
        [Key]
        public int TaxTypeId { get; set; }
        public string TaxType { get; set; }
        public int Order { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), DataMember]
        public DateTime CreatedOn { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), DataMember]
        public DateTime UpdatedOn { get; set; }

        public virtual ICollection<TaxPeriod> TaxPeriod { get; set; }
    }
}
