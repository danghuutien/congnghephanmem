﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace gioithieudaihocvinh.Models
{
    
        [Table("Backgrounds")]
        public class Background
        {

            [Key]
            public int Id { get; set; }

            public int CatId { get; set; }
            public string Title { get; set; }
            public string Thumbnail { get; set; }
            public DateTime? Created_at { get; set; }
            public DateTime? Update_at { get; set; }


        }

    
}