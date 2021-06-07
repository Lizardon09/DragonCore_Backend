using ElasticSearch.Domain.Interfaces;
using Nest;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElasticSearch.Domain.Models
{
    public class SearchSingleQuery<T> : ElasticQuery<T> where T : class
    {
        public SearchDescriptor<T> QueryDescripter { get { return this.GetQuery(); } set { } }

        public SearchSingleQuery(string indexName) : base()
        {
            
            this.QueryDescripter = new SearchDescriptor<T>();

            this.QueryDescripter.Index(indexName);

        }

        public override SearchDescriptor<T> GetQuery()
        {
            return this.QueryDescripter.Query(q => this.BaseQueryContainer);
        }
    }
}
