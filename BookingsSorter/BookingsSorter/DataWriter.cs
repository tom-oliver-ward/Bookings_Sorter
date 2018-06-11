using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingsSorter
{
    class DataWriter
    {
        /// <summary>
        /// initialises stream, then calls functions to write the different project sections
        /// </summary>
        /// <param name="processing"></param>
        /// <param name="form1"></param>
        internal void initialiseStream(Processing processing, Form1 form1)
        {
            //initialises the file path to save the processed file
            string filePath = form1.textBoxOutputLoc.Text + "\\Photon Factory Useage " + form1.textBox_Period.Text + ".csv";
            using (StreamWriter output = new StreamWriter(filePath))
            {
                //writes the header
                header(form1, output);

                //write the number of hours either academic or commercial
                academicCommercial(processing, output);

                //writes out the academic projects
                academicProjectsDW(processing, output);

                //writes out the commercial projects
                commercialProjectDW(processing, output);
            }
        }

        /// <summary>
        /// Writes out the commercial projects in the same grid as they are saved
        /// </summary>
        /// <param name="processing"></param>
        /// <param name="output"></param>
        private void commercialProjectDW(Processing processing, StreamWriter output)
        {
            output.WriteLine();
            output.WriteLine("***COMMERCIAL PROJECTS***");
            output.WriteLine();

            foreach (var project in processing.projectListC)
            {
                string projectName = "---" + project.ProjectName + "---,";
                output.WriteLine(projectName);

                foreach (var row in project.UseageList)
                {
                    foreach (var column in row)
                    {
                        output.Write(column + ",");
                    }
                    output.WriteLine();
                   
                }
                output.WriteLine();
            }
        }

        /// <summary>
        /// Writes out the academic projects in the same grid as they are saved
        /// </summary>
        /// <param name="processing"></param>
        /// <param name="output"></param>
        private void academicProjectsDW(Processing processing, StreamWriter output)
        {
            output.WriteLine();
            output.WriteLine("***ACADEMIC PROJECTS***");
            output.WriteLine();

            foreach (var project in processing.projectList)
            {
                string projectName = "---" + project.ProjectName + "---,";
                output.WriteLine(projectName);

                foreach (var row in project.UseageList)
                {
                    foreach (var column in row)
                    {
                        output.Write(column + ",");
                    }
                    output.WriteLine();                    
                }
                output.WriteLine();

            }


        }

        /// <summary>
        /// writes out the number of hours and percent for academic and commercial
        /// </summary>
        /// <param name="processing"></param>
        /// <param name="output"></param>
        private void academicCommercial(Processing processing, StreamWriter output)
        {
            //calculates and writes total hours
            output.WriteLine();
            float totalHours = processing.academicHours + processing.commercialHour;
            string totalHoursS = "Total Hours," + Convert.ToString(totalHours);
            output.WriteLine(totalHoursS);

            //calculates and writes academic hours and %
            string academicHoursS = "Academic Hours," + Convert.ToString(processing.academicHours);
            float academicHoursPercent = (processing.academicHours / totalHours)*100;
            output.WriteLine(academicHoursS + "," + academicHoursPercent + "%");

            //calculates and writes commercial hours and %
            string commercialHoursS = "Commercial Hours," + Convert.ToString(processing.commercialHour);
            float commercialHoursPercent = (processing.commercialHour / totalHours)*100;
            output.WriteLine(commercialHoursS + "," + commercialHoursPercent + "%");            

            output.WriteLine();
            output.WriteLine("***");
        }

        /// <summary>
        /// writes standard header
        /// </summary>
        /// <param name="form1"></param>
        /// <param name="output"></param>
        private void header(Form1 form1, StreamWriter output)
        {
            output.WriteLine("Photon Factory Useage Summary");
            output.WriteLine(form1.textBox_Period.Text);
            output.WriteLine();
            output.WriteLine("***");
        }
    }
}
