using Newtonsoft.Json;
using SearchEngine.Utils;
using System;
using System.IO;

namespace SearchEngine.Api.Helpers
{
    public static class GlobalSettings
    {
        public static AppSettings Settings { get; set; }


        public static void GetAppSettings()
        {
            try
            {
                using (StreamReader r = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "\\settings.json"))
                {
                    string json = r.ReadToEnd();
                    Settings = JsonConvert.DeserializeObject<AppSettings>(json);
                }
            }
            catch (Exception ex)
            {
                LogManager.Error(ex);
            }
        }

    }
}