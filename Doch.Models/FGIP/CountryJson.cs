using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doch.Models.FGIP
{
    public class CountryJson
    {
        public Data Data { get; set; }
    }

    public class Data
    {
        public Location Location { get; set; }

    }
    public class Location
    {
        public Country Country { get; set; }
    }

    public class Country
    {
        public string Alpha2 { get; set; }
        public string Alpha3 { get; set; }
    }
}
