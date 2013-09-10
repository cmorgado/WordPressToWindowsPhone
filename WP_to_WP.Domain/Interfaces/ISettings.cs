using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WP_to_WP.Domain.Interfaces
{
    public interface ISettings
    {
        T GetAppSettingValue<T>(string Key);
        void SaveAppSettingValue(string Key, object value );
        string AppName();
        string PublisherEmail();
        string SupportEmail();
        string ServiceUrl();
        bool IsConnectedToInternet();
        string AppVersion();
    }
}
