using ProductCatalogConsolidater.Entities;
using System.Collections.Generic;

namespace ProductCatalogConsolidater.Domain
{
    public interface IProductCatalogConfiguration
    {
        public List<SupplierProductBarcodesInputPath> ProductBarcodes { get; }
        public List<Catalog> Catalogs { get; }
    }
}
