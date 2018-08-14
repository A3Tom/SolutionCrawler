using SolutionCrawler.Interfaces;
using SolutionCrawler.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SolutionCrawler.Classes
{
    public class XMLDeserializer : IXMLDeserializer
    {
        public XMLDeserializer()
        {

        }

        public Project_VM InflateProjectFromXML(XDocument document, string filePath)
        {
            Dictionary<Guid, string> DependsOn = new Dictionary<Guid, string>();

            Guid currentProjectGuid = Guid.Empty;
            string currentProjectName = string.Empty;

            DateTime.TryParse(new FileInfo(filePath)?.LastWriteTime.ToLongDateString(), out DateTime lastModified);

            foreach (XElement element in document.Root.Elements().Elements())
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
                        foreach (var referenceElement in element.Elements())
                        {
                            if (referenceElement.Name.LocalName == "Project")
                            {
                                Guid.TryParse(referenceElement.Value, out newProjectGuid);
                            } else if (referenceElement.Name.LocalName == "Name")
                            {
                                newProjectName = referenceElement.Value;
                            }
                        }
                        //foreach (var node in element.Nodes())
                        //{
                        //    var nodeElement = (XElement)node;

                        //    if (nodeElement.Name.LocalName == "Project")
                        //    {
                        //        Guid.TryParse(nodeElement.Value, out newProjectGuid);
                        //    }
                        //    else
                        //    if (nodeElement.Name.LocalName == "Name")
                        //    {
                        //        newProjectName = nodeElement.Value;
                        //    }
                        //}
                        break;
                    default:
                        break;
                }

                if (newProjectName != string.Empty && newProjectGuid != Guid.Empty)
                {
                    DependsOn.TryAdd(newProjectGuid, newProjectName);
                }
            }

            //Console.WriteLine($"Project: {currentProjectName} | Guid: {currentProjectGuid}");
            //foreach (var proj in DependsOn)
            //{
            //    Console.WriteLine($"Dependency: {proj.Value} | Guid: {proj.Key}");
            //}

            return new Project_VM()
            {
                AbsolutePath = filePath,
                ProjectName = currentProjectName,
                ProjectGuid = currentProjectGuid,
                Dependancies = DependsOn.Select(x => x.Key).ToList(),
                LastModified = lastModified
            };
        }
    }
}
