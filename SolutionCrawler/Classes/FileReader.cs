using SolutionCrawler.Interfaces;
using SolutionCrawler.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace SolutionCrawler.Classes
{
    public class FileReader : IFileReader
    {

        public FileReader()
        {

        }

        public Project ReadCSProjFile(string filePath)
        {
            XDocument doc = XDocument.Load(filePath);
            Dictionary<Guid, string> Projects = new Dictionary<Guid, string>();

            foreach (XElement el in doc.Root.Elements())
            {
                Guid newProjectGuid = Guid.Empty;
                string newProjectName = string.Empty;

                Console.WriteLine("{0} {1}", el.Name.LocalName, el?.Attribute("Reference")?.Value);
                Console.WriteLine("Attributes:");
                foreach (XAttribute attr in el.Attributes())
                    Console.WriteLine("    {0}", attr);
                Console.WriteLine("[ ] Elements:");

                foreach (XElement element in el.Elements())
                {
                    Console.WriteLine("[2] {0}: {1}", element.Name.LocalName, element.Value);

                    if (element.Name.LocalName == "ProjectGuid") {
                        Guid.TryParse(element.Value, out newProjectGuid);
                    }
                    else if (element.Name.LocalName == "AssemblyName")
                    {
                        newProjectName = element.Value;
                    }
                    else if (element.Name.LocalName == "ProjectReference")
                    {
                        foreach (var node in element.Nodes())
                        {
                            var nodeElement = (XElement)node;

                            if(nodeElement.Name.LocalName == "Project")
                            {
                                Guid.TryParse(nodeElement.Value, out newProjectGuid);
                            } else 
                            if (nodeElement.Name.LocalName == "Name")
                            {
                                newProjectName = nodeElement.Value;
                            }
                        }

                        Projects.TryAdd(newProjectGuid, newProjectName);
                    }

                    foreach (XAttribute elAttr in element.Attributes())
                    {
                        Console.WriteLine($"[3] [Name]: {elAttr.Name.LocalName} | [Value]: {elAttr.Value}");
                    }
                }

                if (newProjectName != string.Empty && newProjectGuid != Guid.Empty)
                {
                    var result = Projects.TryAdd(newProjectGuid, newProjectName);
                    Console.WriteLine($"Dictionary case matched: {result}");
                }
            }

            Console.WriteLine("-=-=- Dictionary Output =-=-=-");

            foreach (var proj in Projects)
            {
                Console.WriteLine($"Key: {proj.Key} | Value: {proj.Value}");
            }

            Console.WriteLine("-=-=- End of Dictionary Output =-=-=-");

            return new Project();
        }
    }
}
