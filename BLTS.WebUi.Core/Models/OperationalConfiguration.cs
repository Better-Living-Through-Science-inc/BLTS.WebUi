using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BLTS.WebUi.Models
{
    [Table("OperationalConfiguration", Schema = "dbo")]
    public partial class OperationalConfiguration : BaseEntity<long>
    {
        [Key]
        [Column("OperationalConfigurationId")]
        public override long Id { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string PropertyName { get; set; }
        public string Description { get; set; }
        public bool? BoolValue { get; set; }
        public DateTime? DateValue { get; set; }
        [Column(TypeName = "decimal(28, 10)")]
        public decimal? DecimalValue { get; set; }
        public Guid? GuidValue { get; set; }
        public int? IntegerValue { get; set; }
        public long? LongValue { get; set; }
        [Column(TypeName = "nvarchar(4000)")]
        public string StringValue { get; set; }
        public bool IsConnectionString { get; set; }
        public bool? IsEnabled { get; set; }
    }
}
