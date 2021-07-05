using ProductCatalogConsolidater.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ProductCatalogConsolidater
{
    public class CSVReader : IInputService
    {
        List<Catalog> IInputService.GetProductsFromCatalog(string fileName, string catalogType)
        {
            List<Catalog> catalogs = new List<Catalog>();

            List<string[]> results = ReadCSV(fileName);

            int columnCount = results[0].Count();

            foreach (var item in results)
            {
                Catalog catalog = new Catalog();
                catalog.CatalogType = catalogType;

                for (int i = 0; i < columnCount; i++)
                {
                    if(i == 0)
                    {
                        catalog.SKU = item.ElementAt(i);
                    }
                    else
                    {
                        catalog.Description = item.ElementAt(i);
                    }
                }

                catalogs.Add(catalog);
            }

            return catalogs;
        }

        public List<SupplierProductBarcode> ReadSupplierProductBarcode(string fileName)
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

        public List<Suppliers> ReadSuppliers(string fileName)
        {
            List<Suppliers> suppliers = new List<Suppliers>();

            List<string[]> results = ReadCSV(fileName);

            int columnCount = results[0].Count();

            foreach (var item in results)
            {
                Suppliers supplier = new Suppliers();

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
