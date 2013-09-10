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
using Microsoft.Phone.Tasks;
using System.Diagnostics;

namespace WP_to_WP.WP8.UI
{
    public partial class PostPivot : PhoneApplicationPage
    {

        private WebBrowserHelper browserHelper;
        public PostPivot()
        {
            InitializeComponent();

            //browserHelper = new WebBrowserHelper(ContentBrowser);
            //browserHelper.ScrollDisabled = true;
        }


        //void InfoBrowser_ScriptNotify(object sender, NotifyEventArgs e)
        //{
        //    Debug.WriteLine("e.value == " + e.Value);

        //    int height = -1;

        //    browserHelper.ScrollDisabled = true;

        //    //if we got the resize info http://dan.clarke.name/2011/05/resizing-wp7-webbrowser-height-to-fit-content/
        //    if (int.TryParse(e.Value, out height))
        //    {
        //        Debug.WriteLine("\tsetting browser height to " + height);
        //        double newHeight = height * 1.5;

        //        ContentBrowser.Height = newHeight + 0;
        //    }
        //    //if we got a URL to navigate to
        //    else if (Uri.IsWellFormedUriString(e.Value, UriKind.RelativeOrAbsolute))
        //    {
        //        Debug.WriteLine("\tnavigating to e.value");
        //        WebBrowserTask webBrowserTask = new WebBrowserTask { Uri = new Uri(e.Value) };
        //        webBrowserTask.Show();
        //    }

        //}

  

    }
}