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
    using Resources;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public partial class service
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public service()
        {
            this.referalfamilies = new HashSet<referalfamily>();
            this.referalpersons = new HashSet<referalperson>();
        }

        [Display(Name = "idservice", ResourceType = typeof(ServiceResource))]
        public int idservice { get; set; }
        [Display(Name = "idcase_FK", ResourceType = typeof(ServiceResource))]
        [Required]
        public int idcase_FK { get; set; }
        [Required]
        [Display(Name = "name", ResourceType = typeof(ServiceResource))]
        public string name { get; set; }
        [Display(Name = "enabled", ResourceType = typeof(ServiceResource))]
        public bool enabled { get; set; }
        [Required]
        [Display(Name = "description", ResourceType = typeof(ServiceResource))]
        public string description { get; set; }
        [Required]
        [Display(Name = "idrole_FK", ResourceType = typeof(ServiceResource))]
        public string idrole_FK { get; set; }


        public virtual AspNetRole AspNetRole { get; set; }
        public virtual @case @case { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<referalfamily> referalfamilies { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<referalperson> referalpersons { get; set; }
    }
}
