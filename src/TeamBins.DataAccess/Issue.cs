//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SmartPlan.DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class Issue
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Desciption { get; set; }
        public Nullable<System.DateTime> DueDate { get; set; }
        public Nullable<int> PriorityID { get; set; }
        public int CategoryID { get; set; }
        public int StatusID { get; set; }
        public int ProjectID { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int CreatedByID { get; set; }
    
        public virtual Category Category { get; set; }
        public virtual Priority Priority { get; set; }
        public virtual Project Project { get; set; }
        public virtual Status Status { get; set; }
    }
}