//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LoggingService.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Audit
    {
        public int Id { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string Object { get; set; }
        public string Method { get; set; }
        public string Action { get; set; }
        public string Value { get; set; }
    }
}
