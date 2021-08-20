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
            this.BulkDescriptor = new BulkDescriptor(indexname);
            this.BulkDescriptor = this.BulkDescriptor.Index(indexname);
        }

        public void AddCollectionToSave(IEnumerable<T> items)
        {
            this.BulkDescriptor = this.BulkDescriptor.IndexMany(items);
        }

        public void AddCollectionToSave2(IEnumerable<T> items, string idPropertyName)
        {
            foreach (var item in items)
            {
                this.BulkDescriptor = this.BulkDescriptor.Index<T>(i => i
                    .Document(item)
                    .Id((Nest.Id)item.GetType().GetProperty(idPropertyName).GetValue(item))
                );
            }
        }

    }
}
