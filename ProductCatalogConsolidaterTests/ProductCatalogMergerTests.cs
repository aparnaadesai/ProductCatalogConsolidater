using FluentAssertions;
using ProductCatalogConsolidater;
using ProductCatalogConsolidater.Domain;
using ProductCatalogConsolidater.Entities;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ProductCatalogConsolidaterTests
{
    public class ProductCatalogMergerTests
    {
        public IEnumerable<SupplierProductBarcode> productBarcodes;
        public IEnumerable<Product> products;

        public void Setup()
        {
            productBarcodes =
            new List<SupplierProductBarcode> {
                new SupplierProductBarcode
                {
                    ID = 1,
                    SKU = "647-vyk-317",
                    Barcode = "z2783613083817",
                    SourceType = "A"
                },
                new SupplierProductBarcode
                {
                    ID = 1,
                    SKU = "999-vyk-317",
                    Barcode = "z2783613083817",
                    SourceType = "B"
                },
                new SupplierProductBarcode
                {
                    ID = 1,
                    SKU = "111-vyk-317",
                    Barcode = "z2783613083920",
                    SourceType = "C"
                }
            };

        products =
            new List<Product>
            {
                new Product
                {
                    SKU = "647-vyk-317",
                    Description = "Walkers Special Old Whiskey test",
                    CatalogType = "A"
                },
                new Product
                {
                    SKU = "647-vyk-317",
                    Description = "Walkers Special Old Whiskey test",
                    CatalogType = "B"
                },
                new Product
                {
                    SKU = "111-vyk-317",
                    Description = "Coffee",
                    CatalogType = "C"
                }
            };
        }

        [Fact]
        public void ProductCatalogMergerTests_MergeProductCatalogs_Should_Return_Merged_Products_Without_Duplicates()
        {
            Setup();

            ProductCatalogMerger productCatalogMerger = new ProductCatalogMerger();
            var mergedProducts = productCatalogMerger.MergeProductCatalogs(productBarcodes, products);

            mergedProducts.Count().Should().Be(2);
            mergedProducts.First().SKU.Should().Be("647-vyk-317");
            mergedProducts.First().Source.Should().Be("A");
            mergedProducts.First().Description.Should().Be("Walkers Special Old Whiskey test");
        }

        [Fact]
        public void ProductCatalogMergerTests_MergeProductCatalogs_Should_Return_Merged_Products_When_More_Than_Two_Catalogs()
        {
            Setup();

            ProductCatalogMerger productCatalogMerger = new ProductCatalogMerger();
            var mergedProducts = productCatalogMerger.MergeProductCatalogs(productBarcodes, products);

            mergedProducts.Count().Should().Be(2);
            mergedProducts.Last().Source.Should().Be("C");
            mergedProducts.Last().SKU.Should().Be("111-vyk-317");
            mergedProducts.Last().Description.Should().Be("Coffee");
        }
    }
}
