using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RobotControllerApp.Models;
using RobotControllerApp.Robots;

namespace RobotControllerApp.Obstacles
{
    public interface IObstacle
    {
        void HandleObstacle(IRobot robot, Grid grid);
    }
}
