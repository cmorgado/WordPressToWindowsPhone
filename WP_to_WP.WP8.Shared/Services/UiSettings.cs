using Cimbalino.Phone.Toolkit.Helpers;
using Cimbalino.Phone.Toolkit.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Storage;
using WP_to_WP.Domain.Code;

namespace WP_to_WP.Shared.Services
{
   public class UiSettings : Domain.Interfaces.ISettings
    {

        Cimbalino.Phone.Toolkit.Services.StorageService storage = new StorageService();

        public void SaveAppSettingValue(string Key, object value)
        {
            var settings = IsolatedStorageSettings.ApplicationSettings;
            if (!settings.Any(o => o.Key == "SETTINGS_" + Key))

                settings.Add("SETTINGS_" + Key, value.SaveAsJson());
            else
                settings["SETTINGS_" + Key] = value.SaveAsJson();
        }

        public T GetAppSettingValue<T>(string Key)
        {
            string val = string.Empty;
            var settings = IsolatedStorageSettings.ApplicationSettings;
            var pushsett = settings.Any(o => o.Key == "SETTINGS_" + Key);

            if (!pushsett)
            {

                return default(T);
            }
            else
            {
                string v = settings["SETTINGS_" + Key].ToString();
                return v.LoadFromJson<T>();
            }


        }

        public string AppName()
        {
            if (Domain.AppBase.Current.Config == null)
            {
                GetConfig().Wait();
            }
            return Domain.AppBase.Current.Config.AppName;
        }

        public  string AppVersion()
        {
            ApplicationManifestService manifest = new ApplicationManifestService();


            return manifest.GetApplicationManifest().App.Version;
        }

        public string AppKey()
        {

            if (Domain.AppBase.Current.Config == null)
            {
                GetConfig().Wait();
            }
            return Domain.AppBase.Current.Config.AppKey;
        }

        private async Task<Domain.Models.ConfigurationFile> GetConfig()
        {




            string folder = Package.Current.InstalledLocation.Path;
            string path = string.Format(@"{0}/Assets/Config/Configuration.json", folder);



            var storageFile = await StorageFile.GetFileFromPathAsync(path);
            string configjson;
            using (Stream textStream = await storageFile.OpenStreamForReadAsync())
            {
                using (StreamReader textReader = new StreamReader(textStream, System.Text.Encoding.Unicode))
                {

                    configjson = textReader.ReadToEnd();
                }
            }



            var config = configjson.LoadFromJson<Domain.Models.ConfigurationFile>();
            Domain.AppBase.Current.Config = config;
            return config;

        }

        public string PublisherEmail()
        {

            if (Domain.AppBase.Current.Config == null)
            {
                GetConfig().Wait();
            }
            return Domain.AppBase.Current.Config.SupportEmail;
        }

        public string SupportEmail()
        {

            if (Domain.AppBase.Current.Config == null)
            {
                GetConfig().Wait();
            }
            return Domain.AppBase.Current.Config.SupportEmail;
        }

        public string ServiceUrl()
        {
            if (Domain.AppBase.Current.Config == null)
            {
                GetConfig().Wait();
            }
            return Domain.AppBase.Current.Config.ServicesUrl;
        }

        public bool IsConnectedToInternet()
        {
            return Microsoft.Phone.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
        }

        public async Task SetDefaultLanguage()
        {
            if (Domain.AppBase.Current.Config == null)
              await  GetConfig();

            CultureInfo cc, cuic;
            cc = Thread.CurrentThread.CurrentCulture;
            cuic = Thread.CurrentThread.CurrentUICulture;
            if (Domain.AppBase.Current.Config != null)
            {
                CultureInfo newCulture = new CultureInfo(Domain.AppBase.Current.Config.DefaultLanguage);
                Thread.CurrentThread.CurrentCulture = newCulture;
                Thread.CurrentThread.CurrentUICulture = newCulture;
            }
        }

        public  UiSettings()
        {

         

        }
    }
}
