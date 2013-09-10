using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WP_to_WP.Domain.Code;
using WP_to_WP.Domain.Interfaces;

namespace WP_to_WP.Domain.ViewModels
{
    public class Settings : BaseViewModel
    {

        private readonly INavigation _navigationService;
        private readonly IStorage _storageService;
        private readonly ISettings _settingsService;
        private readonly IUxService _uxService;


        private bool _LiveTile = false;
        public bool LiveTile
        {
            get { return this._LiveTile; }
            set
            {
                if (_LiveTile != value)
                {
                    _LiveTile = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private RelayCommand _Save;
        public RelayCommand Save
        {
            get
            {
                return _Save ?? (_Save = new RelayCommand(
                 () =>
                 {

                     try
                     {
                         _settingsService.SaveAppSettingValue(Constants.SETTINGS.LIVE_TILE, this.LiveTile);

                         if (this.LiveTile)
                         {
                             if (_uxService.HasLiveTile("/Home.xaml") == false)
                             {
                                 _uxService.CreateLiveTile("/Home.xaml");
                             }
                             _uxService.StartAgent();
                         }
                         else
                         {
                             if (_uxService.AgentEnable())
                                 _uxService.EndAgent();
                         }



                     }
                     catch (Exception ex)
                     {
                         LoadingCounter--;
                         throw ex;
                     }

                 }));
            }

        }


        private RelayCommand _Load;
        public RelayCommand Load
        {
            get
            {
                return _Load ?? (_Load = new RelayCommand(
                 () =>
                 {

                     try
                     {
                         this.LiveTile = _settingsService.GetAppSettingValue<bool>(Constants.SETTINGS.LIVE_TILE);

                     }
                     catch (Exception ex)
                     {
                         LoadingCounter--;
                         throw ex;
                     }

                 }));
            }

        }


        public Settings(INavigation navigationService, IStorage storageService, ISettings settingsService, IUxService uxService) :
            base(navigationService, storageService, settingsService, uxService)
        {

            _navigationService = navigationService;
            _storageService = storageService;
            _settingsService = settingsService;
            _uxService = uxService;
        }

    }
}
