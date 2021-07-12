using ProductCatalogConsolidater.Entities;
using System.Collections.Generic;

namespace ProductCatalogConsolidater
{
    public interface IInputService
    {
        List<Product> GetProductsFromCatalog(Catalog catalog);
        List<Supplier> GetSuppliers(string fileName);
        List<SupplierProductBarcode> GetSupplierProductBarcodes(SupplierProductBarcodesInputPath supplierProductBarcodesInputPath);
    }
}
