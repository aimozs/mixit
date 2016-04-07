using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Advertisements;

public class UnityAds : MonoBehaviour {

	private static UnityAds instance;
	public static UnityAds Instance {
		get {
			if(instance == null) {
				instance = GameObject.FindObjectOfType<UnityAds>();
			}
			return instance;
		}
	}

	public void DisplayAd ()
	{
		Advertisement.Show ();
	}
}
