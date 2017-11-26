//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SDS_SanadDistributedSystem.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class family
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public family()
        {
            this.people = new HashSet<person>();
            this.referalfamilies = new HashSet<referalfamily>();
            this.managelists = new HashSet<managelist>();
        }
    
        public string idfamily { get; set; }
        public string familynature { get; set; }
        public Nullable<int> personcount { get; set; }
        public string lastaddress { get; set; }
        public string currentaddress { get; set; }
        public Nullable<System.DateTime> displacementdate { get; set; }
        public string phone1 { get; set; }
        public string phone2 { get; set; }
        public string note { get; set; }
        public string iduser { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<person> people { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<referalfamily> referalfamilies { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<managelist> managelists { get; set; }
    }
}
