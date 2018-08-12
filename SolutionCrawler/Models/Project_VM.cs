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

        [XmlAttribute("AssemblyName")]
        public string ProjectName { get; set; }

        public string AbsolutePath { get; set; }

        public List<Project_VM> Dependancies { get; set; }

        [XmlAttribute("Reference")]
        public List<string> Deps { get; set; }

        public DateTime LastModified { get; set; }
    }
}
