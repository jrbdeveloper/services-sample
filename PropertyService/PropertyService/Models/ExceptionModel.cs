using System;

namespace PropertyService.Models
{
    public class ExceptionModel
    {
        public DateTime? Date { get; set; }

        public string Message { get; set; }

        public string InnerException { get; set; }

        public string Source { get; set; }

        public string StackTrace { get; set; }

        public string TargetSite { get; set; }
    }
}