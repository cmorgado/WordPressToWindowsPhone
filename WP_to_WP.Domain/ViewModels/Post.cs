using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using WP_to_WP.Domain.Interfaces;

namespace WP_to_WP.Domain.ViewModels
{
    public class Post : BaseViewModel
    {

        private readonly INavigation _navigationService;
        private readonly IStorage _storageService;
        private readonly ISettings _settingsService;
        private readonly IUxService _uxService;


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


        private string _Url;
        public string Url
        {
            get { return this._Url; }
            set
            {
                if (_Url != value)
                {
                    _Url = value;
                    NotifyPropertyChanged();
                }
            }
        }


        private string _ArticleUrl;
        public string ArticleUrl
        {
            get { return this._ArticleUrl; }
            set
            {
                if (_ArticleUrl != value)
                {
                    _ArticleUrl = value;
                    NotifyPropertyChanged();
                }
            }
        }


        private string _Content;
        public string Content
        {
            get { return this._Content; }
            set
            {
                if (_Content != value)
                {
                    _Content = value;
                    NotifyPropertyChanged();
                }
            }
        }


        private string _Date;
        public string Date
        {
            get { return this._Date; }
            set
            {
                if (_Date != value)
                {
                    _Date = value;
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


        private string _ShortTitle;
        public string ShortTitle
        {
            get { return this._ShortTitle; }
            set
            {
                if (_ShortTitle != value)
                {
                    _ShortTitle = value;
                    NotifyPropertyChanged();
                }
            }
        }


        private ObservableCollection<Models.Ui.PostPropertie> _Tags = new ObservableCollection<Models.Ui.PostPropertie>();
        public ObservableCollection<Models.Ui.PostPropertie> Tags
        {
            get { return this._Tags; }
            set
            {
                if (_Tags != value)
                {
                    _Tags = value;
                    NotifyPropertyChanged();
                }
            }
        }


        private ObservableCollection<Models.Ui.PostPropertie> _Categories = new ObservableCollection<Models.Ui.PostPropertie>();
        public ObservableCollection<Models.Ui.PostPropertie> Categories
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



        private ObservableCollection<Models.Ui.ItemWithUrl> _Photos = new ObservableCollection<Models.Ui.ItemWithUrl>();
        public ObservableCollection<Models.Ui.ItemWithUrl> Photos
        {
            get { return this._Photos; }
            set
            {
                if (_Photos != value)
                {
                    _Photos = value;
                    NotifyPropertyChanged();
                }
            }
        }



        private Models.Ui.Author _Author = new Models.Ui.Author();
        public Models.Ui.Author Author
        {
            get { return this._Author; }
            set
            {
                if (_Author != value)
                {
                    _Author = value;
                    NotifyPropertyChanged();
                }
            }
        }


        private RelayCommand _ShareSocial;
        public RelayCommand ShareSocial
        {
            get
            {
                return _ShareSocial ?? (_ShareSocial = new RelayCommand(
                 () =>
                 {

                     try
                     {

                         _uxService.Share( this.ArticleUrl, this.Title);
                     }
                     catch (Exception ex)
                     {
                         LoadingCounter--;
                         throw ex;
                     }

                 }));
            }

        }


        private RelayCommand _OpenIe;
        public RelayCommand OpenIe
        {
            get
            {
                return _OpenIe ?? (_OpenIe = new RelayCommand(
                 () =>
                 {

                     try
                     {

                         _uxService.OpenIe(this.ArticleUrl);
                     }
                     catch (Exception ex)
                     {
                         LoadingCounter--;
                         throw ex;
                     }

                 }));
            }

        }

        private RelayCommand _LoadPost;

        public RelayCommand LoadPost
        {
            get
            {
                return _LoadPost ?? (_LoadPost = new RelayCommand(
                 () =>
                 {
                     LoadingCounter++;
                     try
                     {

                         var post = AppBase.Current.Posts[AppBase.Current.CurrentCategory.ItemId].posts.SingleOrDefault(o => o.id == AppBase.Current.CurrentPostId);

                         if (post.author != null)
                             this.Author = new Models.Ui.Author
                             {
                                 Id = post.author.id,
                                 Name = post.author.name,
                                 Nickname = post.author.nickname
                             };

                         this.Content = _uxService.CleanHTML(post.excerpt);// _uxService.PrepHTML(post.content, _uxService.BackgroundColor(), _uxService.FontColor());
                         this.Title = _uxService.CleanHTML(post.title_plain);
                         this.ShortTitle = _uxService.CleanHTML(post.title.Substring(0, 11) + "...").ToLower();
                         string img = post.thumbnail;
                         if (string.IsNullOrEmpty(img))
                         {

                             img = Regex.Match(post.content, "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value;
                         }
                         this.Url = img;
                         this.ArticleUrl = post.url;
                         this.Photos.Clear();
                         if (post.attachments != null && post.attachments.Count > 0)
                         {
                             var item = post.attachments[0];
                             this.BigImage = item.images.full.url;


                             foreach (var photo in post.attachments)
                             {
                                 Debug.WriteLine(photo.images.medium.url);
                                 this.Photos.Add(new Models.Ui.ItemWithUrl { Title = photo.title, Url = photo.images.medium.url });
                             }
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


        public Post(INavigation navigationService, IStorage storageService, ISettings settingsService, IUxService uxService) :
            base(navigationService, storageService, settingsService, uxService)
        {

            _navigationService = navigationService;
            _storageService = storageService;
            _settingsService = settingsService;
            _uxService = uxService;
        }

    }
}
