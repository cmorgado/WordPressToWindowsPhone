using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WP_to_WP.Domain.Interfaces;
using Logger = WP_to_WP.Domain.Services.Logger;

namespace WP_to_WP.Domain.ViewModels
{
    public partial class BaseViewModel : Models.ModelBase //  Models.ModelBase
    {

        public Services.ServiceBroker Broker = new Services.ServiceBroker ();
        private readonly INavigation _navigationService;
        private readonly IStorage _storageService;
        private readonly ISettings _settingsService;
        private readonly IUxService _uxService;


        private int loadingCounter = 0;
        public int LoadingCounter
        {
            get { return loadingCounter; }
            set
            {
                loadingCounter = value;
                if (value != loadingCounter)
                {
                    loadingCounter = value;
                    // NotifyPropertyChanged();
                }
                if (loadingCounter < 0)
                    loadingCounter = 0;

                if (loadingCounter > 0)
                {
                    IsLoading = true;
                }
                else
                {
                    IsLoading = false;
                }
            }
        }

        private bool isLoading = false;
        public bool IsLoading
        {
            get { return isLoading; }
            set
            {
                if (value != isLoading)
                {
                    isLoading = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string pageTitle = string.Empty;
        public string PageTitle
        {
            get { return pageTitle; }
            set
            {
                if (value != pageTitle)
                {
                    pageTitle = value;
                    NotifyPropertyChanged();
                }
            }
        }


        private string appName = "";
        public string ApplicationName
        {
            get { return appName; }
            set
            {
                if (value != appName)
                {
                    appName = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public BaseViewModel(INavigation navigationService, IStorage storageService, ISettings settingsService, IUxService uxService)
        {

            _navigationService = navigationService;
            _storageService = storageService;
            _settingsService = settingsService;
            _uxService = uxService;

            Broker.BaseUrl = _settingsService.ServiceUrl();
            this.ApplicationName = _settingsService.AppName();
        }

    }
}
