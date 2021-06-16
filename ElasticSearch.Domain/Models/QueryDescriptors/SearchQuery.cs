using ElasticSearch.Domain.Interfaces;
using Nest;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElasticSearch.Domain.Models
{
    public class SearchQuery<T> : ISearchQuery<T> where T : class
    {
        public SearchDescriptor<T> QueryDescripter { get; set; }
        protected QueryContainer BaseQueryContainer { get; set; }
        protected BoolQuery BoolQuery { get; set; }
        protected List<QueryContainer> BoolMust { get; set; }
        protected List<QueryContainer> BoolShould { get; set; }
        protected List<QueryContainer> BoolMustNot { get; set; }
        protected List<QueryContainer> BoolShouldNot { get; set; }
        protected IdsQuery IdsQuery { get; set; }

        public SearchQuery(string indexName)
        {
            this.BaseQueryContainer = new QueryContainer();
            this.BoolQuery = new BoolQuery();
            this.BoolMust = new List<QueryContainer>();
            this.BoolShould = new List<QueryContainer>();

            this.IdsQuery = new IdsQuery();

            this.QueryDescripter = new SearchDescriptor<T>();
            this.QueryDescripter.Index(indexName);
            this.UpdateContainers();
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

        public void AddMustNotMatchCondtion<G>(Field field, G value)
        {
            this.BoolMustNot.Add(
                new MatchQuery()
                {
                    Field = field,
                    Query = value.ToString()
                }
            );

            this.UpdateContainers();
        }

        public void AddDocIds(params Id[] values)
        {
            this.IdsQuery = new IdsQuery()
            {
                Values = values
            };

            this.UpdateContainers();
        }

        public void UpdateContainers()
        {
            this.BoolQuery.Must = this.BoolMust;
            this.BoolQuery.Should = this.BoolShould;
            this.BoolQuery.MustNot = this.BoolMustNot;

            this.BaseQueryContainer &= this.BoolQuery;
            this.BaseQueryContainer &= this.IdsQuery;
            this.QueryDescripter.Query(q => this.BaseQueryContainer);
        }
    }
}
