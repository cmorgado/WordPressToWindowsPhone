using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using WP_to_WP.Domain.Interfaces;

namespace WP_to_WP.Domain.ViewModels
{
    public class Posts : BaseViewModel
    {
        private readonly INavigation _navigationService;
        private readonly IStorage _storageService;
        private readonly ISettings _settingsService;
        private readonly IUxService _uxService;

        private string idCategory = "*";

        private ObservableCollection<Models.Ui.Item> _Categories = new ObservableCollection<Models.Ui.Item>();
        public ObservableCollection<Models.Ui.Item> Categories
        {
            get { return this._Categories; }
            set
            {
                if (_Categories != value)
                {
                    _Categories = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private ObservableCollection<Models.Ui.HomeItem> _Items = new ObservableCollection<Models.Ui.HomeItem>();
        public ObservableCollection<Models.Ui.HomeItem> Items
        {
            get { return this._Items; }
            set
            {
                if (_Items != value)
                {
                    _Items = value;
                    NotifyPropertyChanged();
                }
            }
        }



        private string _Title;
        public string Title
        {
            get { return this._Title; }
            set
            {
                if (_Title != value)
                {
                    _Title = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private Models.Ui.Item _SelectedCategory;
        public Models.Ui.Item SelectedCategory
        {
            get { return this._SelectedCategory; }
            set
            {
                if (_SelectedCategory != value)
                {
                    _SelectedCategory = value;
                    NotifyPropertyChanged();
                }
            }
        }


        private Models.Ui.HomeItem _SelectedPost;
        public Models.Ui.HomeItem SelectedPost
        {
            get { return this._SelectedPost; }
            set
            {
                if (_SelectedPost != value)
                {
                    _SelectedPost = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private RelayCommand _GoToPost;
        public RelayCommand GoToPost
        {
            get
            {
                return _GoToPost ?? (_GoToPost = new RelayCommand(
                 () =>
                 {
                     LoadingCounter++;
                     try
                     {
                         AppBase.Current.CurrentCategory = new Models.Ui.Item { ItemId = SelectedPost.CategoryId, Title = AppBase.Current.Categories.categories.Single(o => o.id == int.Parse(SelectedPost.CategoryId)).title.ToLower() };
                         AppBase.Current.CurrentPage = 0;
                         AppBase.Current.CurrentPostId = int.Parse(SelectedPost.UniqueId);
                         _navigationService.Navigate<Post>(true);
                         LoadingCounter--;
                     }
                     catch (Exception ex)
                     {
                         LoadingCounter--;
                         throw ex;
                     }

                 }));
            }

        }


        private RelayCommand _GoToCategory;
        public RelayCommand GoToCategory
        {
            get
            {
                return _GoToCategory ?? (_GoToCategory = new RelayCommand(
                 () =>
                 {
                     LoadingCounter++;
                     try
                     {
                         AppBase.Current.CurrentCategory = SelectedCategory;
                         AppBase.Current.CurrentPage = 0;
                         _navigationService.Navigate<Posts>(true, new { Id = Guid.NewGuid() });
                         LoadingCounter--;
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
                return _Load ?? (_Load = new RelayCommand(async
                    () =>
                {
                    LoadingCounter++;
                    try
                    {
                        if (idCategory != AppBase.Current.CurrentCategory.ItemId)
                        {
                            idCategory = AppBase.Current.CurrentCategory.ItemId;
                            if (Categories.Count == 0)
                            {
                                var categories = AppBase.Current.Categories;
                                var filtered = categories.categories.Where(o => o.parent == 0);
                                //var filtered = categories.categories.Where(o => o.parent == int.Parse(AppBase.Current.CurrentCategory.ItemId));
                                foreach (var item in filtered)
                                {
                                    this.Categories.Add(new Models.Ui.Item { Title = item.title.ToLower(), ItemId = item.id.ToString() });
                                }
                            }

                            Title = AppBase.Current.CurrentCategory.Title;
                            this.Items.Clear();
                            var latest = await Broker.GetPostsFrom(AppBase.Current.CurrentCategory.ItemId, AppBase.Current.CurrentPage);

                            if (latest != null)
                            {
                                if (AppBase.Current.Posts.ContainsKey(AppBase.Current.CurrentCategory.ItemId))
                                {

                                }
                                else
                                {
                                    AppBase.Current.Posts.Add(AppBase.Current.CurrentCategory.ItemId, latest);
                                }


                            }
                           
                            foreach (var item in latest.posts)
                            {
                                string img = item.thumbnail;
                                if (string.IsNullOrEmpty(img))
                                {

                                    img = Regex.Match(item.content, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value;
                                }
                                this.Items.Add(new Models.Ui.HomeItem
                                {
                                    Title = _uxService.CleanHTML( item.title_plain),
                                    Resume = _uxService.CleanHTML( item.excerpt),
                                    CategoryId = AppBase.Current.CurrentCategory.ItemId,
                                    Date = item.date,
                                    UniqueId = item.id.ToString(),
                                    Url = img
                                });
                            }
                        }

                        else
                        {

                        }





                        LoadingCounter--;
                    }
                    catch (Exception ex)
                    {
                        LoadingCounter--;
                        throw ex;
                    }

                }));
            }

        }



        public Posts(INavigation navigationService, IStorage storageService, ISettings settingsService, IUxService uxService) :
            base(navigationService, storageService, settingsService, uxService)
        {

            _navigationService = navigationService;
            _storageService = storageService;
            _settingsService = settingsService;
            _uxService = uxService;
        }
    }
}
