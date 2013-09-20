using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WP_to_WP.Domain.Interfaces;
using System.Text.RegularExpressions;
using WP_to_WP.Domain.Code;

namespace WP_to_WP.Domain.ViewModels
{
    public class Home : BaseViewModel
    {

        private readonly INavigation _navigationService;
        private readonly IStorage _storageService;
        private readonly ISettings _settingsService;
        private readonly IUxService _uxService;

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


        private string _BigImage;
        public string BigImage
        {
            get { return this._BigImage; }
            set
            {
                if (_BigImage != value)
                {
                    _BigImage = value;
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
                         _navigationService.Navigate<Posts>(true);
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


        private RelayCommand _GoToAbout;
        public RelayCommand GoToAbout
        {
            get
            {
                return _GoToAbout ?? (_GoToAbout = new RelayCommand(
                 () =>
                 {
                     LoadingCounter++;
                     try
                     {

                         _navigationService.Navigate<About>(false);
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

        private RelayCommand _GoToSettings;
        public RelayCommand GoToSettings
        {
            get
            {
                return _GoToSettings ?? (_GoToSettings = new RelayCommand(
                 () =>
                 {
                     LoadingCounter++;
                     try
                     {

                         _navigationService.Navigate<Settings>(false);
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


        private RelayCommand _LoadHome;
        public RelayCommand LoadHome
        {

            get
            {
                return _LoadHome ?? (_LoadHome = new RelayCommand(async
                    () =>
                {

                    if (_settingsService.IsConnectedToInternet())
                    {
                        try
                        {


                            LoadingCounter++;

                                                  

                            if (this.Categories.Count == 0)
                            {
                                try
                                {
                                    var categories = await Broker.GetCategories();
                                    AppBase.Current.Categories = categories;
                                    var filtered = categories.categories.Where(o => o.parent == 0 && o.id != 137);
                                    foreach (var item in filtered)
                                    {
                                        this.Categories.Add(new Models.Ui.Item { Title = item.title.ToLower(), ItemId = item.id.ToString() });
                                    }

                                    await LoadLastPosts();
                                }
                                catch (Exception ex)
                                {

                                    _uxService.ShowAlert(International.Translations.ErrorLoadingPosts);
                                    throw ex;
                                }


                            }

                            LoadingCounter--;
                        }
                        catch (Exception ex)
                        {
                            LoadingCounter--;
                            throw ex;
                        }
                    }
                    else
                    {
                        await _uxService.ShowAlert(International.Translations.NoConnection);

                    }



                }));
            }

        }


        private RelayCommand _Refresh;
        public RelayCommand Refresh
        {

            get
            {
                return _Refresh ?? (_Refresh = new RelayCommand(async
                    () =>
                {

                    if (_settingsService.IsConnectedToInternet())
                    {
                        try
                        {
                            LoadingCounter++;

                            try
                            {
                                await LoadLastPosts();
                            }
                            catch (Exception ex)
                            {

                                throw ex;
                            }




                            LoadingCounter--;
                        }
                        catch (Exception ex)
                        {
                            LoadingCounter--;
                            throw ex;
                        }
                    }
                    else
                    {
                        await _uxService.ShowAlert(International.Translations.NoConnection);

                    }



                }));
            }

        }



        private async Task LoadLastPosts()
        {
            AppBase.Current.Posts = new Dictionary<string, Models.REST.CategoryPosts.RootObject>();
            var latest = await Broker.GetPostsFrom("112", 0);
            Items.Clear();

            foreach (var item in latest.posts)
            {
                AppBase.Current.Posts["112"] = latest;

                string img = item.thumbnail;
                if (string.IsNullOrEmpty(img))
                {

                    img = Regex.Match(item.content, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value;
                }
                var p = new Models.Ui.HomeItem
                {
                    Title = _uxService.CleanHTML(item.title_plain),
                    Resume = item.excerpt,
                    CategoryId = "112",
                    Date = item.date,
                    UniqueId = item.id.ToString(),
                    Url = img,
                    Author = item.author.name
                };
                this.Items.Add(p);

            }

            await _storageService.WriteData(Constants.StorageKeys.AGENT_DATA, Items.SaveAsJson());

            if (latest.posts[0].attachments != null && latest.posts[0].attachments.Count > 0)
                BigImage = latest.posts[0].attachments[0].images.full.url;
        }


        public Home(INavigation navigationService, IStorage storageService, ISettings settingsService, IUxService uxService) :
            base(navigationService, storageService, settingsService, uxService)
        {

            _navigationService = navigationService;
            _storageService = storageService;
            _settingsService = settingsService;
            _uxService = uxService;
        }
    }
}
