using SolutionCrawler.Interfaces;
using SolutionCrawler.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace SolutionCrawler.Classes
{
    public class App : IApp
    {
        private const string BASE_DIRECTORY = @"..\Target\";

        private readonly IFileReader _fileReader;
        private Dictionary<Guid, string> _projects = new Dictionary<Guid, string>();

        public App(IFileReader fileReader)
        {
            _fileReader = fileReader;
        }

        public void Run()
        {
            Console.WriteLine("Application Starting");

            string[] solutionFiles = Directory.GetFiles(BASE_DIRECTORY, "*.sln", SearchOption.AllDirectories);
            string[] projectFilePaths = Directory.GetFiles(BASE_DIRECTORY, "*.csproj", SearchOption.AllDirectories);

            Console.WriteLine("----------------");
            foreach (string filePath in projectFilePaths)
            {
                Console.WriteLine($"New Project found: {filePath}");

                foreach(var project in _fileReader.ReadCSProjFile(filePath))
                {
                    _projects.TryAdd(project.Key, project.Value);
                }
                
                Console.WriteLine();
            }

            Console.WriteLine("Unique projects:");
            foreach (var project in _projects)
            {
                Console.WriteLine($"Name: {project.Value} | Guid: {project.Key}");
            }

            Console.ReadLine();
        }
    }
}