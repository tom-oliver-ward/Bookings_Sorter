using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace BookingsSorter
{
    public partial class Form1 : Form
    {
        //initiallises an instance of Processing
        Processing processing = new Processing();        
        //initialises the file location variable
        public string file;       

        public Form1()
        {
            InitializeComponent();

            //initialises drag drop handler
            this.SpreadSheets2Sort.DragDrop += new
            System.Windows.Forms.DragEventHandler(this.SpreadSheets2Sort_DragDrop);
            this.SpreadSheets2Sort.DragEnter += new
            System.Windows.Forms.DragEventHandler(this.SpreadSheets2Sort_DragEnter);

            //starts convert button as disabled until the matrix file has been selected
            buttonSort.Enabled = false;
            buttonOutputLoc.Enabled = false;
        }

        /// <summary>
        /// Drag Drop handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SpreadSheets2Sort_DragDrop(object sender, DragEventArgs e)
        {
            //Adds items to the list
            string[] s = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            int i;
            for (i = 0; i < s.Length; i++)
                SpreadSheets2Sort.Items.Add(s[i]);
        }

        /// <summary>
        /// Drag enter handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SpreadSheets2Sort_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.None;
        }
        
        //reads and sorts spreadsheet     
        private void buttonSort_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(textBoxOutputLoc.Text))
            {
                processing.readCSV(this);
            }
            else
            {
                MessageBox.Show("Invalid Folder");
            }
            
        }

        /// <summary>
        /// Opens the dialog box to select the file location for the output file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOutputLoc_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBoxOutputLoc.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void textBoxOutputLoc_TextChanged(object sender, EventArgs e)
        {
            buttonSort.Enabled = true;            
        }

        private void textBox_Period_TextChanged(object sender, EventArgs e)
        {
            buttonOutputLoc.Enabled = true;
            textBoxOutputLoc.ReadOnly = false;
        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }

        private void button_Clear_Click_1(object sender, EventArgs e)
        {
            SpreadSheets2Sort.Items.Clear();
            processing.CurrentLine.Clear();
            processing.headingsRead = false;
            processing.projectList.Clear();
            processing.projectListC.Clear();
            processing.equipmentList.Clear();
            processing.academicHours = 0;
            processing.commercialHour = 0;            
        }
    }
}
