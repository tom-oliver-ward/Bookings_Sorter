using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BookingsSorter
{
    class DataTransfer
    {

        //initialises instance of class
        HeadingPostitions headingPostitions = new HeadingPostitions(0, 0, 0, 0, 0, 0);
        internal bool add = true;                                        //variable to confirm whether to add the current project or if it already exists
        internal int posProject=0;          //int position storer for the position of the new project
        internal bool addE = true;           //initialises variable for whether to add Equipment
        internal int posEquipment = 1;       //variable for where to add equipment

        internal bool addU = true;           //variable for whether to add user
        internal int posUser = 1;            //variable for whether to add Equipment
        //test whether these get reset when it reopens


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
                   
                }
                //if the project does exist, finds what data needs to be added
                else
                {
                    testExistingProjectData(processing);             
                }
            }
        }

        /// <summary>
        /// This tests whether a new equipment or user header needs to be added or wether hour addition exists
        /// </summary>
        /// <param name="processing"></param>
        private void testExistingProjectData(Processing processing)
        {
            //establishes sublist for convenient adding of lists
            List<string> sublist = new List<string>();


            //tests if equipment exists
            testExistingEquipment(processing);

            //test if user exists
            testExistingUser(processing);

            //add Equipment item to existing first list
            if (addE)
            {
                addEquipment(processing);
            }

            //add user as start of new list
            if (addU)
            {
                addUser(processing);               
            }


        }

        /// <summary>
        /// This creates a new list to add to the base list with the new user listed
        /// </summary>
        /// <param name="processing"></param>
        private void addUser(Processing processing)
        {
            //creates list
            List<string> sublistU = new List<string>();
            //adds the laser user to the list 
            sublistU.Add(processing.CurrentLine[headingPostitions.LaserUserPosition]);
            //adds a null point as each sub list will be at least 2 points long
            sublistU.Add(null);
            //adds the list to the base list
            processing.projectList[posProject].UseageList.Add(sublistU);
            //sets the variables back to default
            posUser = 1;
        }

        /// <summary>
        /// This adds the equipment to the first list  in the base list
        /// </summary>
        /// <param name="processing"></param>
        private void addEquipment(Processing processing)
        {
            //adds the entry
            processing.projectList[posProject].UseageList[0].Add(processing.CurrentLine[headingPostitions.EquipmentPosition]);

            //sets the variables back to default            
            posEquipment = 1;
        }

        /// <summary>
        /// Adds the hours at the specified position by posUser and posEquipment
        /// </summary>
        /// <param name="processing"></param>
        private void addHours(Processing processing)
        {
            //calculates hours
            float hours = hoursCalc(processing);

            //test if there is already an entry at posUser,posEquipment
            testHourPosExists(processing);

            //adds the hours variable to that given point
            hours = sumHours(processing,hours);

            //sets the given coordinate to the hours output
            processing.projectList[posProject].UseageList[posUser][posEquipment] = Convert.ToString(hours);            
        }

        /// <summary>
        /// Tests if there is a value there or not
        /// </summary>
        /// <param name="processing"></param>
        /// <param name="hours"></param>
        /// <returns></returns>
        private float sumHours(Processing processing, float hours)
        {
            //is entry null?
            if (processing.projectList[posProject].UseageList[posUser][posEquipment] == null)
            {
                processing.projectList[posProject].UseageList[posUser][posEquipment] = Convert.ToString(hours);
            }
            //else reads current value and adds the new value
            else
            {
                float existing = Convert.ToSingle(processing.projectList[posProject].UseageList[posUser][posEquipment]);
                hours = hours + existing;
                processing.projectList[posProject].UseageList[posUser][posEquipment] = Convert.ToString(hours);
            }
            //returns the updated hours variable
            return hours;
        }

        //tests whether the co-ordinate defined by posUser,PosEquipment exists, if not extends required list to that length
        private void testHourPosExists(Processing processing)
        {
            //if long enough, return
            if (posEquipment <= processing.projectList[posProject].UseageList[posUser].Count-1) { return; }
            //else add null until it is long enough
            else
            {
                while (posEquipment <= processing.projectList[posProject].UseageList[posUser].Count - 1)
                {
                    processing.projectList[posProject].UseageList[posUser].Add(null);
                }
            }
        }

        /// <summary>
        /// Tests if given user entry already exists
        /// </summary>
        /// <param name="processing"></param>
        private void testExistingUser(Processing processing)
        {
            //iterates through the existing list matrix
            for (int i = 0; i < processing.projectList[posProject].UseageList.Count; i++)
            {
                //tests whether for a match, if so sets addU and stores user position
                if (processing.CurrentLine[headingPostitions.LaserUserPosition] == processing.projectList[posProject].UseageList[i][0])
                {
                    addU = false;
                    posUser = i;
                }
            }
        }

        /// <summary>
        /// Tests if a given equipment entry exists
        /// </summary>
        /// <param name="processing"></param>
        private void testExistingEquipment(Processing processing)
        {
            //iterates through the existing list matrix
            for (int i = 0; i < processing.projectList[posProject].UseageList[0].Count; i++)
            {
                //tests whether for a match, if so sets addE and stores Equipment position
                if (processing.CurrentLine[headingPostitions.EquipmentPosition] == processing.projectList[posProject].UseageList[0][i])
                {
                    addE = false;
                    posEquipment = i;
                }
            }
        }

        /// <summary>
        /// If project doesn't exist then it adds it as instance of the class Project to the list (projectList)
        /// </summary>
        /// <param name="processing"></param>
        private void addProject(Processing processing)
        {
            //Adds a new instance
            processing.projectList.Add(null);
            //Adds the project name to this instance
            processing.projectList[processing.projectList.Count].ProjectName = processing.CurrentLine[headingPostitions.ProjectPosition];

            //Adds User to list
            addUser(processing);

            //Adds equipment to list
            addEquipment(processing);

            //Adds hours
            addHours(processing);

            //resets hours
            posProject = 0;
        }

        /// <summary>
        /// Tests if the project already exists
        /// </summary>
        /// <param name="processing"></param>
        private void testExistingProject(Processing processing)
        {
            //iterates through each instance of project
            for (int i = 0; i > processing.projectList.Count; i++)
            {
                //tests if the current entry matches any of the existing project names
                if (processing.CurrentLine[headingPostitions.ProjectPosition] == processing.projectList[i].ProjectName)
                {
                    //sets add to false, stores position and breaks
                    add = false;
                    posProject = i;
                    break;
                }
            }
        }

        /// <summary>
        /// Calculates the number of hours for a given bookinh
        /// </summary>
        /// <param name="formObject"></param>
        /// <returns></returns>
        private float hoursCalc(Processing processing)
        {
            //variable positions initialised where xS denotes start, xF denotes finish, and x denotes sum
            int dayS; int dayF; int day;
            int monthS; int monthF; int month;
            int yearS; int yearF; int year;
            int hourS; int hourF; int hour;
            float minuteS; float minuteF; float minute;
            //total hours
            float hoursTotal;

            //variables customised to the position that the file stores data - this could perhaps be improved to be more automated
            int yearStart = 0;
            int yearLength = 4;
            int monthStart = 5;
            int otherLength = 2;
            int dayStart = 8;
            int hourStart = 13;
            int minuteStart = 16;

            //factor, how many hours in a given entry
            int yearFactor = 8760;
            float monthFactor = 730;
            int dayFactor = 24;
            int minuteInvFactor = 60;

            //reads length of each entry type
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

            //sums total hours from all types
            hoursTotal = year * yearFactor + month * monthFactor + day * dayFactor + hour + minute / minuteInvFactor;
            //returns this
            return hoursTotal;
        }

    }
}
