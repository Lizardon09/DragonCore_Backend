using Elasticsearch.Net;
using ElasticSearch.Infrastructure.Services.Interfaces;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
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

        public async Task<IApiCallDetails> CreateIndex<T>(string indexName) where T : class
        {
            if (!_elasticClient.Indices.Exists(indexName).Exists)
            {
                var createIndexResponse = await _elasticClient.Indices.CreateAsync(indexName,
                    index => index
                        .Map<T>(x => x
                            .AutoMap()
                        )
                    );
                return createIndexResponse.ApiCall;
            }
            else
            {
                throw new ArgumentException(string.Format("Index {0} already exists", indexName)); //Placeholder for better handling later
            }
        }

        public async Task<IApiCallDetails> SaveSingleAsync<T>(T item, string indexname) where T : class
        {
            var saveDocumentResponse = await _elasticClient.IndexAsync(item, i => i
                .Index(indexname)
                );
            return saveDocumentResponse.ApiCall;
        }

        public async Task<IApiCallDetails> UpdateAsync<T>(T itemId, string indexname) where T : class
        {
            var updateDocumentResponse = await _elasticClient.UpdateAsync<T>(itemId, i => i
                .Index(indexname)
                );

            return updateDocumentResponse.ApiCall;
        }

        public async Task<IApiCallDetails> SaveManyAsync<T>(IEnumerable<T> items, string indexname) where T: class
        {
            var result = await _elasticClient.IndexManyAsync(items, indexname);
            if(result.Errors)
            {
                BulkErrorLogging(result);
            }
            return result.ApiCall;
        }

        public async Task<IApiCallDetails> SaveBulkAsync<T>(IEnumerable<T> items, string indexname) where T: class
        {
            var result = await _elasticClient.BulkAsync(b => b
                .Index(indexname)
                .IndexMany(items)
                );
            if(result.Errors)
            {
                BulkErrorLogging(result);
            }
            return result.ApiCall;
        }






        public async Task<IEnumerable<T>> SearchAsync<T>(SearchDescriptor<T> searchDescriptor) where T: class
        {
            var result = await _elasticClient.SearchAsync<T>(i => searchDescriptor);
            return result.Documents;
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
