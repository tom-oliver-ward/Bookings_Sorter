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
        internal bool add = true;                                        //variable to confirm whether to add the current project or if it already exists
        internal int posProject;          //int position storer for the position of the new project


        /// <summary>
        /// Reads through Data to find headings positions & stores them
        /// </summary>
        /// <param name="CurrentLine"></param>
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

        /// <summary>
        /// Sorts data by project
        /// </summary>
        /// <param name="length"></param>
        /// <param name="pos"></param>
        /// <param name="input"></param>
        /// <param name="formObject"></param>
        /// <param name="k"></param>
        internal void writeEntry(int length, int pos, string input, Processing formObject, int k)
        {
            Processing processing = formObject;

            //tests whether the heading positions have been found yet
            if (processing.headingsRead == false)
            {
                //reads heading positions
                findHeadingPostitons(processing.CurrentLine);
                //marks the tracking variable as complete
                processing.headingsRead = true;                
            }
            //Otherwise proceeds to enter data
            else
            {
                posProject = processing.projectList.Count;
                //finds if the existing project exists
                testExistingProject(processing);


                //adds a new project
                if (add)
                {
                    addProject(processing);   
                    add = false;
                }
                //if the project does exist, finds what data needs to be added
                else
                {
                    testExistingProjectData(processing);             
                }
            }
        }

        /// <summary>
        /// Fix Me
        /// </summary>
        /// <param name="processing"></param>
        private void testExistingProjectData(Processing processing)
        {
            List<string> sublist = new List<string>();
            bool addE = true;
            int posEquipment = 0;
            for (int i = 0; i < processing.projectList[posProject].UseageList[0].Count; i++)
            {
                if (processing.CurrentLine[headingPostitions.EquipmentPosition] == processing.projectList[posProject].UseageList[0][i])
                {
                    addE = false;
                    posEquipment = i;
                }
            }

            if (addE)
            {
                processing.projectList[posProject].UseageList[0].Add(processing.CurrentLine[headingPostitions.EquipmentPosition]);

                bool addU = true;
                int posUser = 0;
                for (int i = 0; i < processing.projectList[posProject].UseageList.Count; i++)
                {
                    if (processing.CurrentLine[headingPostitions.LaserUserPosition] == processing.projectList[posProject].UseageList[i][0])
                    {
                        addU = false;
                        posUser = i;
                    }
                }

                if (addU)
                {
                    List<string> sublistU = new List<string>();
                    sublistU.Add(processing.CurrentLine[headingPostitions.LaserUserPosition]);
                    processing.projectList[posProject].UseageList.Add(sublistU);
                }
            }
        }

        /// <summary>
        /// If project doesn't exist then it adds it as instance of the class Project to the list (projectList)
        /// </summary>
        /// <param name="processing"></param>
        private void addProject(Processing processing)
        {
            //sublist for User data
            List<string> sublistU = new List<string>();
            //sublist for equipment data
            List<string> sublistE = new List<string>();
            //Adds a new instance
            processing.projectList.Add(null);
            //Adds the project name to this instance
            processing.projectList[processing.projectList.Count].ProjectName = processing.CurrentLine[headingPostitions.ProjectPosition];

            //sets top left corner of "table" (actually list) to blank
            sublistU.Add(null);
            //Adds the laser user to sublist U
            sublistU.Add(processing.CurrentLine[headingPostitions.LaserUserPosition]);
            //Adds sublist U to a new line in the "table"
            processing.projectList[processing.projectList.Count].UseageList.Add(sublistU);

            //Adds equipment to list
            sublistE.Add(processing.CurrentLine[headingPostitions.EquipmentPosition]);
            //calculates hours
            float hours = hoursCalc(processing);
            //Adds hours to sublistE
            sublistE.Add(Convert.ToString(hours));
            //Adds sublist E to "table" of projectList
            processing.projectList[processing.projectList.Count].UseageList.Add(sublistE);
        }

        private void testExistingProject(Processing processing)
        {
            throw new NotImplementedException();
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

            monthS = Convert.ToInt32(processing.CurrentLine[headingPostitions.StartPosition].Substring(monthStart, otherLength));
            monthF = Convert.ToInt32(processing.CurrentLine[headingPostitions.FinishPosition].Substring(monthStart, otherLength));
            month = monthF - monthS;

            dayS = Convert.ToInt32(processing.CurrentLine[headingPostitions.StartPosition].Substring(dayStart, otherLength));
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

        internal void addToProject(int j, Processing processing)
        {
            for (int i = 0; i > processing.projectList.Count; i++)
            {
                if (processing.CurrentLine[headingPostitions.ProjectPosition] == processing.projectList[i].ProjectName)
                {
                    add = false;
                    posProject = i;
                }
            }
        }


    }
}
