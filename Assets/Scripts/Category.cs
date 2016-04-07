using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Category : MonoBehaviour {

	public GameModel.categories category;

	public GameObject panel;
	public GameObject btnPrefab;
	public List<Item> items = new List<Item>();

	// Use this for initialization
	void Start () {
		
		foreach(Item item in items){
			GameObject newItem = Instantiate(btnPrefab);
			newItem.name = item.itemName;
			newItem.transform.SetParent(panel.transform, false);
			newItem.GetComponent<ItemBtn>().Init(item);
		}
	}

	public void TogglePanel(){
		UIManager.Instance.DisplayCategory(panel);
	}
}
