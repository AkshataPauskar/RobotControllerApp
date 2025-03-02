using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RobotControllerApp.Models;
using RobotControllerApp.Robots;

namespace RobotControllerApp.Obstacles
{
    public class SpinnerObstacle : IObstacle
    {
        private int rotationInDegrees;

        public SpinnerObstacle(int rotationInDegrees)
        {
            this.rotationInDegrees = rotationInDegrees;
        }

        public void HandleObstacle(IRobot robot, Grid grid)
        {
            int turnsRequired = (rotationInDegrees % 360) / 90 % 4;

            for (int i = 0; i < turnsRequired; i++)
            {
                robot.TurnRight();
            }
            Console.WriteLine("Robot spins by "+ rotationInDegrees + " degrees to the right");
        }
    }
}
