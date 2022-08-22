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

        public async Task<IApiCallDetails> TestConnectionAsync()
        {
            var result = await _elasticClient.PingAsync();
            return result.ApiCall;
        }

        public async Task<IApiCallDetails> IndexAsync<T>(T item, IndexDescriptor<T> indexDescriptor) where T : class
        {
            var result = await _elasticClient.IndexAsync(item, indexDescriptor => indexDescriptor);
            return result.ApiCall;
        }

        public async Task<IApiCallDetails> CreateIndexAsync(string indexname, CreateIndexDescriptor createIndexDescriptor)
        {
            var result = await _elasticClient.Indices.CreateAsync(indexname, cid => createIndexDescriptor);
            return result.ApiCall;
        }

        public async Task<IApiCallDetails> BulkAsync(BulkDescriptor BulkDescriptor)
        {
            var result = await _elasticClient.BulkAsync(BulkDescriptor);
            return result.ApiCall;
        }

        public async Task<IApiCallDetails> UpdateAsync<T>(UpdateDescriptor<T, object> updateDescriptor) where T : class
        {
            var result = await _elasticClient.UpdateAsync(updateDescriptor);
            return result.ApiCall;
        }

        public async Task<IEnumerable<T>> SearchAsync<T>(SearchDescriptor<T> searchDescriptor) where T: class
        {
            var result = await _elasticClient.SearchAsync<T>(i => searchDescriptor);
            return result.Documents;
        }
    }
}
