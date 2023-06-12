using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace activity2.Models
{
    public class Holiday
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string NameEn { get; set; }
        public string NameFr { get; set; }
        public int Federal { get; set; }
        public DateTime ObservedDate { get; set; }
        public List<Province> Provinces { get; set; }
    }

    public class Province
    {
        public string Id { get; set; }
        public string NameEn { get; set; }
        public string NameFr { get; set; }
        public string SourceLink { get; set; }
        public string SourceEn { get; set; }
    }
}