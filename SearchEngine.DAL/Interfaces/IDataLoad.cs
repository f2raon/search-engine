using SearchEngine.Models;
using System.Collections.Generic;

namespace SearchEngine.DAL.Interfaces
{
    public interface IDataLoad
    {
        /// <summary>
        /// Search in search results
        /// </summary>
        /// <param name="query">query for searching by title or headline</param>
        /// <returns></returns>
        ResponseModel<IList<SearchResultModel>> Search(string query);
    }
}
