using ProductCatalogConsolidater.Entities;
using System.Collections.Generic;

namespace ProductCatalogConsolidater
{
    public interface IInputService
    {
        List<Product> GetProductsFromCatalog(string fileName, string catalogType);
        List<Supplier> GetSuppliers(string fileName);
        List<SupplierProductBarcode> GetSupplierProductBarcodes(string fileName, string sourceType);
    }
}
