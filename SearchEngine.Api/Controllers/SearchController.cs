using System.Collections.Generic;
using System.Web.Http;
using SearchEngine.Api.Helpers;
using SearchEngine.DAL.Repos;
using SearchEngine.Models;
using SearchEngine.Services.Services;
using SearchEngine.Utils;
using System;
using Swashbuckle.Swagger.Annotations;
using System.Net;
using SearchEngine.DAL.Interfaces;
using System.Threading.Tasks;
using System.Linq;

namespace SearchEngine.Api.Controllers
{
    public class SearchController : ApiController
    {
        private IDataLoad dataLoad;
        private IDataInsert dataInsert;

        [HttpGet, Route("api/v1/SearchEngine/Search")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(ResponseModel<List<SearchResultModel>>), Description = "Search")]
        public IHttpActionResult Search(string query = null)
        {
            if (string.IsNullOrEmpty(query))
                return Ok(new ResponseModel<List<SearchResultModel>>(1, "not ok", "search query is empty"));

            try
            {
                #region setting tasks
                GlobalSettings.GetAppSettings();
                dataInsert = new DataInsertRepo();

                var yandex = SearchHelper.SetTask(new YandexSearchEngineService(), "Yandex", query);
                var google = SearchHelper.SetTask(new GoogleSearchEngineService(), "Google", query);
                var bing = SearchHelper.SetTask(new BingSearchEngineService(), "Bing", query);

                Task<ResponseModel<IList<SearchResultModel>>>[] tasks = new Task<ResponseModel<IList<SearchResultModel>>>[3] 
                { 
                    yandex, google, bing
                };

                int a = Task.WaitAny(tasks);
                var result = tasks[a].Result;
                #endregion

                #region save data
                if (result.Code == 0 && result.Data.Count > 0)
                    dataInsert.InsertSearchResults(result.Data.ToList());
                #endregion

                return Ok(result);
            }
            catch (Exception ex)
            {
                LogManager.Error(ex);
                return Ok(new ResponseModel<List<SearchResultModel>>(-1, "error", ex.GetAllMessages()));
            }
        }

        [HttpGet, Route("api/v1/SearchEngine/Filter")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(ResponseModel<List<SearchResultModel>>), Description = "Filter searches")]
        public IHttpActionResult Filter(string query = null)
        {
            if (string.IsNullOrEmpty(query))
                return Ok(new ResponseModel<List<SearchResultModel>>(1, "not ok", "search query is empty"));

            try
            {
                dataLoad = new DataLoadRepo();
                return Ok(dataLoad.Search(query.ToStr()));
            }
            catch (Exception ex)
            {
                LogManager.Error(ex);
                return Ok(new ResponseModel<List<SearchResultModel>>(-1, "error", ex.GetAllMessages()));
            }
        }
    }
}
