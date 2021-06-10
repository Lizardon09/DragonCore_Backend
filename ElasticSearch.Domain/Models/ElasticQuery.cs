using System;
using System.Collections.Generic;
using System.Text;
using ElasticSearch.Domain.Interfaces;
using Nest;

namespace ElasticSearch.Domain.Models
{
    public abstract class ElasticQuery<T> : IQueryBuilder where T : class
    {
        public string indexname { get; set; }
        protected QueryContainer BaseQueryContainer { get; set; }
        protected BoolQuery BoolQuery { get; set; }
        protected List<QueryContainer> BoolMust { get; set; }
        protected List<QueryContainer> BoolShould { get; set; }
        protected List<QueryContainer> BoolMustNot { get; set; }
        protected List<QueryContainer> BoolShouldNot { get; set; }

        public ElasticQuery()
        {
            this.BaseQueryContainer = new QueryContainer();
            this.BoolQuery = new BoolQuery();
            this.BoolMust = new List<QueryContainer>();
            this.BoolShould = new List<QueryContainer>();
        }

        public abstract void UpdateContainers();
    }
}
