using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WP_to_WP.Domain.Models.Ui
{
    public  class PostPropertie:ModelBase
    {


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



        private int _Count;
        public int Count
        {
            get { return this._Count; }
            set
            {
                if (_Count != value)
                {
                    _Count = value;
                    NotifyPropertyChanged();
                }
            }
        }
        

    }
}
