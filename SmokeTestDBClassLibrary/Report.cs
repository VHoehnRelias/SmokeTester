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
    
    public partial class Report
    {
        public int Id { get; set; }
        public string Report_Name { get; set; }
        public string File_Name { get; set; }
        public string Left_Navigation_Menu_location { get; set; }
        public string AssociatedTickets { get; set; }
        public Nullable<System.DateTime> DateAdded { get; set; }
        public string AddedBy { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
        public string ModifiedBy { get; set; }
    }
}
