//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SmokeTestDBClassLibrary
{
    using System;
    using System.Collections.Generic;
    
    public partial class Report_Evaluation
    {
        public int Id { get; set; }
        public int Release_ID { get; set; }
        public int Evaluator_ID { get; set; }
        public int Report_ID { get; set; }
        public Nullable<int> Status_ID { get; set; }
        public string Comment { get; set; }
        public Nullable<System.DateTime> DateAdded { get; set; }
        public string AddedBy { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
        public string ModifiedBy { get; set; }
    
        public virtual Evaluator Evaluator { get; set; }
        public virtual Release Release { get; set; }
        public virtual Report Report { get; set; }
        public virtual Status Status { get; set; }
    }
}
