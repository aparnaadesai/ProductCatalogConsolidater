using Microsoft.Extensions.Configuration;
using Moq;
using ProductCatalogConsolidater;
using Xunit;

namespace ProductCatalogConsolidaterTests
{
    public class ProductCatalogMergerTests
    {
        [Fact]
        public void ProductCatalogMergerTests_MergeProductCatalogs_Returns_Merged_Products()
        {
            CSVReader reader = new CSVReader();
            
            Mock<IConfigurationRoot> configurationRoot = new Mock<IConfigurationRoot>();
            configurationRoot.SetupGet(x => x["barcodesA"]).Returns("barcodesA.csv");

            CSVWriter writer = new CSVWriter(configurationRoot.Object);

            ProductCatlalogMerger productCatalogMerger = new ProductCatlalogMerger(reader, writer, configurationRoot.Object);

            
            productCatalogMerger.MergeProductCatalogs();
        }
    }
}
