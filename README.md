Robot Controller - A 2D Grid Navigation System
This repository contains the implementation of a Robot Controller that can navigate a 2D grid of known size. The robot is given a starting position and a series of commands to control its movement. The controller interacts with various obstacles placed on the grid, including rocks, holes, and spinners, each affecting the robot’s movement in different ways.

Features:
* Basic Movement: The robot can move forward, turn left, or turn right based on a series of commands (e.g., "LFFFRFFLFFFFRLFFFR").
* Obstacle Interactions:
  * Rock: The robot can't move onto a grid cell with a rock.
  * Hole: The robot falls into a hole and is transported to a connected grid location, maintaining its direction.
  * Spinner: The robot gets rotated by a given number of degrees (in 90-degree increments) when it encounters a spinner.
* Expandable: The system is designed to be easily extendable with new robots and new types of obstacles without recompiling the code.
* Path Tracking: The robot's path is logged and can be printed out, showing all the positions it visited during its traversal.

Usage:
* The robot starts at a specified grid position and direction.
* The user provides a sequence of commands (L, R, F) to control the robot.
* The robot responds to obstacles based on the type it encounters.
* After processing the commands, the final position of the robot and its traversal path are displayed.

Unit Tests:
* The repository includes unit tests to validate the robot's movement logic, obstacle interactions, and grid boundaries.

Future Work:
* Multiple Robots: In the future, support for different types of robots with varied responses to obstacles will be added.
* Additional Obstacles: More types of obstacles can be introduced with appropriate handling logic, such as moving obstacles, teleportation, or damage zones.

How to Run:
1. Clone the repository to your local machine.
2. Open the project in Visual Studio or your preferred C# IDE.
3. Build and run the console application to test robot navigation on a 2D grid.
