using Microsoft.Extensions.Configuration;
using ProductCatalogConsolidater.Domain;
using ProductCatalogConsolidater.Entities;
using System.Collections.Generic;

namespace ProductCatalogConsolidater
{
    public class ProductCatlalogMergerService : IProductCatalogMergerService
    {
        private readonly IInputService _reader;
        private readonly IOutputService _writer;
        private readonly IConfigurationRoot _configurationRoot;
        private readonly IProductCatalogMerger _productCatalogMerger;

        private List<SupplierProductBarcodes> _productBarcodes = new List<SupplierProductBarcodes>();
        private List<SupplierProductBarcode> _productBarcodeList = new List<SupplierProductBarcode>();
        private List<Catalog> _catalogs = new List<Catalog>();
        private List<Product> _productSets = new List<Product>();

        public ProductCatlalogMergerService(IInputService reader,
            IOutputService writer,
            IConfigurationRoot configurationRoot,
            IProductCatalogMerger productCatalogMerger)
        {
            _reader = reader;
            _writer = writer;
            _configurationRoot = configurationRoot;
            _productCatalogMerger = productCatalogMerger;
            _configurationRoot.GetSection("supplierProductBarcodes").Bind(_productBarcodes);
            _configurationRoot.GetSection("productCatalogs").Bind(_catalogs);
        }

        public void MergeProductCatalogs()
        {
            foreach (var barcodeDataSet in _productBarcodes)
            {
                _productBarcodeList.AddRange(_reader.GetSupplierProductBarcodes(barcodeDataSet.Path, barcodeDataSet.Type));
            }

            foreach (var item in _catalogs)
            {
                _productSets.AddRange(_reader.GetProductsFromCatalog(item.Path, item.Type));
            }

            var mergedProducts = _productCatalogMerger.MergeProductCatalogs(_productBarcodeList, _productSets);

            _writer.Write(mergedProducts);
        }
    }
}
