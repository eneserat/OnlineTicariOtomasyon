using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnlineTicariOtomasyon.Models.Classes
{
    public class Expense
    {
        [Key]
        public int ExpensesID { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(100)]
        public string ExpensesExplanation { get; set; }  

        public DateTime ExpensesDate { get; set; }
        public decimal ExpensesAmount { get; set; }
    }
}