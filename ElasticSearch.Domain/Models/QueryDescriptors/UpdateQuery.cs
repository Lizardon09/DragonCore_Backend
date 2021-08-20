using ElasticSearch.Domain.Interfaces.QueryDescriptors;
using Nest;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElasticSearch.Domain.Models.QueryDescriptors
{
    public class UpdateQuery<T> : IUpdateQuery<T> where T : class
    {
        public UpdateDescriptor<object, object> QueryDescripter { get; set; }

        public UpdateQuery(string indexName, Nest.Id id)
        {
            this.QueryDescripter = new UpdateDescriptor<object,object>(id);
            this.QueryDescripter = this.QueryDescripter.Index(indexName);
        }
        
        public void UpdateDocument(object doc)
        {
            this.QueryDescripter = this.QueryDescripter.Doc(doc);
        }

        public void EnableElasticShardRefresh()
        {
            this.QueryDescripter = this.QueryDescripter.Refresh(Elasticsearch.Net.Refresh.True);
        }

        public void EnableDocAsUpsert()
        {
            this.QueryDescripter = this.QueryDescripter.DocAsUpsert();
        }
    }
}
