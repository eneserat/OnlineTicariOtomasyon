using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnlineTicariOtomasyon.Models.Classes
{
    public class ToDoList
    {
        [Key]
        public int TodolistID { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(100)]
        public string TodolistTitle { get; set; }

        [Column(TypeName = "bit")]
        public bool TodolistSituation { get; set; }

    
    }
}