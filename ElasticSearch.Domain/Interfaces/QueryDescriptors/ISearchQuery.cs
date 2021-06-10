using ElasticSearch.Domain.Interfaces.QueryTypes;
using Nest;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElasticSearch.Domain.Interfaces
{
    public interface ISearchQuery<T> : IMatchQueryJob 
                                       where T : class
    {
        SearchDescriptor<T> GetQuery();
    }
}
