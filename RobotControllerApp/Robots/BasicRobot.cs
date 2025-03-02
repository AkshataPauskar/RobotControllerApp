using RobotControllerApp.Models;
using RobotControllerApp.Obstacles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotControllerApp.Robots
{
    public class BasicRobot : RobotBase
    {
        public BasicRobot(Position startPosition, Direction startDirection)
            : base(startPosition, startDirection) { }

        public override void HandleObstacle(IObstacle obstacle, Grid grid)
        {
            obstacle.HandleObstacle(this, grid);
        }

    }
}
