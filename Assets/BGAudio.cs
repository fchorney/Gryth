using UnityEngine;
using System.Collections;

public class BGAudio : MonoBehaviour {
	public AudioClip intro, mainSlow, mainFast, transition;
	private bool playIntro, playSlow, playFast;
	public bool playTransition = false;
	private bool introPlaying, slowPlaying, transitionPlaying, fastPlaying;
	private bool introBreak, slowBreak, transitionBreak, fastBreak;
	public float introTime, mainSlowTime, mainFastTime, transitionTime;
	private ulong audioDelay = 88200;
	private AudioSource source1, source2, source3, source4;
	
	// Use this for initialization
	void Start () {
		AudioSource[] aSources = (AudioSource[])gameObject.GetComponents<AudioSource>();
		source1 = aSources[0];
		source2 = aSources[1];
		source3 = aSources[2];
		source4 = aSources[3];
		
		playIntro = true;
		playSlow = false;
		playFast = false;
		
		introPlaying = false;
		slowPlaying = false;
		transitionPlaying = false;
		fastPlaying = false;
		
		introBreak = false;
		slowBreak = false;
		transitionBreak = false;
		fastBreak = false;
	}
	
	
	// Update is called once per frame
	void Update () {
		if (!introPlaying && playIntro) {
			StartCoroutine (startPlayIntro ());
		}
		
		if (!slowPlaying && playSlow) {
			slowPlaying = true;
			StartCoroutine (startPlaySlow ());
		}
		
		if (!transitionPlaying && playTransition) {
			StartCoroutine (startPlayTransition ());
		}
		
		if (!fastPlaying && playFast) {
			fastPlaying = true;
			StartCoroutine (startPlayFast ());
		}
	}
	
	IEnumerator startPlayIntro() {
		source1.clip = intro;
		source1.Play(audioDelay);
		introPlaying = true;
		playIntro = false;
		yield return new WaitForSeconds(introTime);
		introPlaying = false;
		
		if (introBreak) {
			introBreak = false;
			yield break;
		}
		
		playSlow = true;
		
		yield return new WaitForSeconds(90f);
		playTransition = true;
	}
	
	IEnumerator startPlaySlow() {
		playSlow = false;
		while (slowPlaying) {
			if (source2.isPlaying) {
				source3.clip = mainSlow;
				source3.Play(audioDelay);
			}
			else {
				source2.clip = mainSlow;
				source2.Play(audioDelay);
			}
			yield return new WaitForSeconds(mainSlowTime);
			
			if (slowBreak) {
				slowBreak = false;
				slowPlaying = false;
				yield break;
			}
		}
	}
	
	IEnumerator startPlayTransition() {
		playIntro = false;
		playSlow = false;
		playTransition = false;
		playFast = false;
		
		introPlaying = false;
		slowPlaying = false;
		fastPlaying = false;
		transitionPlaying = true;
		
		introBreak = true;
		
		source4.clip = transition;
		source4.Play();
		
		source1.Stop();
		source2.Stop();
		source3.Stop();
		yield return new WaitForSeconds(transitionTime);
		transitionPlaying = false;
		playFast = true;
	}
	
	IEnumerator startPlayFast() {
		playFast = false;
		while (fastPlaying) {
			if (source2.isPlaying) {
				source3.clip = mainFast;
				source3.Play (audioDelay);
			}
			else {
				source2.clip = mainFast;
				source2.Play(audioDelay);
			}
			yield return new WaitForSeconds(mainFastTime);
			if (fastBreak) {
				fastBreak = false;
				fastPlaying = false;
				yield break;
			}
		}
	}
}