using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingsSorter
{
    /// <summary>
    /// Class to store the heading positions of all the data in the excel document
    /// </summary>
    public class HeadingPostitions
    {
        public int ProjectPosition{get; set;}
        public int EquipmentPosition{get; set;}        
        public int StartPosition{get; set;}
        public int FinishPosition{get; set;}
        public int LaserUserPosition{get; set;}
        public int CommercialPosition{get; set;}

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="projectPosition"></param>
        /// <param name="equipmentPosition"></param>
        /// <param name="startPosition"></param>
        /// <param name="finishPosition"></param>
        /// <param name="laserUserPosition"></param>
        /// <param name="commercialPosition"></param>
        public HeadingPostitions(int projectPosition, int equipmentPosition, int startPosition, int finishPosition, int laserUserPosition, int commercialPosition)
        {
            ProjectPosition = projectPosition;
            EquipmentPosition = equipmentPosition;
            StartPosition = startPosition;
            FinishPosition = finishPosition;
            LaserUserPosition = laserUserPosition;
            CommercialPosition = commercialPosition;
            
        }

    }
}
