using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace gioithieudaihocvinh.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int Id { get; set; }
        
        public string Fullname { get; set; }
        
        public string Gmail { get; set; }
        public double Phone { get; set; }
        
        public string Password { get; set; }
        public DateTime? Created_at { get; set; }
        public DateTime? Update_at { get; set; }

        


    }
}