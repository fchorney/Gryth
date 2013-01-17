using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour { 
	public float move_speed = 1.0f;
	public float run_multiplier = 3.0f;
	
	public float left_right_threshold = 0.19f;
	
	public Texture up_texture;
	public Texture down_texture;
	public Texture left_texture;
	public Texture right_texture;
	
	private GameObject MazeGenerator;
		
	// Use this for initialization
	void Start () {
		MazeGenerator = GameObject.FindWithTag("MazeGenerator");
	}
		
	void FixedUpdate() {
		float my_move_speed = move_speed;
		Vector3 move_direction = new Vector3(0f,0f,0f);
		
		// only do input if intro isn't running :)
		if (!MazeGenerator.GetComponent<MazeGeneratorScript>().intro_running) {
			
			if (this.gameObject == GameObject.FindWithTag("Player1")) {
				if (Input.GetButton("Fire1-1")) {
					// while it is pressed
					my_move_speed = move_speed * run_multiplier;
				}
				
				move_direction = new Vector3(my_move_speed * Input.GetAxis("Horizontal-1"), 0, my_move_speed * Input.GetAxis ("Vertical-1"));
				
				if ((move_direction.x/my_move_speed) > left_right_threshold) {
					// right
					this.gameObject.renderer.material.mainTexture = right_texture;
				} else if ((move_direction.x/my_move_speed) < -left_right_threshold) {
					// left
					this.gameObject.renderer.material.mainTexture = left_texture;
				} else {
					this.gameObject.renderer.material.mainTexture = down_texture;
				}
			} else {
				if (Input.GetButton("Fire1-2")) {
					// while it is pressed
					my_move_speed = move_speed * run_multiplier;
				}
				
				move_direction = new Vector3(my_move_speed * Input.GetAxis("Horizontal-2"), 0, my_move_speed * Input.GetAxis ("Vertical-2"));
				
				if ((move_direction.x/my_move_speed) > left_right_threshold) {
					// right
					this.gameObject.renderer.material.mainTexture = right_texture;
				} else if ((move_direction.x/my_move_speed) < -left_right_threshold) {
					// left
					this.gameObject.renderer.material.mainTexture = left_texture;
				} else {
					this.gameObject.renderer.material.mainTexture = down_texture;
				}
			}
		}
		
		this.gameObject.rigidbody.velocity = move_direction;

	}
	
	// Update is called once per frame
	void Update () {
		/*
		if (Input.GetButtonDown("Fire1")) {
			// button pressed
			foreach (GameObject go in GameObject.FindGameObjectsWithTag("StringTrail")) {
				StringTrail st = (StringTrail)go.GetComponent("StringTrail");
				st.follow = false;
			}
		}
		
		if (Input.GetButtonUp("Fire1")) {
			GameObject go = (GameObject)Instantiate(GameObject.FindWithTag("StringTrail"), transform.position, transform.rotation);
			((StringTrail)go.GetComponent("StringTrail")).follow = true;
			//StringTrail st = (StringTrail)go;
			//st.follow = true;
		}
		*/
	}
}
