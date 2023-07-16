using ReindexerClient.Attributes;
using ReindexerClient.RxCore.Enums;
using ReindexerClient.RxCore.Models;
using System.Collections.Generic;

namespace ReindexerClient.Utils
{
    public static class RxGenerator
    {
        public static Namespace GenerateNamespaceFromType(System.Type t)
        {
            var indexes = GenerateIndexesFromType(t);

            return new Namespace()
            {
                Name = GetNamespaceFromAttribute(t),
                Storage = new InlineModel2 { Enabled = true },
                Indexes = indexes
            };
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

        private static Index[] GenerateIndexesFromType(System.Type type)
        {
            var indexes = new List<Index>();

            var properties = type.GetProperties();

            var compositePaths = new List<string>();

            foreach (var property in properties)
            {
                System.Attribute[] attrs = System.Attribute.GetCustomAttributes(property);

                foreach (System.Attribute attr in attrs)
                {
                    if (attr is SearchIndex)
                    {
                        var index = new Index();
                        var searchIndex = (SearchIndex)attr;

                        var name = property.Name.ToLowerInvariant();

                        index.Name = name;
                        index.JsonPaths = new string[] { name };
                        index.FieldType = ToRxType(property.PropertyType);
                        index.IsPk = searchIndex.isPk;
                        index.IsDense = searchIndex.isDense;
                        index.IsArray = searchIndex.isArray;
                        
                        if (!string.IsNullOrEmpty(searchIndex.indexType))
                            index.IndexType = searchIndex.indexType;

                        if (searchIndex.search)
                            compositePaths.Add(name);

                        indexes.Add(index);
                    }
                }
            }

            if (compositePaths.Count > 0)
            {
                indexes.Add(new Index
                {
                    Name = "search",
                    FieldType = FieldTypes.Composite,
                    IndexType = IndexTypes.Text,
                    JsonPaths = compositePaths.ToArray(),
                    Config = new FulltextConfig
                    {
                        Bm25Boost = 0.1f,
                        Bm25Weight = 0.3f,
                        Distance_Boost = 2,
                        DistanceWeight = 0.5f,
                        TermLenBoost = 1,
                        TermLenWeight = 0.3f,
                        PositionBoost = 1,
                        PositionWeight = 0.1f,
                        FullMatchBoost = 1.1f,
                        PartialMatchDecrease = 15,
                        MinRelevancy = 0.2f,
                        MaxTypos = 1,
                        MaxTypoLen = 15,
                        MaxRebuildSteps = 50,
                        MaxStepSize = 4000,
                        MergeLimit = 20000,
                        Stemmers = new string[] { "en", "ru" },
                        EnableTranslit = true,
                        EnableKbLayout = true,
                        StopWords = null,
                        LogLevel = 3,
                        EnableNumbersSearch = false,
                        EnableWarmupOnUsCopy = false,
                        ExtraWordSymbols = "/-+",
                        SumRanksByFieldsRatio = 0
                    }
                });
            }

            return indexes.ToArray();
        }

        private static string ToRxType(System.Type type)
        {
            if (type == typeof(int))
                return FieldTypes.Int;
            if (type == typeof(long))
                return FieldTypes.Int64;
            if (type == typeof(double) || type == typeof(float))
                return FieldTypes.Double;
            if (type == typeof(string) || type == typeof(string[]))
                return FieldTypes.String;
            if (type == typeof(bool))
                return FieldTypes.Bool;

            return FieldTypes.Composite;
        }
    }
}
