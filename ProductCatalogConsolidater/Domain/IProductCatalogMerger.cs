using ProductCatalogConsolidater.Entities;
using System.Collections.Generic;

namespace ProductCatalogConsolidater.Domain
{
    public interface IProductCatalogMerger
    {
        IEnumerable<FinalProductCatalog> MergeProductCatalogs(IEnumerable<SupplierProductBarcode> barcodesA,
             IEnumerable<SupplierProductBarcode> barcodesB,
             IEnumerable<Product> productsFromCatalogA,
             IEnumerable<Product> productsFromCatalogB);
    }
}
