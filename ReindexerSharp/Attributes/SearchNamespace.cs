using System;

namespace ReindexerClient.Attributes
{
    public class SearchNamespace : Attribute
    {
        string name;

        public SearchNamespace(string name)
        {
            this.name = name;
        }

        public string Name { get { return name; } }
    }
}
