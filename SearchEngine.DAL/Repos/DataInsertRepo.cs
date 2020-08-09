using SearchEngine.DAL.Interfaces;
using SearchEngine.Models;
using SearchEngine.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SearchEngine.DAL.Repos
{
    public class DataInsertRepo : IDataInsert
    {
        private SearchEngineContext context;
        public DataInsertRepo()
        {
            context = new SearchEngineContext();
        }

        public ResponseModel<bool> InsertSearchResults(IList<SearchResultModel> models)
        {
            ResponseModel<bool> response = new ResponseModel<bool>(1, string.Empty, string.Empty);

            try
            {
                context.SearchResult.AddRange(models.ToList());
                int res = context.SaveChanges();
                response = new ResponseModel<bool>(0, "ok", "items added -> " + res.ToStr());
            }
            catch (Exception ex)
            {
                LogManager.Error(ex);
                response = new ResponseModel<bool>(-1, "error", ex.GetAllMessages());
            }

            return response;
        }
    }
}
