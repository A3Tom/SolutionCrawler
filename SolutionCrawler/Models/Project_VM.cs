using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SolutionCrawler.Models
{
    public class Project_VM
    {
        [Key]
        public string MD5Ref { get; set; }

        public string FullFilePath { get; set; }

        public Guid ProjectGuid { get; set; }

        public string ProjectName { get; set; }

        public string AbsolutePath { get; set; }

        public List<string> Dependencies { get; set; }

        public List<string> DependancyHashObjects { get; set; }

        public DateTime LastModified { get; set; }

        public int TierValue { get; set; }
    }
}
