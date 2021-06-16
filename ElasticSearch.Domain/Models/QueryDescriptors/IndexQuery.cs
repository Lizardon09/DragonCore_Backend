using ElasticSearch.Domain.Interfaces.QueryDescriptors;
using System;
using Nest;
using System.Collections.Generic;
using System.Text;

namespace ElasticSearch.Domain.Models.QueryDescriptors
{
    public class IndexQuery<T> : IIndexQuery<T> where T : class
    {
        public IndexDescriptor<T> IndexQueryDescripter { get; set; }
        public CreateIndexDescriptor CreateIndexQueryDescripter { get; set; }
        protected TypeMappingDescriptor<T> TypeMappingDescriptor { get; set; }
        protected List<QueryContainer> Map { get; set; }

        public IndexQuery(string indexName)
        {
            this.TypeMappingDescriptor = new TypeMappingDescriptor<T>();

            this.CreateIndexQueryDescripter = new CreateIndexDescriptor(indexName);
            this.IndexQueryDescripter = new IndexDescriptor<T>();

            this.IndexQueryDescripter.Index(indexName);
        }

        public void AutoMapIndex()
        {
            this.TypeMappingDescriptor.AutoMap();
            this.UpdateContainers();
        }

        public void UpdateContainers()
        {
            this.CreateIndexQueryDescripter.Map<T>(m => this.TypeMappingDescriptor);
        }
    }
}
