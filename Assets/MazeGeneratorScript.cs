using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MazeGeneratorScript : MonoBehaviour {
	private GameObject coin;
	public int width = 5;
	public int height = 5;
	public Boolean debug = false;
	
	public float total_zoom_time = 2.0f;
	public int max_zoom_size = 20;
	public int min_zoom_size = 2;
	public float intro_time_delay = 5.0f;
	
	private static float golden_ratio = 1.618033988749894848f;
	
	private BrickMazeGeneratorClass maze_generator;
	
	private static readonly System.Random Random = new System.Random();
	
	private float intro_time_left;
	public Boolean intro_running;
	
	
	private UnityEngine.Camera camera1;
	private UnityEngine.Camera camera2;
	private GameObject player1;
	private GameObject player2;
	
	private Vector3 start;
	private Vector3 end;
	
	void Awake() {
		camera1 = GameObject.FindWithTag("Camera1").camera;
		camera2 = GameObject.FindWithTag("Camera2").camera;
		player1 = GameObject.FindWithTag("Player1");
		player2 = GameObject.FindWithTag("Player2");
		coin = GameObject.FindWithTag("Coin");
		maze_generator = new BrickMazeGeneratorClass(width, height);
		
		int[,] maze = maze_generator.to2DArray();
		
		render(maze);
			
		// zoom out to full map
		// zoom in to player
		start = new Vector3(((maze_generator.start_x*2)+1)*1.0f, 10f, -((maze_generator.start_y*2)+1)*1.0f);
		end = new Vector3(((maze_generator.end_x*2)+1)*1.0f, 10f, -((maze_generator.end_y*2)+1)*1.0f);
		intro_time_left = total_zoom_time;
		intro_running = true;
		camera1.transform.position = end;
		camera2.transform.position = end;
		
	}
	
	void Update() {
		if (intro_running) {
			if (intro_time_delay > 0.01f) {
				intro_time_delay -= Time.deltaTime;
			} else {
				intro_time_left -= Time.deltaTime;
				
				Vector3 point = Vector3.Lerp(start, end, (intro_time_left/total_zoom_time));
				//((end - start) * (intro_time_left/total_zoom_time)) + start;
				
				if (intro_time_left > (total_zoom_time/2)) {
					// zooming out
					camera1.transform.position = point;
					camera1.orthographicSize = ((Math.Abs((intro_time_left-total_zoom_time))*max_zoom_size)) + min_zoom_size;
						
					camera2.transform.position = point;
					camera2.orthographicSize = ((Math.Abs((intro_time_left-total_zoom_time))*max_zoom_size)) + min_zoom_size;
				} else if (intro_time_left > 0.01f) {
					// zooming back in
					camera1.transform.position = point;
					camera1.orthographicSize = (((intro_time_left)*max_zoom_size)) + min_zoom_size;
						
					camera2.transform.position = point;
					camera2.orthographicSize = (((intro_time_left)*max_zoom_size)) + min_zoom_size;
				} else {
					// done
					camera1.orthographicSize = 2;
					camera1.transform.position = start;
						
					camera2.orthographicSize = 2;
					camera2.transform.position = start;
					intro_running = false;
				}
			}
		}
	}
	
	void render(int[,] maze) {
		GameObject brick1 = GameObject.FindWithTag("Brick1");
		GameObject brick2 = GameObject.FindWithTag("Brick2");
		GameObject text = GameObject.FindWithTag("Text");
		GameObject floor = GameObject.FindWithTag("Floor");
		GameObject newText, startText, endText;
		List<Cell>  dead_ends;
		Cell dead_end;
		
		int width = maze.GetLength(0);
		int height = maze.GetLength(1);
		
		// scale the floor so it is big enough
		floor.transform.localScale = new Vector3(width, 1f, height);
		floor.renderer.material.mainTextureScale = new Vector2(width, height);
		floor.transform.position = new Vector3((width/2), -1f, -(height/2));
		
		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {			

				if (maze[x, y] == 1) { // wall
					if (Random.Next(2) == 1) {
						Instantiate(brick1, new Vector3(x*1.0f, 0f, -y*1.0f), Quaternion.identity);
					} else {
						Instantiate(brick2, new Vector3(x*1.0f, 0f, -y*1.0f), Quaternion.identity);
					}
				} else { // no wall
					if (debug) {
						newText = (GameObject)Instantiate(text, new Vector3(x*1.0f, 0f, -y*1.0f), Quaternion.identity);
						newText.transform.rotation = text.transform.rotation;
						newText.GetComponent<TextMesh>().text = "" + maze_generator.maze[(x-1)/2, (y-1)/2].distance_to_start;
					}
				}
				// put block at x, y if it is a 1	
			}
		}
		
		// mark start
		//startText = (GameObject)Instantiate(text, new Vector3(((maze_generator.start_x*2)+1)*1.0f, 0f, -((maze_generator.start_y*2)+1)*1.0f), text.transform.rotation);
		//startText.GetComponent<TextMesh>().text = "Start";
		// mark end
		//endText = (GameObject)Instantiate(text, new Vector3(((maze_generator.end_x*2)+1)*1.0f, 0f, -((maze_generator.end_y*2)+1)*1.0f), text.transform.rotation);
		//endText.GetComponent<TextMesh>().text = "End";
		
		// place coin
		coin.transform.position = new Vector3(((maze_generator.end_x*2)+1)*1.0f, 0f, -((maze_generator.end_y*2)+1)*1.0f);
		coin.GetComponent<CoinScript>().end = new Vector3((((width*2)+1)/2)*1f, 3f, -(((height*2)+1)/2)*1f);
		/*
		dead_ends = maze_generator.get_dead_ends();
		dead_ends = dead_ends.Shuffle();
		
		coins_left = ((width+height)/2) * golden_ratio; // that's right, the GOLDEN RATIO
		
		while(coins_left > 0) {
			dead_end = dead_ends[0];
			dead_ends.Remove(dead_end);
			
			
			
			coins_left--;
		}
		*/
	} 
}
