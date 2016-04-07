using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour {

	public AudioSource fxSource;

	private static AudioManager instance;
	public static AudioManager Instance {
		get {
			if(instance == null) {
				instance = GameObject.FindObjectOfType<AudioManager>();
			}
			return instance;
		}
	}
	
	public void PlayAudio(AudioClip audio){
		if(fxSource != null){
			fxSource.PlayOneShot(audio);
		}
	}
}
