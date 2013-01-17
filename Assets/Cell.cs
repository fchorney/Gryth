using System;

public class Cell
{
	public Boolean visited = false;
	public int x;
	public int y;
	public int distance_to_start = -1;

	public Boolean north_wall = true;
	public Boolean east_wall = true;
	public Boolean south_wall = true;
	public Boolean west_wall = true;

	public Cell (int x, int y)
	{
		this.x = x;
		this.y = y;
	}
	
	public Boolean is_dead_end() {
		int num_walls = 0;
		
		if (north_wall)
			num_walls++;
		
		if (east_wall)
			num_walls++;
		
		if (south_wall)
			num_walls++;
		
		if (west_wall)
			num_walls++;
		
		if (num_walls == 3)
			return true;
		
		return false;
	}
	
	public Boolean wall_exists_to(Cell neighbor)
	{
		if (neighbor.y == (y - 1))
			if (north_wall == true)
				return true;
		
		if (neighbor.y == (y + 1)) 
			if (south_wall == true)
				return true;
		
		if (neighbor.x == (x + 1))
			if (east_wall == true)
				return true;
		
		if (neighbor.x == (x - 1))
			if (west_wall == true)
				return true;
		
		return false;
	}

	public void remove_wall (Cell neighbor)
	{
		if (neighbor.y == (y - 1)) {
			north_wall = false;
			neighbor.south_wall = false;
		}
		if (neighbor.y == (y + 1)) {
			south_wall = false;
			neighbor.north_wall = false;
		}
		if (neighbor.x == (x + 1)) {
			east_wall = false;
			neighbor.west_wall = false;
		}
		if (neighbor.x == (x - 1)) {
			west_wall = false;
			neighbor.east_wall = false;
		}
	}
	public string info ()
	{
		return "Cell[" + x + ", " + y + "]"; //, " + visited + ", " + north_wall + ", " + east_wall + ", " + south_wall + ", " + west_wall + "]";
	}
}

