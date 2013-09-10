using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WP_to_WP.Domain.Models.Ui
{
   public class Author:ModelBase
    {


        private int _Id;
        public int Id
        {
            get { return this._Id; }
            set
            {
                if (_Id != value)
                {
                    _Id = value;
                    NotifyPropertyChanged();
                }
            }
        }
        
        private string _Nickname;
        public string Nickname
        {
            get { return this._Nickname; }
            set
            {
                if (_Nickname != value)
                {
                    _Nickname = value;
                    NotifyPropertyChanged();
                }
            }
        }
        
        private string _Name;
        public string Name
        {
            get { return this._Name; }
            set
            {
                if (_Name != value)
                {
                    _Name = value;
                    NotifyPropertyChanged();
                }
            }
        }
        
    }
}
