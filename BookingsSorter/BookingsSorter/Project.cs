using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingsSorter
{
    public class Project
    {
        public bool Commercial { get; set; }
        public string ProjectName { get; set; }
        public List<List<string>> UseageList = new List<List<string>>();

        public Project(bool commercial, string projectName, List<List<string>> useageList)
        {
            useageList = UseageList;
            Commercial = commercial;
            ProjectName = projectName;
        }
    }
}
