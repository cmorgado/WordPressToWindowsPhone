using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WP_to_WP.Domain.Interfaces;

namespace WP_to_WP.Domain.ViewModels
{
    public class About : BaseViewModel
    {
        private readonly INavigation _navigationService;
        private readonly IStorage _storageService;
        private readonly ISettings _settingsService;
        private readonly IUxService _uxService;






        private string _AppVersion;
        public string AppVersion
        {
            get { return this._AppVersion; }
            set
            {
                if (_AppVersion != value)
                {
                    _AppVersion = value;
                    NotifyPropertyChanged();
                }
            }
        }



        private RelayCommand<string> _SendEmail;
        public RelayCommand<string> SendEmail
        {
            get
            {
                return _SendEmail ?? (_SendEmail = new RelayCommand<string>(async
                 (email) =>
                 {

                     try
                     {

                         _uxService.SendEmail(email,  _settingsService.AppName(), "");
                     }
                     catch (Exception ex)
                     {
                         LoadingCounter--;
                         throw ex;
                     }

                 }));
            }

        }


        public About(INavigation navigationService, IStorage storageService, ISettings settingsService, IUxService uxService) :
            base(navigationService, storageService, settingsService, uxService)
        {

            _navigationService = navigationService;
            _storageService = storageService;
            _settingsService = settingsService;
            _uxService = uxService;
            _AppVersion = this._settingsService.AppVersion();
        }
    }
}
