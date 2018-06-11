using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BookingsSorter
{
    class DataTransfer
    {

        //initialises instance of class        
        AddData addData = new AddData();
        TestExist testExist = new TestExist();

        internal bool add = true;                                        //variable to confirm whether to add the current project or if it already exists
        internal int posProject = 0;          //int position storer for the position of the new project
        internal int posProjectC = 0;          //int position storer for the position of the new project
        internal bool addE = true;           //initialises variable for whether to add Equipment
        internal int posEquipment = 1;       //variable for where to add equipment
        internal bool addU = true;           //variable for whether to add user
        internal int posUser = 1;            //variable for whether to add Equipment
        internal bool commercial;
        //test whether these get reset when it reopens


        /// <summary>
        /// Reads through Data to find headings positions & stores them
        /// </summary>
        /// <param name="CurrentLine"></param>
        internal void findHeadingPostitons(List<string> CurrentLine, Processing processing)
        {
            for (int i = 0; i < CurrentLine.Count; i++)
            {

                if (CurrentLine[i] == "Photon Factory Systems") { processing.headingPostitions.EquipmentPosition = i; }
                if (CurrentLine[i] == "Start time") { processing.headingPostitions.StartPosition = i; }
                if (CurrentLine[i] == "Finish time") { processing.headingPostitions.FinishPosition = i; }
                if (CurrentLine[i] == "User") { processing.headingPostitions.LaserUserPosition = i; }
                if (CurrentLine[i] == "Project") { processing.headingPostitions.ProjectPosition = i; }
                if (CurrentLine[i] == "Commercial?") { processing.headingPostitions.CommercialPosition = i; }
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
                findHeadingPostitons(processing.CurrentLine, processing);
                //marks the tracking variable as complete
                processing.headingsRead = true;
            }
            //Otherwise proceeds to enter data
            else
            {
                projectEntry(processing);
                equipmentEntry(processing);                
            }
        }

        private void equipmentEntry(Processing processing)
        {
            posEquipment = processing.equipmentList.Count;

            testExist.testExistingEquipmentList(processing, this);

            if (addE) {addData.addEquipmentList(processing,this); }

            addData.addEquipmentListHours(processing, this);

            posEquipment = 1;
            addE = true;
        }

        private void projectEntry(Processing processing)
        {
            posProject = processing.projectList.Count;
            posProjectC = processing.projectListC.Count;
            //finds if the existing project exists

            testExist.testExistingProject(processing, this);
            //adds a new project
            if (add)
            {
                addData.addProject(processing, this, testExist);

            }
            //if the project does exist, finds what data needs to be added
            else
            {
                //test if user exists
                testExist.testExistingUser(processing, this);
                // tests if equipment exists
                testExist.testExistingEquipment(processing, this);


                //add Equipment item to existing first list
                if (addE) { addData.addEquipment(processing, this); }

                //add user as start of new list
                if (addU) { addData.addUser(processing, this); }

                //add the hours at the appropriate point
                addData.addHours(processing, this, testExist);

                add = true;
                addE = true;
                addU = true;

                posProject = 0;          //int position storer for the position of the new project
                posProjectC = 0;          //int position storer for the position of the new project
                posEquipment = 1;       //variable for where to add equipment
                posUser = 1;            //variable for whether to add Equipment
            }
        }


        /// <summary>
        /// Sums the hours to the existing value
        /// </summary>
        /// <param name="processing"></param>
        /// <param name="hours"></param>
        /// <returns></returns>
        internal float sumHours(Processing processing, float hours)
        {
            if (commercial)
            {
                //is entry null?
                if (processing.projectListC[posProjectC].UseageList[posEquipment][posUser] == null) { }
                //else reads current value and adds the new value
                else
                {
                    float existing = Convert.ToSingle(processing.projectListC[posProjectC].UseageList[posUser][posEquipment]);
                    hours = hours + existing;
                }
            }
            else
            {
                //is entry null?
                if (processing.projectList[posProject].UseageList[posEquipment][posUser] == null) { }
                //else reads current value and adds the new value
                else
                {
                    float existing = Convert.ToSingle(processing.projectList[posProject].UseageList[posEquipment][posUser]);
                    hours = hours + existing;
                }
            }


            //returns the updated hours variable
            return hours;
        }


        /// <summary>
        /// Calculates the number of hours for a given bookinh
        /// </summary>
        /// <param name="formObject"></param>
        /// <returns></returns>
        internal float hoursCalc(Processing processing)
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
            yearS = Convert.ToInt32(processing.CurrentLine[processing.headingPostitions.StartPosition].Substring(yearStart, yearLength));
            yearF = Convert.ToInt32(processing.CurrentLine[processing.headingPostitions.FinishPosition].Substring(yearStart, yearLength));
            year = yearF - yearS;

            monthS = Convert.ToInt32(processing.CurrentLine[processing.headingPostitions.StartPosition].Substring(monthStart, otherLength));
            monthF = Convert.ToInt32(processing.CurrentLine[processing.headingPostitions.FinishPosition].Substring(monthStart, otherLength));
            month = monthF - monthS;

            dayS = Convert.ToInt32(processing.CurrentLine[processing.headingPostitions.StartPosition].Substring(dayStart, otherLength));
            dayF = Convert.ToInt32(processing.CurrentLine[processing.headingPostitions.FinishPosition].Substring(dayStart, otherLength));
            day = dayF - dayS;

            try
            {
                hourS = Convert.ToInt32(processing.CurrentLine[processing.headingPostitions.StartPosition].Substring(hourStart, otherLength));
                minuteS = Convert.ToInt32(processing.CurrentLine[processing.headingPostitions.StartPosition].Substring(minuteStart, otherLength));
            }
            catch (System.FormatException)
            {
                hourS = Convert.ToInt32(processing.CurrentLine[processing.headingPostitions.StartPosition].Substring(hourStart, otherLength - 1));
                minuteS = Convert.ToInt32(processing.CurrentLine[processing.headingPostitions.StartPosition].Substring(minuteStart - 1, otherLength));
            }

            try
            {
                hourF = Convert.ToInt32(processing.CurrentLine[processing.headingPostitions.FinishPosition].Substring(hourStart, otherLength));
                minuteF = Convert.ToInt32(processing.CurrentLine[processing.headingPostitions.FinishPosition].Substring(minuteStart, otherLength));
            }
            catch (System.FormatException)
            {
                hourF = Convert.ToInt32(processing.CurrentLine[processing.headingPostitions.FinishPosition].Substring(hourStart, otherLength - 1));
                minuteF = Convert.ToInt32(processing.CurrentLine[processing.headingPostitions.FinishPosition].Substring(minuteStart - 1, otherLength));
            }

            hour = hourF - hourS;
            minute = minuteF - minuteS;

            //sums total hours from all types
            hoursTotal = year * yearFactor + month * monthFactor + day * dayFactor + hour + minute / minuteInvFactor;
            //returns this
            return hoursTotal;
        }

    }
}
