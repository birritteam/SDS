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
    
    public partial class referalperson
    {
        public int idreferalperson { get; set; }
        public string idperson_FK { get; set; }
        public int idcase_FK { get; set; }
        public Nullable<int> idservice_FK { get; set; }
        public Nullable<System.DateTime> submittingdate { get; set; }
        public string referalstate { get; set; }
        public Nullable<System.DateTime> referaldate { get; set; }
        public string servicestate { get; set; }
        public Nullable<System.DateTime> servicestartdate { get; set; }
        public Nullable<System.DateTime> serviceenddate { get; set; }
        public string referalsender_FK { get; set; }
        public string senderevalution { get; set; }
        public string recieverevalution { get; set; }
        public string idcenter_FK { get; set; }
        public string outreachnote { get; set; }
        public string referalreicver_FK { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual @case @case { get; set; }
        public virtual center center { get; set; }
        public virtual service service { get; set; }
        public virtual person person { get; set; }
        public virtual AspNetUser AspNetUser1 { get; set; }
    }
}
