using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineTicariOtomasyon.Controllers
{
    public class CurrentReportViewModel
    {
        public int CurrentID { get; set; }
        public string CurrentName { get; set; }
        public decimal Total { get; set; }
        public DateTime Date { get; set; }
    }
}