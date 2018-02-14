using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SDS_SanadDistributedSystem.viewModel
{
    public class RPSearchViewModel
    {
        //   public int idreferalperson { get; set; }


        //  public string idperson_FK { get; set; }

        public string idcase { get; set; }
        public int idperson { get; set; }
        public string idreferalperson { get; set; }

        public string name { get; set; }


       

    //    public string referalreciever_FK { get; set; }

         public string submittingdate { get; set; }

        //public string referalstate { get; set; }

        public string referaldate { get; set; }

        public string type { get; set; }

        //  public string servicestate { get; set; }

        public string servicestartdate { get; set; }


        public string serviceenddate { get; set; }

        //public string referalsender_FK { get; set; }

        public string senderevalution { get; set; }

        public string recieverevalution { get; set; }

        //public string idcenter_FK { get; set; }

        public string outreachnote { get; set; }
    }
}