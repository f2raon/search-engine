using SearchEngine.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SearchEngine.Api.Helpers
{
    public static class Extensions
    {
        public static async Task<TResult> WaitForFirstCompleted<TResult>(this IEnumerable<Task<TResult>> tasks)
        {
            var taskList = new List<Task<TResult>>(tasks);
            while (taskList.Count > 0)
            {
                Task<TResult> firstCompleted = await Task.WhenAny(taskList).ConfigureAwait(false);
                ResponseModel<IList<SearchResultModel>> res = firstCompleted.Result as ResponseModel<IList<SearchResultModel>>;
                if (firstCompleted.Status == TaskStatus.RanToCompletion && res.Code == 0)
                {
                    return firstCompleted.Result;
                }
                taskList.Remove(firstCompleted);
            }
            throw new InvalidOperationException("No task completed successful");
        }
    }
}