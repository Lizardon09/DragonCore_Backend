using ElasticSearch.Domain.Interfaces;
using Nest;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElasticSearch.Domain.Models
{
    public class SearchQuery<T> : ElasticQuery<T>, 
                                  ISearchQuery<T> where T : class
    {
        public SearchDescriptor<T> QueryDescripter { get { return this.GetQuery(); } set { } }

        public SearchQuery(string indexName) : base()
        {
            
            this.QueryDescripter = new SearchDescriptor<T>();

            this.QueryDescripter.Index(indexName);

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

        public override void UpdateContainers()
        {
            this.BoolQuery.Must = this.BoolMust;
            this.BoolQuery.Should = this.BoolShould;
            this.BoolQuery.MustNot = this.BoolMustNot;

            this.BaseQueryContainer &= this.BoolQuery;
        }

        public SearchDescriptor<T> GetQuery()
        {
            return this.QueryDescripter.Query(q => this.BaseQueryContainer);
        }
    }
}
