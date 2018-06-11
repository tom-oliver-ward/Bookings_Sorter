using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingsSorter
{
    //derived class from academic project
    public class CommercialProject : AcademicProject
    {
        public CommercialProject(string projectName, List<List<string>> useageList) : base(projectName, useageList)
        {
        }
    }
}
