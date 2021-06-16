using ElasticSearch.Domain.Interfaces.QueryDescriptors;
using System;
using Nest;
using System.Collections.Generic;
using System.Text;

namespace ElasticSearch.Domain.Models.QueryDescriptors
{
    public class BulkQuery<T> : IBulkQuery<T> where T : class
    {
        public BulkDescriptor BulkDescriptor { get; set; }

        public BulkQuery(string indexname)
        {
            this.BulkDescriptor = new BulkDescriptor();
            this.BulkDescriptor.Index(indexname);
        }

        public void AddCollectionToSave(IEnumerable<T> items)
        {
            this.BulkDescriptor.IndexMany<T>(items);
        }

    }
}
