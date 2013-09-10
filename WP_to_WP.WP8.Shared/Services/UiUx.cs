

using Coding4Fun.Toolkit.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using Telerik.Windows.Controls;



namespace WP_to_WP.Shared.Services
{
    public class UiUx : Domain.Interfaces.IUxService
    {
        public Task ShowAlert(string m)
        {
            return Task.Factory.StartNew(() =>
            {
                MessageBox.Show(m);
            });


        }

        public Task ShowToast(string m)
        {
            return Task.Factory.StartNew(() =>
            {
                ToastPrompt toast = new ToastPrompt();
                toast.FontSize = 20;
                toast.Title = Domain.AppBase.Current.Config.AppName;
                toast.Message = m;
                // toast.ImageSource = new BitmapImage(new Uri("/ApplicationIcon.png", UriKind.RelativeOrAbsolute));
                toast.TextOrientation = System.Windows.Controls.Orientation.Horizontal;
                toast.Show();
            });
        }

        public void Share(string url, string title)
        {
            ShareLinkTask taskL = new ShareLinkTask();
            taskL.LinkUri = new Uri(url);
            taskL.Message = "";
            taskL.Title = title;
            taskL.Show();
        }

        public void OpenIe(string Url)
        {
            WebBrowserTask webBrowserTask = new WebBrowserTask { Uri = new Uri(Url, UriKind.RelativeOrAbsolute) };
            webBrowserTask.Show();
        }

        public string CleanHTML(string baseHTML)
        {
            string expn = "<.*?>";

            baseHTML = baseHTML.Replace("&nbsp;", "");
            var Result = Regex.Replace(baseHTML, expn, string.Empty);
            return HttpUtility.HtmlDecode(Result);
        }

        public string PrepHTML(string baseHTML, string BackgroundColor, string FontColor)
        {

            string expn = "<.*?>";

            baseHTML = baseHTML.Replace("&nbsp;", "");
            var Result = Regex.Replace(baseHTML, expn, string.Empty);

            if (Result.Length > 2000)
            {
                Result = Result.Substring(0, 2000) + "...";
            }

            return HttpUtility.HtmlDecode(Result);

            //            string o = "";
            //            o += "<html><head>";

            //            //prevent zooming
            //            o += "<meta name='viewport' content='width=320,user-scalable=no'/>";

            //            //inject the theme
            //            o += "<style type='text/css'>" +
            //                "body {{font-size:1.0em;background-color:#c6bea6;" +
            //                "color:" + FontColor + ";}} " + "</style>";

            //            //inject the script to pass link taps out of the browser
            //            o += "<script type='text/javascript'>";
            //            o += @"function getLinks(){ 
            //                a = document.getElementsByTagName('a');
            //                    for(var i=0; i < a.length; i++){
            //                    var msg = a[i].href;
            //                    a[i].onclick = function() {notify(msg);
            //                    };
            //                    }
            //                    }
            //                    function notify(msg) {
            //                    window.external.Notify(msg);
            //                    event.returnValue=false;
            //                    return false;
            //                }";

            //            //inject the script to find height
            //            o += @"function Scroll() {
            //                            var elem = document.getElementById('content');
            //                            window.external.Notify(elem.scrollHeight + '');
            //                        }
            //                    ";

            //            //remove all anchors
            //            while (baseHTML.Contains("<a class=\"anchor\""))
            //            {
            //                int start = baseHTML.IndexOf("<a class=\"anchor\"");
            //                int end = baseHTML.IndexOf("</h2>", start);

            //                baseHTML = baseHTML.Remove(start, end - start);
            //            }

            //            //FIXME remove this when we fix the webbrowser
            //            //remove all links
            //            while (baseHTML.Contains("<a href"))
            //            {
            //                //remove most of the link
            //                int start = baseHTML.IndexOf("<a href");
            //                int end = baseHTML.IndexOf(">", start);

            //                baseHTML = baseHTML.Remove(start, end + 1 - start);

            //                //remove end tag
            //                start = baseHTML.IndexOf("</a>", start);
            //                baseHTML = baseHTML.Remove(start, "</a>".Length);
            //            }

            //            o += @"window.onload = function() {
            //                    Scroll();
            //                    getLinks();
            //                }";

            //            o += "</script>";
            //            o += "</head>";
            //            o += "<body><div id='content'>";
            //            o += baseHTML.Trim();
            //            o += "</div></body>";
            //            o += "</html>";
            //            return o;
        }


        public string FontColor()
        {
            return Application.Current.Resources["Mainforground"].ToString();
        }
        public string BackgroundColor()
        {
            return Application.Current.Resources["MainBackground"].ToString();
        }

        public void SendEmail(string destinationEmail, string subject, string body)
        {


            EmailComposeTask emailComposeTask = new EmailComposeTask();
            emailComposeTask.To = destinationEmail;
            emailComposeTask.Subject = subject;
            emailComposeTask.Body = body;
            emailComposeTask.Show();
        }


        public bool HasLiveTile(string Uri)
        {
            if (!string.IsNullOrEmpty(Uri))
                return ShellTile.ActiveTiles.Any(o => o.NavigationUri == new Uri(Uri, UriKind.RelativeOrAbsolute));
            else
                return ShellTile.ActiveTiles.Count() > 1 ? true : false;
        }

        public void CreateLiveTile(string Uri)
        {


            if (ShellTile.ActiveTiles.Any())
            {
                var tile = ShellTile.ActiveTiles.First();
                var flipTileData = new RadFlipTileData
                {
                    Count = 0,
                    Title = Domain.AppBase.Current.Config.AppName,
                    IsTransparencySupported = true,
                    //Title = International.Translations.AppName,
                    //BackTitle = International.Translations.AppName,
                    BackgroundImage = new Uri("/Assets/FlipCycleTileSmall_159_159.png", UriKind.RelativeOrAbsolute),
                    WideBackgroundImage = new Uri("/Assets/FlipCycleTitleLarge_691_336.png", UriKind.RelativeOrAbsolute),
                    BackBackgroundImage = new Uri("/",UriKind.RelativeOrAbsolute),
                    WideBackBackgroundImage = new Uri("/", UriKind.RelativeOrAbsolute), BackTitle=""

                };
                if (!string.IsNullOrEmpty(Uri))
                    LiveTileHelper.CreateOrUpdateTile(flipTileData, new Uri(Uri, UriKind.RelativeOrAbsolute), true, true);
                else
                    LiveTileHelper.CreateOrUpdateTile(flipTileData, new Uri("/Home.xaml", UriKind.RelativeOrAbsolute), true, true);

            }

        }



        public void StartAgent()
        {
            WP_to_WP.Shared.Code.PeriodicTaskClient.Current.Start();
        }

        public void EndAgent()
        {
            WP_to_WP.Shared.Code.PeriodicTaskClient.Current.Remove();
        }


        public bool AgentEnable()
        {
            return WP_to_WP.Shared.Code.PeriodicTaskClient.Current.Exists();
        }
    }
}
