using System.Collections.Generic;

namespace ProductCatalogConsolidater
{
    public interface IOutputService
    {
        void WriteToFile(IEnumerable<FinalProductCatalog> mergedCatalog);
    }
}
