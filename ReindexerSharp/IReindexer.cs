using ReindexerClient.RxCore.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReindexerClient
{
    public interface IReindexer
    {
        // Database
        Task CreateDatabaseAsync();
        Task<bool> DatabaseExistAsync();

        
        // Namespaces
        Task CreateNamespaceFromTypeAsync(Type t);
        
        Task DeleteNamespaceAsync(string nsName);
        
        Task DeleteAllItemsFromNamespaceAsync(string nsName);

        Task<bool> NamespaceExistAsync(string nsName);

        Task<Namespace> GetNamespaceDescriptionAsync(string nsName);


        // Items
        Task GetDocumentsFromNamespaceAsync(string nsName);
        
        Task InsertDocumentsToNamespaceAsync<T>(string nsName, IEnumerable<T> items);
        
        Task UpsertDocumentsInNamespaceAsync<T>(string nsName, IEnumerable<T> items);

        Task UpdateDocumentsInNamespaceAsync<T>(string nsName, IEnumerable<T> items);

        Task DeleteDocumentsFromNamespaceAsync(string nsName, IEnumerable<int> ids);


        // Queries
        Task<QueryItemsResponse<T>> Query<T>(DSLQuery query);
    }
}
