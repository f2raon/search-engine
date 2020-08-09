using SearchEngine.Models;
using System.Collections.Generic;

namespace SearchEngine.DAL.Interfaces
{
    public interface IDataInsert
    {
        /// <summary>
        /// Inserts Search results
        /// returns true if everything is fine, othervise false
        /// </summary>
        /// <param name="models">list of search results</param>
        /// <returns></returns>
        ResponseModel<bool> InsertSearchResults(IList<SearchResultModel> models);
    }
}
