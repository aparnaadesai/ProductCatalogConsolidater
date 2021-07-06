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
            var directoryPath = _configurationRoot.GetSection("Output:outputDirectory").Value;
            var filePath = $"{directoryPath}/{_configurationRoot.GetSection("Output:outputFile").Value}";

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            var csvContent = new List<string>() {"SKU, Description, Source"};

            foreach (var product in mergedCatalog)
            {
                csvContent.Add(product.ToString());
            }

            if (!File.Exists( filePath))
            {
                File.WriteAllLines(filePath, csvContent);
            }
        }
    }
}
