﻿using Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;

namespace BookingsSorter
{

    class Processing
    {
        //initialises instances
        DataTransfer dataTransfer = new DataTransfer();
        public HeadingPostitions headingPostitions = new HeadingPostitions(0, 0, 0, 0, 0, 0);

        //initialises list for the current line of the csv file
        public List<string> CurrentLine = new List<string>();


        //initialises dictionary for "dynamic" creation of instances of the project list
        public List<AcademicProject> projectList = new List<AcademicProject>();
        public List<CommercialProject> projectListC = new List<CommercialProject>();

        //variable initialisation
        public int ProjectCount = 0;            //Count of Projects
        public int pos = 0;                     //position holder for reading in lines
        public int comma;                       //variable for comma position - ie next input
        public int newline = 1;                     //variable for new line input reading
        public bool headingsRead = false;       //variable to test for headings read in each file
        internal bool end = false;
        public float academicHours=0;               //count variable of academic hours
        public float commercialHour=0;             //count variable of commercial hours


        /// <summary>
        /// Reads through each spreadsheet and processes the data
        /// </summary>
        /// <param name="formObject"></param>
        internal void readCSV(Form1 formObject)
        {
            Form1 form1 = formObject;

            //finds the length of the list of spreadsheets
            int length = System.Convert.ToInt32(System.Convert.ToDouble(form1.SpreadSheets2Sort.Items.Count.ToString()));

            for (int i = 0; i < length; i++)
            {
                //updates user on which spreadsheet
                form1.FilenumTB.Text = "Processing file " + (i + 1) + " of " + length;
                Application.DoEvents();

                readFile(i, form1);           //reads data into a given line
                
            }
        }

        /// <summary>
        /// Reads the file and passes to a subclass to process the line into an array
        /// </summary>
        /// <param name="i"></param>
        /// <param name="formObject"></param>
        private void readFile(int i, Form1 formObject)
        {

            Form1 form1 = formObject;

            //sets filename to that of the current spreadsheet
            string Filename = (string)form1.SpreadSheets2Sort.Items[i];
            string input = File.ReadAllText(Filename);           //Reads and extracts important data from spectrum - extracting average values

            //ensures that it stops at the end of the file - maybe can use input/new line value to calculate
            while (newline < input.Count() && !end)
            {
                ArrayMaker(input, form1);       //proceses the input
            }
        }

        /// <summary>
        /// finds variable and newline values in the code
        /// </summary>
        /// <param name="input"></param>
        /// <param name="formObject"></param>
        private void ArrayMaker(string input, Form1 formObject)
        {
            Form1 form1 = formObject;
            int j = -1;         //initialise count variable for line count
            int k = 0;          //initialise count variable for ??? project number
            int length = 0;     //initialise length variable, for the length of each csv entry
 
            comma = input.IndexOf(",", pos);        //finds the next comma indicator
            newline = input.IndexOf("\n", pos);     //finds the next new line indicator

            //check whether variable is in line or is a newline variable
            if (comma < newline)
            {                
                length = comma - pos;                               //length of entry for this condition
                CurrentLine.Add(input.Substring(pos, length));      //runs function to add variable to the current line
                pos = pos + length + 1; //updates position variable
            }
            //else if end of line
            else if (newline < 0)
            {
                end = true;
            }
            else
            {                
                length = newline - pos - 2;                             //length of entry for this condition
                CurrentLine.Add(input.Substring(pos, length));      //runs function to add variable to the current line
                dataTransfer.writeEntry(length, pos, input, this, k);   //add entry for this line
                j++;                                                    //adds to line count
                
                //updates user as to current line
                form1.textBoxExcelLine.Text = "Processing line " + (j + 1);
                Application.DoEvents();
                //clears the variable
                CurrentLine.Clear();
                pos = pos + length + 3; //updates position variable
            }
            
        }
    }
}
