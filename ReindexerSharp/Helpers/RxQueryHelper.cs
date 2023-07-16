using ReindexerClient.Attributes;
using ReindexerClient.Models;
using ReindexerClient.RxCore.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ReindexerClient.Helpers
{
    public static class RxQueryHelper
    {
        public static DSLQuery CreateDSLQuery(System.Type t, string query)
        {
            var queryParams = GetQueryParamsFromType(t, query);

            return new DSLQuery
            {
                Namespace = RxNamespaceHelper.GetNsNameFromType(t),
                Type = "select",
                RequestTotal = "enabled",
                Filters = queryParams.Filters,
                SelectFunctions = queryParams.SelectFunctions,
            };
        }

        private static DslQueryParams GetQueryParamsFromType(System.Type t, string input)
        {
            List<string> fieldsBoost = new List<string>();
            List<string> functions = new List<string>();
            List<FilterDef> filters = new List<FilterDef>();

            fieldsBoost.Add("*^0.4");

            var properties = t.GetProperties();

            foreach (var property in properties)
            {
                System.Attribute[] attrs = System.Attribute.GetCustomAttributes(property);

                foreach (System.Attribute attr in attrs)
                {
                    if (attr is SearchIndex)
                    {
                        var searchIndex = (SearchIndex)attr;
                        var name = property.Name.ToLowerInvariant();

                        if (searchIndex.search && searchIndex.boost != 0)
                        {
                            fieldsBoost.Add($"{name}^{searchIndex.boost.ToString(new CultureInfo("en-us", false))}");                            
                        }

                        if (!string.IsNullOrEmpty(searchIndex.function))
                        {
                            functions.Add($"{name} = {searchIndex.function}");
                        }

                        if (!string.IsNullOrEmpty(searchIndex.filterValue))
                        {
                            filters.Add(new FilterDef()
                            {
                                Field = name,
                                Condition = "EQ",
                                Value = searchIndex.filterValue
                            });
                        }
                    }
                }
            }

            var fields = string.Join(',', fieldsBoost);

            filters.Add(new FilterDef()
            {
                Field = "search",
                Condition = "EQ",
                Value = TextToReindexerFullTextDSL(fields, input)
            });

            return new DslQueryParams
            {
                Filters = filters.ToArray(),
                SelectFunctions = functions.ToArray()
            };
        }

        private static string TextToReindexerFullTextDSL(string fields, string input)
        {
            string cur = string.Empty;
            var output = new StringBuilder();

            // Boost fields
            if (!string.IsNullOrEmpty(fields))
                output.Append($"@{fields} ");

            int term = 0; 
            int termLen = 0;
            bool interm = false;

            // trim input spaces, and add trailing space
            input = input.Trim() + " ";

            foreach(var ch in input)
            {
                if ((Char.IsDigit(ch) || Char.IsLetter(ch)) && !interm)
                {
                    cur = string.Empty;
                    interm = true;
                    termLen = 0;
                }

                if (!Char.IsDigit(ch) && 
                    !Char.IsLetter(ch) && 
                    !ch.ToString().Contains("-+/") && interm)
                {
                    if (term > 0)
                    {
                        output.Append("+");
                    }

                    switch(termLen)
                    {
                        case int n when n >= 3:
                            // enable typos search from 3 symbols in term
                            output.Append("*");
                            output.Append(cur);
                            output.Append("~*");
                            break;
                        case int n when n >= 2:
                            // enable prefix from 2 symbol or on 2-nd+ term
                            output.Append(cur);
                            output.Append("~*");
                            break;
                        default:
                            output.Append(cur);
                            break;
                    }

                    output.Append(" ");
                    interm = false;
                    term++;

                    if (term > 15)
                        break;

                }

                if (interm)
                {
                    cur += ch;
                    termLen++;
                }
            }

            if (termLen <= 1 && term == 1)
            {
                return "";
            }

            return output.ToString();
        }
    }
}
