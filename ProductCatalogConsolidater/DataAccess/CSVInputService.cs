using ProductCatalogConsolidater.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ProductCatalogConsolidater
{
    public class CSVInputService : IInputService
    {
        public List<Product> GetProductsFromCatalog(string fileName, string catalogType)
        {
            List<Product> products = new List<Product>();

            List<string[]> results = ReadCSV(fileName);

            int columnCount = results[0].Count();

            foreach (var item in results)
            {
                Product product = new Product();
                product.CatalogType = catalogType;

                for (int i = 0; i < columnCount; i++)
                {
                    if(i == 0)
                    {
                        product.SKU = item.ElementAt(i);
                    }
                    else
                    {
                        product.Description = item.ElementAt(i);
                    }
                }

                products.Add(product);
            }

            return products;
        }

        public List<SupplierProductBarcode> GetSupplierProductBarcodes(string fileName)
        {
            List<SupplierProductBarcode> supplierProductBarcodes =
                new List<SupplierProductBarcode>();
            
            List<string[]> results = ReadCSV(fileName);

            int columnCount = results[0].Count();

            foreach (var item in results)
            {
                SupplierProductBarcode productBarcode = new SupplierProductBarcode();
                for (int i = 0; i < columnCount; i++)
                {
                    if (i == 0)
                    {
                        productBarcode.ID = Int32.Parse(item.ElementAt(i));
                    }
                    else if (i == 1)
                    {
                        productBarcode.SKU = item.ElementAt(i);
                    }
                    else
                    {
                        productBarcode.Barcode = item.ElementAt(i);
                    }
                }
                supplierProductBarcodes.Add(productBarcode);
            }

            return supplierProductBarcodes;
        }

        public List<Supplier> GetSuppliers(string fileName)
        {
            List<Supplier> suppliers = new List<Supplier>();

            List<string[]> results = ReadCSV(fileName);

            int columnCount = results[0].Count();

            foreach (var item in results)
            {
                Supplier supplier = new Supplier();

                for (int i = 0; i < columnCount; i++)
                {
                    if (i == 0)
                    {
                        supplier.ID = Int32.Parse(item.ElementAt(i));
                    }
                    else
                    {
                        supplier.Name = item.ElementAt(i);
                    }
                }

                suppliers.Add(supplier);
            }

            return suppliers;
        }

        private List<string[]> ReadCSV(string fileName)
        {
            string[] lines = File.ReadAllLines(fileName);

            var supplierProductBarcode = lines.Select(l => l.Split(","));
            var results = supplierProductBarcode.Skip(1).ToList();

            return results;
        }
    }
}
