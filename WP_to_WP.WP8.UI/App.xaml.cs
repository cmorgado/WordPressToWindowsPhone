using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Telerik.Windows.Controls;
using System.Windows.Media.Imaging;
using WP_to_WP.WP8.UI.Code;
using WP_to_WP.Domain.Code;
using WP_to_WP.Shared.Code;


namespace WP_to_WP.WP8.UI
{
    public partial class App : Application
    {
        WP_to_WP.Shared.Services.UiSettings Settings ;
        /// <summary>
        /// Component used to handle unhandle exceptions, to collect runtime info and to send email to developer.
        /// </summary>
        public RadDiagnostics diagnostics;
        /// <summary>
        /// Component used to raise a notification to the end users to rate the application on the marketplace.
        /// </summary>
        public RadRateApplicationReminder rateReminder;


        /// <summary>
        /// Provides easy access to the root frame of the Phone Application.
        /// </summary>
        /// <returns>The root frame of the Phone Application.</returns>
        public PhoneApplicationFrame RootFrame { get; private set; }

        /// <summary>
        /// Constructor for the Application object.
        /// </summary>
        public App()
        {

            Settings = new WP_to_WP.Shared.Services.UiSettings();
          
            // Global handler for uncaught exceptions. 
            UnhandledException += Application_UnhandledException;

            // Standard Silverlight initialization
            InitializeComponent();

            // Phone-specific initialization
            InitializePhoneApplication();

            // Show graphics profiling information while debugging.
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // Display the current frame rate counters.
                Application.Current.Host.Settings.EnableFrameRateCounter = true;

                // Show the areas of the app that are being redrawn in each frame.
                //Application.Current.Host.Settings.EnableRedrawRegions = true;

                // Enable non-production analysis visualization mode, 
                // which shows areas of a page that are being GPU accelerated with a colored overlay.
                //Application.Current.Host.Settings.EnableCacheVisualization = true;

                // Disable the application idle detection by setting the UserIdleDetectionMode property of the
                // application's PhoneApplicationService object to Disabled.
                // Caution:- Use this under debug mode only. Application that disables user idle detection will continue to run
                // and consume battery power when the user is not using the phone.
                PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Disabled;
            }
            LocalizationManager.GlobalResourceManager = WP_to_WP.International.Translations.ResourceManager;
            //InputLocalizationManager.Instance.ResourceManager = WP_to_WP.International.Translations.ResourceManager;
            //Creates an instance of the Diagnostics component.
            diagnostics = new RadDiagnostics();

            //Defines the default email where the diagnostics info will be send.
            diagnostics.EmailTo = Settings.SupportEmail();

            //Initializes this instance.
            diagnostics.Init();

            //Creates a new instance of the RadRateApplicationReminder component.
            rateReminder = new RadRateApplicationReminder();

            //Sets how often the rate reminder is displayed.
            rateReminder.RecurrencePerUsageCount = 1;
            ThemeManager.ToDarkTheme();
        }

        // Code to execute when the application is launching (eg, from Start)
        // This code will not execute when the application is reactivated
        private void Application_Launching(object sender, LaunchingEventArgs e)
        {
            //Before using any of the ApplicationBuildingBlocks, this class should be initialized with the version of the application.
            ApplicationUsageHelper.Init(Settings.AppVersion());

            if (Settings.GetAppSettingValue<bool>(Constants.SETTINGS.LIVE_TILE))
                PeriodicTaskClient.Current.Start();
        }

        // Code to execute when the application is activated (brought to foreground)
        // This code will not execute when the application is first launched
        private void Application_Activated(object sender, ActivatedEventArgs e)
        {
            if (!e.IsApplicationInstancePreserved)
            {
                //This will ensure that the ApplicationUsageHelper is initialized again if the application has been in Tombstoned state.
                ApplicationUsageHelper.OnApplicationActivated();
               
            
            }



        }

        // Code to execute when the application is deactivated (sent to background)
        // This code will not execute when the application is closing
        private void Application_Deactivated(object sender, DeactivatedEventArgs e)
        {
            // Ensure that required application state is persisted here.
        }

        // Code to execute when the application is closing (eg, user hit Back)
        // This code will not execute when the application is deactivated
        private void Application_Closing(object sender, ClosingEventArgs e)
        {
        }

        // Code to execute if a navigation fails
        private void RootFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // A navigation has failed; break into the debugger
                System.Diagnostics.Debugger.Break();
            }
        }

        // Code to execute on Unhandled Exceptions
        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // An unhandled exception has occurred; break into the debugger
                System.Diagnostics.Debugger.Break();
            }
        }

        #region Phone application initialization

        // Avoid double-initialization
        private bool phoneApplicationInitialized = false;

        // Do not add any additional code to this method
        private void InitializePhoneApplication()
        {

            Settings.SetDefaultLanguage();

            if (phoneApplicationInitialized)
                return;

     

            int ImageSize = 1280;
            Uri image = new Uri("Assets/SplashScreenImage-WXGA.png", UriKind.RelativeOrAbsolute);
            switch (ResolutionHelper.CurrentResolution)
            {
                case Resolutions.HD720p:
                    image = new Uri("Assets/SplashScreenImage-720p.png", UriKind.RelativeOrAbsolute);
                    ImageSize = 1280;
                    break;
                case Resolutions.WXGA:
                    image = new Uri("Assets/SplashScreenImage-WXGA.png", UriKind.RelativeOrAbsolute);
                    ImageSize = 1280;
                    break;
                case Resolutions.WVGA:
                    image = new Uri("Assets/SplashScreenImage-WVGA.png", UriKind.RelativeOrAbsolute);
                    ImageSize = 800;
                    break;
                default:
                    ImageSize = 800;
                    break;
            }


            // Create the frame but don't set it as RootVisual yet; this allows the splash
            // screen to remain active until the application is ready to render.
            RootFrame = new RadPhoneApplicationFrame
            {
                Background = new ImageBrush
                {
                    ImageSource = new BitmapImage(image),
                    Opacity = 1,
                    Stretch = Stretch.UniformToFill
                }
                ,
                Height = 800
            };
            RootFrame.Navigated += CompleteInitializePhoneApplication;

            // Handle navigation failures
            RootFrame.NavigationFailed += RootFrame_NavigationFailed;

            // Ensure we don't initialize again
            phoneApplicationInitialized = true;


            RootFrame.Navigating += RootFrame_Navigating;
            RootFrame.Navigated += RootFrame_Navigated;
        }

        private bool reset;

        void RootFrame_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            string Home = "/Home.xaml";
            if (reset && e.IsCancelable && e.Uri.OriginalString == Home)
            {
                e.Cancel = true;
                reset = false;
            }
        }

        void RootFrame_Navigated(object sender, NavigationEventArgs e)
        {
            reset = e.NavigationMode == NavigationMode.Reset;
        }

        // Do not add any additional code to this method
        private void CompleteInitializePhoneApplication(object sender, NavigationEventArgs e)
        {
            // Set the root visual to allow the application to render
            if (RootVisual != RootFrame)
                RootVisual = RootFrame;

            // Remove this handler since it is no longer needed
            RootFrame.Navigated -= CompleteInitializePhoneApplication;
        }

        #endregion
    }
}
