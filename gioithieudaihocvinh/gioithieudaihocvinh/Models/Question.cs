using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace gioithieudaihocvinh.Models
{
    [Table("Questions")]
    public class Question
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Answer { get; set; }
        public string Content { get; set; }
        public bool Status { get; set; }
        public int PostId { get; set; }

        public DateTime? Created_at { get; set; }
        public DateTime? Created_by { get; set; }
        public DateTime? Updated_by { get; set; }
        public DateTime? Updated_at { get; set; }
    }
}