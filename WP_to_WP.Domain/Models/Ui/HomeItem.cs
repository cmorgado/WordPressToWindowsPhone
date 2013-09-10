using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace WP_to_WP.Domain.Models.Ui
{



    public class DetailedItem : HomeItem
    {

        private string _Body;
        public string Body
        {
            get { return this._Body; }
            set
            {
                if (_Body != value)
                {
                    _Body = value;
                    NotifyPropertyChanged();
                }
            }
        }


        private ObservableCollection<Item> _Categories;
        public ObservableCollection<Item> Categories
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

        private ObservableCollection<Item> _Tags;
        public ObservableCollection<Item> Tags
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


        private ObservableCollection<ItemWithUrl> _Attachments;
        public ObservableCollection<ItemWithUrl> Attachments
        {
            get { return this._Attachments; }
            set
            {
                if (_Attachments != value)
                {
                    _Attachments = value;
                    NotifyPropertyChanged();
                }
            }
        }
        

        
    }

    public class HomeItem : Domain.Models.ModelBase
    {

        private string _Author;
        public string Author
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

        private string _Resume;
        public string Resume
        {
            get { return this._Resume; }
            set
            {
                if (_Resume != value)
                {
                    _Resume = value;
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



        private string _UniqueId;
        public string UniqueId
        {
            get { return this._UniqueId; }
            set
            {
                if (_UniqueId != value)
                {
                    _UniqueId = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string _CategoryId;
        public string CategoryId
        {
            get { return this._CategoryId; }
            set
            {
                if (_CategoryId != value)
                {
                    _CategoryId = value;
                    NotifyPropertyChanged();
                }
            }
        }
        
        
        

    }
}
