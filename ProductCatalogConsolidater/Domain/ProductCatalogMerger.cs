using ProductCatalogConsolidater.Entities;
using System.Collections.Generic;
using System.Linq;

namespace ProductCatalogConsolidater.Domain
{
    public class ProductCatalogMerger : IProductCatalogMerger
    {
        public IEnumerable<FinalProductCatalog> MergeProductCatalogs(
            IEnumerable<SupplierProductBarcode> productBarcodes,
            IEnumerable<Product> products)
        {
            var mergedSupplierProductBarcodes = 
                productBarcodes
                .GroupBy(x => x.Barcode)
                .Select( y => 
                new { 
                    barcode = y.Key,
                    productBarcodes = y.ToList()
                });

            Dictionary<string, string> sKUSourceTypes = new Dictionary<string, string>(); 

            foreach (var supplierProductBarcode in mergedSupplierProductBarcodes)
            {
                var productBarcode = supplierProductBarcode
                    .productBarcodes.OrderBy(x => x.SourceType).First();
                
                if (!sKUSourceTypes.ContainsKey(productBarcode.SKU))
                    sKUSourceTypes.Add(productBarcode.SKU, productBarcode.SourceType);
            }

            var mergedProducts = products.Where(
                x => sKUSourceTypes.ContainsKey(x.SKU) &&
                sKUSourceTypes.GetValueOrDefault(x.SKU).Equals(x.CatalogType)).
                Select(y => new FinalProductCatalog { 
                    SKU = y.SKU,
                    Description = y.Description,
                    Source = y.CatalogType
                });

            return mergedProducts;
        }
    }
}
