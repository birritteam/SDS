using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SDS_SanadDistributedSystem.Models
{
    public class PersonDTO
    {
        public string idperson { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public string fathername { get; set; }
        public string mothername { get; set; }
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> birthday { get; set; }
        public string birthplace { get; set; }
        public string gender { get; set; }
        public string nationality { get; set; }
        public string martial { get; set; }
        public string relationtype { get; set; }
        public string onoffflag { get; set; }
        public string education { get; set; }
        public string educationstate { get; set; }
        public string phone1 { get; set; }
        public string phone2 { get; set; }
        public string currentaddress { get; set; }
        public Nullable<System.DateTimeOffset> registrationdate { get; set; }
        public string idfamily_FK { get; set; }
        public string idcenter_FK { get; set; }
        public Nullable<int> formnumber { get; set; }
        public string note { get; set; }
        public string iduser { get; set; }
        public string nationalnumber { get; set; }
    }
}