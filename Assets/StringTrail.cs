using UnityEngine;
using System.Collections;
using System;

public class StringTrail : MonoBehaviour {
	
	public Boolean follow = true;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (follow) {
			if (this.gameObject == GameObject.FindWithTag ("StringTrail1")) {
				transform.position = GameObject.FindWithTag("Player1").transform.position + new Vector3(0f, 0f, -0.5f);
			} else {
				transform.position = GameObject.FindWithTag("Player2").transform.position + new Vector3(0f, 0f, -0.5f);
			}
		}
	}
}
