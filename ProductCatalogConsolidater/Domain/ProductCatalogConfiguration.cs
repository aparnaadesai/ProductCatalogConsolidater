using Microsoft.Extensions.Configuration;
using ProductCatalogConsolidater.Entities;
using System.Collections.Generic;

namespace ProductCatalogConsolidater.Domain
{
    public class ProductCatalogConfiguration : IProductCatalogConfiguration
    {
        private readonly IConfigurationRoot _configurationRoot;
        List<SupplierProductBarcodesInputPath> _productBarcodes;
        List<Catalog> _catalogs;

        public ProductCatalogConfiguration(IConfigurationRoot configurationRoot)
        {
            _configurationRoot = configurationRoot;
            _productBarcodes = _configurationRoot
                .GetSection("supplierProductBarcodesInputPath")
                .Get<List<SupplierProductBarcodesInputPath>>();
            _catalogs = _configurationRoot.GetSection("products").Get<List<Catalog>>();
        }
        public List<SupplierProductBarcodesInputPath> ProductBarcodes => _productBarcodes;

        public List<Catalog> Catalogs => _catalogs;
    }
}
