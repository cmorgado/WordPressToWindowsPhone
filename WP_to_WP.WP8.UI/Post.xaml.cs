using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using WP_to_WP.UI;

namespace WP_to_WP.WP8.UI
{
    public partial class Post : PhoneApplicationPage
    {  //used to prevent scrolling in browser
       
        private WebBrowserHelper browserHelper;
        public Post()
        {
            InitializeComponent();

            browserHelper = new WebBrowserHelper(ContentBrowser);
            browserHelper.ScrollDisabled = true;
        }
    }
}