using ProductCatalogConsolidater.Domain;
using ProductCatalogConsolidater.Entities;
using System.Collections.Generic;

namespace ProductCatalogConsolidater
{
    public class ProductCatlalogMergerService : IProductCatalogMergerService
    {
        private readonly IInputService _reader;
        private readonly IOutputService _writer;
        private readonly IProductCatalogMerger _productCatalogMerger;
        private readonly IProductCatalogConfiguration _productCatalogConfiguration;

        public ProductCatlalogMergerService(IInputService reader,
            IOutputService writer,
            IProductCatalogMerger productCatalogMerger,
            IProductCatalogConfiguration productCatalogConfiguration)
        {
            _reader = reader;
            _writer = writer;
            _productCatalogMerger = productCatalogMerger;
            _productCatalogConfiguration = productCatalogConfiguration;
        }

        public void MergeProductCatalogs()
        {
            List<SupplierProductBarcode> productBarcodes = GetSupplierProductBarcodes();

            List<Product> products = GetProducts();

            var mergedProducts = _productCatalogMerger.MergeProductCatalogs(productBarcodes, products);

            _writer.Write(mergedProducts);
        }

        private List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();
            foreach (var productsDataset in _productCatalogConfiguration.Catalogs)
            {
                products.AddRange(_reader.GetProductsFromCatalog(productsDataset));
            }

            return products;
        }

        private List<SupplierProductBarcode> GetSupplierProductBarcodes()
        {
            List<SupplierProductBarcode> productBarcodes = new List<SupplierProductBarcode>();
            foreach (var barcodeDataSet in _productCatalogConfiguration.ProductBarcodes)
            {
                productBarcodes.AddRange(_reader.GetSupplierProductBarcodes(barcodeDataSet));
            }

            return productBarcodes;
        }
    }
}
