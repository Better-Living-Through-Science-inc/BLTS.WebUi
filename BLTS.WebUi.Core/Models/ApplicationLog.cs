using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BLTS.WebUi.Models
{
    public partial class ApplicationLog : BaseEntity<long>
    {
        public ApplicationLog()
        {
            ExecutionTime = DateTime.UtcNow;
        }

        [Key]
        [Column("ApplicationLogId")]
        public override long Id { get; set; }
        public string ApplicationName { get; set; }
        public string EnvironmentName { get; set; }
        public DateTime ExecutionTime { get; set; }
        public long ExecutionDuration { get; set; }
        public string MethodName { get; set; }
        public string ServiceName { get; set; }
        public string CustomData { get; set; }
        public string Exception { get; set; }
    }
}
