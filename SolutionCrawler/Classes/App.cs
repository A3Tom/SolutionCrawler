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
        private const string BASE_DIRECTORY = @"C:\SolutionCrawler\Target\";

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

            Console.WriteLine($"Total projects: {_projects.Count()} | Unique: {_projects.Distinct().Count()}");

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

            DoTheFuckinThing(_projects);

            _fileWriter.WriteFiles_ToJSON(_projects);
        }

        private void DoTheFuckinThing(List<Project_VM> projects)
        {
            Dictionary<string, int> projectsTierValued = new Dictionary<string, int>();
            int tierValue = 0;

            var tier0projects = projects.Where(x => x.Dependencies.Count == 0).ToList();
            var assignedProjects = tier0projects;

            while (projectsTierValued.Count() < projects.Distinct().Count())
            {
                if (projectsTierValued.Count() > 0)
                {
                    assignedProjects = new List<Project_VM>();

                    var graded = projectsTierValued.Select(x => x.Key).ToList();
                    var toGrade = projects.Where(x => !graded.Contains(x.MD5Ref));

                    foreach (var hash in toGrade)
                    {
                        if(hash.Dependencies.All(x => graded.Contains(x)))
                        {
                            assignedProjects.Add(hash);
                        }
                    }
                }

                Console.WriteLine();
                Console.WriteLine($"=== Tier {tierValue} Projects ===");
                foreach (var proj in assignedProjects)
                {
                    proj.TierValue = tierValue;
                    projectsTierValued.TryAdd(proj.MD5Ref, tierValue);

                    Console.WriteLine($"Project: {proj.MD5Ref} : {proj.ProjectName} : {proj.TierValue}");
                }
                Console.WriteLine($"");

                tierValue++;
            }
        }
    }
}