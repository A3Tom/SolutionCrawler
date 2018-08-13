using System;
using System.Collections.Generic;

namespace SolutionCrawler.Interfaces
{
    public interface IFileReader
    {
        Dictionary<Guid, string> ReadCSProjFile(string fileLocation);
    }
}
