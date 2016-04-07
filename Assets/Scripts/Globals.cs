using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

[Serializable]
public class Globals : MonoBehaviour {

	public PlayerData playerData;

	private static Globals instance;
	public static Globals Instance {
		get {
			if(instance == null) {
				instance = GameObject.FindObjectOfType<Globals>();
			}
			return instance;
		}

		set {
			instance = value;
		}
	}
}

[Serializable]
public class PlayerData {
	public int cosmopolitanT = 0;
	public int cosmopolitanS = 0;

	public int acapulcoT = 0;
	public int acapulcoS = 0;

}
