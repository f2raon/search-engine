using SearchEngine.Models;
using SearchEngine.Services.Interfaces;
using SearchEngine.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace SearchEngine.Services.Services
{
    public class YandexSearchEngineService : ISearchEngineService
    {
        #region properties
        public string ServiceName { get; set; }
        public string Key { get; set; }
        public string Id { get; set; }
        public string Url { get; set; }
        #endregion

        private string GetValue(XElement group, string name)
        {
            try
            {
                return group.Element("doc").Element(name).Value;
            }
            catch
            {
                return string.Empty;
            }
        }

        public async Task<ResponseModel<IList<SearchResultModel>>> Search(string query)
        {
            List<SearchResultModel> list = new List<SearchResultModel>();
            ResponseModel<IList<SearchResultModel>> response = new ResponseModel<IList<SearchResultModel>>(1, string.Empty, string.Empty, list);

            try
            {
                #region prepare query url
                string _url = @"{0}?user={1}&key={2}&query={3}&l10n={4}&sortby={5}&filter={6}&groupby=attr%3Dd.mode%3Ddeep.groups-on-page%3D10.docs-in-group%3D1";
                string completeUrl = string.Format(_url, Url, Id, Key, query, "en", "tm.order", "none");
                #endregion

                #region request and response
                var request = (HttpWebRequest)WebRequest.Create(completeUrl);
                var res = await request.GetResponseAsync();
                #endregion

                #region parse to xml document
                var xmlReader = XmlReader.Create(res.GetResponseStream());
                var xmlResponse = XDocument.Load(xmlReader);
                #endregion

                #region get data from xml document
                var groupQuery = from gr in xmlResponse.Elements().
                                                    Elements("response").
                                                    Elements("results").
                                                    Elements("grouping").
                                                    Elements("group")
                                select gr;

                groupQuery.ToList().ForEach(r => list.Add(new SearchResultModel(
                    null, GetValue(r, "title"), !string.IsNullOrEmpty(GetValue(r, "headline")) ?
                                                GetValue(r, "headline") : 
                                                GetValue(r, "passages"), 
                    GetValue(r, "modtime").ToDateTime("yyyyMMddTHHmmss"), GetValue(r, "saved-copy-url"), ServiceName
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
