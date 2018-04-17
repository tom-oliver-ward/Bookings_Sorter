using Excel;
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
        DataTransfer dataTransfer = new DataTransfer();
        HeadingPostitions headingPostitions = new HeadingPostitions(0, 0, 0, 0, 0, 0);
        public List<string> CurrentLine = new List<string>();
        public List<List<string>> Data = new List<List<string>>();
        public int ProjectCount = 0;
        public int pos = 0;
        public int comma;
        public int newline;
        public bool headingsRead = false;
        public int academicHours;
        public int commercialHours;
        public int fsAcademicHours;
        public int fsCommercialHours;

        //make an object


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

                readFile(i, form1);           //Runs process to read the given excel file and extract info & spectra
                SortData(i, form1);
            }
        }

        private void SortData(int i, Form1 form1)
        {
            for (int j = 0; j < Data.Count(); j++)
            {
                dataTransfer.addToProject(j, this);
            }

        }

        private void readFile(int i, Form1 formObject)
        {

            Form1 form1 = formObject;

            //sets filename to that of the current spreadsheet
            string Filename = (string)form1.SpreadSheets2Sort.Items[i];
            String input = File.ReadAllText(Filename);           //Reads and extracts important data from spectrum - extracting average values

            while (newline < input.Count())
            {
                ArrayMaker(input, form1);
            }
        }

        private void ArrayMaker(string input, Form1 formObject)
        {
            Form1 form1 = formObject;
            int j = -1;
            int k = 0;
            int length = 0;
            int type = 0;
            comma = input.IndexOf(",", pos);
            newline = input.IndexOf("\n", pos);

            if (comma > newline)
            {
                length = comma - pos;
                CurrentLine.Add(input.Substring(pos, length));
            }
            else
            {
                length = newline - pos;
                CurrentLine.Add(input.Substring(pos, length));
                dataTransfer.writeEntry(length, pos, input, this, k);
                j++;
                //updates user as to which row hence spectra
                form1.textBoxExcelLine.Text = "Processing line " + (j + 1);
                Application.DoEvents();
                CurrentLine.Clear();
            }
            pos = pos + length;
        }







    }
}
