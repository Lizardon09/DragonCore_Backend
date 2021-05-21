using Elasticsearch.Net;
using ElasticSearch.Infrastructure.Services.Interfaces;
using Nest;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ElasticSearch.Infrastructure.Services.Models
{
    public class ElasticSearchService : IElasticSearchService
    {
        public IElasticClient _elasticClient { get; set; }

        public ElasticSearchService(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public async Task<IApiCallDetails> CreateIndex<T>(IElasticClient client, string indexName) where T : class
        {
            if (!client.Indices.Exists(indexName).Exists)
            {
                var createIndexResponse = await client.Indices.CreateAsync(indexName,
                    index => index.Map<T>(x => x.AutoMap())
                );
                return createIndexResponse.ApiCall;
            }
            else
            {
                throw new ArgumentException(""); //Placeholder for better handling later
            }
        }

        public async Task<IApiCallDetails> SaveSingleAsync<T>(T item, string indexname) where T : class
        {
            var documentExists = CheckDocumentExist(item, indexname);

            if (documentExists.Result)
            {
                var updateDocumentResponse = await _elasticClient.UpdateAsync<T>(item, u => u.Doc(item));
                return updateDocumentResponse.ApiCall;
            }
            else
            {
                var saveDocumentResponse = await _elasticClient.IndexDocumentAsync(item);
                return saveDocumentResponse.ApiCall;
            }
        }

        public async Task<IApiCallDetails> SaveManyAsync<T>(T[] items, string indexname) where T: class
        {
            var result = await _elasticClient.IndexManyAsync(items, indexname);
            if(result.Errors)
            {
                BulkErrorLogging(result);
            }
            return result.ApiCall;
        }

        public async Task<IApiCallDetails> SaveBulkAsync<T>(T[] items, string indexname) where T: class
        {
            var result = await _elasticClient.BulkAsync(b => b.Index(indexname).IndexMany(items));
            if(result.Errors)
            {
                BulkErrorLogging(result);
            }
            return result.ApiCall;
        }

        public async Task<bool> CheckDocumentExist<T>(T item, string indexname) where T : class
        {
            var response =  await _elasticClient.DocumentExistsAsync<T>(item, d => d
                        .Index(indexname)
                        );

            return response.Exists;
        }

        public void BulkErrorLogging(BulkResponse result)
        {
            foreach (var itemWithError in result.ItemsWithErrors)
            {
                // Log here errors for each item OR find better refactor for Bulk Logging indexing
                //Method to be converted to asynchronous for logging
            }

        }
    }
}
