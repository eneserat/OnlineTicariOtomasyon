using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OnlineTicariOtomasyon.Models.Classes
{
    public class CurrentLog:DbContext
    {
        [Key]
        public int LogID { get; set; }

        public int CurrentID { get; set; }

        public string ActionType { get; set; } // Update, Insert, Delete

        public DateTime ActionDate { get; set; }

        public string EmployeeName { get; set; }

        public string Description { get; set; }
      

    }


}