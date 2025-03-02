using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RobotControllerApp.Models;
using RobotControllerApp.Obstacles;
using RobotControllerApp.Robots;

namespace RobotControllerApp.Controller
{
    public class RobotController
    {
        private IRobot robot;
        private Grid grid;
        private List<Position> pathNavigated = new List<Position>();
        public RobotController(IRobot robot, Grid grid)
        {
            this.robot = robot;
            this.grid = grid;
        }
        private bool IsValidCommand(char command)
        {
            return command == 'L' || command == 'R' || command == 'F';
        }
        public void NavigateCommands(string commands)
        {
            pathNavigated.Add(new Position(robot.CurrentPosition.XCoordinate, robot.CurrentPosition.YCoordinate));
            foreach (var command in commands)
            {
                char normalizedCommand = Char.ToUpper(command);
                if (!IsValidCommand(normalizedCommand))
                {
                    Console.WriteLine($"Warning: Invalid command '{command}' encountered. Skipping it. " +
                        $"Valid commands are 'L' for Left, 'R' for Right, and 'F' for Forward.\n");
                    continue;
                }
        

                switch (normalizedCommand)
                {
                    case 'L':
                        robot.TurnLeft();
                        break;
                    case 'R':
                        robot.TurnRight();
                        break;
                    case 'F':
                        int nextX = robot.CurrentPosition.XCoordinate;
                        int nextY = robot.CurrentPosition.YCoordinate;
                        switch (robot.CurrentDirection)
                        {
                            case Direction.North:
                                nextY--;
                                break;
                            case Direction.East:
                                nextX++;
                                break;
                            case Direction.South:
                                nextY++;
                                break;
                            case Direction.West:
                                nextX--;
                                break;
                        }
                        if (grid.IsWithinBonds(nextX, nextY))
                        {
                            IObstacle obstacle = grid.GetObstacle(nextX, nextY);
                            if (obstacle != null)
                            {
                                robot.HandleObstacle(obstacle, grid);
                                if(obstacle is HoleObstacle)
                                {
                                    pathNavigated.Add(new Position(robot.CurrentPosition.XCoordinate, robot.CurrentPosition.YCoordinate));
                                }
                            }
                            else
                            {
                                robot.MoveForward();
                                pathNavigated.Add(new Position(robot.CurrentPosition.XCoordinate, robot.CurrentPosition.YCoordinate));
                            }
                        }
                        break;
                    default:
                        Console.WriteLine($"Warning: Invalid command '{command}' encountered. Skipping...\n");
                        break;
                }
            }
        }
        public List<Position> GetPathNavigated()
        {
            return pathNavigated;
        }
    }
}
