using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineTicariOtomasyon.Models.Classes
{
    public class SalesTransaction
    {
        [Key]
        public int SalesTransactionsID { get; set; }

        public DateTime SalesTransactionsDate { get; set; }

        public decimal SalesTransactionsPrice { get; set; }

        public decimal SalesTransactionsTotalAmount { get; set; }

        public int ProductID { get; set; } 
        public Product Product { get; set; }

        public int CurrentID { get; set; } 
        public Current Current { get; set; } 

        public int EmployeeID { get; set; } 
        public Employee Employee { get; set; }
        public int Quantity { get; set; }


    }
}