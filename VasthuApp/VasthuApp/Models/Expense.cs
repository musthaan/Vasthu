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
    
    public partial class Expense
    {
        public long Id { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<long> CategoryId { get; set; }
        public string Note { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<bool> IsDelete { get; set; }
    
        public virtual ExpenseCategory ExpenseCategory { get; set; }
    }
}
