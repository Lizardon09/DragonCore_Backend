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
        Task<IApiCallDetails> IndexAsync<T>(T item, IndexDescriptor<T> indexDescriptor) where T : class;
        Task<IApiCallDetails> CreateIndexAsync(string indexname, CreateIndexDescriptor createIndexDescriptor);
        Task<IApiCallDetails> BulkAsync(BulkDescriptor BulkDescriptor);
        Task<IApiCallDetails> UpdateAsync<T>(UpdateDescriptor<T, object> updateDescriptor) where T : class;
        Task<IEnumerable<T>> SearchAsync<T>(SearchDescriptor<T> searchDescriptor) where T : class;
    }
}
