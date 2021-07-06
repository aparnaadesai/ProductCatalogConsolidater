using Microsoft.Extensions.Configuration;
using ProductCatalogConsolidater.Domain;

namespace ProductCatalogConsolidater
{
    public class ProductCatlalogMergerService : IProductCatalogMergerService
    {
        private readonly IInputService _reader;
        private readonly IOutputService _writer;
        private readonly IConfigurationRoot _configurationRoot;
        private readonly IProductCatalogMerger _productCatalogMerger;

        public ProductCatlalogMergerService(IInputService reader,
            IOutputService writer,
            IConfigurationRoot configurationRoot,
            IProductCatalogMerger productCatalogMerger)
        {
            _reader = reader;
            _writer = writer;
            _configurationRoot = configurationRoot;
            _productCatalogMerger = productCatalogMerger;
        }

        public void MergeProductCatalogs()
        {

            var barcodeAPath = _configurationRoot.GetSection("Input:barcodesA").Value;
            var barcodeBPath = _configurationRoot.GetSection("Input:barcodesB").Value;
            var catalogAPath = _configurationRoot.GetSection("Input:catalogA").Value;
            var catalogBPath = _configurationRoot.GetSection("Input:catalogB").Value;

            var barcodesA = _reader.GetSupplierProductBarcodes(barcodeAPath);
            var barcodesB = _reader.GetSupplierProductBarcodes(barcodeBPath);

            var catalogA = _reader.GetProductsFromCatalog(catalogAPath, "A");
            var catalogB = _reader.GetProductsFromCatalog(catalogBPath, "B");

            var mergedProducts = _productCatalogMerger.MergeProductCatalogs(barcodesA, barcodesB, catalogA, catalogB);

            _writer.Write(mergedProducts);
        }
    }
}
