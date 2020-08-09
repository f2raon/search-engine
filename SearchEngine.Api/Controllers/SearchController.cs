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

namespace SearchEngine.Api.Controllers
{
    public class SearchController : ApiController
    {
        private IDataLoad dataLoad;

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
                SearchHelper helper = new SearchHelper(new DataInsertRepo());
                var yandex = helper.SetTask(new YandexSearchEngineService(), "Yandex", query);
                var google = helper.SetTask(new GoogleSearchEngineService(), "Google", query);
                var bing = helper.SetTask(new BingSearchEngineService(), "Bing", query);
                #endregion

                #region start tasks and wait
                yandex.Start();
                bing.Start();
                google.Start();

                yandex.Wait();
                bing.Wait();
                google.Wait();
                #endregion

                #region prepare results
                int code = 1;
                string msg = "no data found";

                if (helper.searchResults.Count > 0)
                {
                    code = 0;
                    msg = "ok";
                }
                #endregion

                return Ok(new ResponseModel<List<SearchResultModel>>(code, msg, helper.serviceName, helper.searchResults));
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
