using Microsoft.Phone.Scheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP_to_WP.Domain.Code;

namespace WP_to_WP.UI.Code
{
    public class PeriodicTaskClient
    {

        private static PeriodicTaskClient current = new PeriodicTaskClient();

        public static PeriodicTaskClient Current
        {
            get { return current; }
        }

        public void Remove()
        {
            PeriodicTask periodicTask = new PeriodicTask(Constants.SETTINGS.LIVE_TILE_AGENT);
            if (ScheduledActionService.Find(periodicTask.Name) != null)
            {
                ScheduledActionService.Remove(Constants.SETTINGS.LIVE_TILE_AGENT);
            }
        }

        public bool Exists()
        {
            return ScheduledActionService.Find(Constants.SETTINGS.LIVE_TILE_AGENT) != null ? true : false;
        }

        public void Start()
        {

            PeriodicTask periodicTask = new PeriodicTask(Constants.SETTINGS.LIVE_TILE_AGENT);

            WP_to_WP.UI.Services.UiSettings Settings = new WP_to_WP.UI.Services.UiSettings();

            periodicTask.Description = Settings.AppName() + " Task";
            periodicTask.ExpirationTime = System.DateTime.Now.AddDays(10);

            if (Exists())
            {
                ScheduledActionService.Remove(Constants.SETTINGS.LIVE_TILE_AGENT);
            }

            try
            {
                ScheduledActionService.Add(periodicTask);

#if DEBUG
                ScheduledActionService.LaunchForTest(Constants.SETTINGS.LIVE_TILE_AGENT, TimeSpan.FromSeconds(60));
#endif


            }
            catch (InvalidOperationException ex)
            {
                if (ex.Message.Contains("BNS Error: The action is disabled"))
                {
                    // MessageBox.Show("Background agents for this application have been disabled by the user.");
                }

            }
        }
    }
}
