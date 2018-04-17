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
        public Dictionary<string, Type> projectNames = new Dictionary<string, Type>();
        //Project project = new Project(false, null, null, null, null);

        internal void findHeadingPostitons(List<string> CurrentLine)
        {
            for (int i = 0; i < CurrentLine.Count; i++)
            {
                if (CurrentLine[i] == "Photon Factory Systems") { headingPostitions.EquipmentPosition = i; }
                if (CurrentLine[i] == "Start time") { headingPostitions.StartPosition = i; }
                if (CurrentLine[i] == "Finish time") { headingPostitions.FinishPosition = i; }
                if (CurrentLine[i] == "Laser User") { headingPostitions.LaserUserPosition = i; }
                if (CurrentLine[i] == "Project") { headingPostitions.ProjectPosition = i; }
                if (CurrentLine[i] == "Commercial?") { headingPostitions.CommercialPosition = i; }
            }
        } 
        

        internal void writeEntry(int length, int pos, string input, Processing formObject, int k)
        {
            Processing processing = formObject;
            
            if (processing.headingsRead == false)
            {
                findHeadingPostitons(processing.CurrentLine);
                processing.headingsRead = true;
                string x = processing.CurrentLine[headingPostitions.ProjectPosition];
            }

            else
            {
                if(projectNames.Count==0)
                {          
                    Type projectType = projectNames[k.ToString()];
                    object myInstance = Activator.CreateInstance(projectType);
                    Project myproject = (Project)myInstance;
                    myproject.ProjectName = processing.CurrentLine[headingPostitions.ProjectPosition];
                    myproject.Equipment = processing.CurrentLine[headingPostitions.EquipmentPosition];
                    myproject.Commercial = processing.CurrentLine[Convert.ToBoolean(Convert.ToInt16(headingPostitions.CommercialPosition))];

                    //projectNames.Add(k.ToString(),()=> new Project(false, null, null, null, null));
                }
                    
                else
                {
                    
                    for(int i =0;i<projectNames.Count;i++)
                    {
                        //string y = projectNames.ke
                        //string x = projectNames(1);
                        //var xx1 = projectNames[i.ToString()];
                    }
                }
              
                
                processing.Data.Add(processing.CurrentLine);

            }
        }

        internal void addToProject(int j, Processing formObject)
        {
            Processing processing = formObject;
            
        }


    }
}
