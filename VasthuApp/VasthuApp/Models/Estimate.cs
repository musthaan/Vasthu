//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VasthuApp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Estimate
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Estimate()
        {
            this.EstimateDetails = new HashSet<EstimateDetail>();
        }
    
        public long Id { get; set; }
        public System.DateTime Date { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerAddress { get; set; }
        public Nullable<decimal> NetTotal { get; set; }
        public Nullable<decimal> Discount { get; set; }
        public Nullable<decimal> GrandTotal { get; set; }
        public bool IsDeleted { get; set; }
        public string Note { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EstimateDetail> EstimateDetails { get; set; }
    }
}
