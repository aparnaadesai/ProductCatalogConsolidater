using Microsoft.Extensions.Configuration;
using System.Linq;

namespace ProductCatalogConsolidater
{
    public class ProductCatlalogMerger : IProductCatalogMerger
    {
        private readonly IInputService _reader;
        private readonly IOutputService _writer;
        private readonly IConfigurationRoot _configurationRoot;

        public ProductCatlalogMerger(IInputService reader, IOutputService writer, IConfigurationRoot configurationRoot)
        {
            _reader = reader;
            _writer = writer;
            _configurationRoot = configurationRoot;
        }
        public void MergeProductCatalogs()
        {

            var barcodeAPath = _configurationRoot.GetSection("Input:barcodesA").Value;
            var barcodeBPath = _configurationRoot.GetSection("Input:barcodesB").Value;
            var catalogAPath = _configurationRoot.GetSection("Input:catalogA").Value;
            var catalogBPath = _configurationRoot.GetSection("Input:catalogB").Value;

            var barcodesA = _reader.ReadSupplierProductBarcode(barcodeAPath);
            var barcodesB = _reader.ReadSupplierProductBarcode(barcodeBPath);

            var catalogA = _reader.GetProductsFromCatalog(catalogAPath, "A");
            var catalogB = _reader.GetProductsFromCatalog(catalogBPath, "B");

            var overlappingBarcodesSKU = (from barcodeA in barcodesA
                                          join barcodeB in barcodesB
                                          on barcodeA.Barcode equals barcodeB.Barcode
                                          select new { SKUA= barcodeA.SKU, SKUB= barcodeB.SKU}).Distinct();

            var overlappingProducts = from productA in catalogA
                                      join overlappingBarcodeSKU in overlappingBarcodesSKU
                                      on productA.SKU equals overlappingBarcodeSKU.SKUA
                                      select new FinalProductCatalog { SKU = overlappingBarcodeSKU.SKUA, Description= productA.Description, Source= "A"};

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
                                          join productA in catalogA
                                          on nonOverlappingBarcodeSKU equals productA.SKU
                                          select new FinalProductCatalog { SKU = productA.SKU, Description = productA.Description, Source = "A" };

            var nonOverlappingProductsB = from nonOverlappingBarcodeSKU in nonOverlappingBarcodesSKUB
                                          join productB in catalogB
                                          on nonOverlappingBarcodeSKU equals productB.SKU
                                          select new FinalProductCatalog { SKU = productB.SKU, Description = productB.Description, Source = "B" };

            var mergedProducts = overlappingProducts.Union(nonOverlappingProductsA).Union(nonOverlappingProductsB);

            _writer.WriteToFile(mergedProducts);
        }
    }
}
