//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace datalayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class booking
    {
        public booking()
        {
            this.users = new HashSet<user>();
            this.customers = new HashSet<customer>();
            this.payments = new HashSet<payment>();
        }
    
        public int Booking_id { get; set; }
        public Nullable<int> Room_ { get; set; }
        public System.DateTime booking_date { get; set; }
        public System.DateTime leave_date { get; set; }
        public Nullable<int> cust_id { get; set; }
    
        public virtual ICollection<user> users { get; set; }
        public virtual ICollection<customer> customers { get; set; }
        public virtual room room { get; set; }
        public virtual ICollection<payment> payments { get; set; }
    }
}
