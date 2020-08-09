using Newtonsoft.Json;
using SearchEngine.Models;
using SearchEngine.Services.Interfaces;
using SearchEngine.Services.Models;
using SearchEngine.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine.Services.Services
{
    public class GoogleSearchEngineService : ISearchEngineService
    {
        #region properties
        public string ServiceName { get; set; }
        public string Key { get; set; }
        public string Id { get; set; }
        public string Url { get; set; }
        #endregion

        public ResponseModel<IList<SearchResultModel>> Search(string query)
        {
            List<SearchResultModel> list = new List<SearchResultModel>();
            ResponseModel<IList<SearchResultModel>> response = new ResponseModel<IList<SearchResultModel>>(1, string.Empty, string.Empty, list);

            try
            {
                #region prepare query url
                string _url = @"{0}?key={1}&cx={2}&q={3}&num={4}";
                string completeUrl = string.Format(_url, Url, Key, Id, query, "10");
                #endregion

                #region request and response
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(completeUrl);
                HttpWebResponse res = (HttpWebResponse)request.GetResponse();

                var httpResponse = (HttpWebResponse)request.GetResponse();
                string result;
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }

                var model = JsonConvert.DeserializeObject<GoogleSearchResultModel>(result);
                model.items.ToList().ForEach(s => list.Add(new SearchResultModel(
                    null, s.title, s.snippet, DateTime.Now, s.link, ServiceName
                )));
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
