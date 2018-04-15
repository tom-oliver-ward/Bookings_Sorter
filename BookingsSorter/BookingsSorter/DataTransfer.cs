using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BookingsSorter
{
    class DataTransfer
    {
        public List<Project> ProjectList=new List<Project>();
        HeadingPostitions headingPostitions = new HeadingPostitions(0, 0, 0, 0, 0, 0);
        //Project project = new Project(false, null, null, null, null);

        internal void readHeadings(int j, Excel.Cell cell, Processing formObject)
        {
            Processing processing = formObject;
            if(j==0)
            {
                processing.Headings.Add(cell.Text);
            }
        }

        internal void readCurrentLine(int j, Excel.Cell cell, Processing formObject)
        {
            Processing processing = formObject;
            if (j>=1)
            {
                processing.CurrentLine.Add(cell.Text);
            }
        }

        internal void findHeadingPostitons(List<string> Headings, List<string> CurrentLine)
        {
            for (int i = 0; i < CurrentLine.Count; i++)
            {
                if (Headings[i] == "Photon Factory Systems") { headingPostitions.EquipmentPosition = i; }
                if (Headings[i] == "Start time") { headingPostitions.StartPosition = i; }
                if (Headings[i] == "Finish time") { headingPostitions.FinishPosition = i; }
                if (Headings[i] == "Laser User") { headingPostitions.LaserUserPosition = i; }
                if (Headings[i] == "Project") { headingPostitions.ProjectPosition = i; }
                if (Headings[i] == "Commercial?") { headingPostitions.CommercialPosition = i; }
            }
        }
        
        internal void dataEntry(List<string> Headings, List<string> CurrentLine)
        {

            
                
        }

        internal void transferLine(List<string> CurrentLine)
        {
            Dictionary<string,int> projectNames = new Dictionary<string,int>();
            
            ProjectList.Find(x => x.ProjectName.Contains("test"));
            
            //need itemized list
            
        }

       
    }
}
