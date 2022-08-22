using BasicHelpers.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicHelpers.Infrastructure.Services.Models
{
    public class IOHelperService : IIOHelperService
    {
        public void InspectDirectory(string directory)
        {
            if(Directory.Exists(directory))
            {
                // Process the list of files found in the directory.
                string[] fileEntries = Directory.GetFiles(directory);
                foreach (string fileName in fileEntries)
                    InspectFile(fileName);

                // Recurse into subdirectories of this directory.
                string[] subdirectoryEntries = Directory.GetDirectories(directory);
                foreach (string subdirectory in subdirectoryEntries)
                    InspectDirectory(subdirectory);
            }
            else
            {
                throw new DirectoryNotFoundException($"Directory of path '{directory}' not found!");
            }
        }

        public void InspectFile(string path)
        {
            if(File.Exists(path))
            {
                Console.WriteLine("Found file '{0}'.", path);
            }
            else
            {
                throw new FileNotFoundException($"File at path '{path}' not found!");
            }
            
        }

        public Type? GetMainInterface()
        {
            return typeof(IIOHelperService);
        }

        public IEnumerable<Dependency>? GetDependencies()
        {
            return null;
        }
    }
}
