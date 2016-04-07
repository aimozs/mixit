using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class UIManager : MonoBehaviour {

	public RectTransform itemList;

	[Header("Scroll Items Params")]
	public ScrollRect itemScroll;
	public float scrollDelta;
	public int itemPerRowLand = 9;
	public int itemPerRowPort = 5;
	GameObject previouslySelected;
	DeviceOrientation lastUnknownDeviceOrientation = DeviceOrientation.Unknown;

	public Category initialCat;
	public int rows;
	int extraBasedOnModulo = 1;

	public GameObject optionPanel;
	public GameObject notifPanel;

	public Text deviceOrientation;

	public GameObject recipePanel;
	public Text textLiquids;
	public Text textOther;

	float posY;
	private List<Category> categoryBtns = new List<Category>();

	private static UIManager instance;
	public static UIManager Instance {
		get {
			if(instance == null) {
				instance = GameObject.FindObjectOfType<UIManager>();
			}
			return instance;
		}
	}

	// Use this for initialization
	void Start () {
		Category[] catBtns = GameObject.FindObjectsOfType<Category>();

		foreach(Category btn in catBtns){
			categoryBtns.Add(btn);
		}

		DisableAllCatgeroyOptions();

		initialCat.panel.SetActive(true);
	}
	
	public void DisableAllCatgeroyOptions(){
		foreach(Category catBtn in categoryBtns){
			catBtn.panel.SetActive(false);
		}
	}

	public void ToggleOptionPanel(){
		optionPanel.SetActive(!optionPanel.activeInHierarchy);
	}

//	public void CenterList(int index){
//		itemList.GetComponent<RectTransform>().localPosition = new Vector3(-250f * index, 0f, 0f);
//	}
//
//	public void SetListWidth(int n){
//		float width = 250f * n;
////		Debug.Log(width + " " + itemList.GetComponent<RectTransform>().rect.width);
//		itemList.GetComponent<RectTransform>().sizeDelta = new Vector2(width, 250f);
//	}

	public void DisplayCategory(GameObject category){
		DisableAllCatgeroyOptions();
		category.SetActive(!category.activeInHierarchy);
		Debug.Log(category.name + category.transform.childCount);
		DetermineSize(category.transform.childCount);

	}

	public void UpdateScrollRect(bool up){
		Debug.Log(EventSystem.current.currentSelectedGameObject.transform.name);
//		Debug.Log(EventSystem.current.currentSelectedGameObject.transform.parent.parent.name == "itemList");
		if(previouslySelected != null){
			if(previouslySelected.GetComponentInParent<ScrollRect>() != null && EventSystem.current.currentSelectedGameObject.GetComponentInParent<ScrollRect>() != null){
				if(up){
					posY = itemList.transform.GetComponent<RectTransform>().localPosition.y - scrollDelta;
				} else {
					posY = itemList.transform.GetComponent<RectTransform>().localPosition.y + scrollDelta;
				}
				itemList.transform.GetComponent<RectTransform>().localPosition = new Vector3(itemList.transform.GetComponent<RectTransform>().localPosition.x, posY, 0f);
			}
		}

		previouslySelected = EventSystem.current.currentSelectedGameObject;
	}

	void DetermineSize(int childCount){

		if(Input.deviceOrientation != DeviceOrientation.Unknown && Input.deviceOrientation != DeviceOrientation.FaceUp && Input.deviceOrientation != DeviceOrientation.FaceDown)
			lastUnknownDeviceOrientation = Input.deviceOrientation;

//		Debug.Log(childCount % 5);

		switch(lastUnknownDeviceOrientation){
		case DeviceOrientation.LandscapeLeft:
			extraBasedOnModulo = (childCount % itemPerRowLand) == 0 ? 0 : 1;
			rows = (childCount / itemPerRowLand) + extraBasedOnModulo;
			itemList.GetComponent<RectTransform>().sizeDelta = new Vector2(itemList.GetComponent<RectTransform>().sizeDelta.x, 200 * rows);
//			deviceOrientation.text = "LL";
			break;
		case DeviceOrientation.LandscapeRight:
			extraBasedOnModulo = (childCount % itemPerRowLand) == 0 ? 0 : 1;
			rows = (childCount / itemPerRowLand) + extraBasedOnModulo;
			itemList.GetComponent<RectTransform>().sizeDelta = new Vector2(itemList.GetComponent<RectTransform>().sizeDelta.x, 200 * rows);
//			deviceOrientation.text = "LR";
			break;
		case DeviceOrientation.Portrait:
			extraBasedOnModulo = (childCount % itemPerRowPort) == 0 ? 0 : 1;
			rows = (childCount / itemPerRowPort) + extraBasedOnModulo;
			itemList.GetComponent<RectTransform>().sizeDelta = new Vector2(itemList.GetComponent<RectTransform>().sizeDelta.x, 200 * rows);
//			deviceOrientation.text = "P";
			break;
		case DeviceOrientation.PortraitUpsideDown:
			extraBasedOnModulo = (childCount % itemPerRowPort) == 0 ? 0 : 1;
			rows = (childCount / itemPerRowPort) + extraBasedOnModulo;

			itemList.GetComponent<RectTransform>().sizeDelta = new Vector2(itemList.GetComponent<RectTransform>().sizeDelta.x, 200 * rows);
//			deviceOrientation.text = "RP";
			break;
		default:
//			Debug.Log(childCount + " " + itemPerRowPort + " = " + childCount % itemPerRowPort);
			extraBasedOnModulo = (childCount % itemPerRowLand) == 0 ? 0 : 1;
//			Debug.Log("extra " + extraBasedOnModulo);
			rows = (childCount / itemPerRowLand) + extraBasedOnModulo;
//			Debug.Log("rows " + rows);
			itemList.GetComponent<RectTransform>().sizeDelta = new Vector2(itemList.GetComponent<RectTransform>().sizeDelta.x, 200 * rows);
			break;
			
		}
	}

	public void DisplayRecipe(){
		
		Recipe recipe = BarManager.Instance.GetCurrentRecipe();
		if(recipe != null){
			textLiquids.text = textOther.text = "";
			BarManager.Instance.recipeViewed = true;
			recipePanel.SetActive(true);
			foreach(Item item in recipe.spirit){
				textLiquids.text += "\n- " + item.itemName;
			}
			foreach(Item item in recipe.liqueur){
				textLiquids.text += "\n- " + item.itemName;
			}
			foreach(Item item in recipe.mixer){
				textLiquids.text += "\n- " + item.itemName;
			}
			foreach(Item item in recipe.glassware){
				textOther.text += "\n- " + item.itemName;
			}
			foreach(Item item in recipe.preparation){
				textOther.text += "\n- " + item.itemName;
			}
			foreach(Item item in recipe.presentation){
				textOther.text += "\n- " + item.itemName;
			}

		}
	}

	public void HideRecipe(){
		recipePanel.SetActive(false);
	}

	public static void Notify(string message){
		Instance.notifPanel.SetActive(true);
		Instance.notifPanel.GetComponentInChildren<Text>().text = message;
		Instance.StartCoroutine(Instance.Deactivate(Instance.notifPanel, 3f));
	}

	IEnumerator Deactivate(GameObject obj, float sec){
		yield return new WaitForSeconds(sec);
		obj.SetActive(false);
	}

}
