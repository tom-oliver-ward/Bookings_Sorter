using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingsSorter
{
    public class Project
    {
        public bool Academic { get; set; }
        public string ProjectName { get; set; }
        public string Equipment { get; set; }
        public List<string> User = new List<string>();
        public List<int> Hours = new List<int>();


        public Project(bool academic, string projectName, string equipment, List<string> user, List<int> hours)
        {
            Academic = academic;
            ProjectName = projectName;
            Equipment = equipment;
            User = user;
            hours = Hours;
        }
    }
}
