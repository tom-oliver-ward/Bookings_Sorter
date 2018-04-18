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
        public string Equipment { get; set; }
        public List<string> User = new List<string>();
        public List<float> Hours = new List<float>();


        public Project(bool commercial, string projectName, string equipment, List<string> user, List<float> hours)
        {
            Commercial = commercial;
            ProjectName = projectName;
            Equipment = equipment;
            User = user;
            hours = Hours;
        }
    }
}
