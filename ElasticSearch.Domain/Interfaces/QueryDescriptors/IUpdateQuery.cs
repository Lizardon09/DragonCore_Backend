using Nest;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElasticSearch.Domain.Interfaces.QueryDescriptors
{
    public interface IUpdateQuery<T> where T : class
    {
        void UpdateDocument(object doc);
    }
}
