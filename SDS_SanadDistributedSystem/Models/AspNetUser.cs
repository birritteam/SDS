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

    public partial class AspNetUser
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AspNetUser()
        {
            this.AspNetUserClaims = new HashSet<AspNetUserClaim>();
            this.AspNetUserLogins = new HashSet<AspNetUserLogin>();
            this.families = new HashSet<family>();
            this.people = new HashSet<person>();
            this.referalfamilies = new HashSet<referalfamily>();
            this.referalfamilies1 = new HashSet<referalfamily>();
            this.referalpersons = new HashSet<referalperson>();
            this.referalpersons1 = new HashSet<referalperson>();
            this.temporals = new HashSet<temporal>();
            this.temporals1 = new HashSet<temporal>();
            this.AspNetRoles = new HashSet<AspNetRole>();
        }


        [Display(Name = "Id", ResourceType = typeof(AspNetUserResource))]
        public string Id { get; set; }

        [Required]
        [EmailAddress]
        [Remote("IsAlreadySignedEmail", "AspNetUsers", AdditionalFields = "Id", HttpMethod = "Post", ErrorMessage = "������ ���������� ����� �����")]
        [Display(Name = "Email", ResourceType = typeof(AspNetUserResource))]
        public string Email { get; set; }
        [Display(Name = "EmailConfirmed", ResourceType = typeof(AspNetUserResource))]
        public bool EmailConfirmed { get; set; }
        [Required]
        [Display(Name = "PasswordHash", ResourceType = typeof(AspNetUserResource))]
        public string PasswordHash { get; set; }
        [Display(Name = "SecurityStamp", ResourceType = typeof(AspNetUserResource))]
        public string SecurityStamp { get; set; }
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$")]
        //   [DataType(DataType.PhoneNumber)]
        [Remote("IsAlreadySignedPhone", "AspNetUsers", AdditionalFields = "Id", HttpMethod = "Post", ErrorMessage = "��� ������ ����� �����")]
        [Display(Name = "PhoneNumber", ResourceType = typeof(AspNetUserResource))]
        public string PhoneNumber { get; set; }
        [Display(Name = "PhoneNumberConfirmed", ResourceType = typeof(AspNetUserResource))]
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public Nullable<System.DateTime> LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        [Required]
        [Remote("IsAlreadySignedUserName", "AspNetUsers", AdditionalFields = "Id", HttpMethod = "Post", ErrorMessage = "��� �������� ����� �����")]
        [Display(Name = "UserName", ResourceType = typeof(AspNetUserResource))]
        public string UserName { get; set; }
        [Display(Name = "idcenter_FK", ResourceType = typeof(AspNetUserResource))]
        public string idcenter_FK { get; set; }
        [Display(Name = "enabled", ResourceType = typeof(AspNetUserResource))]
        public bool enabled { get; set; }
        [Display(Name = "ShowName", ResourceType = typeof(AspNetUserResource))]
        public string ShowName { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AspNetUserClaim> AspNetUserClaims { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual center center { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<family> families { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<person> people { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<referalfamily> referalfamilies { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<referalfamily> referalfamilies1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<referalperson> referalpersons { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<referalperson> referalpersons1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<temporal> temporals { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<temporal> temporals1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AspNetRole> AspNetRoles { get; set; }
    }
}
