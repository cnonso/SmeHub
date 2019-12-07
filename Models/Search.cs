using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SME_Hub.Models
{
    public partial class SimpleSearch
    {
        public string KeyWord { get; set; }
        public string Location { get; set; }
    }

    public partial class ComplexSearch
    {
        public string KeyWord { get; set; }
        public string Category { get; set; }
        public string Location { get; set; }
    }

    public class Search
    {
        public SimpleSearch SimpleSearch { get; set; }
        public ComplexSearch ComplexSearch { get; set; }
    }
}