using SearchEngine.Utils;
using System;

namespace SearchEngine.Models
{
    public class SearchResultModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Headline { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string Url { get; set; }
        public string ServiceName { get; set; }
        public DateTime CreateTime { get; set; }

        public SearchResultModel(long? inId, string inTitle, string inHeadline, DateTime inModDate, string inUrl, string inServiceName)
        {
            Id = inId.ToInt64();
            Title = inTitle.ToStr();
            Headline = inHeadline.ToStr();
            ModifiedDate = inModDate;
            Url = inUrl.ToStr();
            ServiceName = inServiceName.ToStr();
            CreateTime = DateTime.Now;
        }

        public SearchResultModel(long? inId, string inTitle, string inHeadline, string inModDate, string inUrl, string inServiceName)
        {
            Id = inId.ToInt64();
            Title = inTitle.ToStr();
            Headline = inHeadline.ToStr();
            ModifiedDate = inModDate.ToDateTime();
            Url = inUrl.ToStr();
            ServiceName = inServiceName.ToStr();
            CreateTime = DateTime.Now;
        }

        public SearchResultModel() { }
    }
}
