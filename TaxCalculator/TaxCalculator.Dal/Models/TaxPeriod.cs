using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;

namespace TaxCalculator.Dal.Models
{
    public class TaxPeriod
    {
        [Key]
        public int TaxPeriodId { get; set; }
        [ForeignKey("Municipalities")]
        public int MunicipalityId { get; set; }
        [Column(TypeName = "date")]
        public DateTime From { get; set; }
        [Column(TypeName = "date")]
        public DateTime? To { get; set; }
        public double Tax { get; set; }
        [ForeignKey("TaxTypes")]
        public int TaxTypeId { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), DataMember]
        public DateTime CreatedOn { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), DataMember]
        public DateTime UpdatedOn { get; set; }

        public virtual TaxTypes TaxTypes { get; set; }
        public virtual Municipalities Municipalities { get; set; }
    }
}
