using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RobotControllerApp.Models;
using RobotControllerApp.Robots;

namespace RobotControllerApp.Obstacles
{
    public class HoleObstacle : IObstacle
    {
        private Position ConnectedLocation { get; set; }

        public HoleObstacle(Position connectedLocation)
        {
            ConnectedLocation = connectedLocation;
        }

        public void HandleObstacle(IRobot robot, Grid grid)
        {
            robot.CurrentPosition.XCoordinate = ConnectedLocation.XCoordinate;
            robot.CurrentPosition.YCoordinate = ConnectedLocation.YCoordinate;
            Console.WriteLine($"Robot falls into the hole and current updated position of the Robot is {robot.CurrentPosition.XCoordinate},{robot.CurrentPosition.YCoordinate}");
        }
    }
}
