using Microsoft.Extensions.Configuration;
using Moq;
using ProductCatalogConsolidater;
using ProductCatalogConsolidater.Domain;
using Xunit;

namespace ProductCatalogConsolidaterTests
{
    public class ProductCatalogMergerTests
    {
        [Fact]
        public void ProductCatalogMergerTests_MergeProductCatalogs_Returns_Merged_Products()
        {
            CSVInputService reader = new CSVInputService();
            Mock<IConfigurationRoot> configurationRoot = new Mock<IConfigurationRoot>();
            configurationRoot.SetupGet(x => x["barcodesA"]).Returns("barcodesA.csv");
            CSVOutputService writer = new CSVOutputService(configurationRoot.Object);
            ProductCatalogMerger productCatalog = new ProductCatalogMerger();
            ProductCatlalogMergerService productCatalogMerger = new ProductCatlalogMergerService(reader, writer, configurationRoot.Object, productCatalog);

            productCatalogMerger.MergeProductCatalogs();
        }
    }
}
