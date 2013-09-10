/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:WP_to_WP.WP8.UI"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace WP_to_WP.UI.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            //if (ViewModelBase.IsInDesignModeStatic)
            //{
            //    // Create design time view services and models
            //   // SimpleIoc.Default.Register<WP_to_WP.WP8.UI.SampleData.HomeSample>();
            //}
            //else
            //{
            //    // Create run time view services and models

            //}


                SimpleIoc.Default.Register<WP_to_WP.Domain.Interfaces.INavigation, WP_to_WP.Shared.Services.UiNavigation>();
                SimpleIoc.Default.Register<WP_to_WP.Domain.Interfaces.IStorage, WP_to_WP.Shared.Services.UiStorage>();
                SimpleIoc.Default.Register<WP_to_WP.Domain.Interfaces.ISettings, WP_to_WP.Shared.Services.UiSettings>();
                SimpleIoc.Default.Register<WP_to_WP.Domain.Interfaces.IUxService, WP_to_WP.Shared.Services.UiUx>();

                SimpleIoc.Default.Register<Domain.ViewModels.Home>();
                SimpleIoc.Default.Register<Domain.ViewModels.Posts>();
                SimpleIoc.Default.Register<Domain.ViewModels.Post>();
                SimpleIoc.Default.Register<Domain.ViewModels.About>();
                SimpleIoc.Default.Register<Domain.ViewModels.Settings>();
        }

        public WP_to_WP.Domain.ViewModels.Home MainPage
        {
            get
            {
                return ServiceLocator.Current.GetInstance<WP_to_WP.Domain.ViewModels.Home>();
            }
        }

        public WP_to_WP.Domain.ViewModels.Posts Posts
        {
            get
            {
                return ServiceLocator.Current.GetInstance<WP_to_WP.Domain.ViewModels.Posts>();
            }
        }

        public WP_to_WP.Domain.ViewModels.Post Post
        {
            get
            {
                return ServiceLocator.Current.GetInstance<WP_to_WP.Domain.ViewModels.Post>();
            }
        }

        public WP_to_WP.Domain.ViewModels.About About
        {
            get
            {
                return ServiceLocator.Current.GetInstance<WP_to_WP.Domain.ViewModels.About>();
            }
        }
        public WP_to_WP.Domain.ViewModels.Settings Settings
        {
            get
            {
                return ServiceLocator.Current.GetInstance<WP_to_WP.Domain.ViewModels.Settings>();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}