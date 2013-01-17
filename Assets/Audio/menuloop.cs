using UnityEngine;
using System.Collections;

public class menuloop : MonoBehaviour {
	public AudioClip menuClip;
	public float clipLength;
	private ulong delay;
	private AudioSource source1, source2;
	private bool changePlaces = false;
	
	// Use this for initialization
	void Start () {
		AudioSource[] aSources = (AudioSource[])gameObject.GetComponents<AudioSource>();
		source1 = aSources[0];
		source2 = aSources[1];
		source1.Stop();
		source2.Stop();
		
		source1.clip = menuClip;
		source2.clip = menuClip;
		
		StartCoroutine(StartMusic());
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButton("Fire1-1") || Input.GetButton ("Fire1-2")) {
			Application.LoadLevel("SceneTest1");	
		}
	}
	
	IEnumerator StartMusic() {
		yield return new WaitForSeconds(0.3f);
		source1.Play (delay);
		yield return new WaitForSeconds(clipLength);
		StartCoroutine (LoopMusic ());
	}
	
	IEnumerator LoopMusic() {
		while (true) {
			if (changePlaces) {
				source2.Play (delay);
				changePlaces = !changePlaces;
			}
			else {
				source1.Play (delay);
				changePlaces = !changePlaces;
			}
			yield return new WaitForSeconds(clipLength);
		}
	}
}
