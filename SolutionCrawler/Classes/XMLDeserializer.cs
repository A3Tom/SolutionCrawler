using SolutionCrawler.Interfaces;
using SolutionCrawler.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace SolutionCrawler.Classes
{
    public class XMLDeserializer : IXMLDeserializer
    {
        private readonly IDankCommander _md5Hasher;

        public XMLDeserializer(IDankCommander md5Hasher)
        {
            _md5Hasher = md5Hasher;
        }

        public Project_VM InflateProjectFromXML(XDocument document, string filePath)
        {
            Dictionary<string, string> DependsOn = new Dictionary<string, string>();

            Guid currentProjectGuid = Guid.Empty;
            string currentProjectName = string.Empty;

            DateTime.TryParse(new FileInfo(filePath)?.LastWriteTime.ToLongDateString(), out DateTime lastModified);

            foreach (XElement element in document.Root.Elements().Elements())
            {
                string newReferenceProjectHash = string.Empty;
                string newReferenceProjectHashObject = string.Empty;

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
                            var refRelativePath = element.Attribute("Include").Value;
                            var refAbsolutePath = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(filePath), refRelativePath));

                            newReferenceProjectHashObject = refAbsolutePath;
                            newReferenceProjectHash = _md5Hasher.CalculateMD5Hash(refAbsolutePath);
                        }
                        break;
                    default:
                        break;
                }

                if (newReferenceProjectHash != string.Empty)
                {
                    DependsOn.Add(newReferenceProjectHash, newReferenceProjectHashObject);
                }
            }

            return new Project_VM()
            {
                FullFilePath = filePath,
                AbsolutePath = Path.GetDirectoryName(filePath),
                MD5Ref = _md5Hasher.CalculateMD5Hash(filePath),
                ProjectName = currentProjectName,
                ProjectGuid = currentProjectGuid,
                Dependencies = DependsOn.Select(x => x.Key).ToList(),
                DependancyHashObjects = DependsOn.Select(x => x.Value).ToList(),
                LastModified = lastModified
            };
        }
    }
}
