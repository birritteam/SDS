using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SDS_SanadDistributedSystem.viewModel
{
    public class referalPersonViewModel
    {
      
     //   public int idreferalperson { get; set; }

      
        public string idperson_FK { get; set; }


        public string idcase_FK { get; set; }

       
        public string idservice_FK { get; set; }

       // public string submittingdate { get; set; }

       // public string referalstate { get; set; }

        //public Nullable<System.DateTime> referaldate { get; set; }


      //  public string servicestate { get; set; }

        //public Nullable<System.DateTime> servicestartdate { get; set; }


        //public Nullable<System.DateTime> serviceenddate { get; set; }

        //public string referalsender_FK { get; set; }

        public string senderevalution { get; set; }
    
        //public string recieverevalution { get; set; }

        //public string idcenter_FK { get; set; }

        public string outreachnote { get; set; }
    }
}