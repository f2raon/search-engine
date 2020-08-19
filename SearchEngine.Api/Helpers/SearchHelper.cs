using SearchEngine.Models;
using SearchEngine.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SearchEngine.Api.Helpers
{
    public static class SearchHelper
    {
        /// <summary>
        /// Creates task for search
        /// </summary>
        /// <param name="service">service model</param>
        /// <param name="serName">search service name</param>
        /// <param name="query">search query value</param>
        /// <returns></returns>
        public static Task<ResponseModel<IList<SearchResultModel>>> SetTask(ISearchEngineService service, string serName, string query)
        {
            Settings settings = (Settings)GlobalSettings.Settings.GetType().GetProperty(serName).GetValue(GlobalSettings.Settings, null);
            service.ServiceName = serName;
            service.Url = settings.Url;
            service.Id = settings.Id;
            service.Key = settings.Key;

            return Task<ResponseModel<IList<SearchResultModel>>>.Factory.StartNew((() =>
            {
                return service.Search(query).Result;
            }));
        }
    }
}   