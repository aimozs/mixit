using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameModel : MonoBehaviour {

	public enum categories {glassware, preparation, spirit, liqueur, mixer, presentation, cocktail}


	public Sprite empty;

	private static GameModel instance;
	public static GameModel Instance {
		get {
			if(instance == null) {
				instance = GameObject.FindObjectOfType<GameModel>();
			}
			return instance;
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}



}
