using Nest;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ElasticSearch.Infrastructure.Services.Models
{
    public class ElasticSearchService
    {
        public IElasticClient _elasticClient { get; set; }

        public ElasticSearchService(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        private async static void CreateIndex<T>(IElasticClient client, string indexName) where T : class
        {
            if (!client.Indices.Exists(indexName).Exists)
            {
                var createIndexResponse = await client.Indices.CreateAsync(indexName,
                    index => index.Map<T>(x => x.AutoMap())
                );
            }
            else
            {
                throw new ArgumentException(""); //Placeholder for better handling later
            }
        }

        public async Task SaveSingleAsync<T>(T item, string indexname) where T : class
        {
            var documentExists = this.CheckDocumentExist<T>(item, indexname);

            if (documentExists.Result)
            {
                await _elasticClient.UpdateAsync<T>(item, u => u.Doc(item));
            }
            else
            {
                await _elasticClient.IndexDocumentAsync(item);
            }
        }

        public async Task<Boolean> CheckDocumentExist<T>(T item, string indexname) where T : class
        {
            var response =  await _elasticClient.DocumentExistsAsync<T>(item, d => d
                        .Index(indexname)
                        );

            return response.Exists;
        }

    }
}
