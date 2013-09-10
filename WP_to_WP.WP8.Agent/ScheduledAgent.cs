using System.Diagnostics;
using System.Windows;
using Microsoft.Phone.Scheduler;
using Telerik.Windows.Controls;
using Microsoft.Phone.Shell;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace WP_to_WP.WP8.Agent
{
    public class ScheduledAgent : ScheduledTaskAgent
    {


        /// <remarks>
        /// ScheduledAgent constructor, initializes the UnhandledException handler
        /// </remarks>
        static ScheduledAgent()
        {
            // Subscribe to the managed exception handler
            Deployment.Current.Dispatcher.BeginInvoke(delegate
            {
                Application.Current.UnhandledException += UnhandledException;
            });
        }

        /// Code to execute on Unhandled Exceptions
        private static void UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (Debugger.IsAttached)
            {
                // An unhandled exception has occurred; break into the debugger
                Debugger.Break();
            }
        }

        /// <summary>
        /// Agent that runs a scheduled task
        /// </summary>
        /// <param name="task">
        /// The invoked task
        /// </param>
        /// <remarks>
        /// This method is called when a periodic or resource intensive task is invoked
        /// </remarks>
        protected async override void OnInvoke(ScheduledTask task)
        {
            //TODO: Add code to perform your task in background

            WP_to_WP.Shared.Services.UiSettings settings = new WP_to_WP.Shared.Services.UiSettings();

            WP_to_WP.Shared.Services.UiStorage storage = new WP_to_WP.Shared.Services.UiStorage();

            Domain.Services.AgentService service = new Domain.Services.AgentService(storage, settings);

            var result = await service.GetUpdates("112", storage);


            if (result.Count > 0)
            {

                string toastMessage = string.Format(International.Translations.NewPosts, result.Count);

                ShellToast toast = new ShellToast();
                toast.Title = settings.AppName();
                toast.Content = toastMessage;
                toast.Show();

                if (ShellTile.ActiveTiles.Any())
                {
                    var tile = ShellTile.ActiveTiles.FirstOrDefault(o => o.NavigationUri == new Uri("/Home.xaml", UriKind.RelativeOrAbsolute));
                    var flipTileData = new RadFlipTileData
                    {
                        Count = result.Count,
                        Title = Domain.AppBase.Current.Config.AppName,
                        IsTransparencySupported = true,
                        //Title = International.Translations.AppName,
                        BackTitle = result[0].Title,
                        BackgroundImage = new Uri("/Assets/FlipCycleTileSmall_159_159.png", UriKind.RelativeOrAbsolute),
                        WideBackgroundImage = new Uri("/Assets/FlipCycleTitleLarge_691_336.png", UriKind.RelativeOrAbsolute),

                     
                        WideBackBackgroundImage = new Uri(result[0].Url, UriKind.RelativeOrAbsolute),
                        BackBackgroundImage = new Uri(result[0].Url, UriKind.RelativeOrAbsolute)

                    };


                    //List<Uri> images = new List<Uri>();
                    //RadCycleTileData RadTile = new RadCycleTileData();

                    //int start = 0;
                    //int max = 10;


                    //for (int i = 0; i < 9; i++)
                    //{
                    //    images.Add(null);

                    //}

                    //foreach (var item in result)
                    //{
                    //    images[start] = new Uri(item.Url, UriKind.Absolute);
                    //    start++;
                    //    if (start >= max)
                    //        break;

                    //}



                    //IEnumerable<Uri> CycleImages = images;

                    //RadTile.CycleImages = CycleImages;

                    LiveTileHelper.UpdateTile(tile, flipTileData);


                }
            }



            NotifyComplete();
        }
    }
}