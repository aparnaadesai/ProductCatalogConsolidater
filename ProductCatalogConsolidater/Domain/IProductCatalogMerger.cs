using ProductCatalogConsolidater.Entities;
using System.Collections.Generic;

namespace ProductCatalogConsolidater.Domain
{
    public interface IProductCatalogMerger
    {
        IEnumerable<FinalProductCatalog> MergeProductCatalogs(IEnumerable<SupplierProductBarcode> productBarcodeSets,
             IEnumerable<Product> productCatalogSets);
    }
}
