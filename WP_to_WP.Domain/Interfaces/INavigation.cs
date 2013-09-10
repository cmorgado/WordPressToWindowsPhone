using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WP_to_WP.Domain.Interfaces
{
   
        public interface INavigation
        {
            /// <summary>
            /// 
            /// </summary>
            bool CanGoBack { get; }

            /// <summary>
            /// 
            /// </summary>
            void GoBack();

            /// <summary>
            /// 
            /// </summary>
            /// <typeparam name="TDestinationViewModel"></typeparam>
            /// <param name="parameter"></param>
            void Navigate<TDestinationViewModel>(object parameter = null);

            void Navigate<TDestinationViewModel>(bool sameContext, object parameter = null);
        }
    
}
