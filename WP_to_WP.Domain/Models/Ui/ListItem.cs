using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WP_to_WP.Domain.Models.Ui
{

    public class ItemWithUrl : Item
    {

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
        
    }

  public  class Item:ModelBase
    {

        private string _ItemId;
        public string ItemId
        {
            get { return this._ItemId; }
            set
            {
                if (_ItemId != value)
                {
                    _ItemId = value;
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
        
    }
}
