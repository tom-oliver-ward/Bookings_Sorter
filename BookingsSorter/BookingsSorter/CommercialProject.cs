using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingsSorter
{
    public class CommercialProject : AcademicProject
    {
        public CommercialProject(bool commercial, string projectName, List<List<string>> useageList) : base(commercial, projectName, useageList)
        {
        }
    }
}
