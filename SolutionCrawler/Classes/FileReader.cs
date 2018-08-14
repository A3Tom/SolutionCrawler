using SolutionCrawler.Interfaces;
using SolutionCrawler.Models;
using System.Xml.Linq;

namespace SolutionCrawler.Classes
{
    public class FileReader : IFileReader
    {
        private readonly IXMLDeserializer _xmlDeserializer;

        public FileReader(IXMLDeserializer xmlDeserializer)
        {
            _xmlDeserializer = xmlDeserializer;
        }

        public Project_VM ReadCSProjFile(string filePath)
        {
            XDocument doc = XDocument.Load(filePath);

            return _xmlDeserializer.InflateProjectFromXML(doc, filePath);
        }
    }
}
