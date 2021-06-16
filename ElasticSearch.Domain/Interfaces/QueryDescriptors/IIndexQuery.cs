using System;
using System.Collections.Generic;
using System.Text;

namespace ElasticSearch.Domain.Interfaces.QueryDescriptors
{
    public interface IIndexQuery<T> where T : class
    {
        void AutoMapIndex();
        void UpdateContainers();
    }
}
