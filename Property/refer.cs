//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Property
{
    using System;
    using System.Collections.Generic;
    
    public partial class refer
    {
        public int refer_id { get; set; }
        public Nullable<int> advisor_id { get; set; }
        public Nullable<int> account_id { get; set; }
        public Nullable<int> property_id { get; set; }
    
        public virtual aacount aacount { get; set; }
    }
}
