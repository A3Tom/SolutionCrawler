using SolutionCrawler.Interfaces;
using SolutionCrawler.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace SolutionCrawler.Classes
{
    public class FileReader : IFileReader
    {

        public FileReader()
        {

        }

        public Dictionary<Guid, string> ReadCSProjFile(string filePath)
        {
            XDocument doc = XDocument.Load(filePath);

            return Crawl_nodes(doc);
        }

        public Dictionary<Guid, string> Crawl_nodes(XDocument doc)
        {
            Dictionary<Guid, string> Projects = new Dictionary<Guid, string>();

            Guid currentProjectGuid = Guid.Empty;
            string currentProjectName = string.Empty;

            foreach (XElement element in doc.Root.Elements().Elements())
            {
                Guid newProjectGuid = Guid.Empty;
                string newProjectName = string.Empty;
                
                var switchCase = element.Name.LocalName;

                switch (switchCase)
                {
                    case "AssemblyName":
                        currentProjectName = element.Value;
                        break;
                    case "ProjectGuid":
                        Guid.TryParse(element.Value, out currentProjectGuid);
                        break;
                    case "ProjectReference":
                        foreach (var node in element.Nodes())
                        {
                            var nodeElement = (XElement)node;

                            if (nodeElement.Name.LocalName == "Project")
                            {
                                Guid.TryParse(nodeElement.Value, out newProjectGuid);
                            }
                            else
                            if (nodeElement.Name.LocalName == "Name")
                            {
                                newProjectName = nodeElement.Value;
                            }
                        }
                        break;
                    default:
                        break;
                }

                if (currentProjectName != string.Empty && currentProjectGuid != Guid.Empty)
                {
                    Projects.TryAdd(currentProjectGuid, currentProjectName);
                }

                if (newProjectName != string.Empty && newProjectGuid != Guid.Empty)
                {
                    Projects.TryAdd(newProjectGuid, newProjectName);
                }
            }

            Console.WriteLine($"Project: {currentProjectName} | Guid: {currentProjectGuid}");
            foreach (var proj in Projects)
            {
                Console.WriteLine($"Dependency: {proj.Value} | Guid: {proj.Key}");
            }

            return Projects;
        }
    }
}
