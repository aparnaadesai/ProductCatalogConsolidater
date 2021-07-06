using System.Collections.Generic;

namespace ProductCatalogConsolidater
{
    public interface IOutputService
    {
        void Write(IEnumerable<FinalProductCatalog> mergedCatalog);
    }
}
