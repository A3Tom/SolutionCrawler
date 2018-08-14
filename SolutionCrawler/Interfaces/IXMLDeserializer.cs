using SolutionCrawler.Models;
using System.Xml.Linq;

namespace SolutionCrawler.Interfaces
{
    public interface IXMLDeserializer
    {
        Project_VM InflateProjectFromXML(XDocument document, string filePath);
    }
}
