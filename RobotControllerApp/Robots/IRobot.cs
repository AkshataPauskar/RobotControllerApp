using RobotControllerApp.Models;
using RobotControllerApp.Obstacles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotControllerApp.Robots
{
    public interface IRobot
    {
        Position CurrentPosition { get; }
        Direction CurrentDirection { get; }
        void MoveForward();
        void TurnLeft();
        void TurnRight();
        void HandleObstacle(IObstacle obstacle, Grid grid);

    }
}
