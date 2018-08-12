using SolutionCrawler.Models;

namespace SolutionCrawler.Interfaces
{
    public interface IFileReader
    {
        Project ReadCSProjFile(string fileLocation);
    }
}
