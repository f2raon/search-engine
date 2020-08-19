using Microsoft.Azure.CognitiveServices.Search.CustomSearch;
using SearchEngine.Models;
using SearchEngine.Services.Interfaces;
using SearchEngine.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchEngine.Services.Services
{
    public class BingSearchEngineService : ISearchEngineService
    {
        #region properties
        public string ServiceName { get; set; }
        public string Key { get; set; }
        public string Id { get; set; }
        public string Url { get; set; }
        #endregion

        public async Task<ResponseModel<IList<SearchResultModel>>> Search(string query)
        {
            List<SearchResultModel> list = new List<SearchResultModel>();
            ResponseModel<IList<SearchResultModel>> response = new ResponseModel<IList<SearchResultModel>>(1, string.Empty, string.Empty, list);

            try
            {
                #region query
                var client = new CustomSearchClient(new ApiKeyServiceClientCredentials(Key));
                var webData = await client.CustomInstance.SearchAsync(query: query, customConfig: Id);
                #endregion

                #region prepare data
                if (webData?.WebPages?.Value?.Count > 0)
                {
                    var data = webData.WebPages.Value;
                    data.ToList().ForEach(r => list.Add(new SearchResultModel(
                        null, r.Name, r.Snippet, r.DateLastCrawled, r.Url, ServiceName 
                    )));
                }
                #endregion

                #region prepare results
                int code = 1;
                string msg = "no data found";

                if (list.Count > 0)
                {
                    code = 0;
                    msg = "ok";
                }
                #endregion

                response = new ResponseModel<IList<SearchResultModel>>(code, msg, ServiceName, list);
            }
            catch (Exception ex)
            {
                LogManager.Error(ex);
                response = new ResponseModel<IList<SearchResultModel>>(-1, "error", ex.GetAllMessages());
            }

            return response;
        }
    }
}
