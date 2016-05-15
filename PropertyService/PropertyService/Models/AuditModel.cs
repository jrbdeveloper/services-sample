using System;

namespace PropertyService.Models
{
    public class AuditModel
    {
        public string Object { get; set; }

        public string Method { get; set; }

        public string Action { get; set; }

        public string Value { get; set; }

        public DateTime? Date { get; set; }
    }
}