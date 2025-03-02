using RobotControllerApp.Models;
using RobotControllerApp.Obstacles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace RobotControllerApp.Robots
{
    public enum Direction
    {
        North,
        East,
        South,
        West
    }
    public abstract class RobotBase : IRobot
    {
        public Position CurrentPosition { get;  set; }
        public Direction CurrentDirection { get; protected set; }

        public RobotBase(Position currentPosition, Direction currentDirection)
        {
            CurrentPosition = currentPosition;
            CurrentDirection = currentDirection;
        }
        public void MoveForward()
        {
            switch (CurrentDirection)
            {
                case Direction.North:
                    CurrentPosition.YCoordinate--;
                    break;
                case Direction.South:
                    CurrentPosition.YCoordinate++;
                    break;
                case Direction.East:
                    CurrentPosition.XCoordinate++;
                    break;
                case Direction.West:
                    CurrentPosition.XCoordinate--;
                    break;

            }
        }
        public void TurnRight()
        {
            CurrentDirection = (Direction)(((int)CurrentDirection + 1) % 4);
        
        }
        public void TurnLeft()
        {
            CurrentDirection = (Direction)(((int)CurrentDirection + 3) % 4);
        }

        public abstract void HandleObstacle(IObstacle obstacle, Grid grid);
    }
}
