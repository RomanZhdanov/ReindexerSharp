using System;

namespace ReindexerClient.Attributes
{
    public class SearchIndex : Attribute
    {
        public bool isPk;

        public bool isDense;

        public bool isArray;

        public bool search;

        public double boost;

        public string function;

        public string filterValue;

        public string indexType;

        public SearchIndex()
        {

        }

        public SearchIndex(string indexType)
        {
            this.indexType = indexType;
        }
    }
}
