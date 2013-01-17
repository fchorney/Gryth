using UnityEngine;
using System.Collections;
using System;

public class CoinScript : MonoBehaviour {
	private GameObject player1;
	private GameObject player2;
	private GameObject win_text;
	private Boolean winning = false;
	private Boolean won = false;
	
	public float zoom_time = 1.0f;
	public float zoom_size = 20f;
	public float min_zoom_size = 2f;
	public float zoom_time_left;
	
	private Vector3 start;
	public Vector3 end;
	
	private UnityEngine.Camera camera1;
	private UnityEngine.Camera camera2;

	// Use this for initialization
	void Start () {
		player1 = GameObject.FindWithTag("Player1");
		player2 = GameObject.FindWithTag("Player2");
		win_text = GameObject.FindWithTag("WinText");
		camera1 = GameObject.FindWithTag("Camera1").camera;
		camera2 = GameObject.FindWithTag("Camera2").camera;
	}
	
	// Update is called once per frame
	void Update () {
		if (winning) {
			if (zoom_time_left > 0.1f) {
				zoom_time_left -= Time.deltaTime;	
			} else {
				winning = false;
				won = true;
			}
			camera1.transform.position = Vector3.Lerp(start, end, (zoom_time_left/zoom_time));
			camera2.transform.position = Vector3.Lerp(start, end, (zoom_time_left/zoom_time));
			
			camera1.orthographicSize = ((Math.Abs((zoom_time_left-zoom_time))*zoom_size)) + min_zoom_size;
			camera2.orthographicSize = ((Math.Abs((zoom_time_left-zoom_time))*zoom_size)) + min_zoom_size;
		}
		
		if (won) {
			if (Input.GetButtonDown("Fire1-1") || Input.GetButtonDown("Fire1-2")) {
				Application.LoadLevel(Application.loadedLevel);	
			}
		}
	}
	
	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject == player1) {
			// player 1 wins
			win_text.guiText.enabled = true;
			win_text.guiText.text = "Theseus Wins!";
		}
		
		if (collision.gameObject == player2) {
			// player 2 wins
			win_text.guiText.enabled = true;
			win_text.guiText.text = "Minotaur Wins!";
		}
		
		zoom_time_left = zoom_time;
		start = this.gameObject.transform.position + new Vector3(0, 3f, 0);
		// end should get set by maze generator
		winning = true;
	}
}
