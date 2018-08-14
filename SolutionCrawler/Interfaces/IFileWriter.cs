using SolutionCrawler.Models;
using System.Collections.Generic;

namespace SolutionCrawler.Interfaces
{
    public interface IFileWriter
    {
        void WriteFiles_ToJSON(List<Project_VM> files);
    }
}
