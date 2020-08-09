using SearchEngine.DAL.Interfaces;
using SearchEngine.Models;
using SearchEngine.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SearchEngine.DAL.Repos
{
    public class DataLoadRepo : IDataLoad
    {
        public SearchEngineContext context { get; set; }

        public DataLoadRepo()
        {
            context = new SearchEngineContext();
        }

        public ResponseModel<IList<SearchResultModel>> Search(string query)
        {
            if (string.IsNullOrEmpty(query))
                return new ResponseModel<IList<SearchResultModel>>(1, "not ok", "query text is empty");

            List<SearchResultModel> list = new List<SearchResultModel>();
            ResponseModel<IList<SearchResultModel>> response = new ResponseModel<IList<SearchResultModel>>(1, string.Empty, string.Empty);

            try
            {
                query = query.ToLower();
                var res = context.SearchResult.Where(s => s.Title.ToLower().Contains(query) || s.Headline.ToLower().Contains(query)).ToList();

                #region prepare results
                int code = 1;
                string msg = "no data found";

                if (res.Count > 0)
                {
                    code = 0;
                    msg = "ok";
                }
                #endregion

                response = new ResponseModel<IList<SearchResultModel>>(code, msg, res.Count.ToStr(), res);
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
