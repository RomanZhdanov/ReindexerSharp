using ReindexerClient.Attributes;

namespace ReindexerClient.Helpers
{
    public static class RxNamespaceHelper
    {
        public static string GetNsNameFromType(System.Type t)
        {
            return GetNamespaceFromAttribute(t);
        }

        private static string GetNamespaceFromAttribute(System.Type t)
        {
            string name = null;

            System.Attribute[] attrs = System.Attribute.GetCustomAttributes(t);

            foreach (System.Attribute attr in attrs)
            {
                if (attr is SearchNamespace)
                {
                    var ns = (SearchNamespace)attr;

                    name = ns.Name;
                }
            }

            return name;
        }
    }
}
