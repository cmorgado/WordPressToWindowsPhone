using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WP_to_WP.Domain.Models.REST.Categories
{
    public class Category
    {
        public int id { get; set; }
        public string slug { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public int parent { get; set; }
        public int post_count { get; set; }
    }

    public class RootObject
    {
        public string status { get; set; }
        public int count { get; set; }
        public List<Category> categories { get; set; }
    }
}
