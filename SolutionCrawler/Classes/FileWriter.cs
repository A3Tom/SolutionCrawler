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

        public void WriteFiles_ToJSON(List<Project> files)
        {
            foreach (Project file in files)
            {
                string outputDirectory = @"./output/";
                string outputName = $"{file.PropertyGroup.AssemblyName}.json";
                //string outputDirectory = @"./Tests/Output/" + file.PropertyGroup.AssemblyName + "/";
                //string outputName = file.FileName.Replace(".cs", "") + ".Json";

                if (!Directory.Exists(outputDirectory))
                    Directory.CreateDirectory(outputDirectory);

                var _jsonSerializerSettings = new JsonSerializerSettings()
                {
                    NullValueHandling = NullValueHandling.Ignore
                };

                string json = JsonConvert.SerializeObject(file, Newtonsoft.Json.Formatting.Indented, _jsonSerializerSettings);
                System.IO.File.WriteAllText(outputDirectory + outputName, json);
            }
        }
    }
}
