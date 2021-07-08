using ProductCatalogConsolidater.Entities;
using System.Collections.Generic;
using System.Linq;

namespace ProductCatalogConsolidater.Domain
{
    public class ProductCatalogMerger : IProductCatalogMerger
    {
        public IEnumerable<FinalProductCatalog> MergeProductCatalogs(
            IEnumerable<SupplierProductBarcode> productBarcodeSets,
            IEnumerable<Product> productCatalogSets)
        {
            var groupedBarcode = productBarcodeSets.GroupBy(x => x.Barcode).Select( y => new { 
                barcode = y.Key,
                productBarcodes = y.ToList()
            });

            Dictionary<string, string> sKUSourceTypes = new Dictionary<string, string>(); 

            foreach (var item in groupedBarcode)
            {
                var productBarcode = item.productBarcodes.OrderBy(x => x.SourceType).First();
                if (!sKUSourceTypes.ContainsKey(productBarcode.SKU))
                    sKUSourceTypes.Add(productBarcode.SKU, productBarcode.SourceType);
            }

            var mergedProducts = from products in productCatalogSets
                                 join sKUSourceType in sKUSourceTypes
                                 on products.SKU equals sKUSourceType.Key
                                 select new FinalProductCatalog { SKU = sKUSourceType.Key, Description = products.Description, Source = sKUSourceType.Value };

            return mergedProducts;
        }
    }
}
