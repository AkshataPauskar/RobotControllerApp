using RobotControllerApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotControllerApp.Robots
{
    public static class RobotFactory
    {
        public static IRobot CreateRobot(string robotType, Position startPosition, Direction startDirection)
        {
            switch (robotType)
            {
                case "Basic":
                    return new BasicRobot(startPosition, startDirection);
                default:
                    throw new ArgumentException("Invalid robot type");
            }
        }
    }
}
