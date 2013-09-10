using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WP_to_WP.Domain.Models.REST.CategoryPosts
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

    public class Category2
    {
        public int id { get; set; }
        public string slug { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public int parent { get; set; }
        public int post_count { get; set; }
    }

    public class Tag
    {
        public int id { get; set; }
        public string slug { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public int post_count { get; set; }
    }

    public class Author
    {
        public int id { get; set; }
        public string slug { get; set; }
        public string name { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string nickname { get; set; }
        public string url { get; set; }
        public string description { get; set; }
    }

    public class Full
    {
        public string url { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    public class Thumbnail
    {
        public string url { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    public class Medium
    {
        public string url { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    public class Large
    {
        public string url { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    public class Images
    {
        public Full full { get; set; }
        public Thumbnail thumbnail { get; set; }
        public Medium medium { get; set; }
        public Large large { get; set; }
    }

    public class Attachment
    {
        public int id { get; set; }
        public string url { get; set; }
        public string slug { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string caption { get; set; }
        public int parent { get; set; }
        public string mime_type { get; set; }
        public Images images { get; set; }
    }

    public class Post
    {
        public int id { get; set; }
        public string type { get; set; }
        public string slug { get; set; }
        public string url { get; set; }
        public string status { get; set; }
        public string title { get; set; }
        public string title_plain { get; set; }
        public string content { get; set; }
        public string excerpt { get; set; }
        public string date { get; set; }
        public string modified { get; set; }
        public List<Category2> categories { get; set; }
        public List<Tag> tags { get; set; }
        public Author author { get; set; }
        public List<object> comments { get; set; }
        public List<Attachment> attachments { get; set; }
        public int comment_count { get; set; }
        public string comment_status { get; set; }
        public string thumbnail { get; set; }
    }

    public class RootObject
    {
        public string status { get; set; }
        public int count { get; set; }
        public int pages { get; set; }
        public Category category { get; set; }
        public List<Post> posts { get; set; }
    }
}
