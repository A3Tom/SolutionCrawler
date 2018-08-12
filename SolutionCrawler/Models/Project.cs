using System;
using System.Collections.Generic;

namespace SolutionCrawler.Models
{
    public class Project
    {
        public PropertyGroup PropertyGroup { get; set; }
        public ItemGroup ItemGroup { get; set; }
        public string Import { get; set; }
    }

    public class PropertyGroup
    {
        public string AssemblyName { get; set; }

        public Guid ProjectGuid { get; set; }

        public string TargetFrameworkVersion { get; set; }
    }

    public class ItemGroup
    {
        public List<string> Reference { get; set; }
    }
}
