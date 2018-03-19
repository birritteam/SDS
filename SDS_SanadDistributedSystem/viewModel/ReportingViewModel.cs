using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SDS_SanadDistributedSystem.viewModel
{
    public class ReportingViewModel
    {
        // for counts people according to gender, familynature, age
        public string familynature { get; set; }

        public string gender { get; set; }

        public string age { get; set; }

        // for three addresses 

        public string addressname { get; set; }

        // for three ways that person know socity center

        public string waysknow { get; set; }


        //service

        public string type { get; set; }

        public string servicename { get; set; }

        public int count { get; set; }

        public int malecount { get; set; }

        public int femalecount { get; set; }

        public int lesseighteenmalecount { get; set; }

        public int lesseighteenfemalecount { get; set; }

        public int betweenmalecount { get; set; }

        public int betweenfemalecount { get; set; }

        public int heighersixtymalecount { get; set; }

        public int heighersixtyfemalecount { get; set; }

        public int internaldisplacemenmalecount { get; set; }

        public int internaldisplacemenfemalecount { get; set; }

        public int hostcommunirtmalecount { get; set; }

        public int hostcommunirtfemalecount { get; set; }

        public int internaldisplacedreturneemalecount { get; set; }


        public int internaldisplacedreturnefeemalecount { get; set; }

        public int refugeereturningtosyriamalecount { get; set; }

        public int refugeereturningtosyriafemalecount { get; set; }

        public int refugeewantedmalecount { get; set; }

        public int refugeewantedfemalecount { get; set; }


        public int? inprogressstatemalecount { get; set; }
        public int? inprogressstatefemalecount { get; set; }

        public int? closedstatemalecount { get; set; }

        public int? closedstatefemalecount { get; set; }

    }
}