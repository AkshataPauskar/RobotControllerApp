using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RobotControllerApp.Obstacles;

namespace RobotControllerApp.Models
{
    public class Grid
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        private IObstacle[,] grid;

        public Grid(int width, int height)
        {
            Width = width;
            Height = height;
            grid = new IObstacle[Width, Height];
        }
        private bool IsValidPosition(int x, int y)
        {
            if (x < 0 || x >= Width || y < 0 || y >= Height)
            {
                Console.WriteLine($"Error: Position ({x},{y}) is outside the grid boundaries.");
                Console.WriteLine($"Grid size: {Width}x{Height}. Valid coordinates range from (0,0) to ({Width - 1},{Height - 1}).");
                Console.WriteLine("Please choose a position within the valid range.\n");
                return false;
            }

            if (grid[x, y] != null)
            {
                Console.WriteLine($"Error: Position ({x},{y}) is already occupied by an obstacle.\n");
                return false;
            }
            return true;
        }
        public void SetObstacle(int x, int y, IObstacle obstacle)
        {
            if (IsValidPosition(x, y))
            {
                grid[x, y] = obstacle;
            }
        }

        public IObstacle GetObstacle(int x, int y)
        {
            return grid[x, y];
        }

        public bool IsWithinBonds(int x, int y)
        {
            return x >= 0 && y >= 0 && x < Width && y < Height;
        }
    }
}
