using Nest;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElasticSearch.Domain.Interfaces.QueryTypes
{
    public interface IMatchQueryJob
    {
        void AddMustMatchConditon<G>(Field field, G value);
        void AddShouldMatchCondtion<G>(Field field, G value);
        void AddMustNotMatchCondtion<G>(Field field, G value);
    }
}
