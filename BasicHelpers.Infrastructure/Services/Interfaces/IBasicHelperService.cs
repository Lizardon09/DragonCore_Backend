using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicHelpers.Infrastructure.Services.Interfaces
{
    public interface IBasicHelperService
    {
        Type? GetMainInterface();
        IEnumerable<Dependency>? GetDependencies();
    }

    public class Dependency
    {
        internal Type Implimentation { get; set; }
        internal Type Interface { get; set; }
    }
}
