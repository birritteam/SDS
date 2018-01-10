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
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using Resources;

    public partial class person
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public person()
        {
            this.referalpersons = new HashSet<referalperson>();
            this.personmanages = new HashSet<personmanage>();
        }

        [Remote("idAlreadyExisted", "people",
       ErrorMessageResourceType = typeof(ErrorResource),
       ErrorMessageResourceName = "id_existed")
                 ]
        //[Required]
        [Display(Name = "person_id", ResourceType = typeof(PersonAndFamilyResources))]
        public string idperson { get; set; }

        [Display(Name = "fname", ResourceType = typeof(PersonAndFamilyResources))]
        public string fname { get; set; }
        [Display(Name = "lname", ResourceType = typeof(PersonAndFamilyResources))]

        public string lname { get; set; }

        [StringLength(11)]
        [RegularExpression(@"^[0-9]+$")]
        [Range(00000000000, 99999999999)]
        [Display(Name = "nationalnumber", ResourceType = typeof(PersonAndFamilyResources))]
        public string nationalnumber { get; set; }
        [Display(Name = "fathername", ResourceType = typeof(PersonAndFamilyResources))]
        public string fathername { get; set; }
        [Display(Name = "mothername", ResourceType = typeof(PersonAndFamilyResources))]
        public string mothername { get; set; }
        //[DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date, ErrorMessage = "���� ������� ��� ����")]
        [Display(Name = "birthday", ResourceType = typeof(PersonAndFamilyResources))]
        public Nullable<System.DateTime> birthday { get; set; }
        [Display(Name = "birthplace", ResourceType = typeof(PersonAndFamilyResources))]
        public string birthplace { get; set; }
        [Display(Name = "gender", ResourceType = typeof(PersonAndFamilyResources))]
        public string gender { get; set; }
        [Display(Name = "nationality", ResourceType = typeof(PersonAndFamilyResources))]
        public string nationality { get; set; }
        [Display(Name = "martial", ResourceType = typeof(PersonAndFamilyResources))]
        public string martial { get; set; }
        [Display(Name = "relationtype", ResourceType = typeof(PersonAndFamilyResources))]
        public string relationtype { get; set; }
        public string onoffflag { get; set; }
        [Display(Name = "education", ResourceType = typeof(PersonAndFamilyResources))]
        public string education { get; set; }
        [Display(Name = "educationstate", ResourceType = typeof(PersonAndFamilyResources))]
        public string educationstate { get; set; }

        [StringLength(10)]
        [RegularExpression(@"^[0-9]+$")]
        [Display(Name = "phone1", ResourceType = typeof(PersonAndFamilyResources))]
        public string phone1 { get; set; }
        [StringLength(10)]
        [RegularExpression(@"^[0-9]+$")]
        [Display(Name = "phone2", ResourceType = typeof(PersonAndFamilyResources))]
        public string phone2 { get; set; }
        [Display(Name = "currentaddress", ResourceType = typeof(PersonAndFamilyResources))]
        public string currentaddress { get; set; }
        [Display(Name = "registrationdate", ResourceType = typeof(PersonAndFamilyResources))]
        public Nullable<System.DateTimeOffset> registrationdate { get; set; }
        public string idfamily_FK { get; set; }
        public string idcenter_FK { get; set; }
        [Display(Name = "formnumber", ResourceType = typeof(PersonAndFamilyResources))]
        public Nullable<int> formnumber { get; set; }
        [Display(Name = "note", ResourceType = typeof(PersonAndFamilyResources))]
        public string note { get; set; }

        public string iduser { get; set; }
        public bool is_secret { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }
        public virtual center center { get; set; }
        public virtual family family { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<referalperson> referalpersons { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<personmanage> personmanages { get; set; }
    }
}
