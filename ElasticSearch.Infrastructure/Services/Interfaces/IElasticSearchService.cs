using Elasticsearch.Net;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ElasticSearch.Infrastructure.Services.Interfaces
{
    public interface IElasticSearchService
    {
        Task<IApiCallDetails> CreateIndex<T>(string indexName) where T : class;
        Task<IApiCallDetails> SaveSingleAsync<T>(T item, string indexname) where T : class;
        Task<IApiCallDetails> SaveManyAsync<T>(IEnumerable<T> items, string indexname) where T : class;
        Task<IApiCallDetails> SaveBulkAsync<T>(IEnumerable<T> items, string indexname) where T : class;


        Task<IEnumerable<T>> SearchAsync<T>(SearchDescriptor<T> searchDescriptor) where T : class;


        void BulkErrorLogging(BulkResponse result);
    }
}
