using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicHelpers.Infrastructure.Services.Interfaces
{
    public interface IIOHelperService : IBasicHelperService
    {
        /// <summary>
        /// Process all files in the directory passed in, recurse on any directories
        /// that are found, and process the files they contain.
        /// </summary>
        void InspectDirectory(string directory);

        /// <summary>
        /// Process file path provided
        /// </summary>
        void InspectFile(string path);
    }
}
