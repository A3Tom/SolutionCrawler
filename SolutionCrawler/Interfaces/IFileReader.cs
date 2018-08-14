using SolutionCrawler.Models;

namespace SolutionCrawler.Interfaces
{
    public interface IFileReader
    {
        Project_VM ReadCSProjFile(string fileLocation);
    }
}
