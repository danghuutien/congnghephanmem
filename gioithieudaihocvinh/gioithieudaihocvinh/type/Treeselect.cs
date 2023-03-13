using gioithieudaihocvinh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace gioithieudaihocvinh.type
{
    public class Treeselect
    {
        public int Id;
        public string Name;
        public List<Category> Childrens;
        public Treeselect() { }
    }
}