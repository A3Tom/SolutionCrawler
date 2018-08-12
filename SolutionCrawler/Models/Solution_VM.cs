using System;
using System.Collections.Generic;

namespace SolutionCrawler.Models
{
    public class Solution_VM
    {
        public Guid Guid { get; set; }

        public string Name { get; set; }

        public List<Project_VM> IncludedProjects { get; set; }
    }
}