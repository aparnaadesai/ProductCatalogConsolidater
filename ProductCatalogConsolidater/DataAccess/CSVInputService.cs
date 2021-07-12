using ProductCatalogConsolidater.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ProductCatalogConsolidater
{
    public class CSVInputService : IInputService
    {
        public List<Product> GetProductsFromCatalog(Catalog catalog)
        {
            List<string[]> results = ReadCSV(catalog.Path);

            List<Product> products = new List<Product>();
            foreach (var item in results)
            {
                products.Add(new Product { 
                    SKU = item.ElementAt(0),
                    Description = item.ElementAt(1),
                    CatalogType = catalog.Type
                });
            }

            return products;
        }

        public List<SupplierProductBarcode> GetSupplierProductBarcodes(SupplierProductBarcodesInputPath supplierProductBarcodesInputPath)
        {
            List<string[]> results = ReadCSV(supplierProductBarcodesInputPath.Path);
            
            List<SupplierProductBarcode> supplierProductBarcodes = new List<SupplierProductBarcode>();
            foreach (var item in results)
            {
                supplierProductBarcodes.Add(new SupplierProductBarcode
                {
                    ID = Int32.Parse(item.ElementAt(0)),
                    SKU = item.ElementAt(1),
                    Barcode = item.ElementAt(2),
                    SourceType = supplierProductBarcodesInputPath.Type
                });
            }

            return supplierProductBarcodes;
        }

        public List<Supplier> GetSuppliers(string fileName)
        {
            List<string[]> results = ReadCSV(fileName);

            List<Supplier> suppliers = new List<Supplier>();
            foreach (var item in results)
            {
                suppliers.Add(new Supplier
                {
                    ID = Int32.Parse(item.ElementAt(0)),
                    Name = item.ElementAt(1)
                });
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
