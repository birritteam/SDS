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
    using System.Web.Mvc;

    public partial class center
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public center()
        {
            this.AspNetUsers = new HashSet<AspNetUser>();
            this.centerservices = new HashSet<centerservice>();
            this.families = new HashSet<family>();
            this.people = new HashSet<person>();
            this.referalfamilies = new HashSet<referalfamily>();
            this.referalpersons = new HashSet<referalperson>();
            this.temporals = new HashSet<temporal>();
        }

        public string idcenter { get; set; }
        [Required]
        [Display(Name = "name", ResourceType = typeof(CenterResource))]
        public string name { get; set; }
        [Display(Name = "location", ResourceType = typeof(CenterResource))]
        public string location { get; set; }
        [Required]
        [Display(Name = "flag", ResourceType = typeof(CenterResource))]
        [Remote("IsAlreadyUsedFlag", "centers", HttpMethod = "Post", ErrorMessage = "����� ������ �����")]
        public string flag { get; set; }
        [Display(Name = "idpartner_FK", ResourceType = typeof(CenterResource))]
        public string idpartner_FK { get; set; }
        public int min_family_id { get; set; }
        public int max_family_id { get; set; }
        public int min_person_id { get; set; }
        public int max_person_id { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AspNetUser> AspNetUsers { get; set; }
        public virtual partner partner { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<centerservice> centerservices { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<family> families { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<person> people { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<referalfamily> referalfamilies { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<referalperson> referalpersons { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<temporal> temporals { get; set; }
    }
}
