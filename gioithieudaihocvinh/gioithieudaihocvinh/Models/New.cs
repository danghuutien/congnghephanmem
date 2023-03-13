using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace gioithieudaihocvinh.Models
{
    [Table("News")]
    public class New
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Create_at { get; set; }
        public DateTime Update_at { get; set; }

    }
}