using Newtonsoft.Json;
using SolutionCrawler.Interfaces;
using SolutionCrawler.Models;
using System.Collections.Generic;
using System.IO;

namespace SolutionCrawler.Classes
{
    public class FileWriter : IFileWriter
    {
        private readonly ILog logger;

        public FileWriter(ILog logger)
        {
            this.logger = logger;
        }

        public void WriteFiles_ToJSON(List<Project_VM> files)
        {
            string outputDirectory = @"../../../Output/";
            string outputName = $"Output.json";

            if (!Directory.Exists(outputDirectory))
                Directory.CreateDirectory(outputDirectory);

            var _jsonSerializerSettings = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Include
            };

            string json = JsonConvert.SerializeObject(files, Formatting.Indented, _jsonSerializerSettings);
            File.WriteAllText(outputDirectory + outputName, json);
        }
    }
}
