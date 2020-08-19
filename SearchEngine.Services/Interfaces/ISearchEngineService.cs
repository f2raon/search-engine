using SearchEngine.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SearchEngine.Services.Interfaces
{
    public interface ISearchEngineService
    {
        /// <summary>
        /// Service name like Google, Yandex and Bing
        /// </summary>
        string ServiceName { get; set; }

        /// <summary>
        /// Api Id
        /// </summary>
        string Id { get; set; }

        /// <summary>
        /// Api Keys
        /// </summary>
        string Key { get; set; }

        /// <summary>
        /// Url
        /// </summary>
        string Url { get; set; }

        /// <summary>
        /// Returns first 10 (if exists) search results
        /// returns code 0 if everything fine & data exist
        /// returns code 1 if everything fine, but no data found
        /// returns code -1 if something happen with error message
        /// </summary>
        /// <param name="query">searhc query text</param>
        /// <returns></returns>
        Task<ResponseModel<IList<SearchResultModel>>> Search(string query);
    }
}
