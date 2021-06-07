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

        public ElasticQuery()
        {
            this.BaseQueryContainer = new QueryContainer();
            this.BoolQuery = new BoolQuery();
            this.BoolMust = new List<QueryContainer>();
            this.BoolShould = new List<QueryContainer>();
        }

        public void AddMustMatchConditon<G>(Field field, G value)
        {
            this.BoolMust.Add(
                new MatchQuery()
                {
                    Field = field,
                    Query = value.ToString()
                }
            );

            this.UpdateContainers();
        }

        public void AddShouldMatchCondtion<G>(Field field, G value)
        {
            this.BoolShould.Add(
                new MatchQuery()
                {
                    Field = field,
                    Query = value.ToString()
                }
            );

            this.UpdateContainers();
        }

        public void UpdateContainers()
        {
            this.BoolQuery.Must = this.BoolMust;
            this.BoolQuery.Should = this.BoolShould;

            this.BaseQueryContainer &= this.BoolQuery;
        }

        public abstract IRequest GetQuery();
    }
}
