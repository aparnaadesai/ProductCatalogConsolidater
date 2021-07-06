using ProductCatalogConsolidater.Entities;
using System.Collections.Generic;
using System.Linq;

namespace ProductCatalogConsolidater.Domain
{
    public class ProductCatalogMerger : IProductCatalogMerger
    {
        public IEnumerable<FinalProductCatalog> MergeProductCatalogs(IEnumerable<SupplierProductBarcode> barcodesA, IEnumerable<SupplierProductBarcode> barcodesB, IEnumerable<Product> productsFromCatalogA, IEnumerable<Product> productsFromCatalogB)
        {
            var overlappingBarcodesSKU = (from barcodeA in barcodesA
                                          join barcodeB in barcodesB
                                          on barcodeA.Barcode equals barcodeB.Barcode
                                          select new { SKUA = barcodeA.SKU, SKUB = barcodeB.SKU }).Distinct();

            var overlappingProducts = from productA in productsFromCatalogA
                                      join overlappingBarcodeSKU in overlappingBarcodesSKU
                                      on productA.SKU equals overlappingBarcodeSKU.SKUA
                                      select new FinalProductCatalog { SKU = overlappingBarcodeSKU.SKUA, Description = productA.Description, Source = "A" };

            var nonOverlappingBarcodesSKUA = (from barcodeA in barcodesA
                                              join overlappingBarcodeSKU in overlappingBarcodesSKU
                                              on barcodeA.SKU equals overlappingBarcodeSKU.SKUA into nonOverlappingGroup
                                              from nonOverlapping in nonOverlappingGroup.DefaultIfEmpty()
                                              where nonOverlapping == null
                                              select barcodeA.SKU).Distinct();

            var nonOverlappingBarcodesSKUB = (from barcodeB in barcodesB
                                              join overlappingBarcodeSKU in overlappingBarcodesSKU
                                              on barcodeB.SKU equals overlappingBarcodeSKU.SKUB into nonOverlappingGroup
                                              from nonOverlapping in nonOverlappingGroup.DefaultIfEmpty()
                                              where nonOverlapping == null
                                              select barcodeB.SKU).Distinct();

            var nonOverlappingProductsA = from nonOverlappingBarcodeSKU in nonOverlappingBarcodesSKUA
                                          join productA in productsFromCatalogA
                                          on nonOverlappingBarcodeSKU equals productA.SKU
                                          select new FinalProductCatalog { SKU = productA.SKU, Description = productA.Description, Source = "A" };

            var nonOverlappingProductsB = from nonOverlappingBarcodeSKU in nonOverlappingBarcodesSKUB
                                          join productB in productsFromCatalogB
                                          on nonOverlappingBarcodeSKU equals productB.SKU
                                          select new FinalProductCatalog { SKU = productB.SKU, Description = productB.Description, Source = "B" };

            var mergedProducts = overlappingProducts.Union(nonOverlappingProductsA).Union(nonOverlappingProductsB);

            return mergedProducts;
        }
    }
}
