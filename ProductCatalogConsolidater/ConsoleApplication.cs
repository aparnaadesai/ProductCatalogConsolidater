namespace ProductCatalogConsolidater
{
    internal class ConsoleApplication
    {
        private readonly IProductCatalogMerger _productCatalogMerger;

        public ConsoleApplication(IProductCatalogMerger productCatalogMerger)
        {
            _productCatalogMerger = productCatalogMerger;
        }
        internal void Run()
        {
            _productCatalogMerger.MergeProductCatalogs();
        }
    }
}