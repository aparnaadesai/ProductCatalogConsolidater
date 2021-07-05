using ProductCatalogConsolidater.Entities;
using System.Collections.Generic;

namespace ProductCatalogConsolidater
{
    public interface IInputService
    {
        List<Catalog> GetProductsFromCatalog(string fileName, string catalogType);
        List<Suppliers> ReadSuppliers(string fileName);
        List<SupplierProductBarcode> ReadSupplierProductBarcode(string fileName);
    }
}
