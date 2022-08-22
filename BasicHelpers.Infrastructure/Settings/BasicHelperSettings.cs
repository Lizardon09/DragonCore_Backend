using BasicHelpers.Infrastructure.Services.Interfaces;
using BasicHelpers.Infrastructure.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("BasicHelpers.Tests")]

namespace BasicHelpers.Infrastructure.Settings
{
    public class BasicHelperSettings
    {
        internal bool _IOHelper { get; set; } = false;

        public BasicHelperSettings IOHelper()
        {
            _IOHelper = true;
            return this;
        }

        internal IIOHelperService? IOHelperService => _IOHelper ? new IOHelperService() : null;

        public BasicHelperSettings AllHelpers()
        {
            return this.IOHelper();
        }

        internal IEnumerable<IBasicHelperService?> BasicHelperServices { 
            get {
                foreach(PropertyInfo property in GetType().GetProperties(
                            BindingFlags.Instance |
                            BindingFlags.NonPublic |
                            BindingFlags.Public))
                {
                    if(typeof(IBasicHelperService).IsAssignableFrom(property.PropertyType))
                    {
                        yield return property.GetValue(this) as IBasicHelperService;
                    }
                }
                
            } 
        }

    }
}
