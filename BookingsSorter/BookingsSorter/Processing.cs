using Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace BookingsSorter
{
    
    class Processing
    {
        DataTransfer dataTransfer = new DataTransfer();
        HeadingPostitions headingPostitions = new HeadingPostitions(0,0,0,0,0,0);
        public List<string> Headings = new List<string>();
        public List<string> CurrentLine = new List<string>();
        public int ProjectCount = 0;
        //make an object


        internal void readExcel(Form1 formObject)
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
            }
        }

        private void readFile(int i, Form1 formObject)
        {
            
            Form1 form1 = formObject;

            //sets filename to that of the current spreadsheet
            string filename = (string)form1.SpreadSheets2Sort.Items[i];
            int j = 0;      //initialises second count variable for each row

            //steps through worksheets (should only be one)
            foreach (var worksheet in Workbook.Worksheets(filename))
            {
                foreach (var row in worksheet.Rows)
                {
                    //updates user as to which row hence spectra
                    form1.textBoxExcelLine.Text = "Processing line " + (j + 1) + " of " + worksheet.Rows.Length;
                    Application.DoEvents();
                    int k = 0;          //count for cell variable

                    //steps through each cell
                    foreach (var cell in row.Cells)
                    {
                        dataTransfer.readHeadings(j, cell, this);
                        dataTransfer.readCurrentLine(j, cell, this);
                        k++;
                    }
                    dataTransfer.findHeadingPostitons(Headings, CurrentLine);
                    dataTransfer.transferLine(CurrentLine);
                    j++;
                    CurrentLine.Clear();
                }                 
            }

        }

        
    }
}
