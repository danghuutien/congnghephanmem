using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace gioithieudaihocvinh.Models
{
    [Table("Typecategorys")]
    public class Typecategory
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public DateTime? Created_at { get; set; }
        public DateTime? Update_at { get; set; }
    }
}