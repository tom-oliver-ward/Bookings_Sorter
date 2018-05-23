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
            //tests if is commercail, adds a new instance and adds the project name
            if(dataTransfer.commercial)
            {
                processing.projectListC.Add(null);
                processing.projectListC[processing.projectList.Count-1].ProjectName = processing.CurrentLine[processing.headingPostitions.ProjectPosition];
            }
            else
            {
                processing.projectList.Add(new AcademicProject(processing.CurrentLine[processing.headingPostitions.ProjectPosition], null));
                //processing.projectList[processing.projectList.Count-1].ProjectName = processing.CurrentLine[processing.headingPostitions.ProjectPosition];
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
            //creates list
            List<string> sublistU = new List<string>();
            //adds the laser user to the list 
            sublistU.Add(processing.CurrentLine[processing.headingPostitions.LaserUserPosition]);
            //adds a null point as each sub list will be at least 2 points long
            sublistU.Add(null);
            //adds the list to the base list
            if (dataTransfer.commercial) { processing.projectListC[dataTransfer.posProject].UseageList.Add(sublistU); }
            else { processing.projectList[dataTransfer.posProject].UseageList.Add(sublistU); }
            
        }

        /// <summary>
        /// This adds the equipment to the first list  in the base list
        /// </summary>
        /// <param name="processing"></param>
        internal void addEquipment(Processing processing, DataTransfer dataTransfer)
        {
            //adds the entry
            if (dataTransfer.commercial) { processing.projectListC[dataTransfer.posProject].UseageList[0].Add(processing.CurrentLine[processing.headingPostitions.EquipmentPosition]); }
            else { processing.projectList[dataTransfer.posProject].UseageList[0].Add(processing.CurrentLine[processing.headingPostitions.EquipmentPosition]); }
            
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
                processing.projectListC[dataTransfer.posProject].UseageList[dataTransfer.posUser][dataTransfer.posEquipment] = Convert.ToString(hours);
                processing.commercialHour = processing.commercialHour + hours;
            }
            else
            {
                processing.projectList[dataTransfer.posProject].UseageList[dataTransfer.posUser][dataTransfer.posEquipment] = Convert.ToString(hours);
                processing.academicHours = processing.academicHours + hours;
            }            
        }
    }
}
