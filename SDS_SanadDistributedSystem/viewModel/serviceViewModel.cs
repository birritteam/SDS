using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SDS_SanadDistributedSystem.viewModel
{
    public class serviceViewModel
    {
        public int idservice { get; set; }
        public string name { get; set; }

        public List<UserViewModel> recivers { get; set; }
    }
}