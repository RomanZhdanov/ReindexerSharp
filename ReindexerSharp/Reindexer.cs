using Microsoft.Extensions.Configuration;
using ReindexerClient.Extensions;
using ReindexerClient.Models;
using ReindexerClient.RxCore.Models;
using ReindexerClient.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReindexerClient
{
    class Reindexer : IReindexer
    {
        private readonly string _db;
        private readonly RxHttpClient _rxHttpClient;

        public Reindexer(IConfiguration configuration)
        {
            _db = configuration.GetValue<string>("Reindexer:Database");   
            _rxHttpClient = new RxHttpClient(configuration.GetValue<string>("Reindexer:Url"));
        }       
        
        //Databases
        public async Task CreateDatabaseAsync() 
        {
            var url = "db";
            var param = new { name = _db };

            try
            {
                await _rxHttpClient.PostAsync<StatusResponse>(url, param.SerializeToJson().ToJsonStringContent());
            }
            catch (Exception ex)
            {
                throw new Exception($"При попытке создания базы данных \"{_db}\" возникло исключение: {ex.Message}");
            }            
        }

        public async Task<bool> DatabaseExistAsync()
        {
            var dbList = await GetDatabasesListAsync();

            return dbList.Items.Contains(_db);
        }

        private async Task<DatabasesListResponse> GetDatabasesListAsync()
        {
            var url = "db";

            try
            {
                return await _rxHttpClient.GetAsync<DatabasesListResponse>(url);
            }
            catch (Exception ex)
            {
                throw new Exception($"При попытке получить список баз данных на сервере возникло исключение: {ex.Message}");
            }            
        }
                

        // Namespaces
        public async Task<Namespace> GetNamespaceDescriptionAsync(string nsName) 
        {
            var url = $"db/{_db}/namespaces/{nsName}";

            try
            {
                return await _rxHttpClient.GetAsync<Namespace>(nsName);
            }
            catch (Exception ex)
            {
                throw new Exception($"При получании описания namespace \"{nsName}\" произошло исключение: {ex.Message}");
            }
        }
        
        public async Task CreateNamespaceFromTypeAsync(Type nsModel) 
        {   
            var url = $"db/{_db}/namespaces";
            var ns = RxGenerator.GenerateNamespaceFromType(nsModel);

            try
            {
                if (await NamespaceExistAsync(ns.Name))
                    return;

                await _rxHttpClient.PostAsync<StatusResponse>(url, ns.SerializeToJson().ToJsonStringContent());
            }
            catch (Exception ex)
            {
                throw new Exception($"Не удалось создать namespace для модели \"{nsModel.FullName}\": {ex.Message}");
            }
        }

        public async Task<bool> NamespaceExistAsync(string nsName)
        {
            var url = $"db/{_db}/namespaces";
            var nsList = await _rxHttpClient.GetAsync<NamespacesListResponse>(url);
            var nsItem = nsList.Items.FirstOrDefault(i => i.Name == nsName);

            return nsItem != null;
        }

        public async Task DeleteNamespaceAsync(string nsName) { }
        
        public async Task DeleteAllItemsFromNamespaceAsync(string nsName) 
        {
            try
            {
                var url = $"db/{_db}/namespaces/{nsName}/truncate";

                await _rxHttpClient.DeleteAsync<StatusResponse>(url);
            }
            catch (Exception ex)
            {
                throw new Exception($"При удалении всех элементов из пространства \"{nsName}\" возникло исключение: {ex.Message}");
            }
        }


        // Items
        public async Task GetDocumentsFromNamespaceAsync(string nsName) { }

        public async Task InsertDocumentsToNamespaceAsync<T>(string nsName, IEnumerable<T> items) 
        {
            var url = $"db/{_db}/namespaces/{nsName}/items";

            var jsonContent = new StringBuilder();

            foreach (var item in items)
            {
                jsonContent.AppendLine(item.SerializeToJson());
            }

            try
            {
                await _rxHttpClient.PostAsync<ItemsUpdateResponse>(url, jsonContent.ToString().ToJsonStringContent());
            }
            catch (Exception ex)
            {
                throw new Exception($"При добавлении документов в namespace \"{nsName}\" возникло исключение: {ex.Message}");
            }
        }
        
        public async Task UpsertDocumentsInNamespaceAsync<T>(string nsName, IEnumerable<T> items) 
        {
            var url = $"db/{_db}/namespaces/{nsName}/items";

            var jsonContent = new StringBuilder();

            foreach (var item in items)
            {
                jsonContent.AppendLine(item.SerializeToJson());
            }

            try
            {   
                await _rxHttpClient.PatchAsync<ItemsUpdateResponse>(url, jsonContent.ToString().ToJsonStringContent());
            }
            catch (Exception ex)
            {
                throw new Exception($"При добавлении документов в namespace \"{nsName}\" возникло исключение: {ex.Message}");
            }
        }

        public async Task UpdateDocumentsInNamespaceAsync<T>(string nsName, IEnumerable<T> items) 
        {
            var url = $"db/{_db}/namespaces/{nsName}/items";

            var jsonContent = new StringBuilder();

            foreach (var item in items)
            {
                jsonContent.AppendLine(item.SerializeToJson());
            }

            try
            {
                await _rxHttpClient.PutAsync<ItemsUpdateResponse>(url, jsonContent.ToString().ToJsonStringContent());
            }
            catch (Exception ex)
            {
                throw new Exception($"При добавлении документов в namespace \"{nsName}\" возникло исключение: {ex.Message}");
            }
        }

        public async Task DeleteDocumentsFromNamespaceAsync(string nsName, IEnumerable<int> ids) 
        {
            var url = $"db/{_db}/namespaces/{nsName}/items";

            var jsonContent = new StringBuilder();

            foreach (var id in ids)
            {
                var obj = new { id = id };
                jsonContent.AppendLine(obj.SerializeToJson());
            }

            try
            {
                await _rxHttpClient.DeleteAsync<ItemsUpdateResponse>(url, jsonContent.ToString().ToJsonStringContent());
            }
            catch (Exception ex)
            {
                throw new Exception($"При добавлении документов в namespace \"{nsName}\" возникло исключение: {ex.Message}");
            }
        }


        // Queries
        public async Task<QueryItemsResponse<T>> Query<T>(DSLQuery query) 
        {
            var url = $"db/{_db}/query";

            try
            {
                return await _rxHttpClient.PostAsync<QueryItemsResponse<T>>(url, query.SerializeToJson().ToJsonStringContent());
            }
            catch (Exception ex)
            {
                throw new Exception($"При выполнении поискового запроса возникло исключение: {ex.Message}");
            }
        }
    }
}
