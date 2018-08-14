using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace SolutionCrawler.Models
{
    public class Project_VM
    {
        [Key]
        public Guid ProjectGuid { get; set; }

        public string ProjectName { get; set; }

        public string AbsolutePath { get; set; }

        public List<Guid> Dependancies { get; set; }

        public DateTime LastModified { get; set; }
    }
}
