using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {


	float input;
	bool axisInUse = false;

	private static GameManager instance;
	public static GameManager Instance {
		get {
			if(instance == null) {
				instance = GameObject.FindObjectOfType<GameManager>();
			}
			return instance;
		}
	}

	// Use this for initialization
	void Start () {
		Application.targetFrameRate = 25;
		GPGSIds.ActivateGPGS();
	}
	
	// Update is called once per frame
	void Update () {
		GetInput();
	}

	void GetInput(){
		if (Input.GetAxisRaw("Vertical") == 0)
			axisInUse = false;
		
		if (Input.GetAxisRaw("Vertical") == 1) {
			if (axisInUse == false) {
				axisInUse = true;
				UIManager.Instance.UpdateScrollRect(true);
			}
		} else if (Input.GetAxisRaw("Vertical") == -1) {
			if (axisInUse == false) {
				axisInUse = true;
				UIManager.Instance.UpdateScrollRect(false);
			}
		}



//		input = Input.GetAxis("Vertical");
//		if(input > 0f){
//			UIManager.Instance.UpdateScrollRect(true);
//		}
//
//		if(input < 0f){
//			UIManager.Instance.UpdateScrollRect(false);
//		}
		
	}

	public void Save(){
		GPGSIds.Save();

	}

	public void Load(){

	}
}
