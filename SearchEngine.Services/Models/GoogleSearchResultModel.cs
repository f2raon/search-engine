using System.Collections.Generic;

namespace SearchEngine.Services.Models
{
    public class GoogleSearchResultModel
    {
        public IList<Item> items { get; set; } = new List<Item>();
    }

    public class Item
    {
        public string title { get; set; }
        public string snippet { get; set; }
        public string link { get; set; }
    }
}
