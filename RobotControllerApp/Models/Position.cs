using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotControllerApp.Models
{
    public class Position
    {
        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }

        public Position(int xCoordinate, int yCoordinate)
        {
            XCoordinate = xCoordinate;
            YCoordinate = yCoordinate;
        }

    }

}
