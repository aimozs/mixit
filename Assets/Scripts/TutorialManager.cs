using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class TutorialManager : MonoBehaviour {

	public bool showTutorial = true;
	public int tipIndex = 0;
	public List<CanvasGroup> tips = new List<CanvasGroup>();


	private static TutorialManager instance;
	public static TutorialManager Instance {
		get {
			if(instance == null) {
				instance = GameObject.FindObjectOfType<TutorialManager>();
			}
			return instance;
		}
	}

	// Use this for initialization
	void Start () {
		HideAllTutoPanels();

		if(showTutorial)
			StartTutorial();
	}

	public void HideAllTutoPanels(){
		foreach(CanvasGroup canvas in tips){
			SetCanvasVisible(canvas, false);
		}
	}

	void StartTutorial(){
		SetCanvasVisible(tips[tipIndex], true);
	}

	public void ShowNextTip(){
		SetCanvasVisible(tips[tipIndex], false);
		if(tipIndex < tips.Count-1){
			tipIndex++;
			SetCanvasVisible(tips[tipIndex], true);
		} else {
			FinishTutorial();
		}
	}

	void SetCanvasVisible(CanvasGroup canvas, bool visible){
		if(visible){
			canvas.alpha = 1f;
			canvas.interactable = canvas.blocksRaycasts = true;
		} else {
			canvas.alpha = 0f;
			canvas.interactable = canvas.blocksRaycasts = false;
		}
			
	}

	public void FinishTutorial(){
		HideAllTutoPanels();
		showTutorial = false;
	}
}
