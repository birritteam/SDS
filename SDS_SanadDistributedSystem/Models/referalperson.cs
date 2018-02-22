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
    using System.ComponentModel.DataAnnotations.Schema;
    using Resources;

    public partial class referalperson
    {
        [Display(Name = "number", ResourceType = typeof(referalpersonResource))]
        public int idreferalperson { get; set; }

        [Display(Name = "Person", ResourceType = typeof(referalpersonResource))]
        public int idperson_FK { get; set; }

        [Display(Name = "Case_Manager", ResourceType = typeof(referalpersonResource))]
        public int idcase_FK { get; set; }

        [Display(Name = "Service_Type", ResourceType = typeof(referalpersonResource))]
        public Nullable<int> idservice_FK { get; set; }

        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "submittingdate", ResourceType = typeof(referalpersonResource))]
        public Nullable<System.DateTime> submittingdate { get; set; }
        [Display(Name = "referalstate", ResourceType = typeof(referalpersonResource))]
        public string referalstate { get; set; }

        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "referaldate", ResourceType = typeof(referalpersonResource))]
        public Nullable<System.DateTime> referaldate { get; set; }

        [Display(Name = "servicestate", ResourceType = typeof(referalpersonResource))]
        public string servicestate { get; set; }

        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "servicestartdate", ResourceType = typeof(referalpersonResource))]
        public Nullable<System.DateTime> servicestartdate { get; set; }

        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "serviceenddate", ResourceType = typeof(referalpersonResource))]
        public Nullable<System.DateTime> serviceenddate { get; set; }

        [Display(Name = "referalsender_FK", ResourceType = typeof(referalpersonResource))]
        public string referalsender_FK { get; set; }

        [Display(Name = "senderevalution", ResourceType = typeof(referalpersonResource))]
        public string senderevalution { get; set; }
        [Display(Name = "recieverevalution", ResourceType = typeof(referalpersonResource))]
        public string recieverevalution { get; set; }
        [Display(Name = "idcenter_FK", ResourceType = typeof(referalpersonResource))]
        public string idcenter_FK { get; set; }
        [Display(Name = "outreachnote", ResourceType = typeof(referalpersonResource))]
        public string outreachnote { get; set; }

        [Display(Name = "referalreicver_FK", ResourceType = typeof(referalpersonResource))]
        public string referalreicver_FK { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }
        public virtual @case @case { get; set; }
        public virtual center center { get; set; }
        public virtual person person { get; set; }
        public virtual AspNetUser AspNetUser1 { get; set; }
        public virtual service service { get; set; }
    }
}
