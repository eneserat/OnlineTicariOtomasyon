using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnlineTicariOtomasyon.Models.Classes
{
    public class Invoice
    {
        [Key]
        public int InvoiceID { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(50)]
        public string InvoiceSerialNumber { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(50)]
        public string InvoiceSiraNumber { get; set; }

        public DateTime InvoiceDate { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(50)]
        public string InvoiceTaxOffice { get; set; }

        public string InvoiceSubmitter { get; set; }
        public string InvoiceReceiver { get; set; }

        public ICollection<InvoicePen> InvoicePens { get; set; }
    }
}