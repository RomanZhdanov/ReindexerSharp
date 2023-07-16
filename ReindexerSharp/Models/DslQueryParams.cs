using ReindexerClient.RxCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReindexerClient.Models
{
    public class DslQueryParams
    {
        public string[] SelectFunctions { get; set; }

        public FilterDef[] Filters { get; set; }
    }
}
