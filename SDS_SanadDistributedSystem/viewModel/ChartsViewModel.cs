using DotNet.Highcharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SDS_SanadDistributedSystem.viewModel
{
    public class ChartsViewModel
    {
        public List<Highcharts> publiccharts { get; set; }

        public List<Highcharts> centercharts { get; set; }

    }
}