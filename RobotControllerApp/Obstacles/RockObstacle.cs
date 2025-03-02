using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RobotControllerApp.Models;
using RobotControllerApp.Robots;

namespace RobotControllerApp.Obstacles
{
    public class RockObstacle : IObstacle
    {
        public Position ObstaclePosition { get; private set; }

        public RockObstacle(Position position)
        {
            ObstaclePosition = position;
        }
        public void HandleObstacle(IRobot robot, Grid grid)
        {
            Console.WriteLine($"Robot encountered a rock at the position {ObstaclePosition.XCoordinate},{ObstaclePosition.YCoordinate}. Robot can't move through the rock.");
        }
    }
}
