namespace ProductCatalogConsolidater
{
    public class FinalProductCatalog
    {
        public string SKU { get; set; }
        public string Description { get; set; }
        public string Source { get; set; }

        public override string ToString()
        {
            return $"{SKU}, {Description}, {Source}";
        }
    }
}
