using gioithieudaihocvinh.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Gioithieudaihocvinh.Models
{
    public partial class GioithieuContext : DbContext
    {
        public GioithieuContext()
        : base("name=GioiThieuDaiHocVinh")
        { }
        public virtual DbSet<Background> Backgrounds { get; set; }
        public virtual DbSet<Category> Categorys { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<New> News { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Typecategory> Typecategorys { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public object Category { get; internal set; }
    }
}