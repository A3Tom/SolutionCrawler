using SolutionCrawler.Interfaces;
using SolutionCrawler.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SolutionCrawler.Classes
{
    public class App : IApp
    {
        private const string BASE_DIRECTORY = @"C:\Repositories\SolutionCrawler\Target\";

        private readonly IFileReader _fileReader;
        private readonly IFileWriter _fileWriter;
        private List<Project_VM> _projects;

        public App(IFileReader fileReader,
            IFileWriter fileWriter)
        {
            _fileReader = fileReader;
            _fileWriter = fileWriter;
        }

        public void Run()
        {
            Console.WriteLine("Application Starting");

            string[] solutionFiles = Directory.GetFiles(BASE_DIRECTORY, "*.sln", SearchOption.AllDirectories);
            string[] projectFilePaths = Directory.GetFiles(BASE_DIRECTORY, "*.csproj", SearchOption.AllDirectories);

            _projects = new List<Project_VM>();

            foreach (string filePath in projectFilePaths)
            {
                _projects.Add(_fileReader.ReadCSProjFile(filePath));
            }

            var uniqueProjects = _projects.Distinct();

            Console.WriteLine("All projects:");
            foreach (var project in _projects)
            {
                Console.WriteLine($"Name: {project.ProjectName} | Guid: {project.ProjectGuid}");
            }

            Console.WriteLine("Unique projects:");
            foreach (var project in _projects.Distinct())
            {
                Console.WriteLine($"Name: {project.ProjectName}");
                Console.WriteLine($"    Last Modified: {project.LastModified}");
                Console.WriteLine($"    Guid: {project.ProjectGuid}");
                Console.WriteLine($"    Refs: {project.Dependencies.Count()}");
                Console.WriteLine($"    Path: {project.AbsolutePath}");
                Console.WriteLine($"    Ref : {project.MD5Ref}");

                Console.WriteLine();
            }

            _fileWriter.WriteFiles_ToJSON(_projects);
        }
    }
}