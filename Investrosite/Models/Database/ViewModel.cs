using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Investrosite.Models.Database
{
    public class ViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public Nullable<int> eid { get; set; }
    }
}