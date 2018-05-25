using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingsSorter
{
    class TestExist
    {
        /// <summary>
        /// Tests if the project already exists
        /// </summary>
        /// <param name="processing"></param>
        internal void testExistingProject(Processing processing, DataTransfer dataTransfer)
        {

            //tests whether project is commercial or not            
            if (processing.CurrentLine[processing.headingPostitions.CommercialPosition] == "Commercial") { dataTransfer.commercial = true; }
            else { dataTransfer.commercial = false; }

            if (dataTransfer.commercial)
            {
                for (int i = 0; i < processing.projectListC.Count; i++)
                {
                    if (processing.CurrentLine[processing.headingPostitions.ProjectPosition] == processing.projectListC[i].ProjectName)
                    {
                        dataTransfer.add = false;
                        dataTransfer.posProjectC = i;
                    }
                }
            }
            else
            {
                for (int i = 0; i < processing.projectList.Count; i++)
                {
                    if (processing.CurrentLine[processing.headingPostitions.ProjectPosition] == processing.projectList[i].ProjectName)
                    {
                        dataTransfer.add = false;
                        dataTransfer.posProject = i;
                    }
                }
            }
        }

        /// <summary>
        /// Tests if a given equipment entry exists
        /// </summary>
        /// <param name="processing"></param>
        internal void testExistingEquipment(Processing processing, DataTransfer dataTransfer)
        {
            if (dataTransfer.commercial)
            {
                for (int i = 0; i < processing.projectListC[dataTransfer.posProjectC].UseageList.Count; i++)
                {
                    //tests whether for a match, if so sets addE and stores Equipment position
                    if (processing.CurrentLine[processing.headingPostitions.EquipmentPosition] == processing.projectListC[dataTransfer.posProjectC].UseageList[i][0])
                    {
                        dataTransfer.addE = false;
                        dataTransfer.posEquipment = i;
                    }
                }
                if (dataTransfer.addE)
                {
                    dataTransfer.posEquipment = processing.projectListC[dataTransfer.posProjectC].UseageList.Count-1;
                }
            }
            else
            {             //iterates through the existing list matrix
                for (int i = 0; i < processing.projectList[dataTransfer.posProject].UseageList.Count; i++)
                {
                    //tests whether for a match, if so sets addE and stores Equipment position
                    if (processing.CurrentLine[processing.headingPostitions.EquipmentPosition] == processing.projectList[dataTransfer.posProject].UseageList[i][0])
                    {
                        dataTransfer.addE = false;
                        dataTransfer.posEquipment = i;
                    }
                }
                if (dataTransfer.addE)
                {
                    dataTransfer.posEquipment = processing.projectList[dataTransfer.posProject].UseageList.Count-1;
                }
            }

        }

        /// <summary>
        /// Tests if given user entry already exists
        /// </summary>
        /// <param name="processing"></param>
        internal void testExistingUser(Processing processing, DataTransfer dataTransfer)
        {
            if (dataTransfer.commercial)
            {
                //iterates through the existing list matrix
                for (int i = 0; i < processing.projectListC[dataTransfer.posProjectC].UseageList[0].Count; i++)
                {
                    //tests whether for a match, if so sets addU and stores user position
                    if (processing.CurrentLine[processing.headingPostitions.LaserUserPosition] == processing.projectListC[dataTransfer.posProjectC].UseageList[0][i])
                    {
                        dataTransfer.addU = false;
                        dataTransfer.posUser = i;
                    }
                }
                if (dataTransfer.addU)
                {
                    dataTransfer.posUser = processing.projectListC[dataTransfer.posProjectC].UseageList[0].Count-1;
                }
            }
            else
            {
                //iterates through the existing list matrix
                for (int i = 0; i < processing.projectList[dataTransfer.posProject].UseageList[0].Count; i++)
                {
                    //tests whether for a match, if so sets addU and stores user position
                    if (processing.CurrentLine[processing.headingPostitions.LaserUserPosition] == processing.projectList[dataTransfer.posProject].UseageList[0][i])
                    {
                        dataTransfer.addU = false;
                        dataTransfer.posUser = i;
                    }
                }
                if (dataTransfer.addU)
                {
                    dataTransfer.posUser = processing.projectList[dataTransfer.posProject].UseageList[0].Count-1;
                }
            }

        }

        //tests whether the co-ordinate defined by posUser,PosEquipment exists, if not extends required list to that length
        internal void testHourPosExists(Processing processing, DataTransfer dataTransfer)
        {
            if (dataTransfer.commercial)
            {
                //if long enough, return
                if (dataTransfer.posUser <= processing.projectListC[dataTransfer.posProjectC].UseageList[dataTransfer.posEquipment].Count - 1) { return; }
                //else add null until it is long enough
                else
                {
                    while (dataTransfer.posUser > processing.projectListC[dataTransfer.posProjectC].UseageList[dataTransfer.posEquipment].Count - 1)
                    {
                        processing.projectListC[dataTransfer.posProjectC].UseageList[dataTransfer.posEquipment].Add(null);
                    }
                }
            }
            else
            {
                //if long enough, return
                if (dataTransfer.posUser <= processing.projectList[dataTransfer.posProject].UseageList[dataTransfer.posEquipment].Count - 1) { return; }
                //else add null until it is long enough
                else
                {
                    while (dataTransfer.posUser > processing.projectList[dataTransfer.posProject].UseageList[dataTransfer.posEquipment].Count - 1)
                    {
                        processing.projectList[dataTransfer.posProject].UseageList[dataTransfer.posEquipment].Add(null);
                    }
                }
            }
            
        }

    }
}
