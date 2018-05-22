using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingsSorter
{
    public class AcademicProject
    {
        public string ProjectName { get; set; }
        public List<List<string>> UseageList = new List<List<string>>();

        public AcademicProject(bool commercial, string projectName, List<List<string>> useageList)
        {
            useageList = UseageList;            
            ProjectName = projectName;
        }
    }
}
