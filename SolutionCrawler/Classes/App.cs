using SolutionCrawler.Interfaces;
using SolutionCrawler.Models;
using System;
using System.IO;

namespace SolutionCrawler.Classes
{
    public class App : IApp
    {
        private const string BASE_DIRECTORY = @"C:\Billing";

        private readonly IFileReader _fileReader;

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
            Console.WriteLine("Solution Files");
            foreach (string file in solutionFiles)
            {
                Console.WriteLine(file);
            }

            Console.WriteLine("----------------");
            Console.WriteLine("Project Files");
            foreach (string filePath in projectFilePaths)
            {
                Console.WriteLine($"New Project found: {filePath}");
                Project project = _fileReader.ReadCSProjFile(filePath);
                
                Console.WriteLine("End of Project");
            }


            Console.ReadLine();
        }
    }
}