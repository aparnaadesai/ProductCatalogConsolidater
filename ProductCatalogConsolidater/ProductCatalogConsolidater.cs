namespace ProductCatalogConsolidater
{
    internal class ProductCatalogConsolidater
    {
        private readonly IProductCatalogMergerService _productCatalogMerger;

        public ProductCatalogConsolidater(IProductCatalogMergerService productCatalogMerger)
        {
            _productCatalogMerger = productCatalogMerger;
        }
        internal void Run()
        {
            _productCatalogMerger.MergeProductCatalogs();
        }
    }
}