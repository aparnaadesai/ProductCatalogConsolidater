using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;

namespace ProductCatalogConsolidater
{
    public class CSVOutputService : IOutputService
    {
        private readonly IConfigurationRoot _configurationRoot;

        public CSVOutputService(IConfigurationRoot configurationRoot)
        {
            _configurationRoot = configurationRoot;
        }

        public void Write(IEnumerable<FinalProductCatalog> mergedCatalog)
        {
            string filePath = GetFilePath();

            var csvContent = new List<string>() { "SKU, Description, Source" };

            foreach (var product in mergedCatalog)
            {
                csvContent.Add(product.ToString());
            }
            
            File.WriteAllLines(filePath, csvContent);
        }

        private string GetFilePath()
        {
            var directoryPath = _configurationRoot.GetSection("Output:outputDirectory").Value;

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            var filePath = $"{directoryPath}/{_configurationRoot.GetSection("Output:outputFile").Value}";
            return filePath;
        }
    }
}
