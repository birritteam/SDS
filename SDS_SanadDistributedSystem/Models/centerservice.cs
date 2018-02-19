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
    using customValidation;
    using Microsoft.AspNet.Identity;
    using Resources;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class centerservice
    {
        [Required]
        [Display(Name = "idcenter_FK", ResourceType = typeof(CenterServicesResource))]
        public string idcenter_FK { get; set; }
        [Required]
        [CenterServiceValidation]
        [Display(Name = "idservice_FK", ResourceType = typeof(CenterServicesResource))]
        public int idservice_FK { get; set; }
        [Display(Name = "enabled", ResourceType = typeof(CenterServicesResource))]
        public bool enabled { get; set; }
        [Display(Name = "location", ResourceType = typeof(CenterServicesResource))]
        public string location { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date, ErrorMessage = "���� ������� ��� �����")]
        [Display(Name = "start_date", ResourceType = typeof(CenterServicesResource))]
        public Nullable<System.DateTime> start_date { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date, ErrorMessage = "���� ������� ��� �����")]
        [Display(Name = "end_date", ResourceType = typeof(CenterServicesResource))]
        public Nullable<System.DateTime> end_date { get; set; }
        public int Id { get; set; }

        public virtual center center { get; set; }
        public virtual service service { get; set; }


    }
}
