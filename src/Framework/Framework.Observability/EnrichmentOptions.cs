using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Observability
{
    public class EnrichmentOptions
    {
        public Func<IServiceProvider, IDictionary<string, object?>>? CustomContextProvider { get; private set; }

        public void WithCustomContext(Func<IServiceProvider, IDictionary<string, object?>> provider)
        {
            CustomContextProvider = provider;
        }
    }
}
