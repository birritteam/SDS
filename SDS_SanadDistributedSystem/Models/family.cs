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
    using Resources;
    using System.ComponentModel.DataAnnotations;

    public partial class family
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public family()
        {
            this.referalfamilies = new HashSet<referalfamily>();
            this.managelists = new HashSet<managelist>();
            this.people = new HashSet<person>();
        }

        [Display(Name = "idfamily", ResourceType = typeof(PersonAndFamilyResources))]
        public string idfamily { get; set; }
        [Display(Name = "familynature", ResourceType = typeof(PersonAndFamilyResources))]
        public string familynature { get; set; }

        [Display(Name = "personcount", ResourceType = typeof(PersonAndFamilyResources))]
        public Nullable<int> personcount { get; set; }
        [Display(Name = "lastaddress", ResourceType = typeof(PersonAndFamilyResources))]
        public string lastaddress { get; set; }
        [Display(Name = "currentaddress", ResourceType = typeof(PersonAndFamilyResources))]
        public string currentaddress { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date, ErrorMessage = "���� ������� ��� �����")]
        [Display(Name = "displacementdate", ResourceType = typeof(PersonAndFamilyResources))]
        public Nullable<System.DateTime> displacementdate { get; set; }

        [StringLength(10)]
        [RegularExpression(@"^[0-9]+$")]
        [Display(Name = "phone1", ResourceType = typeof(PersonAndFamilyResources))]
        public string phone1 { get; set; }
        [StringLength(10)]
        [RegularExpression(@"^[0-9]+$")]
        [Display(Name = "phone2", ResourceType = typeof(PersonAndFamilyResources))]
        public string phone2 { get; set; }

        [Display(Name = "note", ResourceType = typeof(PersonAndFamilyResources))]
        public string note { get; set; }
        [Display(Name = "iduser", ResourceType = typeof(PersonAndFamilyResources))]
        public string iduser { get; set; }
        [Display(Name = "lname", ResourceType = typeof(PersonAndFamilyResources))]
        public string lastname { get; set; }
        [Display(Name = "phone1owner", ResourceType = typeof(PersonAndFamilyResources))]
        public string phone1owner { get; set; }
        [Display(Name = "phone2owner", ResourceType = typeof(PersonAndFamilyResources))]
        public string phone2owner { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<referalfamily> referalfamilies { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<managelist> managelists { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<person> people { get; set; }
    }
}
