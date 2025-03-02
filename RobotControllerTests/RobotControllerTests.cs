using NUnit.Framework;
using RobotControllerApp.Controller;
using RobotControllerApp.Models;
using RobotControllerApp.Obstacles;
using RobotControllerApp.Robots;
using System.Linq;

namespace RobotControllerApp.Tests
{
    [TestFixture]
    public class RobotControllerTests
    {
        private Grid _grid;
        private RobotController? _controller;

        [SetUp]
        public void SetUp()
        {
            _grid = new Grid(5, 5);
        }

        // 1. Basic Navigation Test Case (No Obstacles)
        [Test]
        public void Test_Basic_Navigation()
        {
            IRobot robot = RobotFactory.CreateRobot("Basic", new Position(2, 2), Direction.North);
            _controller = new RobotController(robot, _grid);

            string commands = "FFRF";
            _controller.NavigateCommands(commands);

            var path = _controller.GetPathNavigated();
            Assert.That(path.Last().XCoordinate, Is.EqualTo(3));
            Assert.That(path.Last().YCoordinate, Is.EqualTo(0));
        }

        // 2. Rock Obstacle Interaction Test Case
        [Test]
        public void Test_RockObstacle_Interaction()
        {
            IRobot robot = RobotFactory.CreateRobot("Basic", new Position(2, 2), Direction.South);
            _controller = new RobotController(robot, _grid);

            IObstacle rock = new RockObstacle(new Position(1, 3));
            _grid.SetObstacle(1, 3, rock);

            string commands = "FRFRF";
            _controller.NavigateCommands(commands);

            var path = _controller.GetPathNavigated();
            Assert.That(path.Last().XCoordinate, Is.EqualTo(2)); 
            Assert.That(path.Last().YCoordinate, Is.EqualTo(2));
        }

        // 3. Hole Obstacle Interaction Test Case
        [Test]
        public void Test_HoleObstacle_Interaction()
        {
            IRobot robot = RobotFactory.CreateRobot("Basic", new Position(1, 1), Direction.South);
            _controller = new RobotController(robot, _grid);

            IObstacle hole = new HoleObstacle(new Position(4, 4));
            _grid.SetObstacle(2, 2, hole);

            string commands = "FLFLF";
            _controller.NavigateCommands(commands);

            var path = _controller.GetPathNavigated();
            Assert.That(path.Last().XCoordinate, Is.EqualTo(4));
            Assert.That(path.Last().YCoordinate, Is.EqualTo(3));
        }

        // 4. Spinner Obstacle Interaction Test Case
        [Test]
        public void Test_SpinnerObstacle_Interaction()
        {
            IRobot robot = RobotFactory.CreateRobot("Basic", new Position(2, 2), Direction.East);
            _controller = new RobotController(robot, _grid);

            IObstacle spinner = new SpinnerObstacle(90);
            _grid.SetObstacle(3, 3, spinner);

            string commands = "FFRFRF";
            _controller.NavigateCommands(commands);

            var path = _controller.GetPathNavigated();
            Assert.That(path.Last().XCoordinate, Is.EqualTo(4));  
            Assert.That(path.Last().YCoordinate, Is.EqualTo(3));
            Assert.That(robot.CurrentDirection, Is.EqualTo(Direction.North));// Robot rotates at spinner and ends up at (3, 3) with direction to North
        }

        // 5. Boundary Movement Test Case
        [Test]
        public void Test_Boundary_Movement()
        {
            IRobot robot = RobotFactory.CreateRobot("Basic", new Position(0, 0), Direction.North);
            _controller = new RobotController(robot, _grid);

            string commands = "F";
            _controller.NavigateCommands(commands);

            var path = _controller.GetPathNavigated();
            Assert.That(path.Last().XCoordinate, Is.EqualTo(0));  // Robot cannot move out of bounds
            Assert.That(path.Last().YCoordinate, Is.EqualTo(0));
        }

        // 6. Invalid Command Test Case
        [Test]
        public void Test_Invalid_Commands()
        {
            IRobot robot = RobotFactory.CreateRobot("Basic", new Position(2, 2), Direction.North);
            _controller = new RobotController(robot, _grid);

            string commands = "FLXYZFFR";
            _controller.NavigateCommands(commands);

            var path = _controller.GetPathNavigated();
            Assert.That(path.Last().XCoordinate, Is.EqualTo(0));
            Assert.That(path.Last().YCoordinate, Is.EqualTo(1));  // Robot should not have moved after invalid commands
        }

        // 7. Mixed Obstacles Test Case
        [Test]
        public void Test_Mixed_Obstacles()
        {
            IRobot robot = RobotFactory.CreateRobot("Basic", new Position(1, 1), Direction.West);
            _controller = new RobotController(robot, _grid);

            IObstacle rock = new RockObstacle(new Position(1, 3));
            IObstacle hole = new HoleObstacle(new Position(4, 4));
            IObstacle spinner = new SpinnerObstacle(180);

            _grid.SetObstacle(1, 3, rock);
            _grid.SetObstacle(2, 2, hole);
            _grid.SetObstacle(3, 3, spinner);

            string commands = "FLFFLFFLFRFFLLFFRFRFFLF";
            _controller.NavigateCommands(commands);

            var path = _controller.GetPathNavigated();
            Assert.That(path.Last().XCoordinate, Is.EqualTo(2));  
            Assert.That(path.Last().YCoordinate, Is.EqualTo(4));
            Assert.That(robot.CurrentDirection, Is.EqualTo(Direction.South));
        }

        // 8. Robot Path and Final Position Test Case
        [Test]
        public void Test_Robot_Path_And_Final_Position()
        {
            IRobot robot = RobotFactory.CreateRobot("Basic", new Position(0,0), Direction.South);
            _controller = new RobotController(robot, _grid);

            string commands = "FFFLFF";
            _controller.NavigateCommands(commands);

            var path = _controller.GetPathNavigated();

            Assert.That(path.Last().XCoordinate, Is.EqualTo(2));
            Assert.That(path.Last().YCoordinate, Is.EqualTo(3));

            Assert.That(path.Count(),Is.EqualTo(6));

            Assert.That(path[0].XCoordinate, Is.EqualTo(0));
            Assert.That(path[0].YCoordinate, Is.EqualTo(0));

            Assert.That(path[1].XCoordinate, Is.EqualTo(0));
            Assert.That(path[1].YCoordinate, Is.EqualTo(1));

            Assert.That(path[2].XCoordinate, Is.EqualTo(0));
            Assert.That(path[2].YCoordinate, Is.EqualTo(2));

            Assert.That(path[3].XCoordinate, Is.EqualTo(0));
            Assert.That(path[3].YCoordinate, Is.EqualTo(3));

            Assert.That(path[4].XCoordinate, Is.EqualTo(1));
            Assert.That(path[4].YCoordinate, Is.EqualTo(3));

            Assert.That(path[5].XCoordinate, Is.EqualTo(2));
            Assert.That(path[5].YCoordinate, Is.EqualTo(3));
        }

    }
}
