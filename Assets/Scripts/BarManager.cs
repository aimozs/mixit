using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BarManager : MonoBehaviour {

	public int currentRecipe = 0;
	public List<Recipe> recipes = new List<Recipe>();

	public Challenge currentChallenge;
	public bool challengeStarted = true;
	public bool recipeViewed = false;

	[Header("Positions")]
	public GameObject prepPos;
	public GameObject spiritPos;
	public GameObject liqueurPos;
	public GameObject mixerPos;
	public GameObject presoPos;
	public GameObject cocktailPos;

	[Header("CocktailPrefabs")]
	public GameObject coupeCocktail;
	public GameObject martiniCocktail;
	public GameObject highballCocktail;
	public GameObject hurricaneCocktail;
	public GameObject rockCocktail;
	public GameObject shotCocktail;

	public GameObject reset;
	public GameObject recipe;
	public ParticleSystem resultPS;

	private static BarManager instance;
	public static BarManager Instance {
		get {
			if(instance == null) {
				instance = GameObject.FindObjectOfType<BarManager>();
			}
			return instance;
		}
	}
	// Use this for initialization
	void Start () {
		foreach(Recipe recipe in GameObject.FindObjectsOfType<Recipe>()){
			if(recipe.includeInBuild)
				recipes.Add(recipe);
		}

		currentChallenge = gameObject.AddComponent<Challenge>();
	}
	
	public void ServeDrink(){
		resultPS.Play();


		recipes[currentRecipe].tried += 1;
		if(IsRightCocktail()){

			DestroyChilds(cocktailPos);
			GameObject newCocktail = (GameObject)Instantiate(GetCocktailPrefab(currentChallenge.glassware[0].itemName), cocktailPos.transform.position, Quaternion.identity);
			newCocktail.transform.SetParent(cocktailPos.transform);
			newCocktail.transform.rotation = new Quaternion(0,0,0,0);


			recipe.GetComponentInChildren<Text>().text = "New order";

			if(newCocktail.GetComponent<Cocktail>()!= null){
				newCocktail.GetComponent<Cocktail>().content.GetComponent<MeshRenderer>().material.color = recipes[currentRecipe].contentColor;
				foreach(Item preso in currentChallenge.presentation){
					Debug.Log(preso.itemName);
					switch(preso.itemName){
					case "Straws":
						if(newCocktail.GetComponent<Cocktail>().straw != null)
							newCocktail.GetComponent<Cocktail>().straw.SetActive(true);
						break;
					case "Rim":
						if(newCocktail.GetComponent<Cocktail>().rim != null){
							newCocktail.GetComponent<Cocktail>().rim.SetActive(true);
						}
						break;
					case "Wedges":
						if(newCocktail.GetComponent<Cocktail>().wedge != null)
							newCocktail.GetComponent<Cocktail>().wedge.SetActive(true);
						break;
					case "Umbrellas":
						if(newCocktail.GetComponent<Cocktail>().umbrella != null)
							newCocktail.GetComponent<Cocktail>().umbrella.SetActive(true);
						break;
					}
				}
			}

			if(!recipeViewed)
				recipes[currentRecipe].success += 1;
			
			GameManager.Save();
		} else {
			UnityAds.Instance.DisplayAd();
		}

	}

	GameObject GetCocktailPrefab(string item){
		Debug.Log(item);
		switch(item){
		case "Coupe":
			return coupeCocktail;
		case "Higball":
			return highballCocktail;
		case "Martini":
			return martiniCocktail;
		case "Hurricane":
			return hurricaneCocktail;
		case "Shot":
			return shotCocktail;
		default:
			return rockCocktail;
		}
	}

	public bool IsRightCocktail(){
		if(
			AreListsSame(currentChallenge.glassware, recipes[currentRecipe].glassware) &&
			AreListsSame(currentChallenge.preparation, recipes[currentRecipe].preparation) &&
			AreListsSame(currentChallenge.spirit, recipes[currentRecipe].spirit) &&
			AreListsSame(currentChallenge.liqueur, recipes[currentRecipe].liqueur) &&
			AreListsSame(currentChallenge.mixer, recipes[currentRecipe].mixer)
		){
			Debug.Log("Yeah!");
			return true;
		} else {
			Debug.Log("NEHH!");
			return false;
		}
	}

	public void ToogleMusic(){

	}

	bool AreListsSame(List<Item> list1, List<Item>list2) {
		if ( list1.Count != list2.Count ){
			return false;
		}

		for(int i = 0; i < list1.Count; i++) {
			if ( list2[i] != list1[i] ) {
				UIManager.Notify(list1[i].itemName);
				return false;
			}
		}
		return true;
	}

	public void Reset(){
		Debug.Log("reset stuff");
		DestroyChilds(prepPos);
		DestroyChilds(spiritPos);
		DestroyChilds(liqueurPos);
		DestroyChilds(mixerPos);
		DestroyChilds(presoPos);
		DestroyChilds(cocktailPos);

		currentChallenge.spirit.Clear();
		currentChallenge.liqueur.Clear();
		currentChallenge.mixer.Clear();
		currentChallenge.glassware.Clear();
		currentChallenge.preparation.Clear();
		challengeStarted = false;

	}

	public void GetRandomRecipe(){
		currentRecipe = Random.Range(0, recipes.Count);
		UpdateStats();
		Reset();
	}

	void UpdateStats(){
		recipe.GetComponentInChildren<Text>().text = recipes[currentRecipe].cocktailName + " (" + recipes[currentRecipe].success + "/" + recipes[currentRecipe].tried + ")";
	}

	void DestroyChilds(GameObject goPos){
		for(int c = goPos.transform.childCount-1; c >= 0; c--){
			Destroy(goPos.transform.GetChild(c).gameObject);
		}
	}

	public Recipe GetCurrentRecipe(){
		return recipes[currentRecipe];
	}

	public void GetNewChallenge(){
		Debug.Log("new challenge");
		GetRandomRecipe();
		recipeViewed = false;
		UIManager.Instance.HideRecipe();
	}

	public void Setup(GameModel.categories category, Item item){
		challengeStarted = true;
		switch(category){
		case GameModel.categories.preparation:
			DestroyChilds(prepPos);
			currentChallenge.preparation.Clear();
			currentChallenge.preparation.Add(item);
			GameObject newPrep = (GameObject)Instantiate(item.itemPrefab, prepPos.transform.position, Quaternion.identity);
			newPrep.transform.SetParent(prepPos.transform);
			newPrep.transform.rotation = new Quaternion(0,0,0,0);
			break;
		case GameModel.categories.spirit:
			DestroyChilds(spiritPos);
			currentChallenge.spirit.Clear();
			currentChallenge.spirit.Add(item);
			GameObject newSpirit = (GameObject)Instantiate(item.itemPrefab, spiritPos.transform.position, Quaternion.identity);
			newSpirit.transform.SetParent(spiritPos.transform);
			newSpirit.transform.rotation = new Quaternion(0,0,0,0);
			break;
		case GameModel.categories.liqueur:
			DestroyChilds(liqueurPos);
			currentChallenge.liqueur.Clear();
			currentChallenge.liqueur.Add(item);
			GameObject newLiqueur = (GameObject)Instantiate(item.itemPrefab, liqueurPos.transform.position, Quaternion.identity);
			newLiqueur.transform.SetParent(liqueurPos.transform);
			newLiqueur.transform.rotation = new Quaternion(0,0,0,0);
			break;
		case GameModel.categories.mixer:
			DestroyChilds(mixerPos);
			currentChallenge.mixer.Clear();
			currentChallenge.mixer.Add(item);
			GameObject newMixer = (GameObject)Instantiate(item.itemPrefab, mixerPos.transform.position, Quaternion.identity);
			newMixer.transform.SetParent(mixerPos.transform);
			newMixer.transform.rotation = new Quaternion(0,0,0,0);
			break;
		case GameModel.categories.presentation:
			DestroyChilds(presoPos);
			currentChallenge.presentation.Clear();
			currentChallenge.presentation.Add(item);
			GameObject newPreso = (GameObject)Instantiate(item.itemPrefab, presoPos.transform.position, Quaternion.identity);
			newPreso.transform.SetParent(presoPos.transform);
			newPreso.transform.rotation = new Quaternion(0,0,0,0);
			break;

		default:
			DestroyChilds(cocktailPos);
			currentChallenge.glassware.Clear();
			currentChallenge.glassware.Add(item);
			GameObject newItem = (GameObject)Instantiate(item.itemPrefab, cocktailPos.transform.position, Quaternion.identity);
			newItem.transform.SetParent(cocktailPos.transform);
			newItem.transform.rotation = new Quaternion(0,0,0,0);
			break;
		}
	}

	public void SetStat(string recipeName, int suc, int tri){
		Recipe that = recipes.Find(obj => obj.cocktailName == recipeName);
		if(that != null){
			that.success = suc;
			that.tried = tri;
		}
	}
}
