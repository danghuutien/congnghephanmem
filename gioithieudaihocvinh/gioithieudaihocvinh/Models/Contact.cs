using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace gioithieudaihocvinh.Models
{
    [Table("Contacts")]
    public class Contact
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string FullName { get; set; }
        public string Message { get; set; }
        public string Gmail { get; set; }

        public DateTime Created_at { get; set; }
        public DateTime Update_at { get; set; }


    }
}