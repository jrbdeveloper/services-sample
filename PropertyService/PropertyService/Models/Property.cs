//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PropertyService.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Property
    {
        public int PropertyId { get; set; }
        public Nullable<int> AddressId { get; set; }
        public string Name { get; set; }
        public Nullable<double> Price { get; set; }
        public string Style { get; set; }
        public Nullable<int> YearBuilt { get; set; }
        public Nullable<int> LotSize { get; set; }
        public Nullable<int> SquareFootage { get; set; }
    }
}