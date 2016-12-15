using NeoMix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoMix.ViewModel
{
    public class StatsVM
    {
        public Stats StatsPage;
        public Stats StatsDate;
        public Stats StatsType;

        public StatsVM(Stats statsPage, Stats statsDate, Stats statsType)
        {
            StatsPage = statsPage;
            StatsDate = statsDate;
            StatsType = statsType;
        }
    }
}