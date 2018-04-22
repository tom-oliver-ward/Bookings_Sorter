using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BookingsSorter
{
    class DataTransfer
    {

        //public List<Project> ProjectList=new List<Project>();         //unnecesary?
        //Project project = new Project(false, null, null, null, null);     //unesccesary?

        //initialises instance of class
        HeadingPostitions headingPostitions = new HeadingPostitions(0, 0, 0, 0, 0, 0);

        
        

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
                if(processing.projectNames.Count==0)
                {          
                    Type projectType = processing.projectNames[k.ToString()];
                    object myInstance = Activator.CreateInstance(projectType);
                    Project myproject = (Project)myInstance;
                    myproject.ProjectName = processing.CurrentLine[headingPostitions.ProjectPosition];
                    myproject.Equipment = processing.CurrentLine[headingPostitions.EquipmentPosition];                   
                    myproject.Commercial = Convert.ToBoolean(Convert.ToInt32(processing.CurrentLine[headingPostitions.CommercialPosition]));
                    myproject.User.Add(processing.CurrentLine[headingPostitions.LaserUserPosition]);
                    float hours = hoursCalc(processing);
                    myproject.Hours.Add(hours);
                }
                    
                else
                {
                    
                    for(int i =0;i< processing.projectNames.Count;i++)
                    {
                        if(processing.projectNames[i.ToString()]==i)
                        {

                        }

                        //string y = projectNames.ke
                        //string x = projectNames(1);
                        //var xx1 = projectNames[i.ToString()];
                    }
                }
              
                
                processing.Data.Add(processing.CurrentLine);

            }
        }

        private float hoursCalc(Processing formObject)
        {
            Processing processing = formObject;
            int dayS; int dayF; int day;
            int monthS; int monthF; int month;
            int yearS; int yearF; int year;
            int hourS; int hourF; int hour;
            float minuteS; float minuteF; float minute;
            float hoursTotal;

            int yearStart = 0;
            int yearLength = 4;
            int monthStart = 5;
            int otherLength = 2;
            int dayStart = 8;
            int hourStart = 13;
            int minuteStart = 16;

            int yearFactor = 8760;
            float monthFactor = 730;
            int dayFactor = 24;
            int minuteInvFactor = 60;


            yearS = Convert.ToInt32(processing.CurrentLine[headingPostitions.StartPosition].Substring(yearStart, yearLength));
            yearF = Convert.ToInt32(processing.CurrentLine[headingPostitions.FinishPosition].Substring(yearStart, yearLength));
            year = yearF - yearS;

            monthS = Convert.ToInt32(processing.CurrentLine[headingPostitions.StartPosition].Substring(monthStart,otherLength));
            monthF = Convert.ToInt32(processing.CurrentLine[headingPostitions.FinishPosition].Substring(monthStart, otherLength));
            month = monthF - monthS;

            dayS = Convert.ToInt32(processing.CurrentLine[headingPostitions.StartPosition].Substring(dayStart,otherLength));
            dayF = Convert.ToInt32(processing.CurrentLine[headingPostitions.FinishPosition].Substring(dayStart, otherLength));
            day = dayF - dayS;

            hourS = Convert.ToInt32(processing.CurrentLine[headingPostitions.StartPosition].Substring(hourStart, otherLength));
            hourF = Convert.ToInt32(processing.CurrentLine[headingPostitions.FinishPosition].Substring(hourStart, otherLength));
            hour = hourF - hourS;

            minuteS = Convert.ToInt32(processing.CurrentLine[headingPostitions.StartPosition].Substring(minuteStart, otherLength));
            minuteF = Convert.ToInt32(processing.CurrentLine[headingPostitions.FinishPosition].Substring(minuteStart, otherLength));
            minute = minuteF - minuteS;

            hoursTotal = year * yearFactor + month * monthFactor + day * dayFactor + hour + minute / minuteInvFactor;

            return hoursTotal;
        }

        internal void addToProject(int j, Processing formObject)
        {
            Processing processing = formObject;
            
        }


    }
}
