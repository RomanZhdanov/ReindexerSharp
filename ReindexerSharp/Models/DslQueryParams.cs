using ReindexerClient.RxCore.Models;
using System.Collections.Generic;

namespace ReindexerClient.Models
{
    public class DslQueryParams
    {
        public string[] SelectFunctions { get; set; }

        public IList<FilterDef> Filters { get; set; }
    }
}
