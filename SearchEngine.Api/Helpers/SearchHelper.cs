using SearchEngine.DAL.Interfaces;
using SearchEngine.Models;
using SearchEngine.Services.Interfaces;
using SearchEngine.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SearchEngine.Api.Helpers
{
    public class SearchHelper
    {
        private IDataInsert dataInsert;
        public string serviceName { get; set; }
        public List<SearchResultModel> searchResults = new List<SearchResultModel>();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="inDataInsert">Repository for storing data</param>
        public SearchHelper(IDataInsert inDataInsert)
        {
            dataInsert = inDataInsert;
        }

        /// <summary>
        /// Creates task for search
        /// </summary>
        /// <param name="service">service model</param>
        /// <param name="serName">search service name</param>
        /// <param name="query">search query value</param>
        /// <returns></returns>
        public Task SetTask(ISearchEngineService service, string serName, string query)
        {
            Settings settings = (Settings)GlobalSettings.Settings.GetType().GetProperty(serName).GetValue(GlobalSettings.Settings, null);
            return new Task<ResponseModel<IList<SearchResultModel>>>(() =>
            {
                service.ServiceName = serName;
                service.Url = settings.Url;
                service.Id = settings.Id;
                service.Key = settings.Key;

                var res = service.Search(query.ToStr());
                return CatchFirstData(res);
            }, TaskCreationOptions.LongRunning);
        }

        /// <summary>
        /// Save results from first response search engine
        /// </summary>
        /// <param name="response">list of search results</param>
        /// <returns></returns>
        private ResponseModel<IList<SearchResultModel>> CatchFirstData(ResponseModel<IList<SearchResultModel>> response)
        {
            if (!string.IsNullOrEmpty(serviceName) || response.Code != 0 || response.Data.Count <= 0)
                return response;

            serviceName = response.Comment;
            searchResults = response.Data.ToList();

            dataInsert.InsertSearchResults(response.Data);

            return response;
        }
    }
}