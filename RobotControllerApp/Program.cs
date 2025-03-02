using RobotControllerApp.Controller;
using RobotControllerApp.Models;
using RobotControllerApp.Obstacles;
using RobotControllerApp.Robots;

public class Program() { 
    public static void Main(string[] args)
    {
        Console.WriteLine("-------------------------------Robot Controller App--------------------------------\n");

        Grid grid = new Grid(5,5);
        //RobotBase robot = new RobotBase(new Position(4,0),Direction.North);
        IRobot robot = RobotFactory.CreateRobot("Basic", new Position(4, 0), Direction.East);
        RobotController controller = new RobotController(robot,grid);

        IObstacle rock = new RockObstacle(new Position(1, 3));
        grid.SetObstacle(1, 3, rock);
        grid.SetObstacle(4, 2, new HoleObstacle(new Position(2, 3)));
        grid.SetObstacle(3, 3, new SpinnerObstacle(90));

        string commands = "RFFFLFFLFLFFFLFLFFRFRFFRFF";
        controller.NavigateCommands(commands);

        var path = controller.GetPathNavigated();
        Console.WriteLine("\nFinal Position: " + path.Last().XCoordinate + "," + path.Last().YCoordinate);
        Console.WriteLine("Path: ");
        foreach(var position in path)
        {
            Console.WriteLine(position.XCoordinate + "," + position.YCoordinate);
        }
    }
}