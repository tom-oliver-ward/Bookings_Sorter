using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingsSorter
{
    public class Project
    {
        public List<bool> Commercial = new List<bool>();
        public string ProjectName { get; set; }
        public List<List<string>> UseageList = new List<List<string>>();
        public List<string> Equipment =new List<string>();
        public List<string> User = new List<string>();
        public List<float> Hours = new List<float>();


        public Project(List<bool> commercial, string projectName, List<string> equipment, List<string> user, List<float> hours, List<List<string>> useageList)
        {
            useageList = UseageList;
            Commercial = commercial;
            ProjectName = projectName;
            Equipment = equipment;
            User = user;
            hours = Hours;
        }
    }
}
