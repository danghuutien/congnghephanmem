using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace gioithieudaihocvinh.Models
{
    [Table("Comments")]
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public string Created_by { get; set; }
        public string Thumbnail { get; set; }
        public string Content { get; set; }
        public int? CatId { get; set; }
        public bool Status { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Update_at { get; set; }
    }
}