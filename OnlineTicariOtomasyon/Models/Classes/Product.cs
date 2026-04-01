using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnlineTicariOtomasyon.Models.Classes
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(50)]
        public string ProductName { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(50)]
        public string ProductBrand { get; set; }

        public short ProductStock { get; set; }

        public decimal PurchasePrice { get; set; }

        public decimal SellingPrice { get; set; }

        public bool Status { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(250)]
        public string ProductImage { get; set; }

        public int CategoryID { get; set; } 
        public Category Category { get; set; } 

        public ICollection<SalesTransaction> SalesTransactions { get; set; } // Ürün satışları

    }
}