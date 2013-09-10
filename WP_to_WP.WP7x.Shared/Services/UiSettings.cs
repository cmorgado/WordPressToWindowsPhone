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
using System.Windows;
using System.Windows.Resources;
using WP_to_WP.Domain.Code;

namespace WP_to_WP.Shared.Services
{
    public class UiSettings : Domain.Interfaces.ISettings
    {

        Cimbalino.Phone.Toolkit.Services.StorageService storage = new StorageService();

        public void SaveAppSettingValue(string Key, object value)
        {
            var settings = IsolatedStorageSettings.ApplicationSettings;

            settings.Add("SETTINGS_" + Key, value.SaveAsJson());
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


            return Domain.AppBase.Current.Config.AppName;
        }

        public string AppVersion()
        {
            ApplicationManifestService manifest = new ApplicationManifestService();


            return manifest.GetApplicationManifest().App.Version;
        }

        public string AppKey()
        {


            return Domain.AppBase.Current.Config.AppKey;
        }

        private async void GetConfig()
        {

            //string folder = Package.Current.InstalledLocation.Path;
            //string path = string.Format(@"{0}/Assets/Config/Configuration.json", folder);

            StreamResourceInfo info = Application.GetResourceStream(new Uri("WP_to_WP.WP7x.UI;component/Configuration/config.txt", UriKind.Relative));
            StreamReader reader = new StreamReader(info.Stream, System.Text.Encoding.Unicode);
            string configjson = reader.ReadToEnd();




            var config = configjson.LoadFromJson<Domain.Models.ConfigurationFile>();
            Domain.AppBase.Current.Config = config;


        }

        public string PublisherEmail()
        {


            return Domain.AppBase.Current.Config.SupportEmail;
        }

        public string SupportEmail()
        {


            return Domain.AppBase.Current.Config.SupportEmail;
        }

        public string ServiceUrl()
        {


            return Domain.AppBase.Current.Config.ServicesUrl;
        }

        public bool IsConnectedToInternet()
        {
            return Microsoft.Phone.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
        }

        public  void SetDefaultLanguage()
        {
            CultureInfo cc, cuic;
            cc = Thread.CurrentThread.CurrentCulture;
            cuic = Thread.CurrentThread.CurrentUICulture;

            CultureInfo newCulture = new CultureInfo(Domain.AppBase.Current.Config.DefaultLanguage);
            Thread.CurrentThread.CurrentCulture = newCulture;
            Thread.CurrentThread.CurrentUICulture = newCulture;
        }

        public UiSettings()
        {

            GetConfig();

        }
    }
}
