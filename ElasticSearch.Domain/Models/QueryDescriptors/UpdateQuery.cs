using ElasticSearch.Domain.Interfaces.QueryDescriptors;
using Nest;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElasticSearch.Domain.Models.QueryDescriptors
{
    public class UpdateQuery<T> : IUpdateQuery<T> where T : class
    {
        public UpdateDescriptor<T,object> QueryDescripter { get; set; }

        public UpdateQuery(string indexName, string id)
        {
            this.QueryDescripter = new UpdateDescriptor<T,object>(indexName,id);
        }

        public void UpdateDocument(object doc)
        {
            this.QueryDescripter = this.QueryDescripter.Doc(doc);
        }

    }
}
