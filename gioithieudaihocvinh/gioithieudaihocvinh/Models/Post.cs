using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace gioithieudaihocvinh.Models
{
    [Table("Posts")]
    public class Post
    {
        [Key]
        public int Id { get; set; }

        
        
        public int? CatId { get; set; }
        
        public string Title { get; set; }

        public string Slug { get; set; }
        public string Thumbnail { get; set; }
        public string Excerpt { get; set; }
        public string Content { get; set; }
        public bool IsHighlight { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }

    }
}