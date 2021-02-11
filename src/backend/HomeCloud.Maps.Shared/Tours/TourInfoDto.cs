using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCloud.Maps.Shared.Tours
{
    public class TourInfoDto
    {
        public string TourId { get; set; }

        public string Name { get; set; }

        public DateTime Date { get; set; }

        public double Distance { get; set; }

        public string ImageUrl { get; set; }
    }
}
