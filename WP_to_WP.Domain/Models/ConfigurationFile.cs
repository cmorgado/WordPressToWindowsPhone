using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WP_to_WP.Domain.Models
{
   public class ConfigurationFile
    {

        public string AppName { get; set; }
        public string SupportEmail { get; set; }
        public string DefaultLanguage { get; set; }
        public bool MultiLanguage { get; set; }
        public string ServicesUrl { get; set; }
        public string AppKey { get; set; }

    }
}
