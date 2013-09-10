using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WP_to_WP.Domain
{
    public class AppBase
    {

        private static AppBase current = new AppBase();
        public static AppBase Current
        {
            get { return current; }
            set { current = value; }
        }


        private Domain.Models.ConfigurationFile config = null;
        public Domain.Models.ConfigurationFile Config
        {
            get { return config; }
            set { config = value; }
        }

        private WP_to_WP.Domain.Models.REST.Categories.RootObject categories;

        public WP_to_WP.Domain.Models.REST.Categories.RootObject Categories
        {
            get { return categories; }
            set { categories = value; }
        }


        private Dictionary<string, WP_to_WP.Domain.Models.REST.CategoryPosts.RootObject> posts = new Dictionary<string, Models.REST.CategoryPosts.RootObject>();
        public Dictionary<string, WP_to_WP.Domain.Models.REST.CategoryPosts.RootObject> Posts
        {
            get { return posts; }
            set { posts = value; }
        }

        public Models.Ui.Item CurrentCategory = new Models.Ui.Item();
        public int CurrentPage = 0;
        public int CurrentPostId = -1;

        public WP_to_WP.Domain.Models.REST.CategoryPosts.RootObject GetPostsFrom(string id)
        {
            return Posts[id];
        }

        public void AddPostsFrom(string id, WP_to_WP.Domain.Models.REST.CategoryPosts.RootObject posts)
        {

            if (Posts.ContainsKey(id))
            {
                Posts[id].posts.AddRange(posts.posts);

            }
            else
                Posts.Add(id, posts);
        }

    }
}
