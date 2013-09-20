using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WP_to_WP.Domain.Services
{
    public class ServiceBroker
    {

        public string BaseUrl { get; set; }

        private const string CategoryIndex = "api/core/get_category_index/";
        private const string PostFromCategory = "api/core/get_category_posts/?category_id={0}&page={1}";
        private const string LatestPosts = "?json=get_recent_posts&page={0}";
        private const string Post = "api/get_post/?post_id={0}";
        private HttpClient client = new HttpClient();



        public async Task<WP_to_WP.Domain.Models.REST.Categories.RootObject> GetCategories()
        {
            StringBuilder Url = new StringBuilder();
            Url.Append(BaseUrl);
            Url.Append(CategoryIndex);

            return await DoHttpGet<WP_to_WP.Domain.Models.REST.Categories.RootObject>(Url);

        }

        public async Task<WP_to_WP.Domain.Models.REST.CategoryPosts.RootObject> GetPostsFrom(string id, int page)
        {
            StringBuilder Url = new StringBuilder();
            Url.Append(BaseUrl);
            Url.AppendFormat(PostFromCategory, id, page);
            Debug.WriteLine(Url.ToString());
            return await DoHttpGet<WP_to_WP.Domain.Models.REST.CategoryPosts.RootObject>(Url);
        }

        public async Task<WP_to_WP.Domain.Models.REST.CategoryPosts.RootObject> GetLatestPosts(int page)
        {
            StringBuilder Url = new StringBuilder();
            Url.Append(BaseUrl);
            Url.AppendFormat(LatestPosts, page);
            return await DoHttpGet<WP_to_WP.Domain.Models.REST.CategoryPosts.RootObject>(Url);
        }

        private async Task<T> DoHttpGet<T>(StringBuilder Url)
        {
            HttpResponseMessage response = await client.GetAsync(Url.ToString());
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            Debug.WriteLine(content);
            return JsonConvert.DeserializeObject<T>(content);



        }
    }
}
