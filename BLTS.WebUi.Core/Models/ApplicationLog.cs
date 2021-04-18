using System;

namespace BLTS.WebUi.Models
{
    public partial class ApplicationLog : Entity<long>
    {
        public ApplicationLog()
        {
            ExecutionTime = DateTime.UtcNow;
            ExecutionDuration = -5555;
            IsException = false;
            NotificationDate = DateTime.Parse("9999-12-31");
        }

        public long ApplicationInfoId { get; set; }
        public string ApplicationName { get; set; }
        public string EnvironmentName { get; set; }
        public string ClassName { get; set; }
        public string MethodName { get; set; }
        public string Description { get; set; }
        public DateTime ExecutionTime { get; set; }
        public long ExecutionDuration { get; set; }
        public string ExceptionStacktrace { get; set; }
        public bool IsException { get; set; }
        public DateTime NotificationDate { get; set; }

        public virtual ApplicationInfo ApplicationInfo { get; set; }
    }
}
