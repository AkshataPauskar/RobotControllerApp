using NUnit.Framework;
using System.Linq;
using RobotControllerApp.Models;
using RobotControllerApp.Obstacles;
using RobotControllerApp.Robots;
using RobotControllerApp.Controller;

namespace RobotControllerApp.Tests
{
    [TestFixture]
    public class RobotControllerTests
    {
        private Grid _grid;
        private RobotController _controller;

        [SetUp]
        public void SetUp()
        {
            // Common setup logic before each test, like initializing the grid
            _grid = new Grid(5, 5);
        }

        // 1. Basic Navigation Test Case (No Obstacles)
        [Test]
        public void TestBasicNavigation()
        {
            IRobot robot = RobotFactory.CreateRobot("Basic", new Position(2, 2), Direction.North);
            _controller = new RobotController(robot, _grid);

            string commands = "FFFRF";
            _controller.NavigateCommands(commands);

            var path = _controller.GetPathNavigated();
            Assert.AreEqual(3, path.Last().XCoordinate);
            Assert.AreEqual(3, path.Last().YCoordinate);
        }

        // 2. Rock Obstacle Interaction Test Case
        [Test]
        public void TestRockObstacleInteraction()
        {
            IRobot robot = RobotFactory.CreateRobot("Basic", new Position(2, 2), Direction.North);
            _controller = new RobotController(robot, _grid);

            IObstacle rock = new RockObstacle(new Position(1, 3));
            _grid.SetObstacle(1, 3, rock);

            string commands = "FFFRF";
            _controller.NavigateCommands(commands);

            var path = _controller.GetPathNavigated();
            Assert.AreEqual(2, path.Last().XCoordinate);  // Robot doesn't move past the rock
            Assert.AreEqual(2, path.Last().YCoordinate);
        }

        // 3. Hole Obstacle Interaction Test Case
        [Test]
        public void TestHoleObstacleInteraction()
        {
            IRobot robot = RobotFactory.CreateRobot("Basic", new Position(1, 1), Direction.North);
            _controller = new RobotController(robot, _grid);

            IObstacle hole = new HoleObstacle(new Position(4, 4));
            _grid.SetObstacle(2, 2, hole);

            string commands = "FFFRF";
            _controller.NavigateCommands(commands);

            var path = _controller.GetPathNavigated();
            Assert.AreEqual(4, path.Last().XCoordinate); // Robot falls into the hole and lands at (4, 4)
            Assert.AreEqual(4, path.Last().YCoordinate);
        }

        // 4. Spinner Obstacle Interaction Test Case
        [Test]
        public void TestSpinnerObstacleInteraction()
        {
            IRobot robot = RobotFactory.CreateRobot("Basic", new Position(2, 2), Direction.North);
            _controller = new RobotController(robot, _grid);

            IObstacle spinner = new SpinnerObstacle(90);
            _grid.SetObstacle(3, 3, spinner);

            string commands = "FFFRF";
            _controller.NavigateCommands(commands);

            var path = _controller.GetPathNavigated();
            Assert.AreEqual(3, path.Last().XCoordinate);  // Robot rotates at spinner and ends up at (3, 3)
            Assert.AreEqual(3, path.Last().YCoordinate);
        }

        // 5. Boundary Movement Test Case
        [Test]
        public void TestBoundaryMovement()
        {
            IRobot robot = RobotFactory.CreateRobot("Basic", new Position(0, 0), Direction.North);
            _controller = new RobotController(robot, _grid);

            string commands = "F";
            _controller.NavigateCommands(commands);

            var path = _controller.GetPathNavigated();
            Assert.AreEqual(0, path.Last().XCoordinate);  // Robot cannot move out of bounds
            Assert.AreEqual(0, path.Last().YCoordinate);
        }

        // 6. Invalid Command Test Case
        [Test]
        public void TestInvalidCommands()
        {
            IRobot robot = RobotFactory.CreateRobot("Basic", new Position(2, 2), Direction.North);
            _controller = new RobotController(robot, _grid);

            string commands = "FLXYZFFR";
            _controller.NavigateCommands(commands);

            var path = _controller.GetPathNavigated();
            Assert.AreEqual(2, path.Last().XCoordinate);
            Assert.AreEqual(2, path.Last().YCoordinate);  // Robot should not have moved after invalid commands
        }

        // 7. Mixed Obstacles Test Case
        [Test]
        public void TestMixedObstacles()
        {
            IRobot robot = RobotFactory.CreateRobot("Basic", new Position(1, 1), Direction.North);
            _controller = new RobotController(robot, _grid);

            IObstacle rock = new RockObstacle(new Position(1, 3));
            IObstacle hole = new HoleObstacle(new Position(4, 4));
            IObstacle spinner = new SpinnerObstacle(90);

            _grid.SetObstacle(1, 3, rock);
            _grid.SetObstacle(2, 2, hole);
            _grid.SetObstacle(3, 3, spinner);

            string commands = "FFFRFFLFFFR";
            _controller.NavigateCommands(commands);

            var path = _controller.GetPathNavigated();
            Assert.AreEqual(4, path.Last().XCoordinate);  // Robot should interact with all obstacles
            Assert.AreEqual(4, path.Last().YCoordinate);
        }
    }
}
