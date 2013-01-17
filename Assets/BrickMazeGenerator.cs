using System;
using System.Collections.Generic;

public class BrickMazeGeneratorClass
{
	public Cell[,] maze;
	private int[,] distance_to_start;
	public int start_x;
	public int start_y;
	public int end_x;
	public int end_y;

	public BrickMazeGeneratorClass (int width, int height, int start_x = 0, int start_y = 0)
	{
		this.start_x = start_x;
		this.start_y = start_y;
		
		// new maze with all walls
		maze = new Cell[width, height];
		for (int i = 0; i < width; i++)
			for (int j = 0; j < height; j++)
				maze[i, j] = new Cell(i, j);
		
		maze[start_x, start_y].distance_to_start = 0;
		
		// generate the maze
		generate_maze(maze[start_x, start_y]);
		
		// find longest path and mark it as the endpoint
		end_x = start_x;
		end_y = start_y;
		
		for (int x = 0; x < maze.GetLength(0); x++) {
			for (int y = 0; y < maze.GetLength(1); y++) {
				if (maze[x, y].distance_to_start > maze[end_x, end_y].distance_to_start) {
					end_x = x;
					end_y = y;
				}
			}
		}
		
		
	}

	private void generate_maze(Cell cell)
	{
		List<Cell> neighbors;
		Cell neighbor;

		cell.visited = true;

		neighbors = get_neighbors(cell);
		neighbors.Shuffle();

		for (int i = 0; i < neighbors.Count; i++) {
			neighbor = neighbors[i];
			if (!neighbor.visited) {
				cell.remove_wall(neighbor);
				neighbor.distance_to_start = cell.distance_to_start + 1;
				generate_maze(neighbor);
			}
		}
	}

	private List<Cell> get_neighbors (Cell cell)
	{
		List<Cell> neighbors = new List<Cell> ();

		if ((cell.x + 1) < maze.GetLength(0))
			neighbors.Add(maze[cell.x + 1, cell.y]);

		if ((cell.x - 1) >= 0)
			neighbors.Add(maze[cell.x-1, cell.y]);

		if ((cell.y + 1) < maze.GetLength(1))
			neighbors.Add(maze[cell.x, cell.y+1]);

		if ((cell.y - 1) >= 0)
			neighbors.Add(maze[cell.x, cell.y-1]);

		return neighbors;
	}
	
	public List<Cell> get_dead_ends() {
		List<Cell> dead_ends = new List<Cell>();
		
		// gives a list of dead ends
		for (int x = 0; x < maze.GetLength(0); x++) {
			for (int y = 0; y < maze.GetLength(1); y++) {
				if (maze[x, y].is_dead_end() && (x != start_x && y != start_y) && (x != end_x && y != end_y)) {
					dead_ends.Add (maze[x, y]);
				}
			}
		}
		
		return dead_ends;
	}
	
	public int[,] to2DArray() {
		int width = (maze.GetLength(0)*2) + 1;
		int length = (maze.GetLength(1)*2) + 1;
		
		int[,] array = new int[width, length];
		char[] array1d = ToString("").ToCharArray();
		
		for (int x = 0; x < width; x++) {
			for (int y = 0; y < length; y++) {
				if (array1d[(y*width)+x] == '#') {
					array[x, y] = 1;
				} else {
					array[x, y] = 0;
				}
			}
		}
		
		return array;
	}
	
	public override string ToString ()
	{
		return ToString("\n");
	}

	public string ToString (string terminator = "")
	{
		// print string representation
		int x, y;
		string theString = "#";

		for (x = 0; x < maze.GetLength(0); x++)
			theString += "##";

		theString += terminator;

		for (y = 0; y < maze.GetLength(1); y++) {
			theString += "#";
			for (x = 0; x < maze.GetLength(0); x++) {
				if (maze[x, y].east_wall) {
					theString += " #";
				} else {
					theString += "  ";
				}
			}
			theString += terminator;
			theString += "#";
			for (x = 0; x < maze.GetLength(0); x++) {
				if (maze[x, y].south_wall) {
					theString += "##";
				} else {
					theString += " #";
				}
			}
			theString += terminator;
		}

		return theString;
	}
}

static class MyExtensions
{
	static readonly Random Random = new Random();
	public static void Shuffle<T>(this IList<T> list)
	{
		int n = list.Count;
		while (n > 1)
		{
			n--;
			int k = Random.Next(n + 1);
			T value = list[k];
			list[k] = list[n];
			list[n] = value;
		}
	}
}
