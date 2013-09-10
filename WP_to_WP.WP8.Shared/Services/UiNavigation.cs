using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Telerik.Windows.Controls;
using WP_to_WP.Domain.Code;

namespace WP_to_WP.Shared.Services
{
    public class UiNavigation : Domain.Interfaces.INavigation
    {
        public static readonly Dictionary<Type, string> ViewModelRouting = new Dictionary<Type, string>
                                                                    {
                                                                          {
                                                                              typeof(Domain.ViewModels.Home), "/Home.xaml"
                                                                          },
                                                                          {
                                                                              typeof(Domain.ViewModels.Posts), "/Posts.xaml"
                                                                         }, 
                                                                         {
                                                                                typeof(Domain.ViewModels.Post), "/PostPivot.xaml"
                                                                            }
                                                                            , 
                                                                            { 
                                                                                typeof(Domain.ViewModels.About),"/About.xaml"
                                                                            }
                                                                            ,{ 
                                                                                 typeof(Domain.ViewModels.Settings), "/Settings.xaml"
                                                                             }
                                                                     };


        private Frame RootFrame
        {
            get { return Application.Current.RootVisual as Frame; }
        }

        public bool CanGoBack
        {
            get
            {
                return RootFrame.CanGoBack;
            }
        }

        public static TJson DecodeNavigationParameter<TJson>(NavigationContext context)
        {
            if (context.QueryString.ContainsKey("param"))
            {
                var param = context.QueryString["param"];
                return string.IsNullOrWhiteSpace(param) ? default(TJson) : param.LoadFromJson<TJson>();
            }

            throw new KeyNotFoundException();
        }

        public void GoBack()
        {
            RootFrame.GoBack();
        }


        public void Navigate<TDestinationViewModel>(object parameter)
        {
            var navParameter = string.Empty;
            if (parameter != null)
            {
                navParameter = "?param=" + parameter.SaveAsJson();
            }

            if (ViewModelRouting.ContainsKey(typeof(TDestinationViewModel)))
            {
                var page = ViewModelRouting[typeof(TDestinationViewModel)];

                this.RootFrame.Navigate(new Uri("/" + page + navParameter, UriKind.Relative));
            }
        }

        public void Navigate<TDestinationViewModel>(bool sameContext, object parameter)
        {
            if (sameContext)
            {
                ((RadPhoneApplicationFrame)this.RootFrame).Transition = new RadContinuumAndSlideTransition();
            }
            else
            {
                ((RadPhoneApplicationFrame)this.RootFrame).Transition = new RadTurnstileTransition();
            }
            Navigate<TDestinationViewModel>(parameter);
        }
    }
}
