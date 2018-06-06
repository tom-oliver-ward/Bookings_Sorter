using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingsSorter
{
    public class AcademicProject
    {
        // Variable to store project name
        public string ProjectName { get; set; }
        //2D jagged list to store table with user columns and equipment rows, with hours values listed
        public List<List<string>> UseageList = new List<List<string>>();

        //constructor
        public AcademicProject(string projectName, List<List<string>> useageList)
        {
            useageList = UseageList;            
            ProjectName = projectName;
        }
    }
}
