using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WP_to_WP.Domain.Code;
using WP_to_WP.Domain.Interfaces;

namespace WP_to_WP.Domain.Services
{
    public class AgentService
    {
        ServiceBroker service = new ServiceBroker();
        private readonly IStorage _storageService;
        private readonly ISettings _settingsService;


        public AgentService(IStorage storageService, ISettings settingsService)
        {
            _storageService = storageService;
            _settingsService = settingsService;
           //AppBase.Current.Config.ServicesUrl;  
            service.BaseUrl =  settingsService.ServiceUrl();
        }

        public async Task<List<Models.Ui.HomeItem>> GetUpdates(string category, Interfaces.IStorage storage)
        {
          

            List<Models.Ui.HomeItem> LastResult = new List<Models.Ui.HomeItem>();
            List<Models.Ui.HomeItem> OutResult = new List<Models.Ui.HomeItem>();
            try
            {
                var onld = await storage.Exists(Constants.StorageKeys.AGENT_DATA);
                if (onld)
                {
                    var lastResultjson = await storage.ReadData(Constants.StorageKeys.AGENT_DATA);
                    LastResult = lastResultjson.LoadFromJson<List<Models.Ui.HomeItem>>();
                }
                var current = await service.GetPostsFrom(category, 0);

                foreach (var item in current.posts)
                {
                    string img = item.thumbnail;
                    if (string.IsNullOrEmpty(img))
                    {

                        img = Regex.Match(item.content, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value;
                    }
                    var p = new Models.Ui.HomeItem
                    {
                        Title = item.title_plain,
                        Resume = item.excerpt,
                        CategoryId = "112",
                        Date = item.date,
                        UniqueId = item.id.ToString(),
                        Url = img,
                        Author = item.author.name
                    };

                    if (!LastResult.Any(o => o.UniqueId ==p.UniqueId))
                    {
                        OutResult.Add(p);
                    }
                }
            }
            catch (Exception ex)
            {
                
                throw ex;
            }

            // await storage.WriteData(Constants.StorageKeys.AGENT_DATA, Result.SaveAsJson());


            return OutResult;
        }

    }
}
