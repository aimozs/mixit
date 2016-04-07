using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ItemBtn : MonoBehaviour {

	public GameModel.categories category;
//	public GameObject itemGO;
//	public int childCount;
	public Item item;


	public void Init (Item newItem) {
		category = newItem.category;
		item = newItem;
		GetComponentInChildren<Text>().text = newItem.itemName;
		GetComponent<Image>().sprite = newItem.itemSprite;
//		itemGO = newItem.itemPrefab;
//		childCount = transform.parent.childCount;
	}

	public void Use(){
		BarManager.Instance.Setup(category, item);
//		UIManager.Instance.CenterList(transform.GetSiblingIndex());
		if(item.audio != null)
			AudioManager.Instance.PlayAudio(item.audio);
	}

	void OnSelected(){
		Debug.Log(gameObject.name);
	}
}
