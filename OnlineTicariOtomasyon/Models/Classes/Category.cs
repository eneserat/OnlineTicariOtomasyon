using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnlineTicariOtomasyon.Models.Classes
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(50)]
        public string CategoryName { get; set; }

        // Category -> Products ilişkisi (1 Category, n Product)
        public ICollection<Product> Products { get; set; }

    }
}