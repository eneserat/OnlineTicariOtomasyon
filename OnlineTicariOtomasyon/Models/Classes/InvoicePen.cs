using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnlineTicariOtomasyon.Models.Classes
{
    public class InvoicePen
    {
        [Key]
        public int InvoicePenID { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(100)]
        public string InvoiceExplanation { get; set; }

        public int InvoicePiece { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Amount { get; set; }

        // Fatura kalem için
        public Invoice Invoice { get; set; } 



    }
}