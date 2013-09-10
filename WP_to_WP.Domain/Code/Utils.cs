using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WP_to_WP.Domain.Code
{
    public static class Utils
    {
        public static string SaveAsJson(this object objectToSave)
        {
            try
            {
                if (objectToSave == null) return null;

                return JsonConvert.SerializeObject(objectToSave);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        public static TJson LoadFromJson<TJson>(this string jsonString)
        {
            try
            {
              
                return JsonConvert.DeserializeObject<TJson>(jsonString);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return default(TJson);
            }
        }
    }
}
