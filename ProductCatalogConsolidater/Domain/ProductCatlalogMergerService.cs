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
        private List<List<SupplierProductBarcode>> _productBarcodeList = new List<List<SupplierProductBarcode>>();
        private List<Catalog> _catalogs = new List<Catalog>();
        private List<List<Product>> _productSets = new List<List<Product>>();

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
            var catalogAPath = _configurationRoot.GetSection("Input:catalogA").Value;
            var catalogBPath = _configurationRoot.GetSection("Input:catalogB").Value;

            foreach (var barcodeDataSet in _productBarcodes)
            {
                _productBarcodeList.Add(_reader.GetSupplierProductBarcodes(barcodeDataSet.Path));
            }

            foreach (var item in _catalogs)
            {
                _productSets.Add(_reader.GetProductsFromCatalog(item.Path, item.Type));
            }

            var mergedProducts = _productCatalogMerger.MergeProductCatalogs(_productBarcodeList, _productSets);

            _writer.Write(mergedProducts);
        }
    }
}
