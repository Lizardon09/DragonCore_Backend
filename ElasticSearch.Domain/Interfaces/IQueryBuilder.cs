using Nest;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElasticSearch.Domain.Interfaces
{
    public interface IQueryBuilder
    {
        void AddMustMatchConditon<G>(Field field, G value);
        void AddShouldMatchCondtion<G>(Field field, G value);
        void UpdateContainers();
        IRequest GetQuery();
    }
}
