using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingsSorter
{
    class AddData
    {
        /// <summary>
        /// If project doesn't exist then it adds it as instance of the class Project to the list (projectList)
        /// </summary>
        /// <param name="processing"></param>
        internal void addProject(Processing processing, DataTransfer dataTransfer, TestExist testExist)
        {

            //tests if is commercial, adds a new instance and adds the project name
            if(dataTransfer.commercial)
            {
                processing.projectListC.Add(new CommercialProject(processing.CurrentLine[processing.headingPostitions.ProjectPosition], null));
                List<string> sublist = new List<string>() {null};
                processing.projectListC[dataTransfer.posProjectC].UseageList.Add(sublist);

            }
            else
            {
                processing.projectList.Add(new AcademicProject(processing.CurrentLine[processing.headingPostitions.ProjectPosition], null));
                List<string> sublist = new List<string>() { null };
                processing.projectList[dataTransfer.posProject].UseageList.Add(sublist);

            }

            //Adds User to list
            addUser(processing, dataTransfer);

            //Adds equipment to list
            addEquipment(processing, dataTransfer);

            //Adds hours
            addHours(processing, dataTransfer, testExist);

        }

        /// <summary>
        /// This creates a new list to add to the base list with the new user listed
        /// </summary>
        /// <param name="processing"></param>
        internal void addUser(Processing processing, DataTransfer dataTransfer)
        {

            if (dataTransfer.commercial) 
            { processing.projectListC[dataTransfer.posProjectC].UseageList[0].Add(processing.CurrentLine[processing.headingPostitions.LaserUserPosition]); }
            else { processing.projectList[dataTransfer.posProject].UseageList[0].Add(processing.CurrentLine[processing.headingPostitions.LaserUserPosition]); }
            
        }

        /// <summary>
        /// This adds the equipment to the first list  in the base list
        /// </summary>
        /// <param name="processing"></param>
        internal void addEquipment(Processing processing, DataTransfer dataTransfer)
        {
            //creates list
            List<string> sublistU = new List<string>();
            //adds the laser user to the list 
            sublistU.Add(processing.CurrentLine[processing.headingPostitions.EquipmentPosition]); 

            //adds the entry
            if (dataTransfer.commercial) { processing.projectListC[dataTransfer.posProjectC].UseageList.Add(sublistU); }
            else { processing.projectList[dataTransfer.posProject].UseageList.Add(sublistU); }
            
        }

        /// <summary>
        /// Adds the hours at the specified position by posUser and posEquipment
        /// </summary>
        /// <param name="processing"></param>
        internal void addHours(Processing processing, DataTransfer dataTransfer, TestExist testExist)
        {
            //calculates hours
            float hours = dataTransfer.hoursCalc(processing);

            //test if there is already an entry at posUser,posEquipment
            testExist.testHourPosExists(processing, dataTransfer);

            //adds the hours variable to that given point
            hours = dataTransfer.sumHours(processing, hours);

            //sets the given coordinate to the hours output
            if (dataTransfer.commercial)
            {
                processing.projectListC[dataTransfer.posProjectC].UseageList[dataTransfer.posEquipment][dataTransfer.posUser] = Convert.ToString(hours);
                processing.commercialHour = processing.commercialHour + hours;
            }
            else
            {
                processing.projectList[dataTransfer.posProject].UseageList[dataTransfer.posEquipment][dataTransfer.posUser] = Convert.ToString(hours);
                processing.academicHours = processing.academicHours + hours;
            }            
        }

        internal void addEquipmentList(Processing processing, DataTransfer dataTransfer)
        {
            processing.equipmentList.Add(new Equipment(null));
            processing.equipmentList[processing.equipmentList.Count - 1].Equipment[0] = processing.CurrentLine[processing.headingPostitions.EquipmentPosition];
        }

        internal void addEquipmentListHours(Processing processing, DataTransfer dataTransfer)
        {
            
        }
    }
}
